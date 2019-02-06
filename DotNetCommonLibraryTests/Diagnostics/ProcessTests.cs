using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.Environment;

namespace DotNetCommonLibrary.Diagnostics.Tests
{
    [TestClass]
    public class ProcessTests
    {
        [TestMethod]
        public void ExecuteAsyncTests()
        {
            // launch notepad.exe
            new Process().ExecuteAsync("notepad.exe");

            // run "dir" command
            var p = new Process
            {
                OutputDataReceived = (sender, e) =>
                {
                    Console.Write(e.Data);
                },
                ErrorDataReceived = (sender, e) =>
                {
                    Console.Write(e.Data);
                },
            };
            p.ExecuteAsync(GetEnvironmentVariable("ComSpec"), input: "dir");
        }
    }
}
