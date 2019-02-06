using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetCommonLibrary.Text.Tests
{
    [TestClass]
    public class ConverterTests
    {
        [TestMethod]
        public void ToWildCardRegexTest()
        {
            {
                var regex = Converter.ToWildCardRegex("?");
                Assert.IsTrue(regex.IsMatch("A"));
                Assert.IsFalse(regex.IsMatch("AA"));
            }
            {
                var regex = Converter.ToWildCardRegex("*");
                Assert.IsTrue(regex.IsMatch(""));
                Assert.IsTrue(regex.IsMatch("A"));
                Assert.IsTrue(regex.IsMatch("AA"));
            }
            {
                var regex = Converter.ToWildCardRegex("test??.*");
                Assert.IsTrue(regex.IsMatch("test00.txt"));
                Assert.IsTrue(regex.IsMatch("test00.log"));
                Assert.IsFalse(regex.IsMatch("test.txt"));
            }
        }

        [TestMethod]
        public void ZenkakuToHankakuTest()
        {
            Equals("ｾﾞﾝｶｸ", Converter.ZenkakuToHankaku("ゼンカク"));
        }

        [TestMethod]
        public void TrimSpacesTest()
        {
            Equals("", Converter.TrimSpaces("     "));
            Equals("aa", Converter.TrimSpaces("  a  a  "));
        }

        [TestMethod]
        public void TrimLastStringTest()
        {
            Equals("aaa_bbb", Converter.TrimLastString("aaa_bbb_ccc", 0, "_"));
            Equals("aaa", Converter.TrimLastString("aaa_bbb_ccc", 1, "_"));
        }

        [TestMethod]
        public void ToTimeSpanTest()
        {
            Converter.ToTimeSpan("13h:00m");
            Converter.ToTimeSpan("-13h:00m");
        }
    }
}
