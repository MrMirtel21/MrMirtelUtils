using MrMirtel21.Utils.HttpConnector.Connections;
using MrMirtel21.Utils.HttpConnector.Connections.CircuitBreaker;
using MrMirtel21.Utils.HttpConnector.Connections.WaitAndRetry;

namespace MrMirtel21.Utils.HttpConnector.Factories
{
    public class ConnectionFactory : IConnectionFactory
    {
        public IConnectionBehavior CircuitBreaker(ICircuitBreakerSettings settings)
        {
            IConnectionBehavior behavior = new CircuitBreakerConnection(settings.ExceptionsAllowedBeforeBreaking,
                settings.DurationOfBreakInSeconds, settings.OnBreak, settings.OnReset);
            return behavior;
        }

        public IConnectionBehavior WaitAndRetry(IWaitAndRetrySetting settings)
        {
            IConnectionBehavior behavior = new WaitAndRetryConnection(settings.RetryCount, settings.DurationOfWaitInSeconds, settings.OnRetry);
            return behavior;
        }
    }
}