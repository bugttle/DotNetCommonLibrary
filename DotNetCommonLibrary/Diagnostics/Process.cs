using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace DotNetCommonLibrary.Diagnostics
{
    public class Process
    {
        /// <summary>
        /// Occurs each time an application writes a line to its redirected StandardOutput stream.
        /// </summary>
        public DataReceivedEventHandler OutputDataReceived;

        /// <summary>
        /// Occurs when an application writes to its redirected StandardError stream.
        /// </summary>
        public DataReceivedEventHandler ErrorDataReceived;

        System.Diagnostics.Process CreateNoWindowProcess(string fileName, string arguments, string workingDirectory)
        {
            return new System.Diagnostics.Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = fileName,
                    Arguments = arguments,
                    WorkingDirectory = workingDirectory,

                    // hide window
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    UseShellExecute = false,
                },
            };
        }

        void SetInputOutputRedirecting(System.Diagnostics.Process process, string input)
        {
            // stdin redirecting
            if (input != null)
            {
                process.StartInfo.RedirectStandardInput = true;
            }
            // stdout redirecting
            if (OutputDataReceived != null)
            {
                process.StartInfo.RedirectStandardOutput = true;
                process.OutputDataReceived += (sender, e) => OutputDataReceived(sender, e);
            }
            // stderr redirecting
            if (ErrorDataReceived != null)
            {
                process.StartInfo.RedirectStandardError = true;
                process.ErrorDataReceived += (sender, e) => ErrorDataReceived(sender, e);
            }
        }

        int Execute(System.Diagnostics.Process process, string input)
        {
            try
            {
                process.Start();

                // read from stdout
                if (process.StartInfo.RedirectStandardOutput)
                {
                    process.BeginOutputReadLine();
                }
                // read from stderr
                if (process.StartInfo.RedirectStandardError)
                {
                    process.BeginErrorReadLine();
                }
                // write to stdin
                if (process.StartInfo.RedirectStandardInput)
                {
                    process.StandardInput.WriteLine(input);
                    process.StandardInput.Flush();
                    process.StandardInput.Close();
                }

                process.WaitForExit();
            }
            catch (Exception e)
            {
                throw new IOException($"failed to execute: {process.StartInfo.FileName} {process.StartInfo.Arguments}", e);
            }

            return process.ExitCode;
        }

        /// <summary>
        /// Execute a command
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="arguments"></param>
        /// <param name="workingDirectory"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="System.IO.IOException"></exception>
        public Task<int> ExecuteAsync(string fileName, string arguments = "", string workingDirectory = "", string input = null)
        {
            return Task.Run(() =>
            {
                // Create a process
                using (var process = CreateNoWindowProcess(fileName, arguments, workingDirectory))
                {
                    SetInputOutputRedirecting(process, input); // Set I/O
                    return Execute(process, input);
                }
            });
        }
    }
}
