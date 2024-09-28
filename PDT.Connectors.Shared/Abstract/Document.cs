using System.Text.Json.Serialization;
using PDT.Connectors.Shared.Interfaces;

namespace PDT.Connectors.Shared.Model;

public abstract class Document
{
    [JsonIgnore]
    public IConnection Connection { get; set; }
    public string Id { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string FileExtension { get; set; } = string.Empty;
    public DateTime CreatedAtUtc { get; set; }
    public DateTime UpdatedAtUtc { get; set; }
    public DateTime LastAccessedAtUtc { get; set; }
    public long FileSize { get; set; }
    public List<string> Tags { get; set; } = new List<string>();
    
    public List<DocumentMetadataGroup> MetadataGroups { get; set; } = new List<DocumentMetadataGroup>();
    
    public abstract MemoryStream Open();
}