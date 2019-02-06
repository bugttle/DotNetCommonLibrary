using System.IO;
using static System.Environment;

namespace DotNetCommonLibrary.Diagnostics
{
    public static class Paths
    {
        /// <summary>
        /// Gets the application folder path.
        /// </summary>
        /// <param name="baseFolder">default: "C:\Users\{USER}\AppData\Local"</param>
        /// <returns></returns>
        public static string GetApplicationFolderPath(SpecialFolder baseFolder = SpecialFolder.LocalApplicationData)
        {
            var rootFolder = GetFolderPath(baseFolder);
            var assembly = FileVersionInfo.GetEntryAssembly();
            if (assembly == null)
            {
                return rootFolder;
            }
            var info = FileVersionInfo.GetVersionInfo(assembly);
            return Path.Combine(rootFolder, info.CompanyName, info.ProductName);
        }

        /// <summary>
        /// Gets the startup folder path.
        /// </summary>
        /// <returns></returns>
        public static string GetStartupFolderPath()
        {
            return GetFolderPath(SpecialFolder.Startup);
        }
    }
}
