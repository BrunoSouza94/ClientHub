using ClientHub.Domain.Abstractions;
using MediatR;

namespace ClientHub.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
        where TQuery : IQuery<TResponse>
{
}