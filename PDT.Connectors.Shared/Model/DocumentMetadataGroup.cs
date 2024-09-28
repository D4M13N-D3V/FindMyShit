namespace PDT.Connectors.Shared.Model;

public class DocumentMetadataGroup
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<DocumentMetadataElement> Metadata { get; set; } = new List<DocumentMetadataElement>();
}