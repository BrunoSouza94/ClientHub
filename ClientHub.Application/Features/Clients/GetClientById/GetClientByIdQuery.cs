using ClientHub.Application.Abstractions.Messaging;
using ClientHub.Application.Features.Clients.Shared;

namespace ClientHub.Application.Features.Clients.GetClientById;

public sealed record GetClientByIdQuery(Guid Id) : IQuery<ClientResponse>;