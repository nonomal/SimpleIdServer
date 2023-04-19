﻿// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;

namespace SimpleIdServer.IdServer.Startup.EthereumDID
{
    public class DIDIdentifierParser
    {
        public static DIDIdentifier Parse(string id)
        {
            const string start = "did:ethr";
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException(nameof(id));
            if (!id.StartsWith(start)) throw new ArgumentException($"the DID Identifier must start by {start}");
            var splitted = start.Split(':');
            if (splitted.Count() != 3 && splitted.Count() != 4) throw new ArgumentException($"the DID Identifier must have the following format {start}:<identifier> or {start}:<source>:<identifier>");
            return new DIDIdentifier(splitted.Last(), splitted.Count() == 4 ? splitted[2] : null);
        }
    }
}