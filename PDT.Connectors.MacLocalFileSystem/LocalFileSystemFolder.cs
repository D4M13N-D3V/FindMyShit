using PDT.Connectors.Shared.Model;

namespace PDT.Connectors.LocalFileSystem;

public class LocalFileSystemFolder:Folder
{
    public const bool Folder = true;
    public override IEnumerable<FileSystemObject> GetChildren()
    {
        return Connection.Fetch(this);
    }
}