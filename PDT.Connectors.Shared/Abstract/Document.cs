using System.Text.Json.Serialization;
using PDT.Connectors.Shared.Interfaces;

namespace PDT.Connectors.Shared.Model;

public abstract class Document:FileSystemObject
{
    [JsonIgnore]
    public IConnection Connection { get; set; }
    
    public string Ext { get; set; } = string.Empty;
    public long Size { get; set; }
    public List<string> Tags { get; set; } = new List<string>();
    
    public List<DocumentMetadataGroup> MetadataGroups { get; set; } = new List<DocumentMetadataGroup>();
    
    public abstract MemoryStream Open();
}