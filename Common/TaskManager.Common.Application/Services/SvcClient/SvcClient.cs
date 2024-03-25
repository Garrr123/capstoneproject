using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace TaskManager.Common.Application.Services.SvcClient;

public class SvcClient
{
    private readonly HttpClient _httpClient;

    public SvcClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<TResponse> RequestAsync<TResponse>(string moduleUrl, object payload, CancellationToken cancellationToken = default)
    {
        // Assuming the payload is a DTO object

        var requestContent = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(moduleUrl, requestContent, cancellationToken);

        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken);

        return JsonSerializer.Deserialize<TResponse>(jsonResponse);
    }
}