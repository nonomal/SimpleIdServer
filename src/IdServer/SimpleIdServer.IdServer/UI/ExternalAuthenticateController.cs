﻿// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using FormBuilder.Repositories;
using FormBuilder.Stores;
using MassTransit;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SimpleIdServer.IdServer.Domains;
using SimpleIdServer.IdServer.Exceptions;
using SimpleIdServer.IdServer.Helpers;
using SimpleIdServer.IdServer.Jwt;
using SimpleIdServer.IdServer.Options;
using SimpleIdServer.IdServer.Resources;
using SimpleIdServer.IdServer.Stores;
using SimpleIdServer.IdServer.UI.Services;
using SimpleIdServer.IdServer.UI.ViewModels;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleIdServer.IdServer.UI;

public class ExternalAuthenticateController : BaseAuthenticateController
{
    public const string SCHEME_NAME = "scheme";
    public const string RETURN_URL_NAME = "returnUrl";
    public const string CURRENT_LINK_NAME = "currentLink";
    private readonly ILogger<ExternalAuthenticateController> _logger;
    private readonly IUserTransformer _userTransformer;
    private readonly IAuthenticationSchemeProviderRepository _authenticationSchemeProviderRepository;
    private readonly IAuthenticationHelper _authenticationHelper;
    private readonly IRealmRepository _realmRepository;

    public ExternalAuthenticateController(
        IOptions<IdServerHostOptions> options,
        IDataProtectionProvider dataProtectionProvider,
        IClientRepository clientRepository,
        IAmrHelper amrHelper,
        IUserRepository userRepository,
        IUserSessionResitory userSessionRepository,
        ILogger<ExternalAuthenticateController> logger,
        IUserTransformer userTransformer,
        IAuthenticationSchemeProviderRepository authenticationSchemeProviderRepository,
        ITokenRepository tokenRepository,
        IJwtBuilder jwtBuilder,
        IAuthenticationHelper authenticationHelper,
        IRealmRepository realmRepository,
        ITransactionBuilder transactionBuilder,
        IBusControl busControl,
        IWorkflowStore workflowStore,
        IAcrHelper acrHelper,
        IFormStore formStore,
        IAuthenticationContextClassReferenceRepository acrRepository,
        IWorkflowHelper workflowHelper) : base(
            clientRepository, 
            userRepository, 
            userSessionRepository, 
            amrHelper, busControl, userTransformer, dataProtectionProvider, authenticationHelper, transactionBuilder, tokenRepository, jwtBuilder, workflowStore, formStore, acrHelper, acrRepository, workflowHelper, options)
    {
        _logger = logger;
        _userTransformer = userTransformer;
        _authenticationSchemeProviderRepository = authenticationSchemeProviderRepository;
        _authenticationHelper = authenticationHelper;
        _realmRepository = realmRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Login([FromRoute] string prefix, string scheme, string returnUrl, string currentLink, CancellationToken cancellationToken)
    {
        prefix = prefix ?? Constants.DefaultRealm;
        if (string.IsNullOrWhiteSpace(scheme))
            throw new OAuthException(ErrorCodes.INVALID_REQUEST, string.Format(Global.MissingParameter, nameof(scheme)));

        var result = await HttpContext.AuthenticateAsync(scheme);
        if(result is {  Succeeded : true })
        {
            var user = await JustInTimeProvision(prefix, scheme, result, cancellationToken);
            var viewModel = new ExternalAuthLoginViewModel
            {
                ReturnUrl = returnUrl,
                CurrentLink = currentLink
            };
            if (!string.IsNullOrWhiteSpace(returnUrl))
                return await Authenticate(prefix, viewModel, Constants.AreaPwd, user, cancellationToken, false);
            return await Sign(prefix, null, "~/", Constants.AreaPwd, user, null, cancellationToken, false);
        }

        var items = new Dictionary<string, string>
        {
            { SCHEME_NAME, scheme }
        };
        if (!string.IsNullOrWhiteSpace(returnUrl))
        {
            items.Add(RETURN_URL_NAME, returnUrl);
        }

        if(!string.IsNullOrWhiteSpace(currentLink))
        {
            items.Add(CURRENT_LINK_NAME, currentLink);
        }

        var props = new AuthenticationProperties(items)
        {
            RedirectUri = Url.Action(nameof(Callback)),
        };
        return Challenge(props, scheme);
    }

    [HttpGet]
    public async Task<IActionResult> Callback([FromRoute] string prefix, CancellationToken cancellationToken)
    {
        prefix = prefix ?? Constants.DefaultRealm;
        var result = await HttpContext.AuthenticateAsync(Constants.DefaultExternalCookieAuthenticationScheme);
        if (result == null || !result.Succeeded)
        {
            if (result.Failure != null)
            {
                _logger.LogError(result.Failure.ToString());
            }

            throw new OAuthException(ErrorCodes.INVALID_REQUEST, Global.BadExternalAuthentication);
        }

        var scheme = result.Properties.Items[SCHEME_NAME];
        var user = await JustInTimeProvision(prefix, scheme, result, cancellationToken);
        await HttpContext.SignOutAsync(Constants.DefaultExternalCookieAuthenticationScheme);
        if (result.Properties.Items.ContainsKey(RETURN_URL_NAME))
        {
            var viewModel = new ExternalAuthLoginViewModel
            {
                ReturnUrl = result.Properties.Items[RETURN_URL_NAME],
                CurrentLink = result.Properties.Items[CURRENT_LINK_NAME]
            };
            return await Authenticate(prefix, viewModel, Constants.AreaPwd, user, cancellationToken, false);
        }

        return await Sign(prefix, null, "~/", Constants.AreaPwd, user, null, cancellationToken, false);
    }

    private async Task<User> JustInTimeProvision(string realm, string scheme, AuthenticateResult authResult, CancellationToken cancellationToken)
    {
        var principal = authResult.Principal;
        var idProvider = await _authenticationSchemeProviderRepository.Get(realm, scheme, cancellationToken);
        if(idProvider == null)
        {
            throw new OAuthException(ErrorCodes.INVALID_REQUEST, string.Format(Global.UnsupportedSchemeProvider, scheme));
        }


        var sub = UserTransformer.ResolveSubject(idProvider, principal);
        if (string.IsNullOrWhiteSpace(sub))
        {
            _logger.LogError("There is not valid subject");
            throw new OAuthException(ErrorCodes.INVALID_REQUEST, Global.BadExternalAuthenticationUser);
        }

        var user = await UserRepository.GetByExternalAuthProvider(scheme, sub, realm, cancellationToken);
        if (user == null)
        {
            using (var transaction = TransactionBuilder.Build())
            {
                _logger.LogInformation($"Start to provision the user '{sub}'");
                var existingUser = await _authenticationHelper.GetUserByLogin(sub, realm, cancellationToken);
                if (existingUser != null)
                {
                    user = existingUser;
                    user.AddExternalAuthProvider(scheme, sub);
                    UserRepository.Update(user);
                }
                else
                {

                    var r = await _realmRepository.Get(realm, cancellationToken);
                    user = _userTransformer.Transform(r, principal, idProvider);
                    user.AddExternalAuthProvider(scheme, sub);
                    UserRepository.Add(user);
                    Counters.UserRegistered();
                }

                await transaction.Commit(cancellationToken);
                _logger.LogInformation($"Finish to provision the user '{sub}'");
            }
        }

        return user;
    }
}