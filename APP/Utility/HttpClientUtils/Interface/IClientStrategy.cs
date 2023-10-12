namespace APP.Utility.HttpClientUtils
{
    using System.Text;

    public interface IClientStrategy
        : APP.Utility.HttpClientUtils.IClient
    {
        /// <summary>
        /// 内容编码
        /// 默认UTF-8
        /// </summary>
        Encoding Charset { get; set; }

        /// <summary>
        /// 参数格式
        /// 默认hash
        /// </summary>
        string Format { get; set; }
    }
}
