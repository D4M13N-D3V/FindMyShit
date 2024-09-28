using PDT.Connectors.Shared.Model;

namespace PDT.Connectors.LocalFileSystem;

public class LocalFileSystemFolder:Folder
{
    public override IEnumerable<FileSystemObject> GetChildren()
    {
        return Connection.Fetch(this);
    }

    public override string Type => "Folder";
}