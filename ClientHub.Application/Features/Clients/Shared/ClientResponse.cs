using ClientHub.Application.Features.Addresses.Shared;

namespace ClientHub.Application.Features.Clients.Shared;

public class ClientResponse
{
    public Guid Id { get; init; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Logo { get; set; }

    public List<AddressResponse> Addresses { get; set; }
}