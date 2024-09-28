using Microsoft.Extensions.Configuration;

namespace PDT.CLI.Configs;

public class MeiliConfiguration
{
    private readonly string _dockerSocket = "PDT:DockerSocket";
    private readonly string _dockerContainerName = "PDT:DockerContainerName";
    private readonly string _meiliImage = "PDT:MeiliImage";
    private readonly string _meiliPort = "PDT:MeiliPort";
    private readonly string _dockerHealthCheckInterval = "PDT:DockerHealthCheckInterval";
    private readonly string _meiliRepositoryIndex = "PDT:MeiliRepositoryIndexName";
    private readonly IConfiguration _configuration;
    public MeiliConfiguration(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public string MeiliRepositoryIndex => _configuration.GetValue<string>(_meiliRepositoryIndex) ?? string.Empty;
    public string MeiliImage => _configuration.GetValue<string>(_meiliImage) ?? string.Empty;
    public string DockerContainerName => _configuration.GetValue<string>(_dockerContainerName) ?? string.Empty;
    public string DockerSocket => _configuration.GetValue<string>(_dockerSocket) ?? string.Empty;
    public int MeiliPort => _configuration.GetValue<int>(_meiliPort);
    public int DockerHealthCheckInterval => _configuration.GetValue<int>(_dockerHealthCheckInterval);
}