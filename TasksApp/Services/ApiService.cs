namespace TasksApp.Services;

using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

public static class ApiService
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        PropertyNamingPolicy = null,
    };
    
    private static readonly HttpClient HttpClient = new ();
    private const string BaseUrl = "http://localhost:5002";

    public static async Task<T> Get<T>(string path)
    {
        var response = await HttpClient.GetAsync(BaseUrl + path);

        return await GetResponse<T>(response);
    }

    public static async Task<T> Post<T>(string path, object? data = null)
    {
        var response = await HttpClient.PostAsJsonAsync(BaseUrl + path, data, JsonSerializerOptions);

        return await GetResponse<T>(response);
    }

    private static async Task<T> GetResponse<T>(HttpResponseMessage response)
    {
        var content = await response.Content.ReadFromJsonAsync<T>();

        return content!;
    }
}