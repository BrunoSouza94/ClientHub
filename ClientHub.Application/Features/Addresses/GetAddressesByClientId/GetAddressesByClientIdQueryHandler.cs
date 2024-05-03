using ClientHub.Application.Abstractions.Data;
using ClientHub.Application.Abstractions.Messaging;
using ClientHub.Application.Features.Addresses.Shared;
using ClientHub.Domain.Abstractions;
using Dapper;
using System.Data;

namespace ClientHub.Application.Features.Addresses.GetAddressesByClientId;

public class GetAddressesByClientIdQueryHandler : IQueryHandler<GetAddressesByClientIdQuery, IReadOnlyList<AddressResponse>>
{
    private ISqlConnectionFactory _connectionFactory;

    public GetAddressesByClientIdQueryHandler(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Result<IReadOnlyList<AddressResponse>>> Handle(GetAddressesByClientIdQuery request, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();

        const string procedure = "GetAddressesByClientId";
        var parameters = new DynamicParameters(new { request.ClientId });

        var result = await connection.QueryAsync<AddressResponse>(procedure, parameters, commandType: CommandType.StoredProcedure);

        return result.ToList();
    }
}