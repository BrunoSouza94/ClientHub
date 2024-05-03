using ClientHub.Application.Abstractions.Messaging;
using ClientHub.Domain.Abstractions;
using ClientHub.Domain.Entities.Addresses;

namespace ClientHub.Application.Features.Addresses.DeleteAddress;

public sealed class DeleteAddressCommandHandler : ICommandHandler<DeleteAddressCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAddressRepository _addressRepository;

    public DeleteAddressCommandHandler(IUnitOfWork unitOfWork, IAddressRepository clientRepository)
    {
        _unitOfWork = unitOfWork;
        _addressRepository = clientRepository;
    }

    public async Task<Result<Guid>> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
    {
        await _addressRepository.RemoveAsync(request.Id);

        await _unitOfWork.SaveChangesAsync();

        return request.Id;
    }
}