namespace ClientHub.Application.Features.Addresses.Shared;

public class AddressResponse
{
    public Guid Id { get; init; }

    public string Thoroughfare { get; set; }

    public string LocationNumber { get; set; }

    public string Neighborhood { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public Guid ClientId { get; set; }
}