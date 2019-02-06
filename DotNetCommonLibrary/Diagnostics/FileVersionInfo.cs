using System.Reflection;

namespace DotNetCommonLibrary.Diagnostics
{
    public static class FileVersionInfo
    {
        /// <summary>
        /// Gets the process executable in the default application domain.
        /// </summary>
        /// <returns></returns>
        public static Assembly GetEntryAssembly()
        {
            return Assembly.GetEntryAssembly();
        }

        /// <summary>
        /// Gets the assembly that contains the code that is currently executing.
        /// </summary>
        /// <returns></returns>
        public static Assembly GetExecutingAssembly()
        {
            return Assembly.GetExecutingAssembly();
        }

        /// <summary>
        /// Returns a FileVersionInfo representing the version information associated with the specified file.
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        /// <exception cref="System.NullReferenceException"></exception>
        public static System.Diagnostics.FileVersionInfo GetVersionInfo(Assembly assembly = null)
        {
            if (assembly == null)
            {
                assembly = GetEntryAssembly();
            }
            return System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
        }

        /// <summary>
        /// Gets the full path or UNC location of the loaded file that contains the manifest.
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static string GetExecutablePath(Assembly assembly)
        {
            if (assembly == null)
            {
                throw new System.ArgumentNullException(nameof(assembly));
            }
            return System.IO.Path.GetDirectoryName(assembly.Location);
        }

        /// <summary>
        /// Gets the name of the company that produced the file.
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        /// <exception cref="System.NullReferenceException"></exception>
        public static string GetCompanyName(Assembly assembly = null)
        {
            var info = GetVersionInfo(assembly);
            if (info == null)
            {
                return null;
            }
            return info.CompanyName;
        }

        /// <summary>
        /// Gets the name of the product this file is distributed with.
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        /// <exception cref="System.NullReferenceException"></exception>
        public static string GetProductName(Assembly assembly = null)
        {
            var info = GetVersionInfo(assembly);
            if (info == null)
            {
                return null;
            }
            return info.ProductName;
        }

        /// <summary>
        /// Gets the version of the product this file is distributed with.
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        /// <exception cref="System.NullReferenceException"></exception>
        public static string GetProductVersion(Assembly assembly = null)
        {
            var info = GetVersionInfo(assembly);
            if (info == null)
            {
                return null;
            }
            return info.ProductVersion;
        }
    }
}
