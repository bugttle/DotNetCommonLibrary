using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetCommonLibrary.Threading
{
    [TestClass]
    public class MutexLockTests
    {
        [TestMethod]
        public void MutexLockTest()
        {
            using (var mutex = new MutexLock("DotNetCommonLibraryTests"))
            {

            }
        }
    }
}
