using ClientHub.Application.Abstractions.Messaging;
using ClientHub.Domain.Abstractions;
using ClientHub.Domain.Entities.Clients;

namespace ClientHub.Application.Features.Clients.RegisterClient;

public class DeleteClientCommandHandler : ICommandHandler<DeleteClientCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClientRepository _clientRepository;

    public DeleteClientCommandHandler(IUnitOfWork unitOfWork, IClientRepository clientRepository)
    {
        _unitOfWork = unitOfWork;
        _clientRepository = clientRepository;
    }

    public async Task<Result<Guid>> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
    {
        await _clientRepository.RemoveAsync(request.Id);

        await _unitOfWork.SaveChangesAsync();

        return request.Id;
    }
}