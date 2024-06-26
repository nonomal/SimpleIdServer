﻿using System.Text.Json.Serialization;

namespace SimpleIdServer.Mobile.DTOs;

public class CredentialDefinitionResult
{
    [JsonPropertyName("format")]
    public string Format { get; set; }
    [JsonPropertyName("display")]
    public List<CredentialDefinitionDisplayResult> Display { get; set; }
    [JsonPropertyName("type")]
    public IEnumerable<string> Type { get; set; }
}

public class CredentialDefinitionDisplayResult
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("description")]
    public string Description { get; set; }
    [JsonPropertyName("locale")]
    public string Locale {  get; set; }
    [JsonPropertyName("background_color")]
    public string BackgroundColor { get; set; }
    [JsonPropertyName("text_color")]
    public string TextColor { get; set; }
    [JsonPropertyName("logo")]
    public CredentialDefinitionDisplayLogo Logo { get; set; }

}

public class CredentialDefinitionDisplayLogo
{
    [JsonPropertyName("uri")]
    public string Uri { get; set; }
    [JsonPropertyName("alt_text")]
    public string AltText { get; set; }
}

