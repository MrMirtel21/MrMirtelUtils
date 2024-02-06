namespace MrMirtel21.Utils.HttpConnector.Connections.CircuitBreaker
{
    public class CircuitBreakerSettings : ICircuitBreakerSettings
    {
        public int ExceptionsAllowedBeforeBreaking { get; init; }
        public int DurationOfBreakInSeconds { get; init; }
        public Action<Exception, TimeSpan>? OnBreak { get; private set; }
        public Action? OnReset { get; private set; }

        public CircuitBreakerSettings(int exceptionsAllowedBeforeBreaking, int durationOfBreakInSeconds)
        {
            ExceptionsAllowedBeforeBreaking = exceptionsAllowedBeforeBreaking;
            DurationOfBreakInSeconds = durationOfBreakInSeconds;
        }

        public void SetOnBreak(Action<Exception, TimeSpan> onBreak)
        {
            if (onBreak != null)
            {
                OnBreak = onBreak;
            }
        }

        public void SetOnReset(Action onReset)
        {
            if (onReset != null)
            {
                OnReset = onReset;
            }
        }
    }
}
