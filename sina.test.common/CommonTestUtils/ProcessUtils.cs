using System.Diagnostics;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace sina.test.common.CommonTestUtils
{
    public static class ProcessUtils
    {
        public static void StartAndWaitForExit(string fileName, string arguments)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = fileName,
                    Arguments = arguments,
                    CreateNoWindow = true,
                    RedirectStandardError = true
                }
            };
            process.Start();
            process.WaitForExit();
            if (process.ExitCode != 0)
            {
                throw new ProcessExitedException(
                    $"Process: '{fileName}' with arguments '{arguments}', exited with non zero exit code: '{process.ExitCode}' and message {process.StandardError.ReadToEnd()}.");
            }
        }
    }
}