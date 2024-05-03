using ClientHub.Application.Abstractions.Messaging;
using ClientHub.Application.Features.Clients.Shared;

namespace ClientHub.Application.Features.Clients.GetAllClients;

public sealed record GetAllClientsQuery() : IQuery<IReadOnlyList<ClientResponse>>;