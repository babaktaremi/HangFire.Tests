using Hangfire;

namespace HangFire.Tests;

public static class Extensions
{
    public static IServiceCollection AddRecurringJobs(this IServiceCollection services)
    {
        var recurringJobs = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(c => c.GetExportedTypes())
            .Where(t => typeof(IRecurringJob).IsAssignableFrom(t) && t is { IsClass: true, IsAbstract: false });

        foreach (var recurringJob in recurringJobs)
        {
            services.AddSingleton(typeof(IRecurringJob), recurringJob);
        }

        return services;
    }

    public static IServiceCollection AddHangFireAuthorizationPolicy(this IServiceCollection services)
    {
        services.AddSingleton<HangFirePanelAuthorization>();

        return services;
    }

    public static void RunRecurringJobs(this WebApplication app)
    {
        var backgroundJobClient = app.Services.GetRequiredService<IRecurringJobManager>();

        var recurringJobs = app.Services.GetRequiredService<IEnumerable<IRecurringJob>>();

        foreach (var recurringJob in recurringJobs)
        {
            backgroundJobClient
                .AddOrUpdate(recurringJob.JobId,  () => recurringJob.ExecuteAsync(),
                recurringJob.ExecutionTime);
        }
    }

    public static void ConfigureHangFirePanel(this WebApplication app)
    {
        var panelAuthorizationPolicy = app.Services.GetRequiredService<HangFirePanelAuthorization>();
        
        
        app.MapHangfireDashboard("/hangfireDashboard",new DashboardOptions()
        {
            IgnoreAntiforgeryToken = true,
            Authorization = panelAuthorizationPolicy.SetBasicAuthorizationFilter(),
            DisplayStorageConnectionString = false
        });
    }
}