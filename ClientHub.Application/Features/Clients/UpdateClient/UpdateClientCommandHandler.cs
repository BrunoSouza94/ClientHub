using ClientHub.Application.Abstractions.Messaging;
using ClientHub.Domain.Abstractions;
using ClientHub.Domain.Entities.Clients;

namespace ClientHub.Application.Features.Clients.RegisterClient;

public class UpdateClientCommandHandler : ICommandHandler<UpdateClientCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClientRepository _clientRepository;

    public UpdateClientCommandHandler(IUnitOfWork unitOfWork, IClientRepository clientRepository)
    {
        _unitOfWork = unitOfWork;
        _clientRepository = clientRepository;
    }

    public async Task<Result<Guid>> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        Client client = new(
            request.Id,
            new Name(request.Name),
            new Email(request.Email),
            new Logo(request.Logo));

        _clientRepository.Update(client);

        await _unitOfWork.SaveChangesAsync();

        return client.Id;
    }
}