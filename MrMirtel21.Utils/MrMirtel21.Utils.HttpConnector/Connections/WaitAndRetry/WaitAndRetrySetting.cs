using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMirtel21.Utils.HttpConnector.Connections.WaitAndRetry
{
    public class WaitAndRetrySetting : IWaitAndRetrySetting
    {
        public int RetryCount { get; init; }
        public int DurationOfWaitInSeconds { get; init; }
        public Action<Exception, TimeSpan>? OnRetry{ get; private set; }

        public WaitAndRetrySetting(int exceptionsAllowedBeforeBreaking, int durationOfBreakInSeconds)
        {
            RetryCount = exceptionsAllowedBeforeBreaking;
            DurationOfWaitInSeconds = durationOfBreakInSeconds;
        }

        public void SetOnRetry(Action<Exception, TimeSpan> onRetry)
        {
            if (onRetry != null)
            {
                OnRetry = onRetry;
            }
        }
    }
}
