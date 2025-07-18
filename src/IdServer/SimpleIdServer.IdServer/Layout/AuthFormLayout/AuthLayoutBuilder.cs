﻿// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using FormBuilder.Models;
using SimpleIdServer.IdServer.Layout.RegisterFormLayout;
using System;
using System.Collections.ObjectModel;

namespace SimpleIdServer.IdServer.Layout.AuthFormLayout;

public class AuthLayoutBuilder
{
    protected AuthLayoutBuilder(FormRecord formRecord)
    {
        FormRecord = formRecord;
    }

    protected FormRecord FormRecord { get; private set; }

    public static AuthLayoutBuilder New(string id, string correlationId, string name, bool actAsStep = true, string realm = null)
    {
        var record = new FormRecord
        {
            Id = id,
            CorrelationId = correlationId,
            Name = name,
            Realm = realm ?? Constants.DefaultRealm,
            ActAsStep = actAsStep,
            UpdateDateTime = DateTime.UtcNow,
            Category = FormCategories.Authentication,
            Status = RecordVersionStatus.Published,
            Elements = new ObservableCollection<IFormElementRecord>()
        };
        return new AuthLayoutBuilder(record);
    }

    public AuthLayoutBuilder AddElement(IFormElementRecord record)
    {
        FormRecord.Elements.Add(record);
        return this;
    }

    public AuthLayoutBuilder AddSuccessMessage(string code, string value)
    {
        FormRecord.SuccessMessageTranslations.Add(new FormMessageTranslation
        {
            Language = Constants.DefaultLanguage,
            Value = value,
            Code = code
        });
        return this;
    }

    public AuthLayoutBuilder AddErrorMessage(string code, string value)
    {
        FormRecord.ErrorMessageTranslations.Add(new FormMessageTranslation
        {
            Language = Constants.DefaultLanguage,
            Value = value,
            Code = code
        });
        return this;
    }

    public FormRecord Build() => FormRecord;
}
