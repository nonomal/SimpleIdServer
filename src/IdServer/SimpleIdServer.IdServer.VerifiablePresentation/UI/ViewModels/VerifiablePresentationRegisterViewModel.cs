﻿// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using SimpleIdServer.IdServer.UI.ViewModels;

namespace SimpleIdServer.IdServer.VerifiablePresentation.UI.ViewModels;

public class VerifiablePresentationRegisterViewModel : IRegisterViewModel
{
    public bool IsNotAllowed { get; set; }
    public bool IsUpdated { get; set; }
    public string Amr { get; set; }
    public List<string> Steps { get; set; }
    public string? RedirectUrl { get; set; }
    public IEnumerable<VerifiablePresentationViewModel> VerifiablePresentations { get; set; }
    public string QrCodeUrl { get; set; }
    public string StatusUrl { get; set; }
    public string EndRegisterUrl { get; set; }
}

public class VerifiablePresentationViewModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<string> VcNames { get; set; }
}