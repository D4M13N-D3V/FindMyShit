using System.Text.Json.Serialization;
using PDT.Connectors.Shared.Interfaces.Converters;

namespace PDT.Connectors.Shared.Model;

public abstract class FileSystemObject
{
    public abstract string Type { get; }
    public string Id { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    [JsonConverter(typeof(DateTimeTicksConverter))]
    public DateTime CreatedAtUtc { get; set; }

    [JsonConverter(typeof(DateTimeTicksConverter))]
    public DateTime UpdatedAtUtc { get; set; }

    [JsonConverter(typeof(DateTimeTicksConverter))]
    public DateTime LastAccessedAtUtc { get; set; }

    [JsonConverter(typeof(DateTimeTicksConverter))]
    public DateTime ScannedAtUtc { get; set; }
}