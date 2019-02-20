using System;
using System.Net.Http;
using System.Text;

namespace APP.Utility.HttpClientUtils
{
    /// <summary>
    /// Common无证书策略Client
    /// 在当前类中保存该业务的HttpClient
    /// </summary>
    public sealed class CommonClient
        : APP.Utility.HttpClientUtils.BaseClient
    {
        private static readonly object lockHelper_CommonClient = new object();
        private static volatile HttpClient httpClient_CommonClient = null;
        private const int Timeout = 15;

        public CommonClient()
        {
            this.Get_ClientInstance();
            base.Client = httpClient_CommonClient;
        }

        public CommonClient(string format)
            : this()
        {
            base.Format = format;
        }

        public CommonClient(string format, Encoding charset)
            : this(format)
        {
            base.Charset = charset;
        }

        private void Get_ClientInstance()
        {
            if (httpClient_CommonClient == null)
            {
                lock (lockHelper_CommonClient)
                {
                    if (httpClient_CommonClient == null)
                    {
                        httpClient_CommonClient = new HttpClient();
                        httpClient_CommonClient.Timeout = new TimeSpan(0, 0, Timeout);
                        httpClient_CommonClient.DefaultRequestHeaders.Add("KeepAlive", "false");
                        httpClient_CommonClient.DefaultRequestHeaders.Add("User-Agent", "SP");
                        httpClient_CommonClient.DefaultRequestHeaders.Add("DNT", "1");
                    }
                }
            }
        }
    }
}
