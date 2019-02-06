using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.Environment;

namespace DotNetCommonLibrary.Diagnostics.Tests
{
    [TestClass]
    public class PathTests
    {
        [TestMethod]
        public void GetApplicationFolderPathTest()
        {
            Assert.IsNotNull(Paths.GetApplicationFolderPath());
            Assert.AreEqual(GetFolderPath(SpecialFolder.LocalApplicationData), Paths.GetApplicationFolderPath());
        }

        [TestMethod]
        public void GetStartupFolderPathTest()
        {
            Assert.IsNotNull(Paths.GetStartupFolderPath());
            Assert.AreEqual(GetFolderPath(SpecialFolder.Startup), Paths.GetStartupFolderPath());
        }
    }
}
