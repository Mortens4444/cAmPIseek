namespace cAmPIseek.Services;

internal class ApiFinder
{
    private static readonly List<string> commonPaths =
    [
        "/api",
        "/api/v1",
        "/api/v2",
        "/api/swagger",
        "/api/swagger.json",
        "/api/swagger-ui/",
        "/api/swagger/v1",
        "/api/swagger/v2",
        "/openapi.json",
        "/swagger/index.html",
        "/swagger/v1/swagger.json",
        "/swagger/v2/swagger.json",
        "/swagger-ui.html",
        "/swagger-resources",
        "/openapi/v1.json",
        "/openapi/v2.json",
        "/v1/openapi.json",
        "/v2/openapi.json",
        "/docs",
        "/v1/docs",
        "/v2/docs",
        "/v1/api-docs",
        "/v2/api-docs",
        "/redoc",
        "/redoc/index.html"
    ];

    public static async Task<List<HttpResponseMessage>> SearchAsync(string baseUrl)
    {
        var apiService = new ApiService();
        var validResponses = new List<HttpResponseMessage>();

        foreach (var path in commonPaths)
        {
            var url = string.Concat(baseUrl, path);
            Console.Write(url);
            var response = await apiService.SendGetRequestAsync(baseUrl, path);
            validResponses.Add(response);
            Console.WriteLine($" - {response.ReasonPhrase}");
        }

        return validResponses;
    }
}
