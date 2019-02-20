using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace APP.Utility.HttpClientUtils
{
    public class WebCommon
    {
        public static string BuildHash(IDictionary parameters, bool urlencode = true)
        {
            string str_param = string.Empty;
            if (parameters != null && parameters.Count > 0)
            {
                Dictionary<string, string> parameters_temp = new Dictionary<string, string>();
                foreach (DictionaryEntry item in parameters)
                    parameters_temp.Add((string)item.Key, (string)item.Value);

                str_param = _BuildHash(parameters: (IDictionary<string, string>)parameters_temp, encode: urlencode);
            }
            return str_param;
        }

        private static string _BuildHash(IDictionary<string, string> parameters, bool encode)
        {
            StringBuilder postData = new StringBuilder();
            bool hasParam = false;

            IEnumerator<KeyValuePair<string, string>> dem = parameters.GetEnumerator();

            while (dem.MoveNext())
            {
                string name = dem.Current.Key;
                string value = dem.Current.Value;

                // 忽略参数名或参数值为空的参数
                if (!string.IsNullOrWhiteSpace(name)
                    || !string.IsNullOrWhiteSpace(value))
                {
                    if (hasParam)
                        postData.Append("&");

                    postData.Append(name);
                    postData.Append("=");

                    if (encode)
                        postData.Append(Globals.UrlEncode(value));
                    else
                        postData.Append(value);

                    hasParam = true;
                }
            }
            return postData.ToString();
        }




        public static string BuildXml(IDictionary parameters)
        {
            string str_param = null;
            if (parameters != null && parameters.Count > 0)
            {
                Dictionary<string, string> parameters_temp = new Dictionary<string, string>();
                foreach (DictionaryEntry item in parameters)
                    parameters_temp.Add((string)item.Key, (string)item.Value);

                str_param = _BuildXml(parameters: (IDictionary<string, string>)parameters_temp);
            }
            return str_param;
        }

        private static string _BuildXml(IDictionary<string, string> parameters)
        {
            StringBuilder postData = new StringBuilder();
            IEnumerator<KeyValuePair<string, string>> dem = parameters.GetEnumerator();
            postData.Append("<xml>");
            while (dem.MoveNext())
            {
                string name = dem.Current.Key;
                string value = dem.Current.Value;

                // 忽略参数名或参数值为空的参数
                if (!string.IsNullOrWhiteSpace(name)
                    || !string.IsNullOrWhiteSpace(value))
                {
                    postData.Append("<").Append(name).Append(">");
                    //postData.Append(Globals.UrlEncode(value));
                    postData.Append(value);
                    postData.Append("</").Append(name).Append(">");
                }
            }
            postData.Append("</xml>");
            return postData.ToString();
        }
    }
}
