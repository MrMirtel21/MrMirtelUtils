namespace MrMirtel21.Utils.HttpConnector.Connections.WaitAndRetry
{
    public interface IWaitAndRetrySetting
    {
        int RetryCount { get; init; }
        int DurationOfWaitInSeconds { get; init; }
        Action<Exception, TimeSpan>? OnRetry { get; }
    }
}
