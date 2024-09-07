using Hangfire;

namespace HangFire.Tests;

public class LoggingRecurringJob(ILogger<LoggingRecurringJob> logger):IRecurringJob
{
    public Task ExecuteAsync()
    {
       logger.LogWarning("I'm running! Time: {time}",TimeProvider.System.GetLocalNow());
        return Task.CompletedTask;
    }

    public Func<string> ExecutionTime => Cron.Minutely;
    public string JobId => "LoggingRecurringJob";
}

public class LoggingRecurringJob2(ILogger<LoggingRecurringJob2> logger):IRecurringJob
{
    public Task ExecuteAsync()
    {
        logger.LogWarning("I'm running! Time: {time}",TimeProvider.System.GetLocalNow());
        return Task.CompletedTask;
    }

    public Func<string> ExecutionTime => Cron.Minutely;
    public string JobId => "LoggingRecurringJob";
}