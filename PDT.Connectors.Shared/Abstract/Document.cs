namespace PDT.Connectors.Shared.Model;

public abstract class Document
{
    public string Id { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;
    public string AuthorEmail { get; set; } = string.Empty;
    public string EditorName { get; set; } = string.Empty;
    public string EditorEmail { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string FileExtension { get; set; } = string.Empty;
    public DateTime CreatedAtUtc { get; set; }
    public DateTime UpdatedAtUtc { get; set; }
    public DateTime LastAccessedAtUtc { get; set; }
    public long FileSize { get; set; }
    public List<DocumentMetadataGroup> MetadataGroups { get; set; } = new List<DocumentMetadataGroup>();
    
    public abstract MemoryStream Open();
}