using MrMirtel21.Utils.HttpConnector.Connections;
using MrMirtel21.Utils.HttpConnector.Connections.CircuitBreaker;
using MrMirtel21.Utils.HttpConnector.Connections.WaitAndRetry;

namespace MrMirtel21.Utils.HttpConnector.Factories
{
    public interface IConnectionFactory
    {
        IConnectionBehavior CircuitBreaker(ICircuitBreakerSettings settings);
        IConnectionBehavior WaitAndRetry(IWaitAndRetrySetting settings);
    }
}