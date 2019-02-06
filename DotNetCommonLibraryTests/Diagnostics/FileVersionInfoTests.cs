using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetCommonLibrary.Diagnostics.Tests
{
    [TestClass]
    public class FileVersionInfoTests
    {
        [TestMethod]
        public void GetEntryAssemblyTest()
        {
            Assert.IsNull(FileVersionInfo.GetEntryAssembly());
        }

        [TestMethod]
        public void GetExecutingAssembly()
        {
            Assert.IsNotNull(FileVersionInfo.GetExecutingAssembly());
        }

        [TestMethod]
        public void GetVersionInfoTest()
        {
            var assembly = FileVersionInfo.GetExecutingAssembly();
            Assert.IsNotNull(FileVersionInfo.GetVersionInfo(assembly));

            Assert.ThrowsException<System.NullReferenceException>(() => FileVersionInfo.GetVersionInfo());
        }

        [TestMethod]
        public void GetExecutablePathTest()
        {
            var assembly = FileVersionInfo.GetExecutingAssembly();
            Assert.IsNotNull(FileVersionInfo.GetExecutablePath(assembly));

            Assert.ThrowsException<System.ArgumentNullException>(() => FileVersionInfo.GetExecutablePath(null));
        }

        [TestMethod]
        public void GetCompanyNameTest()
        {
            var assembly = FileVersionInfo.GetExecutingAssembly();
            Assert.IsNotNull(FileVersionInfo.GetCompanyName(assembly));

            Assert.ThrowsException<System.NullReferenceException>(() => FileVersionInfo.GetCompanyName());
        }

        [TestMethod]
        public void GetProductNameTest()
        {
            var assembly = FileVersionInfo.GetExecutingAssembly();
            Assert.IsNotNull(FileVersionInfo.GetProductName(assembly));

            Assert.ThrowsException<System.NullReferenceException>(() => FileVersionInfo.GetProductName());
        }

        [TestMethod]
        public void GetProductVersionTest()
        {
            var assembly = FileVersionInfo.GetExecutingAssembly();
            Assert.IsNotNull(FileVersionInfo.GetProductVersion(assembly));

            Assert.ThrowsException<System.NullReferenceException>(() => FileVersionInfo.GetProductVersion());
        }
    }
}
