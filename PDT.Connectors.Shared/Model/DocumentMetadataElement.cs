namespace PDT.Connectors.Shared.Model;

public class DocumentMetadataElement
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public bool IsRequired { get; set; }
    public bool MultiValue { get; set; }
    public object[]? Values { get; set; } = Array.Empty<object>();
    public object? Value { get; set; }
    public string? StringValue { get; set; } = string.Empty;
    public string[]? StringValues { get; set; } = Array.Empty<string>();
}