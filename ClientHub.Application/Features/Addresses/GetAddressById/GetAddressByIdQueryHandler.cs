using ClientHub.Application.Abstractions.Data;
using ClientHub.Application.Abstractions.Messaging;
using ClientHub.Application.Features.Addresses.Shared;
using ClientHub.Domain.Abstractions;
using Dapper;
using System.Data;

namespace ClientHub.Application.Features.Addresses.GetAddressById;

public class GetAddressByIdQueryHandler : IQueryHandler<GetAddressByIdQuery, AddressResponse>
{
    private ISqlConnectionFactory _sqlConnectionFactory;

    public GetAddressByIdQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<AddressResponse>> Handle(GetAddressByIdQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string procedure = "GetAddressById";
        var parameters = new DynamicParameters(new { request.Id });

        var result = await connection.QueryFirstAsync<AddressResponse>(procedure, parameters, commandType: CommandType.StoredProcedure);

        return result;
    }
}