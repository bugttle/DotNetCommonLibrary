using System.IO;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetCommonLibrary.IO.File.Watcher
{
    [TestClass]
    public class FileWatcherTests
    {
        [TestInitialize]
        public void TestInitialize()
        {
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        [TestMethod]
        public void WatchTest()
        {
            var directoryName = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(directoryName);

            var fileName = Path.Combine(directoryName, Path.GetRandomFileName());

            var watcher = new FileWatcher(directoryName);
            watcher.AllChanged += (e) =>
            {
                Assert.AreEqual(WatcherChangeTypes.Created, e.ChangeType);
                Assert.AreEqual(fileName, e.FullPath);
            };
            watcher.Start();

            System.IO.File.Create(fileName);

            Thread.Sleep(500); // wait milliseconds

            watcher.Stop();
        }
    }
}
