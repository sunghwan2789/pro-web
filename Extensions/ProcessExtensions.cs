using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace pro_web.Extensions
{
    public static class ProcessExtensions
    {
        // https://stackoverflow.com/a/50461641
        public static async Task WaitForExitAsync(this Process process, CancellationToken cancellationToken = default(CancellationToken))
        {
            var tcs = new TaskCompletionSource<bool>();

            void Process_Exited(object sender, EventArgs e)
            {
                tcs.TrySetResult(true);
            }

            process.EnableRaisingEvents = true;
            process.Exited += Process_Exited;

            try
            {
                if (process.HasExited)
                {
                    return;
                }

                using (cancellationToken.Register(() => tcs.TrySetCanceled()))
                {
                    await tcs.Task.ConfigureAwait(false);
                }
            }
            finally
            {
                process.Exited -= Process_Exited;
            }
        }

        // https://github.com/dotnet/corefx/issues/34689
        public static async Task<bool> WaitForExitWithTimeoutAsync(this Process process, int timeout)
        {
            using (var cts = new CancellationTokenSource(timeout))
            {
                try
                {
                    await process.WaitForExitAsync(cts.Token).ConfigureAwait(false);
                    return process.HasExited;
                }
                catch (OperationCanceledException)
                {
                    return false;
                }
            }
        }
    }
}
