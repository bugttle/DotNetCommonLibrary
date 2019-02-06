using System.Collections.Specialized;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetCommonLibrary.Net.Tests
{
    [TestClass]
    public class CookieAwareWebClientTests
    {
        const string UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36";

        [TestMethod]
        [Timeout(1000)]
        public void GetTest()
        {
            var client = new CookieAwareWebClient
            {
                UserAgent = UserAgent
            };
            client.Get("https://example.com");
        }

        [TestMethod]
        [Timeout(1000)]
        public void GetAsyncTest()
        {
            var client = new CookieAwareWebClient
            {
                UserAgent = UserAgent
            };
            client.GetAsync("https://example.com");
        }

        [TestMethod]
        [Timeout(1000)]
        public void PostTest()
        {
            var client = new CookieAwareWebClient
            {
                UserAgent = UserAgent
            };
            client.Post("https://example.com", new NameValueCollection()
            {
                { "key1", "value1" },
                { "key2", "value2" },
            });
        }

        [TestMethod]
        [Timeout(1000)]
        public void PostAsyncTest()
        {
            var client = new CookieAwareWebClient
            {
                UserAgent = UserAgent
            };
            client.PostAsync("https://example.com", new NameValueCollection()
            {
                { "key1", "value1" },
                { "key2", "value2" },
            });
        }

        [TestMethod]
        [Timeout(4000)]
        public void MultiRequestTest()
        {
            var client = new CookieAwareWebClient
            {
                UserAgent = UserAgent
            };
            client.Get("https://example.com");
            client.GetAsync("https://example.com");
            client.Post("https://example.com", new NameValueCollection()
            {
                { "key1", "value1" },
                { "key2", "value2" },
            });
            client.PostAsync("https://example.com", new NameValueCollection()
            {
                { "key1", "value1" },
                { "key2", "value2" },
            });
        }
    }
}
