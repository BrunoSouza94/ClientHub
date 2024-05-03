using ClientHub.Domain.Abstractions;
using MediatR;

namespace ClientHub.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}