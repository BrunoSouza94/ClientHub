using ClientHub.Application.Abstractions.Messaging;
using ClientHub.Domain.Abstractions;
using ClientHub.Domain.Entities.Addresses;
using ClientHub.Domain.Entities.Clients;

namespace ClientHub.Application.Features.Clients.RegisterClient;

internal sealed class RegisterClientCommandHandler : ICommandHandler<RegisterClientCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClientRepository _clientRepository;
    private readonly IAddressRepository _addressRepository;

    public RegisterClientCommandHandler(IUnitOfWork unitOfWork, IClientRepository clientRepository, IAddressRepository addressRepository)
    {
        _unitOfWork = unitOfWork;
        _clientRepository = clientRepository;
        _addressRepository = addressRepository;
    }

    public async Task<Result<Guid>> Handle(RegisterClientCommand request, CancellationToken cancellationToken)
    {
        Client client = Client.Create(
            new Name(request.Name),
            new Email(request.Email),
            new Logo(request.Logo));

        await _clientRepository.AddAsync(client);

        request.Addresses.ForEach(async address =>
        {
            Address adress = Address.Create(
                new Thoroughfare(address.Thoroughfare),
                new LocationNumber(address.LocationNumber),
                new Neighborhood(address.Neighborhood),
                new City(address.City),
                new State(address.State),
                client.Id);

            await _addressRepository.AddAsync(adress);
        });

        await _unitOfWork.SaveChangesAsync();

        return client.Id;
    }
}