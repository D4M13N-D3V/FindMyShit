using PDT.Connectors.Shared.Model;

namespace PDT.Connectors.Shared.Interfaces;

public interface IConnection
{
    public List<RepositoryConfiguration> Configurations { get; set; }
    public string Name { get; set; }
    public string Id { get; set; }
    public Folder RootFolder { get; set; }
    public IEnumerable<FileSystemObject> Fetch();
    public IEnumerable<FileSystemObject> Fetch(Folder folder);
    // public Document FetchDocument(string documentId);
    // public Folder FetchFolder(string folderId);
    // public void AddDocument(Folder folder, Document document, Stream stream);
    // public void AddDocument(string folderId, Document document, Stream stream);
    // public void AddFolder(Folder folder, string name);
    // public void AddFolder(string folderId, string name);
    // public void RemoveDocument(Document document);
    // public void RemoveDocument(string documentId);
    // public void RemoveFolder(Folder folder);
    // public void RemoveFolder(string folderId);
}