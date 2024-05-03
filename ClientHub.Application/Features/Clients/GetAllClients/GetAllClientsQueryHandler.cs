using ClientHub.Application.Abstractions.Data;
using ClientHub.Application.Abstractions.Messaging;
using ClientHub.Application.Features.Clients.Shared;
using ClientHub.Domain.Abstractions;
using Dapper;
using System.Data;

namespace ClientHub.Application.Features.Clients.GetAllClients;

public class GetAllClientsQueryHandler : IQueryHandler<GetAllClientsQuery, IReadOnlyList<ClientResponse>>
{
    private ISqlConnectionFactory _sqlConnectionFactory;

    public GetAllClientsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<IReadOnlyList<ClientResponse>>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string procedure = "GetAllClients";

        var results = await connection.QueryAsync<ClientResponse>(procedure, commandType: CommandType.StoredProcedure);

        return results.ToList();
    }
}