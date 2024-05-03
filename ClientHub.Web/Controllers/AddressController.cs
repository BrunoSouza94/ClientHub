using ClientHub.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace ClientHub.Web.Controllers;

public class AddressController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Create(Guid clientId)
    {
        return View(new Address()
        {
            ClientId = clientId
        });
    }

    [HttpPost]
    public async Task<IActionResult> Create(Address address)
    {
        HttpClientHandler handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

        HttpClient httpClient = new HttpClient(handler);

        var json = JsonSerializer.Serialize(address);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync("https://clienthub.api:8081/api/addresses", data);

        if (!response.IsSuccessStatusCode)
            return View("Error");

        return RedirectToAction("Details", "Client", new { id = address.ClientId });
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        HttpClientHandler handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

        HttpClient httpClient = new HttpClient(handler);

        HttpResponseMessage response = await httpClient.GetAsync($"https://clienthub.api:8081/api/addresses/{id}");

        if (!response.IsSuccessStatusCode)
        {
            ViewBag.ErrorMessage = "Não foi possível obter os dados.";

            return View("Error");
        }

        var data = await response.Content.ReadAsStringAsync();
        var address = JsonSerializer.Deserialize<Address>(data);

        return View(address);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Address address)
    {
        HttpClientHandler handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

        HttpClient httpClient = new HttpClient(handler);

        var serializedAddress = JsonSerializer.Serialize(address);
        var data = new StringContent(serializedAddress, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await httpClient.PutAsync($"https://clienthub.api:8081/api/addresses", data);

        if (!response.IsSuccessStatusCode)
        {
            ViewBag.ErrorMessage = "Não foi possível editar o cliente.";

            return View("Error");
        }

        return RedirectToAction("Details", "Client", new { id = address.ClientId });
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        HttpClientHandler handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

        HttpClient httpClient = new HttpClient(handler);

        HttpResponseMessage response = await httpClient.GetAsync($"https://clienthub.api:8081/api/addresses/{id}");

        if (!response.IsSuccessStatusCode)
        {
            ViewBag.ErrorMessage = "Não foi possível obter os dados.";

            return View("Error");
        }

        var data = await response.Content.ReadAsStringAsync();
        var address = JsonSerializer.Deserialize<Address>(data);

        return View(address);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(Guid id, Guid clientId)
    {
        HttpClientHandler handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

        HttpClient httpClient = new HttpClient(handler);

        var json = JsonSerializer.Serialize(new { id, clientId });
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        HttpRequestMessage request = new()
        {
            Method = HttpMethod.Delete,
            RequestUri = new Uri("https://clienthub.api:8081/api/addresses"),
            Content = data
        };

        HttpResponseMessage response = await httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            ViewBag.ErrorMessage = "Não foi possível remover os cliente.";

            return View("Error");
        }

        return RedirectToAction("Details", "Client", new { id = clientId });
    }
}