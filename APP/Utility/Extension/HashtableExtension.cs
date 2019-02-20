/************************************************************************
 * 文件标识：  3fcc9651-b38e-49d4-9cc0-b9610835ef81
 * 项目名称：  APP.Utility.Extension  
 * 项目描述：  
 * 类 名 称：  HashtableExtension
 * 版 本 号：  v1.0.0.0 
 * 说    明：  
 * 作    者：  尹自强
 * 创建时间：  2017/12/27 16:06:59
 * 更新时间：  2017/12/27 16:06:59
************************************************************************
 * Copyright @ 尹自强 2017. All rights reserved.
************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace APP.Utility.Extension
{
    public static class HashtableExtension
    {
        /// <summary>
        /// 对Hashtable里的value进行Uri.EscapeDataString，
        /// 返回一个新的Hashtable。
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static Hashtable UrlEscape(this Hashtable param)
        {
            if (param == null)
                return null;

            Hashtable param_new = new Hashtable();
            foreach (string key in param.Keys)
            {
                if (string.IsNullOrWhiteSpace(key))
                    continue;

                if (null == param[key] || string.IsNullOrWhiteSpace(param[key].ToString()))
                    param_new[key] = param[key];
                else
                    param_new[key] = Uri.EscapeDataString(param[key].ToString());
            }

            return param_new;
        }

        /// <summary>
        /// 对Hashtable里的value进行Uri.UnescapeDataString，
        /// 返回一个新的Hashtable。
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static Hashtable UrlUnescape(this Hashtable param)
        {
            if (param == null)
                return null;

            Hashtable param_new = new Hashtable();
            foreach (string key in param.Keys)
            {
                if (string.IsNullOrWhiteSpace(key))
                    continue;

                if (null == param[key] || string.IsNullOrWhiteSpace(param[key].ToString()))
                    param_new[key] = param[key];
                else
                    param_new[key] = Uri.UnescapeDataString(param[key].ToString());
            }

            return param_new;
        }



        /// <summary>
        /// 对Hashtable里的value进行UrlEncode，
        /// 返回一个新的Hashtable。
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
#if DEBUG
        [Obsolete("此方法已过期，建议使用APP.Utility.Extension.HashtableExtension.UrlEscape()方法")]
#endif
        public static Hashtable UrlEncode(this Hashtable param)
        {
            if (param == null)
                return null;

            Hashtable param_new = new Hashtable();
            foreach (string key in param.Keys)
            {
                if (string.IsNullOrWhiteSpace(key)
                    || null == param[key]
                    || string.IsNullOrWhiteSpace(param[key].ToString()))
                    continue;

                param_new[key] = System.Web.HttpUtility.UrlEncode(param[key].ToString());
            }

            return param_new;
        }


        /// <summary>
        /// 对Hashtable里的value进行UrlDecode，
        /// 返回一个新的Hashtable。
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static Hashtable UrlDecode(this Hashtable param)
        {
            if (param == null)
                return null;

            Hashtable param_new = new Hashtable();
            foreach (string key in param.Keys)
            {
                if (string.IsNullOrWhiteSpace(key))
                    continue;

                if (null == param[key] || string.IsNullOrWhiteSpace(param[key].ToString()))
                    param_new[key] = param[key];
                else
                    param_new[key] = System.Web.HttpUtility.UrlDecode(param[key].ToString());
            }

            return param_new;
        }


        public static Dictionary<string, string> ToDictionary(this Hashtable ht)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            foreach (string key in ht.Keys)
            {
                if (ht[key] != null)
                    dic.Add(key, ht[key].ToString());
            }
            return dic;
        }

        public static SortedDictionary<string, string> ToSortedDictionary(this Hashtable table)
        {
            SortedDictionary<string, string> sTable = new SortedDictionary<string, string>();

            foreach (string iterm in table.Keys)
            {
                sTable[iterm] = table[iterm] != null ? table[iterm].ToString() : string.Empty;
            }
            return sTable;
        }

        public static Dictionary<K, V> ToDictionary<K, V>(this Hashtable table)
        {
            return table
              .Cast<DictionaryEntry>()
              .ToDictionary(kvp => (K)kvp.Key, kvp => (V)kvp.Value);
        }


        /// <summary>
        /// 遍历并返回URL参数格式,a=1&b=2
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static string toUrlParams(this Hashtable table)
        {
            if (table.Count < 1)
                return string.Empty;
            var parameters = table.ToDictionary<string, string>();
            IEnumerator<KeyValuePair<string, string>> enumerator = new SortedDictionary<string, string>(parameters).GetEnumerator();
            StringBuilder builder = new StringBuilder();
            while (enumerator.MoveNext())
            {
                KeyValuePair<string, string> current = enumerator.Current;
                string key = current.Key;
                current = enumerator.Current;
                string str2 = current.Value;
                if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(str2))
                {
                    builder.Append(key).Append("=").Append(System.Web.HttpUtility.UrlEncode(str2, Encoding.UTF8)).Append("&");
                }
            }
            return builder.ToString().Substring(0, builder.Length - 1);
        }
    }
}
