using ClientHub.Application.Abstractions.Messaging;
using ClientHub.Domain.Abstractions;
using ClientHub.Domain.Entities.Addresses;

namespace ClientHub.Application.Features.Addresses.UpdateAddress;

public class UpdateAddressCommandHandler : ICommandHandler<UpdateAddressCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAddressRepository _addressRepository;

    public UpdateAddressCommandHandler(IUnitOfWork unitOfWork, IAddressRepository addressRepository)
    {
        _unitOfWork = unitOfWork;
        _addressRepository = addressRepository;
    }

    public async Task<Result<Guid>> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
    {
        Address address = new(
            request.Id,
            new Thoroughfare(request.Thoroughfare),
            new LocationNumber(request.LocationNumber),
            new Neighborhood(request.Neighborhood),
            new City(request.City),
            new State(request.State),
            request.ClientId);
        
        _addressRepository.Update(address);

        await _unitOfWork.SaveChangesAsync();

        return address.Id;
    }
}