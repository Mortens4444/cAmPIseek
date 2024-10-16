namespace cAmPIseek.Model;

internal class Camera
{
    public string IpAddress { get; set; } = String.Empty;

    public string Username { get; set; } = String.Empty;

    public string Password { get; set; } = String.Empty;

    public int Channel { get; set; }

    public int Width { get; set; }

    public int Height { get; set; }

    public override string ToString()
    {
        return IpAddress;
    }
}
