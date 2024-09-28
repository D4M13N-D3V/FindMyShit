using Microsoft.Extensions.Configuration;

namespace PDT.CLI.Configs;

public class TikaConfiguration
{
    
    private readonly string _tikaThreadSize = "PDT:TikaThreadPoolSize";
    private readonly IConfiguration _configuration;
    public TikaConfiguration(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public int TikaThreadPoolSize => _configuration.GetValue<int>(_tikaThreadSize);
}