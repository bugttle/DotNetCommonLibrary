using System.Collections.Generic;
using System.IO;

namespace DotNetCommonLibrary.IO.File.Utilities
{
    public static class FileManager
    {
        /// <summary>
        /// Copies an existing file to a new file.
        /// </summary>
        /// <param name="sourceFileName"></param>
        /// <param name="destFileName"></param>
        /// <param name="overwrite"></param>
        public static void CopyFile(string sourceFileName, string destFileName, bool overwrite = false)
        {
            System.IO.File.Copy(sourceFileName, destFileName, overwrite);
        }

        /// <summary>
        /// Copies an existing directory to a new location.
        /// </summary>
        /// <param name="sourceDirName"></param>
        /// <param name="destDirName"></param>
        /// <param name="overwrite"></param>
        public static void CopyDirectory(string sourceDirName, string destDirName, bool overwrite = false)
        {
            // Validation
            if (sourceDirName == null)
            {
                throw new System.ArgumentNullException(nameof(sourceDirName));
            }
            if (destDirName == null)
            {
                throw new System.ArgumentNullException(nameof(destDirName));
            }

            var sourceDir = new DirectoryInfo(sourceDirName);
            if (!sourceDir.Exists)
            {
                throw new DirectoryNotFoundException(sourceDirName);
            }

            var destDir = Directory.CreateDirectory(destDirName);

            // First, copy files
            foreach (var file in sourceDir.GetFiles())
            {
                var path = Path.Combine(destDir.FullName, file.Name);
                file.CopyTo(path, overwrite);
            }

            // Second, copy sub directories
            foreach (var dir in sourceDir.GetDirectories())
            {
                var path = Path.Combine(destDir.FullName, dir.Name);
                CopyDirectory(dir.FullName, path, overwrite);
            }
        }

        /// <summary>
        /// Moves a specified file to a new location, providing the option to specify a new file name.
        /// </summary>
        /// <param name="sourceFileName"></param>
        /// <param name="destFileName"></param>
        /// <param name="overwrite"></param>
        public static void MoveFile(string sourceFileName, string destFileName, bool overwrite = false)
        {
            // Validation
            if (sourceFileName == null)
            {
                throw new System.ArgumentNullException(nameof(sourceFileName));
            }
            if (destFileName == null)
            {
                throw new System.ArgumentNullException(nameof(destFileName));
            }

            // remove the file if it already exists
            if (overwrite && System.IO.File.Exists(destFileName))
            {
                System.IO.File.Delete(destFileName);
            }

            System.IO.File.Move(sourceFileName, destFileName);
        }

        /// <summary>
        /// Moves a file or a directory and its contents to a new location.
        /// </summary>
        /// <param name="sourceDirName"></param>
        /// <param name="destDirName"></param>
        /// <param name="overwrite"></param>
        public static void MoveDirectory(string sourceDirName, string destDirName, bool overwrite = false)
        {
            // remove the directory if it already exists
            if (overwrite && Directory.Exists(destDirName))
            {
                Directory.Delete(destDirName, recursive: true);
            }
            Directory.Move(sourceDirName, destDirName);
        }

        /// <summary>
        /// Searchs files in a directory.
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <param name="includeSubDirectories"></param>
        /// <returns></returns>
        public static IEnumerable<string> SearchFilesInDirectory(string directoryPath, bool includeSubDirectories = true)
        {
            var searchOption = (includeSubDirectories) ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            return Directory.EnumerateFiles(directoryPath, "*", searchOption);
        }
    }
}
