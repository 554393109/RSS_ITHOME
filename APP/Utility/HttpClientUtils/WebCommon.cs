namespace APP.Utility.HttpClientUtils
{
    using System.Collections;
    using System.Text;

    using APP.Utility.Extension;

    public sealed class WebCommon
    {
        public static string BuildQueryString(IDictionary parameters, bool needEncode = true)
        {
            var builder = new StringBuilder();
            if (parameters is IDictionary dic && parameters.Count > 0)
            {
                foreach (string key in dic.Keys)
                {
                    var val = parameters[key].ValueOrEmpty();

                    if (key.IsNullOrWhiteSpace() || val.IsNullOrWhiteSpace())
                        continue;

                    builder.Append($"&{key}=").Append(needEncode ? val.UrlEscape() : val);
                }
            }

            return builder.ToString().TrimStart('&');
        }

        public static string BuildXml(IDictionary parameters)
        {
            var builder = new StringBuilder();
            if (parameters is IDictionary dic && parameters.Count > 0)
            {
                builder.Append("<xml>");

                foreach (string key in dic.Keys)
                {
                    var val = parameters[key].ValueOrEmpty();

                    if (key.IsNullOrWhiteSpace() || val.IsNullOrWhiteSpace())
                        continue;

                    builder.Append($"<{key}>").Append(val).Append($"</{key}>");
                }

                builder.Append("</xml>");
            }

            return builder.ToString();
        }
    }
}
