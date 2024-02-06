using Microsoft.Extensions.Options;
using Polly;

namespace MrMirtel21.Utils.HttpConnector.Connections.WaitAndRetry
{
    public class WaitAndRetryConnection : IConnectionBehavior
    {
        private int _exceptionsAllowedBeforeBreaking;
        private int _durationOfWaitInSeconds;
        private readonly Action<Exception, TimeSpan> _onRetry = (exception, span) => { };
        public WaitAndRetryConnection(IOptions<WaitAndRetrySetting> waitAndRetrySettings)
        {
            _exceptionsAllowedBeforeBreaking = waitAndRetrySettings.Value.RetryCount;
            _durationOfWaitInSeconds = waitAndRetrySettings.Value.DurationOfWaitInSeconds;

            if (waitAndRetrySettings.Value.OnRetry != null)
            {
                _onRetry = waitAndRetrySettings.Value.OnRetry;
            }
        }

        public WaitAndRetryConnection(int exceptionsAllowedBeforeBreaking, int durationOfWaitInSeconds, Action<Exception, TimeSpan>? onRetry = null)
        {
            _exceptionsAllowedBeforeBreaking = exceptionsAllowedBeforeBreaking;
            _durationOfWaitInSeconds = durationOfWaitInSeconds;
            if(onRetry != null)
            {
                _onRetry = onRetry;
            }     
        }

        public T ExecuteRequest<T>(Func<T> request)
        {
            var policy = Policy
                .Handle<Exception>()
                .WaitAndRetry(
                    retryCount: _exceptionsAllowedBeforeBreaking,
                    attemp => TimeSpan.FromSeconds(_durationOfWaitInSeconds),
                    onRetry: _onRetry
                ); ;

            PolicyResult<T> result = policy.ExecuteAndCapture(request);
            return result.Result;
        }
    }
}
