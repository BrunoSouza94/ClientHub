using ClientHub.Application.Abstractions.Messaging;

namespace ClientHub.Application.Features.Addresses.DeleteAddress;

public record DeleteAddressCommand(Guid Id) : ICommand<Guid>;