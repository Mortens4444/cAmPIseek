using cAmPIseek.Model;
using System.Reflection;

namespace cAmPIseek.Services;

internal class SearchCameraService
{
    private readonly Dictionary<string, List<string>> cameraUrls = [];
    private const string TargetNamespace = "cAmPIseek.Resources.";

    public SearchCameraService()
    {
        InitializeCameraUrls();
    }

    public List<string> GetManufacturers()
    {
        return [.. cameraUrls.Keys];
    }

    public List<string> GetCameraUrls(string manufacturer)
    {
        return [.. cameraUrls[manufacturer]];
    }

    public List<string> GetAllCameraUrls()
    {
        return [.. cameraUrls
            .SelectMany(c => c.Value)
            .Distinct()
            .OrderBy(url => url)];
    }

    public string FindCameraUrl(Camera camera, int timeoutMs)
    {
        var cameraUrls = GetAllCameraUrls();
        foreach (var cameraUrl in cameraUrls)
        {
            var url = cameraUrl.ToString();
            url = ModifyUrl(camera, url);
            if (StreamTester.TestUrl(url, timeoutMs))
            {
                return url;
            }
        }
        return String.Empty;
    }

    public static string ModifyUrl(Camera camera, string url)
    {
        if (!String.IsNullOrEmpty(camera.Username) || !String.IsNullOrEmpty(camera.Password))
        {
            url = url.Replace("[USERNAME]", camera.Username);
            url = url.Replace("[PASSWORD]", camera.Password);
        }
        else
        {
            url = url.Replace("[USERNAME]:[PASSWORD]@", String.Empty);
        }
        url = url.Replace("[IP_ADDRESS]", camera.IpAddress);
        url = url.Replace("[CHANNEL]", camera.Channel.ToString());

        if (url.Contains("[WIDTH]"))
        {
            url = url.Replace("[WIDTH]", camera.Width.ToString());
            url = url.Replace("[HEIGHT]", camera.Height.ToString());
        }

        return url;
    }

    private void InitializeCameraUrls()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resources = assembly.GetManifestResourceNames()
            .Where(name => name.StartsWith(TargetNamespace))
            .OrderBy(name => name)
            .ToList();

        foreach (var resource in resources)
        {
            var lines = ResourceHelper.ReadEmbeddedResourceByLines(resource);
            var name = Path.GetFileNameWithoutExtension(resource).Replace(TargetNamespace, String.Empty);
            cameraUrls[name] = [.. lines];
        }
    }
}
