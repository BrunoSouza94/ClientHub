using System.Text.Json.Serialization;

namespace ClientHub.Web.Models;

public class Client
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("logo")]
    public string Logo { get; set; } = string.Empty;

    public IFormFile? LogoFile { get; set; }
    
    public string FileName { get; set; }

    [JsonPropertyName("addresses")]
    public List<Address> Addresses { get; set; } = new();
}