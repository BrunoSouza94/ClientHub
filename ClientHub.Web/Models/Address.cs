using System.Text.Json.Serialization;

namespace ClientHub.Web.Models;

public class Address
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("thoroughfare")]
    public string Thoroughfare { get; set; }

    [JsonPropertyName("locationNumber")]
    public string LocationNumber { get; set; }

    [JsonPropertyName("neighborhood")]
    public string Neighborhood { get; set; }

    [JsonPropertyName("city")]
    public string City { get; set; }

    [JsonPropertyName("state")]
    public string State { get; set; }

    [JsonPropertyName("clientId")]
    public Guid ClientId { get; set; }
}