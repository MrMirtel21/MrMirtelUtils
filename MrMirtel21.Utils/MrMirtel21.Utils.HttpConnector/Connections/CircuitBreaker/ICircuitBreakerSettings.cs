namespace MrMirtel21.Utils.HttpConnector.Connections.CircuitBreaker
{
    public interface ICircuitBreakerSettings
    {
        int ExceptionsAllowedBeforeBreaking { get; init; }
        int DurationOfBreakInSeconds { get; init; }
        Action<Exception, TimeSpan>? OnBreak { get; }
        Action? OnReset { get; }
        void SetOnBreak(Action<Exception, TimeSpan> onBreak);
        void SetOnReset(Action onReset);
    }
}
