using ClientHub.Application.Abstractions.Data;
using ClientHub.Application.Abstractions.Messaging;
using ClientHub.Application.Features.Addresses.Shared;
using ClientHub.Application.Features.Clients.Shared;
using ClientHub.Domain.Abstractions;
using Dapper;
using System.Data;
using System.Linq;

namespace ClientHub.Application.Features.Clients.GetClientById;

public class GetClientByIdQueryHandler : IQueryHandler<GetClientByIdQuery, ClientResponse>
{
    private ISqlConnectionFactory _sqlConnectionFactory;

    public GetClientByIdQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<ClientResponse>> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string procedure = "GetClientById";
        var parameters = new DynamicParameters(new { request.Id });

        var result = await connection.QueryMultipleAsync(procedure, parameters, commandType: CommandType.StoredProcedure);

        var client = await result.ReadSingleAsync<ClientResponse>();

        client.Addresses = await result.ReadAsync<AddressResponse>() as List<AddressResponse>;

        return client;
    }
}