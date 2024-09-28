using Microsoft.Extensions.Logging;
using PDT.CLI.Services;
using PDT.Connectors.LocalFileSystem;
using PDT.Connectors.Shared.Interfaces;
using PDT.Connectors.Shared.Model;
using PDT.Models;

namespace PDT.CLI;

public class test
{
    private readonly IConnection _connection;
    private readonly MeilisearchService _meilisearchService;
    private readonly TikaService _tikaService;
    private readonly ILogger<test> _logger;
    public test(ILogger<test> logger,MeilisearchService meilisearchService, TikaService tikaService)
    {
        _logger = logger;
        _tikaService = tikaService;
        _meilisearchService = meilisearchService;

        var repository = new Repository()
        {
            Name = "Test",
            Description = "Test",
            CreatedAtUtc = DateTime.UtcNow,
            UpdatedAtUtc = DateTime.UtcNow,
            Configurations = new List<RepositoryConfiguration>
            {
                new RepositoryConfiguration
                {
                    Key = "TargetFolder",
                    Value = "/Users/damienostler/Documents"
                }
            }
        };
        
        _connection = new LocalFileSystemConnection()
        {
            Configurations = repository.Configurations
        };

        _meilisearchService.AddRepository(repository);
        _meilisearchService.CreateIndex(repository.Id);
        
        foreach (var obj in _connection.Fetch())
        {
            _logger.LogInformation($"Processing file {obj.Name}");
            if(obj is Document document)
            {
                _logger.LogInformation($"Processing document {document.Name}");
                _meilisearchService.AddDocument(repository.Id, document);
            }
            
            if(obj is Folder folder)
            {
                _logger.LogInformation($"Processing folder {folder.Name}");
                _meilisearchService.AddFolder(repository.Id, folder);
            }
        }
    }

    private string ExtractText(MemoryStream stream)
    {
        return _tikaService.ExtractTextAsync(stream).Result;
    }
}