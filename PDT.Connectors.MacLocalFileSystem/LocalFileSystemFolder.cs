using PDT.Connectors.Shared.Model;

namespace PDT.Connectors.LocalFileSystem;

public class LocalFileSystemFolder:Folder
{
    public override IEnumerable<Document> GetDocuments()
    {
        var files = Directory.GetFiles(Path, "*.*", SearchOption.AllDirectories);

        foreach (var file in files)
        {
            var document = new LocalFileSystemDocument
            {
                Path = file,
                FileName = System.IO.Path.GetFileName(file),
                FileExtension = System.IO.Path.GetExtension(file),
                FileSize = new FileInfo(file).Length,
                CreatedAtUtc = File.GetCreationTimeUtc(file),
                UpdatedAtUtc = File.GetLastWriteTimeUtc(file),
                LastAccessedAtUtc = File.GetLastAccessTimeUtc(file)
            };
            yield return document;
        }
    }

    public override IEnumerable<Folder> GetFolders()
    {
        var folders = Directory.GetDirectories(Path);
        foreach (var folderPath in folders)
        {
            var subFolder = new LocalFileSystemFolder
            {
                Path = folderPath,
                CreatedAtUtc = Directory.GetCreationTimeUtc(folderPath),
                UpdatedAtUtc = Directory.GetLastWriteTimeUtc(folderPath),
                LastAccessedAtUtc = Directory.GetLastAccessTimeUtc(folderPath)
            };
            yield return subFolder;
        }
    }
}