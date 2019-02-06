using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetCommonLibrary.IO.File.Utilities
{
    [TestClass]
    public class FileManagerTests
    {
        [TestMethod]
        public void CopyFileTest()
        {
            var sourceFileName = Path.GetTempFileName();
            var destFileName = Path.GetTempFileName();

            // System.ArgumentNullException
            Assert.ThrowsException<System.ArgumentNullException>(() =>
            {
                FileManager.CopyFile(null, destFileName);
            });
            Assert.ThrowsException<System.ArgumentNullException>(() =>
            {
                FileManager.CopyFile(sourceFileName, null);
            });

            // System.IO.IOException
            Assert.ThrowsException<System.IO.IOException>(() =>
            {
                FileManager.CopyFile(sourceFileName, destFileName);
            });

            // Success
            FileManager.CopyFile(sourceFileName, destFileName, overwrite: true);
        }

        [TestMethod]
        public void CopyDirectoryTest()
        {
            var sourceDirectoryName = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            var destDirectoryName = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            Directory.CreateDirectory(sourceDirectoryName);
            Directory.CreateDirectory(destDirectoryName);

            // System.ArgumentNullException
            Assert.ThrowsException<System.ArgumentNullException>(() =>
            {
                FileManager.CopyDirectory(null, destDirectoryName);
            });
            Assert.ThrowsException<System.ArgumentNullException>(() =>
            {
                FileManager.CopyDirectory(sourceDirectoryName, null);
            });

            // Success
            FileManager.CopyDirectory(sourceDirectoryName, destDirectoryName);
        }

        [TestMethod]
        public void MoveFileTest()
        {
            var sourceFileName = Path.GetTempFileName();
            var destFileName = Path.GetTempFileName();

            // System.ArgumentNullException
            Assert.ThrowsException<System.ArgumentNullException>(() =>
            {
                FileManager.MoveFile(null, destFileName);
            });
            Assert.ThrowsException<System.ArgumentNullException>(() =>
            {
                FileManager.MoveFile(sourceFileName, null);
            });

            // System.IO.IOException
            Assert.ThrowsException<System.IO.IOException>(() =>
            {
                FileManager.MoveFile(sourceFileName, destFileName);
            });

            // Success
            FileManager.MoveFile(sourceFileName, destFileName, overwrite: true);
        }
    }
}
