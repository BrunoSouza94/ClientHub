using ClientHub.Application.Abstractions.Messaging;
using ClientHub.Domain.Abstractions;
using ClientHub.Domain.Entities.Addresses;

namespace ClientHub.Application.Features.Addresses.RegisterAddress;

public sealed class RegisterAddressCommandHandler : ICommandHandler<RegisterAddressCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAddressRepository _addressRepository;

    public RegisterAddressCommandHandler(IUnitOfWork unitOfWork, IAddressRepository addressRepository)
    {
        _unitOfWork = unitOfWork;
        _addressRepository = addressRepository;
    }

    public async Task<Result<Guid>> Handle(RegisterAddressCommand request, CancellationToken cancellationToken)
    {
        Address address = Address.Create(
            new Thoroughfare(request.Thoroughfare),
            new LocationNumber(request.LocationNumber),
            new Neighborhood(request.Neighborhood),
            new City(request.City),
            new State(request.State),
            request.ClientId);

        await _addressRepository.AddAsync(address);

        await _unitOfWork.SaveChangesAsync();

        return address.Id;
    }
}