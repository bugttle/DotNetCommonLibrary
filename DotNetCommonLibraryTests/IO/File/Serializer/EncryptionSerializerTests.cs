using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetCommonLibrary.IO.File.Serializer
{
    [TestClass]
    public class EncryptionSerializerTests
    {
        static string TempFilePath = ""; // It will be set in ClassInitialize method

        [Serializable]
        class TestData
        {
            public int IntData = 1234567890;
            public float FloatData = 1.0f / 3.0f;
            public double DoubleData = 1.0 / 3.0;
            public string StringData = "abcdefg";
        }

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            TempFilePath = Path.GetTempFileName();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            System.IO.File.Delete(TempFilePath);
        }

        [TestMethod]
        public void EncryptionSerializerTest()
        {
            var originalData = new TestData();
            EncryptionSerializer.Encrypt(originalData, TempFilePath);

            var loadedData = EncryptionSerializer.Decrypt<TestData>(TempFilePath);
            Assert.AreEqual(originalData.IntData, loadedData.IntData);
            Assert.AreEqual(originalData.FloatData, loadedData.FloatData);
            Assert.AreEqual(originalData.DoubleData, loadedData.DoubleData);
            Assert.AreEqual(originalData.StringData, loadedData.StringData);
        }
    }
}
