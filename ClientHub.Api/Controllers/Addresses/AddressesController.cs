using ClientHub.Application.Features.Addresses.DeleteAddress;
using ClientHub.Application.Features.Addresses.GetAddressById;
using ClientHub.Application.Features.Addresses.GetAddressesByClientId;
using ClientHub.Application.Features.Addresses.RegisterAddress;
using ClientHub.Application.Features.Addresses.Shared;
using ClientHub.Application.Features.Addresses.UpdateAddress;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace ClientHub.Api.Controllers.Addresses;

[ApiController]
[Route("api/addresses")]
public class AddressesController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IDistributedCache _cache;

    public AddressesController(ISender sender, IDistributedCache cache)
    {
        _sender = sender;
        _cache = cache;
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetAddressById(Guid id, CancellationToken cancellationToken)
    {
        var address = await _cache.GetStringAsync($"address-{id}", cancellationToken);

        if (address is not null)
        {
            var deserializedAddress = JsonSerializer.Deserialize<AddressResponse>(address);

            return Ok(deserializedAddress);
        }

        var query = new GetAddressByIdQuery(id);

        var result = await _sender.Send(query);

        if (result.IsFailure)
            return Unauthorized(result.Error);

        await _cache.SetStringAsync($"address-{id}", JsonSerializer.Serialize(result.Value), cancellationToken);

        return Ok(result.Value);
    }

    [HttpGet]
    [Route("GetAddressByClientId/{clientId}")]
    public async Task<IActionResult> GetAddressByClientId(Guid clientId, CancellationToken cancellationToken)
    {
        var query = new GetAddressesByClientIdQuery(clientId);

        var result = await _sender.Send(query, cancellationToken);

        if (result.IsFailure)
            return Unauthorized(result.Error);

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> RegisterAddress(RegisterAddressRequest request, CancellationToken cancellationToken)
    {
        var command = new RegisterAddressCommand(
            request.Thoroughfare,
            request.LocationNumber,
            request.Neighborhood,
            request.City,
            request.State,
            request.ClientId);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return Unauthorized(result.Error);

        await _cache.RemoveAsync($"client-{request.ClientId}");

        return Ok(result.Value);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAddress(UpdateAddressRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateAddressCommand(
            request.Id,
            request.Thoroughfare,
            request.LocationNumber,
            request.Neighborhood,
            request.City,
            request.State,
            request.ClientId);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return Unauthorized(result.Error);

        await _cache.RemoveAsync($"client-{request.ClientId}");
        await _cache.RemoveAsync($"address-{request.Id}", cancellationToken);

        return Ok(result.Value);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAddress(DeleteAddressRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteAddressCommand(request.Id);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return Unauthorized(result.Error);

        await _cache.RemoveAsync($"address-{request.Id}", cancellationToken);
        await _cache.RemoveAsync($"client-{request.ClientId}", cancellationToken);

        return Ok(result.Value);
    }
}