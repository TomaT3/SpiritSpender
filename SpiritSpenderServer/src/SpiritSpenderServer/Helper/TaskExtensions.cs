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
        public static async Task<bool> DelayExceptionFree(this int millisecondsDelay, CancellationToken token)
        {
            var delayedSuccessfully = true;
            try
            {
                await Task.Delay(millisecondsDelay, token);
            }
            catch (OperationCanceledException)
            {
                delayedSuccessfully = false;
            }

            return delayedSuccessfully;
        }
    }
}
