using System;
using System.Threading.Tasks;
using ArchPM.NetCore.Extensions;

namespace ArchPM.NetCore.Utilities
{
    /// <summary>
    /// 
    /// </summary>
    public static class TimeUtilities
    {
        /// <summary>
        /// Waits the until asynchronous.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="configAction">The configuration action.</param>
        /// <exception cref="TimeoutException"></exception>
        public static async Task WaitUntilAsync(Func<bool> condition, Action<WaitUntilConfig> configAction = null)
        {
            condition.ThrowExceptionIfNull<ArgumentNullException>(nameof(condition));

            var config = new WaitUntilConfig();
            configAction?.Invoke(config);
            config.Timeout.ThrowExceptionIf(p => p < 0, new ArgumentOutOfRangeException($"{nameof(config.Timeout)} must be greater than zero!"));

            var waitTask = Task.Run(async () =>
            {
                while (!condition())
                {
                    await Task.Delay(config.Frequency);
                }
            });

            if (waitTask != await Task.WhenAny(waitTask, Task.Delay(config.Timeout)))
            {
                if (config.ThrowTimeExceptionWhenTimeoutReached)
                {
                    throw new TimeoutException();
                }
            }

        }
    }
}
