using Microsoft.Extensions.Logging;
using PDT.CLI.Services;
using PDT.Connectors.Shared.Model;
using PDT.Models;

namespace PDT.Services;

public class RepositoryService
{
    private readonly ILogger<RepositoryService> _logger;
    private readonly MeilisearchService _meilisearchService;
    
    public RepositoryService(ILogger<RepositoryService> logger, MeilisearchService meilisearchService)
    {
        _logger = logger;
        _meilisearchService = meilisearchService;
    }
    
    public Repository AddRepository(string name, List<RepositoryConfiguration> configurations)
    {
        _logger.LogInformation($"Adding repository with name: {name}");
        var repository = new Repository
        {
            Name = name,
            Configurations = configurations
        };
        _meilisearchService.AddRepository(repository);
        _meilisearchService.CreateIndex(repository.Id);
        return repository;
    }
    
    public void RemoveRepository(string name)
    {
        _logger.LogInformation($"Removing repository with name: {name}");
        var repository = new Repository
        {
            Name = name
        };
        _meilisearchService.RemoveRepository(repository);
        _meilisearchService.DeleteIndex(repository.Id);
    }
}