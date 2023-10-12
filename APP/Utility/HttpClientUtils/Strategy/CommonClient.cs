namespace APP.Utility.HttpClientUtils
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Text;

    /// <summary>
    /// Common无证书策略Client
    /// 在当前类中保存该业务的HttpClient
    /// </summary>
    public sealed class CommonClient
        : APP.Utility.HttpClientUtils.BaseClient
    {
        private const int timeout = 10_000;
        private static volatile HttpClient httpClient = null;

        public CommonClient()
        {
            base.Timeout = timeout;
            base.Client = httpClient;
        }

        public CommonClient(string format, Encoding charset, string contentType) : this()
        {
            base.Format = format;
            base.Charset = charset;
            base.ContentType = contentType;
        }

        static CommonClient()
        {
            if (httpClient == null)
            {
                var handler = new WebRequestHandler
                {
                    UseProxy = false,
                    Proxy = null,
                    UseCookies = false,
                    AutomaticDecompression = DecompressionMethods.None
                };

                httpClient = new HttpClient(handler);
                httpClient.Timeout = TimeSpan.FromMilliseconds(timeout);
                httpClient.DefaultRequestHeaders.Add("KeepAlive", "false");
                httpClient.DefaultRequestHeaders.Add("User-Agent", "YZQ");
                httpClient.DefaultRequestHeaders.Add("DNT", "1");
            }
        }
    }
}
