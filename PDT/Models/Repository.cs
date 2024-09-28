using System.Text.RegularExpressions;
using PDT.Connectors.Shared.Model;

namespace PDT.Models;

public class Repository
{
    public string Id => ToLowerAndReplaceSpecialChars(Name);
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAtUtc { get; set; }
    public DateTime UpdatedAtUtc { get; set; }
    public string Cursor { get; set; }
    public List<RepositoryConfiguration> Configurations { get; set; } = new List<RepositoryConfiguration>();
    private string ToLowerAndReplaceSpecialChars(string input)
    {
        if (string.IsNullOrEmpty(input)) return string.Empty;
        input = input.ToLower();
        input = Regex.Replace(input, @"[^a-z0-9]+", "-");
        return input;
    }
}