namespace HangFire.Tests;

/// <summary>
/// All classes that implement this interface must have singleton lifetime
/// </summary>
public interface IRecurringJob
{
    Task ExecuteAsync();
    
    Func<string> ExecutionTime { get; }
    
    string JobId { get; }
}