using Microsoft.Extensions.Options;
using Polly;

namespace MrMirtel21.Utils.HttpConnector.Connections.CircuitBreaker
{
    public class CircuitBreakerConnection : IConnectionBehavior
    {
        private readonly int _exceptionsAllowedBeforeBreaking;
        private readonly int _durationOfBreakInSeconds;
        private readonly Action<Exception, TimeSpan> _onBreak = (exception, span) => { };
        private readonly Action _onReset = () => { };

        public CircuitBreakerConnection(IOptions<CircuitBreakerSettings> circuitSettings)
        {
            _exceptionsAllowedBeforeBreaking = circuitSettings.Value.ExceptionsAllowedBeforeBreaking;
            _durationOfBreakInSeconds = circuitSettings.Value.DurationOfBreakInSeconds;

            if (circuitSettings.Value.OnBreak != null)
            {
                _onBreak = circuitSettings.Value.OnBreak;
            }

            if (circuitSettings.Value.OnReset != null)
            {
                _onReset = circuitSettings.Value.OnReset;
            }
        }
        public CircuitBreakerConnection(int exceptionsAllowedBeforeBreaking,
            int durationOfBreakInSeconds,
            Action<Exception, TimeSpan>? onBreak = null, Action? onReset = null)
        {
            _exceptionsAllowedBeforeBreaking = exceptionsAllowedBeforeBreaking;
            _durationOfBreakInSeconds = durationOfBreakInSeconds;

            if (onBreak != null)
            {
                _onBreak = onBreak;
            }

            if (onReset != null)
            {
                _onReset = onReset;
            }
        }

        public T ExecuteRequest<T>(Func<T> request)
        {
            var policy = Policy
                .Handle<Exception>()
                .CircuitBreaker(
                    exceptionsAllowedBeforeBreaking: _exceptionsAllowedBeforeBreaking,
                    onBreak: _onBreak,
                    onReset: _onReset,
                    durationOfBreak: TimeSpan.FromSeconds(_durationOfBreakInSeconds)
                );

            PolicyResult<T> result = policy.ExecuteAndCapture(request);
            return result.Result;
        }
    }
}
