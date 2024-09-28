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
    public test(MeilisearchService meilisearchService, TikaService tikaService)
    {
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
                    Value = "/Users/damienostler/Downloads"
                }
            }
        };
        
        _connection = new LocalFileSystemConnection()
        {
            Configurations = repository.Configurations
        };

        _meilisearchService.AddRepository(repository);
        _meilisearchService.CreateIndex(repository.Id);
        
        foreach (var file in _connection.FetchDocuments())
        {
            _meilisearchService.AddDocument(repository.Id, file);
        }
    }

    private string ExtractText(MemoryStream stream)
    {
        return _tikaService.ExtractTextAsync(stream).Result;
    }
}