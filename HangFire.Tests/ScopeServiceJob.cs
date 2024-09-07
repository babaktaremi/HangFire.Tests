namespace HangFire.Tests;

public class ScopeServiceJob(ILogger<ScopeServiceJob> logger)
{
    public async Task ExecuteSomeTask()
    {
        await Task.Delay(2000);
        logger.LogWarning("Executed Scope Service Job");
    }
}