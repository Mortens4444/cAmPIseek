using System.Net;
using System.Text;

namespace cAmPIseek.Services;

internal class ApiService
{
    private readonly HttpClient httpClient = new();

    public async Task<HttpResponseMessage> SendGetRequestAsync(string baseUrl, string endpoint)
    {
        var url = $"{baseUrl}{endpoint}";
        var isIp = IPAddress.TryParse(baseUrl, out _);
        if (isIp)
        {
            try
            {
                return await httpClient.GetAsync($"https://{url}");
            }
            catch
            {
                try
                {
                    return await httpClient.GetAsync($"http://{url}");
                }
                catch (Exception ex)
                {
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                    {
                        ReasonPhrase = ex.Message
                    };
                }
            }
        }
        return await httpClient.GetAsync(url);
    }

    public async Task<HttpResponseMessage> SendRequestAsync(string url, string method)
    {
        var request = new HttpRequestMessage
        {
            RequestUri = new Uri(url),
            Method = new HttpMethod(method)
        };

        if (method == "POST" || method == "PUT" || method == "PATCH")
        {
            request.Content = new StringContent("{\"key\":\"value\"}", Encoding.UTF8, "application/json");
        }

        return await httpClient.SendAsync(request);
    }
}