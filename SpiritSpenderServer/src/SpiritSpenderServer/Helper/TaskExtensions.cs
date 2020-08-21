using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace SpiritSpenderServer.Helper
{
    public static class TaskExtensions
    {
        public static async Task DelayExceptionFree(this int millisecondsDelay, CancellationToken token)
        {
            try
            {
                await Task.Delay(millisecondsDelay, token);
            }
            catch (OperationCanceledException)
            {
                // do nothing
            }
        }
    }
}
