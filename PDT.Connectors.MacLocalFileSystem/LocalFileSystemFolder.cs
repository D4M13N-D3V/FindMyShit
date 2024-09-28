using PDT.Connectors.Shared.Model;

namespace PDT.Connectors.LocalFileSystem;

public class LocalFileSystemFolder:Folder
{
    public override IEnumerable<Document> GetDocuments()
    {
        return Connection.FetchDocuments(this);
    }

    public override IEnumerable<Folder> GetFolders()
    {
        return Connection.FetchFolders(this);
    }
}