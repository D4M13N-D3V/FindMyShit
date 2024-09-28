using System.Collections.Concurrent;
using System.Threading;
using Microsoft.Extensions.Logging;
using PDT.CLI.Configs;
using TikaOnDotNet.TextExtraction;

namespace PDT.CLI.Services;

public class TikaService
{
    private readonly ILogger<TikaService> _logger;
    private readonly ConcurrentBag<TextExtractor> _textExtractors;
    private readonly SemaphoreSlim _semaphore;

    public TikaService(ILogger<TikaService> logger, TikaConfiguration configuration)
    {
        _logger = logger;
        _textExtractors = new ConcurrentBag<TextExtractor>();
        _semaphore = new SemaphoreSlim(configuration.TikaThreadPoolSize);

        for (int i = 0; i < configuration.TikaThreadPoolSize; i++)
        {
            _textExtractors.Add(new TextExtractor());
        }
    }

    public async Task<string> ExtractTextAsync(MemoryStream stream)
    {
        await _semaphore.WaitAsync();
        try
        {
            if (_textExtractors.TryTake(out var textExtractor))
            {
                try
                {
                    var result = await Task.Run(() => textExtractor.Extract(stream.ToArray()));
                    return result.Text;
                }
                finally
                {
                    _textExtractors.Add(textExtractor);
                }
            }
            else
            {
                _logger.LogError("No available text extractors.");
                throw new InvalidOperationException("No available text extractors.");
            }
        }
        finally
        {
            _semaphore.Release();
        }
    }
}