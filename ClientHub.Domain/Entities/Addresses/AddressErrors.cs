using ClientHub.Domain.Abstractions;

namespace ClientHub.Domain.Entities.Addresses;

public static class AddressErrors
{
    public static Error NotFound = new(
        "Address.NotFound",
        "Não foi possível encontrar o endereço com as informações especificadas.");
}