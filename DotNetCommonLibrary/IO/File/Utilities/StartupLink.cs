using System;
using System.IO;
using System.Runtime.InteropServices;
using IWshRuntimeLibrary;

namespace DotNetCommonLibrary.IO.File.Utilities
{
    /// <summary>
    /// Reference: http://dobon.net/vb/dotnet/file/createshortcut.html
    /// </summary>
    public static class StartupLink
    {
        /// <summary>
        /// Gets a path of the shortcut link
        /// </summary>
        /// <param name="executablePath">ex.) Application.ExecutablePath</param>
        /// <returns>ex.) "C:\Users\Bugttle\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Startup\TestApplication.exe"</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        static string GetShotcutLinkPath(string executablePath)
        {
            if (executablePath == null)
            {
                throw new ArgumentNullException(nameof(executablePath));
            }
            var startupFolder = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            var linkName = Path.GetFileNameWithoutExtension(executablePath) + ".lnk";
            return Path.Combine(startupFolder, linkName);
        }

        /// <summary>
        /// Create a shortcut to startup folder
        /// </summary>
        /// <param name="executablePath">ex.) Application.ExecutablePath</param>
        /// <param name="workingDirectory">ex.) Application.StartupPath</param>
        /// <returns>ex.) "C:\Users\Bugttle\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Startup\TestApplication.exe"</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static string CreateStartup(string executablePath, string workingDirectory = null)
        {
            var shell = new WshShell();
            IWshShortcut shortcut = null;
            try
            {
                var linkPath = GetShotcutLinkPath(executablePath);
                shortcut = (IWshShortcut)shell.CreateShortcut(linkPath);
                shortcut.TargetPath = executablePath;
                shortcut.WorkingDirectory = workingDirectory ?? Path.GetDirectoryName(workingDirectory);
                shortcut.WindowStyle = 1;
                shortcut.IconLocation = executablePath + ",0";
                shortcut.Save();
                return linkPath;
            }
            finally
            {
                if (shortcut != null)
                {
                    Marshal.FinalReleaseComObject(shortcut);
                }
                Marshal.FinalReleaseComObject(shell);
            }
        }

        /// <summary>
        /// Remove shortcut from startup folder
        /// </summary>
        /// <param name="executablePath">ex.) Application.ExecutablePath</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static void RemoveStartup(string executablePath)
        {
            var linkPath = GetShotcutLinkPath(executablePath);
            if (System.IO.File.Exists(linkPath))
            {
                System.IO.File.Delete(linkPath);
            }
        }

        /// <summary>
        /// Check existing in startup folder
        /// </summary>
        /// <param name="executablePath">ex.) Application.ExecutablePath</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static bool Exists(string executablePath)
        {
            var linkPath = GetShotcutLinkPath(executablePath);
            return System.IO.File.Exists(linkPath);
        }
    }
}
