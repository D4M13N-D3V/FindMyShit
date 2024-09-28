namespace PDT.Connectors.Shared.Model;

public abstract class Folder
{ 
    public string Id { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public string RepositoryId { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;
    public string AuthorEmail { get; set; } = string.Empty;
    public string EditorName { get; set; } = string.Empty;
    public string EditorEmail { get; set; } = string.Empty;
    public DateTime CreatedAtUtc { get; set; }
    public DateTime UpdatedAtUtc { get; set; }
    public DateTime LastAccessedAtUtc { get; set; }
    public IEnumerable<Document> Documents => GetDocuments();
    public IEnumerable<Folder> Folders => GetFolders();
    public abstract IEnumerable<Document> GetDocuments();
    public abstract IEnumerable<Folder> GetFolders();
}