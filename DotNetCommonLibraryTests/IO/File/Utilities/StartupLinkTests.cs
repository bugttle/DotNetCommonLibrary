using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetCommonLibrary.IO.File.Utilities
{
    [TestClass]
    public class StartupLinkTests
    {
        const string TestApplicationPath = @"C:\Program Files\TestApplication\DotNetCommonLibraryBuggtleApplicationTest.exe";

        string GetShotcutLinkPath()
        {
            var startupFolder = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            var linkName = Path.GetFileNameWithoutExtension(TestApplicationPath) + ".lnk";
            return Path.Combine(startupFolder, linkName);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            // I believe you don't have the shortcut link. The test can't continue if you already have it.
            if (System.IO.File.Exists(GetShotcutLinkPath()))
            {
                throw new System.SystemException("The shortcut link is already exists.");
            }
        }

        [TestCleanup]
        public void TestCleanup()
        {
            System.IO.File.Delete(GetShotcutLinkPath());
        }

        [TestMethod]
        public void CreateStartupTest()
        {
            try
            {
                var path = StartupLink.CreateStartup(TestApplicationPath);
                Assert.AreEqual(GetShotcutLinkPath(), path);
            }
            finally
            {
                StartupLink.RemoveStartup(TestApplicationPath); // cleanup
            }

            Assert.ThrowsException<ArgumentNullException>(() => StartupLink.CreateStartup(null));

        }

        [TestMethod]
        public void RemoveStartupTest()
        {
            StartupLink.RemoveStartup(TestApplicationPath);
            Assert.IsFalse(StartupLink.Exists(TestApplicationPath));

            Assert.ThrowsException<ArgumentNullException>(() => StartupLink.RemoveStartup(null));
        }

        [TestMethod]
        public void ExistsTest()
        {
            StartupLink.Exists(TestApplicationPath);

            Assert.ThrowsException<ArgumentNullException>(() => StartupLink.Exists(null));
        }
    }
}
