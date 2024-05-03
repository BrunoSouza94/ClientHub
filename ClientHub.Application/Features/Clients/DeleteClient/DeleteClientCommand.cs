using ClientHub.Application.Abstractions.Messaging;

namespace ClientHub.Application.Features.Clients.RegisterClient;

public record DeleteClientCommand(Guid Id) : ICommand<Guid>;