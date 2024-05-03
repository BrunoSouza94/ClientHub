using ClientHub.Domain.Abstractions;

namespace ClientHub.Domain.Entities.Clients;

public static class ClientErrors
{
    public static Error NotFound = new(
        "Client.NotFound",
        "Não foi possível encontrar o cliente com as informações especificadas.");
}