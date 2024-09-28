using PDT.Connectors.Shared.Interfaces;

namespace PDT.Connectors.Shared.Model;

public abstract class Folder
{ 
    public IConnection Connection { get; set; }
    public string Id { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAtUtc { get; set; }
    public DateTime UpdatedAtUtc { get; set; }
    public DateTime LastAccessedAtUtc { get; set; }
    public IEnumerable<Document> Documents => GetDocuments();
    public IEnumerable<Folder> Folders => GetFolders();
    public abstract IEnumerable<Document> GetDocuments();
    public abstract IEnumerable<Folder> GetFolders();
}