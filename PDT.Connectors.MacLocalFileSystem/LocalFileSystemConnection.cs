﻿using System.Security.AccessControl;
using System.Security.Principal;
using System.Text.RegularExpressions;
using MetadataExtractor;
using Microsoft.Extensions.Logging;
using PDT.Connectors.Shared.Interfaces;
using PDT.Connectors.Shared.Model;
using Directory = System.IO.Directory;

namespace PDT.Connectors.LocalFileSystem;

public class LocalFileSystemConnection:IConnection
{
    private readonly ILogger<LocalFileSystemDocument> _logger;
    public List<RepositoryConfiguration> Configurations { get; set; }
    
    public string Name { get; set; }
    public string Id { get; set; }
    public Folder RootFolder { get; set; }
    
    public LocalFileSystemConnection(ILogger<LocalFileSystemDocument>? logger = null)
    {
        _logger = logger;
    }
    
    public IEnumerable<Document> FetchDocuments()
    {
        var targetFolder = Configurations.FirstOrDefault(x=>x.Key == "TargetFolder")?.Value ?? "C:\\";
        RootFolder = new LocalFileSystemFolder
        {
            Path = targetFolder,
            CreatedAtUtc = Directory.GetCreationTimeUtc(targetFolder),
            UpdatedAtUtc = Directory.GetLastWriteTimeUtc(targetFolder),
            LastAccessedAtUtc = Directory.GetLastAccessTimeUtc(targetFolder)
        };
        return FetchDocuments(RootFolder);
    }


    private List<DocumentMetadataGroup> GetMetadata(LocalFileSystemDocument document)
    {
        var result = new List<DocumentMetadataGroup>();
        try
        {
            using(var stream = document.Open())
            {
                var metadata = ImageMetadataReader.ReadMetadata(stream);
                foreach (var directory in metadata)
                {
                    var group = new DocumentMetadataGroup()
                    {
                        Id = directory.Name,
                        Name = directory.Name,
                    };
                    foreach(var tag in directory.Tags)
                    {
                        group.Metadata.Add(new DocumentMetadataElement()
                        {
                            Id = tag.Name,
                            Name = tag.Name,
                            Description = string.Empty,
                            IsRequired = false,
                            MultiValue = false,
                            Value = tag?.Description,
                            StringValue = tag?.Description,
                            Values =null,
                            StringValues = null,
                        });
                    }
                    result.Add(group);
                }
            }
        }
        catch (Exception e)
        {
            _logger?.LogError(e,$"Could not retrieve the metadata for the document. {document.Path} {document.FileName}");
        }
        return result;
    }
    private string ToLowerAndReplaceSpecialChars(string input)
    {
        if (string.IsNullOrEmpty(input)) return string.Empty;
        input = input.ToLower();
        input = Regex.Replace(input, @"[^a-z0-9]+", "-");
        return input;
    }
    public IEnumerable<Document> FetchDocuments(Folder folder)
    {
        var files = Directory.GetFiles(folder.Path, "*.*", SearchOption.AllDirectories);

        foreach (var file in files)
        {
            var document = new LocalFileSystemDocument
            {
                Id = ToLowerAndReplaceSpecialChars(file),
                Path = Path.GetDirectoryName(file),
                FileName = Path.GetFileName(file),
                FileExtension = Path.GetExtension(file),
                FileSize = new FileInfo(file).Length,
                CreatedAtUtc = File.GetCreationTimeUtc(file),
                UpdatedAtUtc = File.GetLastWriteTimeUtc(file),
                LastAccessedAtUtc = File.GetLastAccessTimeUtc(file),
            };
            document.MetadataGroups = GetMetadata(document);
            yield return document;
        }
        
        var folders = Directory.GetDirectories(folder.Path);
        foreach (var folderPath in folders)
        {
            var subFolder = new LocalFileSystemFolder
            {
                Path = folderPath,
                CreatedAtUtc = Directory.GetCreationTimeUtc(folderPath),
                UpdatedAtUtc = Directory.GetLastWriteTimeUtc(folderPath),
                LastAccessedAtUtc = Directory.GetLastAccessTimeUtc(folderPath),
            };
            foreach (var document in FetchDocuments(subFolder))
                yield return document;
        }
    }
 

}