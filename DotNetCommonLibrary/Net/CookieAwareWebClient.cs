using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCommonLibrary.Net
{
    /// <summary>
    /// Cookie aware WebClient
    /// </summary>

    // To avaid the error: "To add components to your class, drag them from the Toolbox and use the Properties window to set their properties"
    [System.ComponentModel.DesignerCategory("Code")]
    public class CookieAwareWebClient : WebClient
    {
        /// <summary>
        /// UserAgent string
        /// </summary>
        public string UserAgent { get; set; }

        readonly object lockObject = new object(); // WebClient does not support concurrent I/O operations.
        readonly CookieContainer cc = new CookieContainer();
        string lastPage;

        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = base.GetWebRequest(address);
            if (request is HttpWebRequest)
            {
                var wr = (HttpWebRequest)request;
                wr.CookieContainer = cc;
                if (lastPage != null)
                {
                    wr.Referer = lastPage;
                }
            }
            lastPage = address.ToString();
            return request;
        }

        void SetHeaders()
        {
            if (UserAgent != null)
            {
                Headers.Set("User-Agent", UserAgent);
            }
        }

        /// <summary>
        /// Send a GET request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public string Get(string url, NameValueCollection query = null)
        {
            lock (lockObject)
            {
                SetHeaders();
                QueryString = query;
                return DownloadString(url);
            }
        }

        /// <summary>
        /// Send a GET request with asynchronous communication
        /// </summary>
        /// <param name="url"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public Task<string> GetAsync(string url, NameValueCollection query = null)
        {
            return Task.Run(() =>
            {
                lock (lockObject)
                {
                    return Get(url, query);
                }
            });
        }

        /// <summary>
        /// Send a POST request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public string Post(string url, NameValueCollection query)
        {
            lock (lockObject)
            {
                SetHeaders();
                QueryString = null;
                var resData = UploadValues(url, query);
                return Encoding.UTF8.GetString(resData);
            }
        }

        /// <summary>
        /// Send a POST request with asynchronous communication
        /// </summary>
        /// <param name="url"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public Task<string> PostAsync(string url, NameValueCollection query)
        {
            return Task.Run(() =>
            {
                return Post(url, query);
            });
        }
    }
}
