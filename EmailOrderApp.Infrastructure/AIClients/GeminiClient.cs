using System;
using System.Text;
using System.Text.Json;
using EmailOrderApp.Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace EmailOrderApp.Infrastructure.AIClients;

public class GeminiClient(HttpClient httpClient, IOptions<GeminiSettings> options)
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly GeminiSettings _settings = options.Value;

    public async Task<string> SendMessageAsync(string prompt)
    {
        var url = $"https://generativelanguage.googleapis.com/v1beta/models/{_settings.Model}:generateContent?key={_settings.ApiKey}";

        var requestBody = new
        {
            contents = new[]
           {
                new { parts = new[] { new { text = prompt } } }
            }
        };

        var json = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(url, content);

        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();

        return responseBody;
        
    }
}
