﻿// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using SimpleIdServer.IdServer.Layout.AuthFormLayout;
using SimpleIdServer.IdServer.UI.ViewModels;

namespace SimpleIdServer.IdServer.Fido.UI.ViewModels
{
    public class AuthenticateWebauthnViewModel : BaseAuthenticateViewModel, IQRCodeAuthViewModel
    {
        public AuthenticateWebauthnViewModel()
        {

        }

        public string SessionId { get; set; }
        public string BeginLoginUrl { get; set; } = null!;
        public string EndLoginUrl { get; set; } = null!;

        public override List<string> Validate()
        {
            var result = new List<string>();
            if (string.IsNullOrWhiteSpace(ReturnUrl))
                result.Add(AuthFormErrorMessages.MissingReturnUrl);

            if (string.IsNullOrWhiteSpace(Login))
                result.Add(AuthFormErrorMessages.MissingLogin);

            if (string.IsNullOrWhiteSpace(SessionId))
                result.Add(AuthFormErrorMessages.MissingSessionId);

            return result;
        }
    }
}
