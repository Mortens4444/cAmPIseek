//#define CAMERA_SEEK

using cAmPIseek.Services;

#if CAMERA_SEEK
using cAmPIseek.Model;

int timeoutMs = 500;
var camera = new Camera
{
    IpAddress = "62.109.19.230",
    Username = "user",
    Password = "pass",
    Channel = 0,
    Width = 320,
    Height = 240,
};

var searchCameraService = new SearchCameraService();
var foundUrl = searchCameraService.FindCameraUrl(camera, timeoutMs);
if (String.IsNullOrEmpty(foundUrl))
{
    Console.Error.WriteLine($"Camera URL not found: {camera}");
}
else
{
    Console.WriteLine($"Camera URL found: {foundUrl}");
}

#else

var url = "https://www.google.com";
await ApiFinder.SearchAsync(url);

#endif

Console.ReadKey();
