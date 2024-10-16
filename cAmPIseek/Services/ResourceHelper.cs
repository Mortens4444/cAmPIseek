using System.Reflection;
using System.Text;

namespace cAmPIseek.Services;

internal static class ResourceHelper
{
    private static readonly char[] lineSeparators = ['\r', '\n'];

    public static Stream GetEmbeddedResourceStream(string resourceName, Assembly assembly)
    {
        return assembly.GetManifestResourceStream(resourceName) ?? throw new ArgumentException($"Resource '{resourceName}' not found.", nameof(resourceName));
    }

    public static string ReadEmbeddedResource(string resourceName, Assembly assembly, Encoding encoding)
    {
        using var stream = GetEmbeddedResourceStream(resourceName, assembly) ?? throw new ArgumentException($"Resource '{resourceName}' not found.", nameof(resourceName));
        using var reader = new StreamReader(stream, encoding);
        return reader.ReadToEnd();
    }

    public static string[] ReadEmbeddedResourceByLines(string resourceName)
    {
        return ReadEmbeddedResourceByLines(resourceName, Assembly.GetExecutingAssembly(), Encoding.UTF8);
    }

    public static string[] ReadEmbeddedResourceByLines(string resourceName, Assembly assembly, Encoding encoding)
    {
        var content = ReadEmbeddedResource(resourceName, assembly, encoding);
        return content.Split(lineSeparators, StringSplitOptions.RemoveEmptyEntries);
    }
}
