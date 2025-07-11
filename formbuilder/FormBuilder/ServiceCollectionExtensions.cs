﻿// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using DataSeeder;
using FormBuilder;
using FormBuilder.Components.FormElements.Anchor;
using FormBuilder.Components.FormElements.Back;
using FormBuilder.Components.FormElements.Button;
using FormBuilder.Components.FormElements.Checkbox;
using FormBuilder.Components.FormElements.Divider;
using FormBuilder.Components.FormElements.Image;
using FormBuilder.Components.FormElements.Input;
using FormBuilder.Components.FormElements.ListData;
using FormBuilder.Components.FormElements.Paragraph;
using FormBuilder.Components.FormElements.Password;
using FormBuilder.Components.FormElements.ReCaptcha;
using FormBuilder.Components.FormElements.Row;
using FormBuilder.Components.FormElements.StackLayout;
using FormBuilder.Components.FormElements.Title;
using FormBuilder.Conditions;
using FormBuilder.DefaultTemplate;
using FormBuilder.Factories;
using FormBuilder.Helpers;
using FormBuilder.Link;
using FormBuilder.Link.Services;
using FormBuilder.Repositories;
using FormBuilder.Rules;
using FormBuilder.Services;
using FormBuilder.Stores;
using FormBuilder.Stores.Default;
using FormBuilder.Transformers;
using FormBuilder.Urls;
using Radzen;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static FormBuilderRegistration AddFormBuilder(this IServiceCollection services, Action<FormBuilderOptions> cb = null)
    {
        services.AddFormBuilderUi(cb);
        services.AddTransient<IVersionedFormService, VersionedFormService>();
        services.AddSingleton<IFormStore>(new DefaultFormStore(new List<FormBuilder.Models.FormRecord>()));
        services.AddSingleton<IWorkflowStore>(new DefaultWorkflowStore(new List<FormBuilder.Models.WorkflowRecord>()));
        services.AddTransient<IDataSeeder, ConfigureRadzenTemplateDataSeeder>();
        return new FormBuilderRegistration(services);
    }

    public static void AddFormBuilderUi(this IServiceCollection services, Action<FormBuilderOptions> cb = null)
    {
        if (cb == null) services.Configure<FormBuilderOptions>((c) => { });
        else services.Configure<FormBuilderOptions>(cb);
        services.AddTransient<IFormBuilderJsService, FormBuilderJsService>();
        services.AddTransient<INavigationHistoryService, NavigationHistoryService>();
        services.AddTransient<IWorkflowLinkHelper, WorkflowLinkHelper>();

        services.AddTransient<IFormElementDefinition, FormInputFieldDefinition>();
        services.AddTransient<IFormElementDefinition, FormPasswordFieldDefinition>();
        services.AddTransient<IFormElementDefinition, FormButtonDefinition>();
        services.AddTransient<IFormElementDefinition, FormStackLayoutDefinition>();
        services.AddTransient<IFormElementDefinition, RowLayoutDefinition>();
        services.AddTransient<IFormElementDefinition, FormCheckboxDefinition>();
        services.AddTransient<IFormElementDefinition, DividerLayoutDefinition>();
        services.AddTransient<IFormElementDefinition, FormAnchorDefinition>();
        services.AddTransient<IFormElementDefinition, ListDataDefinition>();
        services.AddTransient<IFormElementDefinition, ParagraphDefinition>();
        services.AddTransient<IFormElementDefinition, TitleDefinition>();
        services.AddTransient<IFormElementDefinition, ImageDefinition>();
        services.AddTransient<IFormElementDefinition, BackButtonDefinition>();
        services.AddTransient<IFormElementDefinition, ReCaptchaDefinition>();

        services.AddTransient<IDateTimeHelper, DateTimeHelper>();
        services.AddTransient<ITranslationHelper, TranslationHelper>();
        services.AddTransient<IRenderFormElementsHelper, RenderFormElementsHelper>();
        services.AddTransient<ITransformerFactory, TransformerFactory>();
        services.AddTransient<ITransformationRuleEngineFactory, TransformationRuleEngineFactory>();
        services.AddTransient<IRepetitionRuleEngineFactory, RepetitionRuleEngineFactory>();
        services.AddTransient<IFormElementDefinitionFactory, FormElementDefinitionFactory>();
        services.AddTransient<IFakerDataServiceFactory, FakerDataServiceFactory>();
        services.AddTransient<IWorkflowLinkActionFactory, WorkflowLinkActionFactory>();
        services.AddTransient<IUrlEvaluatorFactory, UrlEvaluatorFactory>();
        services.AddTransient<IConditionRuleEngineFactory, ConditionRuleEngineFactory>();
        services.AddTransient<IHtmlClassResolver, HtmlClassResolver>();

        services.AddTransient<ITransformer, DirectTargetUrlTransformer>();
        services.AddTransient<ITransformer, RegexTransformer>();
        services.AddTransient<ITransformer, RelativeUrlTransformer>();

        services.AddTransient<ITransformationRuleEngine, IncomingTokensTransformationRuleEngine>();
        services.AddTransient<ITransformationRuleEngine, PropertyTransformationRuleEngine>();
        services.AddTransient<ITransformationRuleEngine, StaticValueTransformationRuleEngine>();
        services.AddTransient<IRepetitionRuleEngine, IncomingTokensRepetitionRuleEngine>();
        services.AddTransient<IMappingRuleService, MappingRuleService>();
        services.AddTransient<IRuleEngine, RuleEngine>();

        services.AddTransient<IUriProvider, UriProvider>();

        services.AddTransient<IWorkflowLinkAction, WorkflowLinkPopupAction>();
        services.AddTransient<IWorkflowLinkAction, WorkflowLinkUrlAction>();
        services.AddTransient<IWorkflowLinkAction, WorkflowLinkHttpRequestAction>();
        services.AddTransient<IWorkflowLinkAction, WorkflowLinkUrlTransformerAction>();
        services.AddTransient<IWorkflowLinkAction, WorkflowLinkAction>();

        services.AddTransient<IFormElementTransformerFactory, FormElementTransformerFactory>();
        services.AddTransient<IFormElementTransformer, ListDataTransformer>();
        services.AddTransient<IFormElementTransformer, FormStackLayoutDataTransformer>();

        services.AddTransient<IWorkflowLinkUrlTransformerService, WorkflowLinkUrlTransformerService>();
        services.AddTransient<IWorkflowLinkHttpRequestService, WorkflowLinkHttpRequestService>();

        services.AddTransient<IConditionRuleEngine, PresentRuleEngine>();
        services.AddTransient<IConditionRuleEngine, NotPresentRuleEngine>();
        services.AddTransient<IConditionRuleEngine, ComparisonRuleEngine>();
        services.AddTransient<IConditionRuleEngine, LogicalRuleEngine>();
        services.AddTransient<IConditionRuleEngine, UserAuthenticatedRuleEngine>();

        services.AddTransient<IUrlEvaluator, DirectTargetUrlEvaluator>();

        services.AddScoped<DialogService>();
        services.AddScoped<NotificationService>();

        services.AddHttpContextAccessor();
        services.AddHttpClient();
    }
}
