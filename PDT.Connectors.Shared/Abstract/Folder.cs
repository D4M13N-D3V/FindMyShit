using PDT.Connectors.Shared.Interfaces;

namespace PDT.Connectors.Shared.Model;

public abstract class Folder : FileSystemObject
{ 
    public const bool IsFolder = true;
    public IConnection Connection { get; set; }
    public IEnumerable<Folder> Holders => GetFolders();
    public abstract IEnumerable<Folder> GetFolders();
    public IEnumerable<Document> Documents => GetDocuments();
    public abstract IEnumerable<Document> GetDocuments();
}