using PDT.Connectors.Shared.Model;

namespace PDT.Connectors.LocalFileSystem;

public class LocalFileSystemDocument : Document
{
    public override string Type => "Document";
    public override MemoryStream Open()
    {
        using (var stream = File.Open(Path, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            var result = new MemoryStream();
            stream.Position = 0;
            stream.CopyTo(result);
            result.Position = 0;
            return result;
        }
    }
}