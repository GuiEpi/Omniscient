using System.Diagnostics;
using System.Text;

namespace OmniscientClient
{
    public class CommandExecutor
    {
        public Task<string> ExecuteCmdCommand(string command)
        {
            System.Console.WriteLine($"Executing: {command}");
            var processInfo = new ProcessStartInfo("cmd.exe", "/c " + command)
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true
            };
            
            return ExecuteProcessCommand(processInfo);
        }

        private Task<string> ExecuteProcessCommand(ProcessStartInfo processInfo)
        {
            var tcs = new TaskCompletionSource<string>();

            var process = new Process
            {
                StartInfo = processInfo,
                EnableRaisingEvents = true
            };

            var output = new StringBuilder();
            var error = new StringBuilder();

            process.OutputDataReceived += (sender, args) =>
            {
                if (args.Data != null)
                {
                    output.AppendLine(args.Data);
                }
            };

            process.ErrorDataReceived += (sender, args) =>
            {
                if (args.Data != null)
                {
                    error.AppendLine(args.Data);
                }
            };

            process.Exited += (sender, args) =>
            {
                if (process.ExitCode == 0)
                {
                    System.Console.WriteLine($"Output: {output}");
                    tcs.SetResult(output.ToString());
                }
                else
                {
                    tcs.SetResult(error.ToString());
                }

                process.Dispose();
            };

            process.Start();

            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            return tcs.Task;
        }
    }
}