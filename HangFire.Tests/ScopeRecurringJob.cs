using Hangfire;

namespace HangFire.Tests;

public class ScopeRecurringJob(IServiceProvider serviceProvider):IRecurringJob
{
    public async Task ExecuteAsync()
    {
        await using var scope = serviceProvider.CreateAsyncScope();

        var scopeJob = scope.ServiceProvider.GetRequiredService<ScopeServiceJob>();

        await scopeJob.ExecuteSomeTask();
    }

    public Func<string> ExecutionTime => Cron.Minutely;
    public string JobId => "ScopeServiceRecurringJob";
}