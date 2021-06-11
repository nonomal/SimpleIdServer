﻿// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using SimpleIdServer.OAuth.Domains;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleIdServer.OAuth.Persistence
{
    public interface IOAuthUserCommandRepository : ICommandRepository<OAuthUser>
    {
        Task RemoveAllConsents(string clientId, CancellationToken cancellationToken);
    }
}