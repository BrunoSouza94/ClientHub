using ClientHub.Web.Models;
using ClientHub.Web.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace ClientHub.Web.Controllers;

public class ClientController : Controller
{
    public async Task<IActionResult> Index()
    {
        HttpClientHandler handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

        HttpClient httpClient = new HttpClient(handler);

        HttpResponseMessage response = await httpClient.GetAsync("https://clienthub.api:8081/api/clients");

        if (!response.IsSuccessStatusCode)
        {
            ViewBag.ErrorMessage = "Não foi possível obter os dados.";

            return View("Error");
        }

        var data = await response.Content.ReadAsStringAsync();
        var clients = JsonSerializer.Deserialize<List<Client>>(data);

        return View(clients);
    }

    public async Task<IActionResult> Details(Guid id)
    {
        HttpClientHandler handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

        HttpClient httpClient = new HttpClient(handler);

        HttpResponseMessage response = await httpClient.GetAsync($"https://clienthub.api:8081/api/clients/{id}");

        if (!response.IsSuccessStatusCode)
        {
            ViewBag.ErrorMessage = $"Não foi possível obter os dados: {response.RequestMessage}";

            return View(new { ViewBag.ErrorMessage });
        }

        var data = await response.Content.ReadAsStringAsync();
        var client = JsonSerializer.Deserialize<Client>(data);

        return View(client);
    }

    public IActionResult Create()
    {
        return View(new Client());
    }

    [HttpPost]
    public async Task<IActionResult> Create(Client client)
    {
        HttpClientHandler handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

        HttpClient httpClient = new HttpClient(handler);

        using var memoryStream = new MemoryStream();
        await client.LogoFile.CopyToAsync(memoryStream);

        byte[] fileBytes = memoryStream.ToArray();
        string base64string = Convert.ToBase64String(fileBytes);

        client.Logo = base64string;
        client.FileName = client.LogoFile.FileName;

        var json = JsonSerializer.Serialize(client);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync("https://clienthub.api:8081/api/clients", data);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var exceptionDetails = JsonSerializer.Deserialize<ExceptionDetails>(content);
            var detail = exceptionDetails?.detail ?? string.Empty;

            ViewBag.ErrorMessage = $"Não foi possível criar o cliente: {detail}";

            return View("Error", new ErrorViewModel{ ErrorMessage = ViewBag.ErrorMessage });
        }

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        HttpClientHandler handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

        HttpClient httpClient = new HttpClient(handler);

        HttpResponseMessage response = await httpClient.GetAsync($"https://clienthub.api:8081/api/clients/{id}");

        if (!response.IsSuccessStatusCode)
        {
            ViewBag.ErrorMessage = $"Não foi possível obter os dados: {response.RequestMessage}";

            return View("Error", new ErrorViewModel { ErrorMessage = ViewBag.ErrorMessage });
        }

        var data = await response.Content.ReadAsStringAsync();
        var client = JsonSerializer.Deserialize<Client>(data);

        return View(client);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Client client)
    {
        HttpClientHandler handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

        HttpClient httpClient = new HttpClient(handler);

        if(client.LogoFile is not null)
        {
            using var memoryStream = new MemoryStream();
            await client.LogoFile.CopyToAsync(memoryStream);

            byte[] fileBytes = memoryStream.ToArray();
            string base64string = Convert.ToBase64String(fileBytes);

            client.Logo = base64string;
            client.FileName = client.LogoFile.FileName;
        }

        var serializedClient = JsonSerializer.Serialize(client);
        var data = new StringContent(serializedClient, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await httpClient.PutAsync($"https://clienthub.api:8081/api/clients", data);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var exceptionDetails = JsonSerializer.Deserialize<ExceptionDetails>(content);
            var detail = exceptionDetails?.detail ?? string.Empty;

            ViewBag.ErrorMessage = $"Não foi possível criar o cliente: {detail}";

            return View("Error", new ErrorViewModel { ErrorMessage = ViewBag.ErrorMessage });
        }

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        HttpClientHandler handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

        HttpClient httpClient = new HttpClient(handler);

        HttpResponseMessage response = await httpClient.GetAsync($"https://clienthub.api:8081/api/clients/{id}");

        if (!response.IsSuccessStatusCode)
        {
            ViewBag.ErrorMessage = "Não foi possível obter os dados.";

            return View("Error", new ErrorViewModel { ErrorMessage = ViewBag.ErrorMessage });
        }

        var data = await response.Content.ReadAsStringAsync();
        var client = JsonSerializer.Deserialize<Client>(data);

        return View(client);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        HttpClientHandler handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

        HttpClient httpClient = new HttpClient(handler);

        var json = JsonSerializer.Serialize(new { id });
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        HttpRequestMessage request = new()
        {
            Method = HttpMethod.Delete,
            RequestUri = new Uri("https://clienthub.api:8081/api/clients"),
            Content = data
        };

        HttpResponseMessage response = await httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            ViewBag.ErrorMessage = "Não foi possível remover os cliente.";

            return View("Error", new ErrorViewModel { ErrorMessage = ViewBag.ErrorMessage });
        }

        return RedirectToAction("Index");
    }
}