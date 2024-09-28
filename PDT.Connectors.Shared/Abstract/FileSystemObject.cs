namespace PDT.Connectors.Shared.Model;

public abstract class FileSystemObject
{
    public abstract string Type { get; }
    public string Id { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAtUtc { get; set; }
    public DateTime UpdatedAtUtc { get; set; }
    public DateTime LastAccessedAtUtc { get; set; }
}