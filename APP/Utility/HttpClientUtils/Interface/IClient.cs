namespace APP.Utility.HttpClientUtils
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IClient
    {
        #region POST

        string Post(string url, object content, object header = null, int? timeout = null);

        string Post(string url, object content, IDictionary<string, FileItem> fileParams, object header = null, int? timeout = null);

        Task<string> PostAsync(string url, object content, object header = null, int? timeout = null);

        void PostNoResponse(string url, object content, object header = null, int? timeout = null);

        //TResult PostAsync<TResult>(string url, object content, int? timeout = null);

        #endregion POST

        #region GET

        string Get(string url, int? timeout = null);

        Task<string> GetAsync(string url, int? timeout = null);

        #endregion GET
    }
}
