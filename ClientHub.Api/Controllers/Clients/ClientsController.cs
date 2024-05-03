using ClientHub.Application.Features.Clients.DeleteClient;
using ClientHub.Application.Features.Clients.GetAllClients;
using ClientHub.Application.Features.Clients.GetClientById;
using ClientHub.Application.Features.Clients.RegisterClient;
using ClientHub.Application.Features.Clients.Shared;
using ClientHub.Application.Features.Clients.UpdateClient;
using ClientHub.Infrastructure.Image;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace ClientHub.Api.Controllers.Clients;

[ApiController]
[Route("api/clients")]
public class ClientsController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IDistributedCache _cache;
    private readonly IImageService _imageService;

    public ClientsController(ISender sender, IDistributedCache cache, IImageService imageService)
    {
        _sender = sender;
        _cache = cache;
        _imageService = imageService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllClients(CancellationToken cancellationToken)
    {
        var clients = await _cache.GetStringAsync("all-clients", cancellationToken);

        if(clients is not null)
        {
            var deserializedClients = JsonSerializer.Deserialize<List<ClientResponse>>(clients);

            return Ok(deserializedClients);
        }

        var query = new GetAllClientsQuery();

        var result = await _sender.Send(query, cancellationToken);

        if (result.IsFailure)
            return Unauthorized(result.Error);

        await _cache.SetStringAsync("all-clients", JsonSerializer.Serialize(result.Value), cancellationToken);

        return Ok(result.Value);

    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetClientById(Guid id, CancellationToken cancellationToken)
    {
        var client = await _cache.GetStringAsync($"client-{id}", cancellationToken);

        if (client is not null)
        {
            var deserializedClient = JsonSerializer.Deserialize<ClientResponse>(client);

            return Ok(deserializedClient);
        }

        var query = new GetClientByIdQuery(id);

        var result = await _sender.Send(query, cancellationToken);

        if (result.IsFailure)
            return Unauthorized(result.Error);

        await _cache.SetStringAsync($"client-{id}", JsonSerializer.Serialize(result.Value), cancellationToken);

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> RegisterClient(RegisterClientRequest request, CancellationToken cancellationToken)
    {
        var imageUri = await _imageService.UploadImageAsync(Guid.NewGuid(), request.Logo!, request.FileName);

        request.Logo = imageUri;

        var command = new RegisterClientCommand(
            request.Name,
            request.Email,
            request.Logo,
            request.Addresses);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return Unauthorized(result.Error);

        await _cache.RemoveAsync("all-clients", cancellationToken);

        return Ok(result.Value);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateClient(UpdateClientRequest request, CancellationToken cancellationToken)
    {
        if(request.FileName is not null)
        {
            var imageUri = await _imageService.UploadImageAsync(Guid.NewGuid(), request.Logo, request.FileName);

            request.Logo = imageUri;
        }

        var command = new UpdateClientCommand(
            request.Id,
            request.Name,
            request.Email,
            request.Logo,
            request.Addresses);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return Unauthorized(result.Error);

        await _cache.RemoveAsync($"client-{request.Id}", cancellationToken);
        await _cache.RemoveAsync("all-clients", cancellationToken);

        return Ok(result.Value);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteClient(DeleteClientRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteClientCommand(request.Id);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return Unauthorized(result.Error);

        await _cache.RemoveAsync($"client-{request.Id}", cancellationToken);
        await _cache.RemoveAsync("all-clients", cancellationToken);

        return Ok(result.Value);
    }
}