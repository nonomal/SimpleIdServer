﻿// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleIdServer.FastFed.Models;
using System;

namespace SimpleIdServer.FastFed.Store.EF.Configurations;

public class CapabilitySettingsConfiguration : IEntityTypeConfiguration<CapabilitySettings>
{
    public void Configure(EntityTypeBuilder<CapabilitySettings> builder)
    {
        builder.HasKey(c => c.Id);
    }
}
