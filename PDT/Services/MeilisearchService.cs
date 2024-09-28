using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using Meilisearch;
using Microsoft.Extensions.Logging;
using PDT.CLI.Configs;
using PDT.Connectors.Shared.Model;
using PDT.Models;
using PDT.Services;

namespace PDT.CLI.Services;

public class MeilisearchService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<MeilisearchService> _logger;
    public readonly MeilisearchClient Client;
    private readonly MeiliConfiguration _meiliConfiguration;
    private ObservableCollection<KeyValuePair<string,FileSystemObject>> _documentCollection;
    private const int THRESHOLD = 10000; // Define your threshold here

    public MeilisearchService(HttpClient httpClient, ILogger<MeilisearchService> logger, MeiliConfiguration meiliConfiguration)
    {
        _httpClient = httpClient;
        _meiliConfiguration = meiliConfiguration;
        _logger = logger;
        EnsureMeilisearchIsRunning();
        Client = new MeilisearchClient("http://localhost:"+meiliConfiguration.MeiliPort, "kToLWXAc2Qvm7yamYuBNE5DyYFka4koTo0ebGr7nBYo");
        EnsureRepositoryIndexExists();
        _documentCollection = new ObservableCollection<KeyValuePair<string,FileSystemObject>>();
        _documentCollection.CollectionChanged += CheckIfNeedSync;
    }

    private void EnsureMeilisearchIsRunning()
    {
        if (!IsMeilisearchRunning())
        {
            StartMeilisearch().Wait();
        }
    }
    private bool IsMeilisearchRunning()
    {
        var processes = Process.GetProcessesByName("meilisearch");
        return processes.Any();
    }
    private async Task StartMeilisearch()
    {
        
        if (!File.Exists(Path.Combine(AppContext.BaseDirectory, "meilisearch")))
        {
            _logger.LogError("Meilisearch binary not found at root");
            return;
        }

        var path = Path.Combine(AppContext.BaseDirectory, "meilisearch");
        var args = "--http-addr 127.0.0.1:" + _meiliConfiguration.MeiliPort 
                    + " --master-key kToLWXAc2Qvm7yamYuBNE5DyYFka4koTo0ebGr7nBYo" 
                    + " --env development --db-path " + Path.Combine(AppContext.BaseDirectory, "db");
        var processStartInfo = new ProcessStartInfo
        {
            FileName = path,
            Arguments = args,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };

        var process = new Process { StartInfo = processStartInfo };
        process.Start();
        await Task.Delay(5000);
        _logger.LogInformation("Started Meilisearch process.");
    }
    
    
    private void CheckIfNeedSync(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if(_documentCollection.Count>=THRESHOLD)
        {
            _logger.LogInformation("Threshold reached, syncing metadata to server.");
            var grouped = _documentCollection.GroupBy(pair => pair.Key)
                .ToDictionary(group => group.Key, group => group.Select(pair => pair.Value).ToList());
            foreach (var repository in grouped)
            {
                var repositoryIndex = Client.GetIndexAsync(repository.Key).Result;
                var documents = _documentCollection.ToList();
                _documentCollection.Clear();
                var result = repositoryIndex.AddDocumentsAsync(repository.Value, "id").Result;
            }
        }
    }
    
    private async void EnsureRepositoryIndexExists()
    {
        Task.Delay(5000).Wait();
        var indexes = Client.GetAllIndexesAsync().Result;
        if (indexes.Results.Any(x => x.Uid == _meiliConfiguration.MeiliRepositoryIndex))
        {
            _logger.LogInformation("Repository index already exists, skipping creation of index.");
            return;
        }
        _logger.LogInformation("Creating Repository index for application to store documents...");
        Client.CreateIndexAsync(_meiliConfiguration.MeiliRepositoryIndex).Wait();
    }
    
    private readonly List<string> FIELDS = new List<string>
    {
        "ID",
        "PATH",
        "CREATEDATUTC",
        "UPDATEDATUTC",
        "LASTACCESSEDATUTC",
        "NAME",
        "TYPE"
    };
    
    /// <summary>
    /// Creates a new index on the Meilisearch server if it does not already exist.
    /// </summary>
    /// <param name="indexName">The name for the new index.</param>
    public void CreateIndex(string indexName)
    {
        var indexes = Client.GetAllIndexesAsync().Result;
        if (indexes.Results.Any(x => x.Uid == indexName))
        {
            _logger.LogInformation($"Index '{indexName}' already exists, skipping creation of index.");
            return;
        }
        _logger.LogInformation($"Creating index '{indexName}'...");
        Client.CreateIndexAsync(indexName).Wait();
        Task.Delay(5000).Wait();
        var index = Client.GetIndexAsync(indexName).Result;
        var test = index.GetFilterableAttributesAsync().Result;
        index.UpdateFilterableAttributesAsync(FIELDS).Wait();
        index.UpdateFilterableAttributesAsync(FIELDS.Select(x=>x.ToLower())).Wait();
    }

    /// <summary>
    /// Add a repository to the repository index.
    /// </summary>
    /// <param name="repository">The repository</param>
    /// <exception cref="NotImplementedException"></exception>
    public void AddRepository(Repository repository)
    {
        _logger.LogInformation($"Adding repository '{repository.Name}' to the repository index...");
        var repositoryIndex = Client.GetIndexAsync(_meiliConfiguration.MeiliRepositoryIndex).Result;
        repositoryIndex.AddDocumentsAsync(new List<Repository> { repository }).Wait();
    }

    /// <summary>
    /// Remove a repository from the reposuitory index.
    /// </summary>
    /// <param name="repository"The repository></param>
    public void RemoveRepository(Repository repository)
    {
        _logger.LogInformation($"Removing repository '{repository.Name}' to the repository index...");
        var repositoryIndex = Client.GetIndexAsync(_meiliConfiguration.MeiliRepositoryIndex).Result;
        repositoryIndex.DeleteOneDocumentAsync(repository.Id).Wait();
    }

    /// <summary>
    /// Deletes a index for a repoistory.
    /// </summary>
    /// <param name="indexName">The name of the index.</param>
    public void DeleteIndex(string indexName)
    {
        var indexes = Client.GetAllIndexesAsync().Result;
        if (indexes.Results.Any(x => x.Uid != indexName))
        {
            _logger.LogInformation($"Index '{indexName}' does not exist, skipping deletion of index.");
            return;
        }
        _logger.LogInformation($"Deleting index '{indexName}'...");
        Client.DeleteIndexAsync(indexName).Wait();
    }
    
    public void AddDocument(string repositoryId, Document document)
    {
        _logger.LogTrace($"Adding document '{document.Path}{document.Name}.{document.Ext}' to repository '{repositoryId}'...");
        _documentCollection.Add(new KeyValuePair<string, FileSystemObject>(repositoryId, document));
    }
    public void AddFolder(string repositoryId, Folder folder)
    {
        _logger.LogTrace($"Adding folder '{folder.Path}{folder.Name}' to repository '{repositoryId}'...");
        _documentCollection.Add(new KeyValuePair<string, FileSystemObject>(repositoryId, folder));
    }

    public List<string> GetAllIndexes()
    {
        return Client.GetAllIndexesAsync().Result.Results.Select(x => x.Uid).Where(x=>x!=_meiliConfiguration.MeiliRepositoryIndex).ToList();
    }
}