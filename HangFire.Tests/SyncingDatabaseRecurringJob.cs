using Hangfire;

namespace HangFire.Tests;

public class SyncingDatabaseRecurringJob(IHttpClientFactory clientFactory, ILogger<SyncingDatabaseRecurringJob> logger)
    : IRecurringJob
{
    public async Task ExecuteAsync()
    {
        var client = clientFactory.CreateClient();

        var googleApiResponse = await client.GetStringAsync("https://google.com");
        
        logger.LogWarning("Google Responded With {response}",googleApiResponse);
    }

    public Func<string> ExecutionTime => Cron.Hourly;
    public string JobId => "DatabaseSyncJob";
}