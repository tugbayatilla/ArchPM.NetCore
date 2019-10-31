using System;
using ArchPM.NetCore.Utilities;
using Xunit;

namespace ArchPM.NetCore.Tests
{
    public class TimeUtilitiesTests
    {
        [Fact]
        public void WaitUnit_should_throw_timeout_exception_when_expired()
        {
            Assert.ThrowsAsync<TimeoutException>(async () =>
                {
                    await TimeUtilities.WaitUntilAsync(() => false);
                }
            );
        }

        [Fact]
        public void WaitUnit_should_throw_argument_null_exception_when_condition_is_null()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () =>
                {
                    await TimeUtilities.WaitUntilAsync(null);
                }
            );
        }



        [Fact]
        public void WaitUnit_should_not_throw_timeout_exception_when_expired_and_config_set_false()
        {
            TimeUtilities
                .WaitUntilAsync(() => false,
                    p => { p.ThrowTimeExceptionWhenTimeoutReached = false; }).GetAwaiter().GetResult();

        }

        [Fact]
        public void WaitUnit_should_throw_exception_when_timeout_is_lower_than_zero()
        {
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
            {
                await TimeUtilities
                    .WaitUntilAsync(
                        () => false,
                        p => { p.Timeout = -1; }
                    );
            });
        }

    }
}
