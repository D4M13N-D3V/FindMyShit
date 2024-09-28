using PDT.Connectors.Shared.Interfaces;

namespace PDT.Connectors.Shared.Model;

public abstract class Folder : FileSystemObject
{ 
    public IConnection Connection { get; set; }
    public IEnumerable<FileSystemObject> Children => GetChildren();
    public abstract IEnumerable<FileSystemObject> GetChildren();
}