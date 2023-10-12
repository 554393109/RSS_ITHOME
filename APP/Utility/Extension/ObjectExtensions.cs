namespace APP.Utility.Extension
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;

    public static class ObjectExtensions
    {
        #region 类型判断

        /// <summary>
        /// 是否为ip
        /// @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$"
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(this object obj)
        {
            bool isCorrect = false;
            string ip = string.Empty;

            if (obj != null)
                ip = obj.ToString();

            isCorrect = RegexHelper.IP.IsMatch(ip);
            return isCorrect;
        }

        /// <summary>
        /// 是否为Url
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsURL(this object obj)
        {
            bool isCorrect = false;
            string url = string.Empty;

            if (obj != null)
                url = obj.ToString();

            isCorrect = RegexHelper.URL.IsMatch(url);
            return isCorrect;
        }

        /// <summary>
        /// 判断是否Int
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsInt(this object obj)
        {
            bool isCorrect = false;
            int val = 0;

            if (obj != null)
                isCorrect = int.TryParse(obj.ToString(), out val);

            return isCorrect;
        }

        /// <summary>
        /// 判断是否Long
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsLong(this object obj)
        {
            bool isCorrect = false;
            long val = 0;

            if (obj != null)
                isCorrect = long.TryParse(obj.ToString(), out val);

            return isCorrect;
        }

        /// <summary>
        /// 判断是否Decimal
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsDecimal(this object obj)
        {
            bool isCorrect = false;
            decimal val = 0;

            if (obj != null)
                isCorrect = decimal.TryParse(obj.ToString(), out val);

            return isCorrect;
        }

        /// <summary>
        /// 判断是否Date
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsDate(this object obj)
        {
            var isCorrect = false;
            var dt = default(DateTime);

            if (obj != null)
                isCorrect = DateTime.TryParse(obj.ToString(), out dt);

            return isCorrect;
        }

        /// <summary>
        /// 判断是否DateTime
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsDateTime(this object obj)
        {
            var isCorrect = false;
            var dt = default(DateTime);

            if (obj != null)
                isCorrect = DateTime.TryParse(obj.ToString(), out dt);

            return isCorrect;
        }

        /// <summary>
        /// 判断是否DateTime
        /// 【含年月日时分秒使用】
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsDateTime2(this object obj)
        {
            var isCorrect = false;
            if (obj.IsNotEmpty())
            {
                var val = obj.ValueOrEmpty().ReplaceAny(new string[] { " ", "年", "月", "日", "时", "分", "秒", "-", "T", "/", ":" }, string.Empty); // 替换所有特殊字符
                if (val.IsLong())
                {
                    var dt = default(DateTime);

                    if (val.Length == 6)
                        isCorrect = DateTime.TryParseExact(val, "yyMMdd", null, System.Globalization.DateTimeStyles.None, out dt);
                    else if (val.Length == 8)
                        isCorrect = DateTime.TryParseExact(val, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out dt);
                    else if (val.Length == 12)
                        isCorrect = DateTime.TryParseExact(val, "yyMMddHHmmss", null, System.Globalization.DateTimeStyles.None, out dt);
                    else if (val.Length == 14)
                        isCorrect = DateTime.TryParseExact(val, "yyyyMMddHHmmss", null, System.Globalization.DateTimeStyles.None, out dt);
                }
            }

            return isCorrect;
        }

        /// <summary>
        /// 判断是否DateTime
        /// 【含年月日时分秒使用】
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [Obsolete]
        private static bool IsDateTime2_OLD(this object obj)
        {
            var isCorrect = false;
            var dt = default(DateTime);

            var val = obj.ValueOrEmpty();
            if (val.IsNotEmpty())
            {
                if (val.IndexOf("年", StringComparison.OrdinalIgnoreCase) > -1)
                    // 2018年01月02日 12点34分56秒
                    isCorrect
                        = val.IndexOf("年", StringComparison.OrdinalIgnoreCase) == -1
                        ? DateTime.TryParse(val.Replace("  ", " "), out dt)
                        : DateTime.TryParse(val.Replace("  ", " ").Replace("年", "-").Replace("月", "-").Replace("日", string.Empty).Replace("时", ":").Replace("分", ":").Replace("秒", string.Empty), out dt);
            }

            return isCorrect;
        }





        /// <summary>
        /// 判断是否DateTime且是默认时间【1900-01-01 00:00:00】
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsDateTimeDefault(this object obj)
        {
            var isCorrect = false;
            var dt = default(DateTime);

            if (obj != null
                && DateTime.TryParse(obj.ToString(), out dt))
            {
                isCorrect = (new DateTime(1900, 1, 1)) == dt;
            }

            return isCorrect;
        }

        /// <summary>
        /// 为【null】或【string.Empty】或【纯空格字符】
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this object obj)
        {
            bool isCorrect = false;

            if (obj == null || string.IsNullOrWhiteSpace(obj.ToString()))
                isCorrect = true;

            return isCorrect;
        }

        /// <summary>
        /// 有值，不为【null】或【string.Empty】或【纯空格字符】
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNotEmpty(this object obj)
        {
            if (obj == null || string.IsNullOrWhiteSpace(obj.ToString()))
                return false;

            return true;
        }

        #endregion 类型判断

        #region 类型转换

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultvalue">默认值</param>
        /// <returns></returns>
        public static bool ToBool(this object value, bool defaultvalue = false)
        {
            if (value.IsNullOrWhiteSpace())
                return defaultvalue;

            bool result;
            if (bool.TryParse(value.ToString(), out result))
                return result;

            return defaultvalue;
        }

        /// <summary>
        /// 转Short
        /// (-32768, 32767)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultvalue">默认值</param>
        /// <returns></returns>
        public static short ToInt16(this object value, short defaultvalue = 0)
        {
            if (value.IsNullOrWhiteSpace())
                return defaultvalue;

            decimal result;
            if (decimal.TryParse(value.ToString(), out result))
                return (short)result;

            return defaultvalue;
        }

        /// <summary>
        /// 转Int
        /// (-2147483648, 2147483647)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultvalue">默认值</param>
        /// <returns></returns>
        public static int ToInt32(this object value, int defaultvalue = 0)
        {
            if (value.IsNullOrWhiteSpace())
                return defaultvalue;

            decimal result;
            if (decimal.TryParse(value.ToString(), out result))
                return (int)result;

            return defaultvalue;
        }

        /// <summary>
        /// 转Long
        /// (-9223372036854775808, 9223372036854775807)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultvalue">默认值</param>
        /// <returns></returns>
        public static long ToInt64(this object value, long defaultvalue = 0L)
        {
            if (value.IsNullOrWhiteSpace())
                return defaultvalue;

            decimal result;
            if (decimal.TryParse(value.ToString(), out result))
                return (long)result;

            return defaultvalue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultvalue">默认值</param>
        /// <returns></returns>
        public static double ToDouble(this object value, double defaultvalue = 0.00)
        {
            if (value.IsNullOrWhiteSpace())
                return defaultvalue;

            double result;
            if (double.TryParse(value.ToString(), out result))
                return result;

            return defaultvalue;
        }

        /// <summary>
        /// 转Decimal
        /// (-79228162514264337593543950335, 79228162514264337593543950335)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultvalue">默认值</param>
        /// <returns></returns>
        public static decimal ToDecimal(this object value, decimal defaultvalue = 0.00M)
        {
            if (value.IsNullOrWhiteSpace())
                return defaultvalue;

            decimal result;
            if (decimal.TryParse(value.ToString(), out result))
                return result;

            return defaultvalue;
        }

        /// <summary>
        /// 转DateTime
        /// (0001-01-01 00:00:00.000, 9999-12-31 23:59:59.999)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="formatExact">格式化，如：yyyyMMddHHmmss</param>
        /// <param name="defaultvalue">默认值</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this object value, string formatExact = "", string defaultvalue = "1900-01-01 00:00:00.000")
        {
            if (!defaultvalue.IsDateTime())
                defaultvalue = "1900-01-01 00:00:00.000";

            if (value.IsNullOrWhiteSpace())
                return DateTime.Parse(defaultvalue);

            DateTime result;
            if (formatExact.IsNullOrWhiteSpace())
            {
                if (DateTime.TryParse(value.ToString(), out result))
                    return result;
            }
            else
            {
                try
                {
                    result = DateTime.ParseExact(value.ToString(), formatExact, null);
                    return result;
                }
                catch { }
            }

            return DateTime.Parse(defaultvalue);
        }

        /// <summary>
        /// 转DateTime
        /// (0001-01-01 00:00:00.000, 9999-12-31 23:59:59.999)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultvalue">默认值</param>
        /// <param name="formatExact">格式化，如：yyyyMMddHHmmss</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this object value, Nullable<DateTime> defaultvalue, string formatExact = "")
        {
            return ToDateTime(value, formatExact, "{0:yyyy-MM-dd HH:mm:ss.fff}".ToFormat(defaultvalue ?? new DateTime(1990, 1, 1)));
        }

        public static T ToEnum<T>(this object value)
        {
            var result = (T)Enum.Parse(typeof(T), value.ValueOrEmpty(), true);
            return result;
        }

        public static bool ToTryEnum<T>(this object value, out T result)
            where T : struct
        {
            var success = Enum.TryParse<T>(value.ValueOrEmpty(), true, out result);
            return success;
        }

        #endregion 类型转换

        #region 值处理

        /// <summary>
        /// 半角转全角(SBC case)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToStringSBC(this object value)
        {
            if (value.IsNullOrWhiteSpace())
                return string.Empty;

            //半角转全角：
            var c = value.ToString().ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }

                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        }

        /// <summary>
        ///  全角转半角
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToStringDBC(this object value)
        {
            if (value.IsNullOrWhiteSpace())
                return string.Empty;

            var c = value.ToString().ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }

                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }

        #endregion 值处理

        /// <summary>
        /// 为null时使用string.Empty
        /// <code>obj?.ToString() ?? string.Empty;</code>
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ValueOrEmpty(this object obj)
        {
            //return (obj == null || string.IsNullOrWhiteSpace(obj.ToString()))
            //    ? string.Empty
            //    : obj.ToString();

            return obj?.ToString() ?? string.Empty;
        }

        /// <summary>
        /// 为null或空时使用val
        /// <code>(obj == null || string.IsNullOrWhiteSpace(obj.ToString())) ? val.ValueOrEmpty() : obj.ToString();</code>
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="val">val.ValueOrEmpty()</param>
        /// <returns></returns>
        public static string ValueOrEmpty(this object obj, string val)
        {
            return (obj == null || string.IsNullOrWhiteSpace(obj.ToString()))
                ? val.ValueOrEmpty()
                : obj.ToString();
        }

        /// <summary>
        /// 为null或空时使用取值数组中首个非空值；若取值数组不存在非空值，则返回string.Empty
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="arr_val">取值数组</param>
        /// <returns></returns>
        public static string ValueOrFirstNotEmpty(this object obj, params string[] arr_val)
        {
            if (obj == null || string.IsNullOrWhiteSpace(obj.ToString()))
            {
                if (arr_val == null || arr_val.Length == 0)
                    return string.Empty;

                for (int i = 0; i < arr_val.Length; i++)
                {
                    if (!string.IsNullOrWhiteSpace(arr_val[i]))
                        return arr_val[i].ToString();
                }
            }

            return obj.ToString();
        }





        ///// <summary>
        ///// 序列化为Hashtable
        ///// <code><![CDATA[JSON.ConvertToType<Hashtable>(obj)]]></code>
        ///// </summary>
        ///// <param name="obj"></param>
        ///// <returns></returns>
        //public static Hashtable ToHashtable(this object obj)
        //{
        //    if (obj == null)
        //        throw new ArgumentNullException("obj");

        //    return JSON.ConvertToType<Hashtable>(obj);
        //}

        ///// <summary>
        ///// <code>JSON.Serialize(obj)</code>
        ///// </summary>
        ///// <param name="obj"></param>
        ///// <param name="blackField">字段黑名单（递归匹配嵌套对象，集合类型无效）【blackField和whiteField互斥，blackField优先】</param>
        ///// <param name="whiteField">字段白名单（递归匹配嵌套对象，集合类型无效）【blackField和whiteField互斥，blackField优先】</param>
        ///// <returns></returns>
        //public static string ToJson(this object obj
        //    , string[] blackField = null, string[] whiteField = null)
        //{
        //    return JSON.Serialize(obj, blackField: blackField, whiteField: whiteField);
        //}

        ///// <summary>
        ///// <code>Serialize(dynamic obj, string root)</code>
        ///// </summary>
        ///// <param name="obj"></param>
        ///// <param name="root">根节点</param>
        ///// <returns></returns>
        //public static string ToXml(this object obj, string root = "xml")
        //{
        //    return XML.Serialize(obj, root);
        //}

        //public static string ToXmlString(this object obj)
        //{
        //    if (obj == null)
        //        return string.Empty;

        //    #region 转换为IDictionary

        //    IDictionary parameters_temp;
        //    if (obj is IDictionary)
        //        parameters_temp = obj as IDictionary;
        //    else
        //        parameters_temp = JSON.ConvertToType<Hashtable>(obj);

        //    if (parameters_temp == null || parameters_temp.Count == 0)
        //        return string.Empty;

        //    #endregion 转换为IDictionary

        //    #region 拼接QueryString

        //    var builder = new StringBuilder();
        //    builder.Append("<xml>");
        //    foreach (string key in parameters_temp.Keys)
        //    {
        //        if (key.IsNullOrWhiteSpace())
        //            continue;

        //        var val = parameters_temp[key].ValueOrEmpty();
        //        builder.Append($"<{key}>").Append(val).Append($"</{key}>");
        //    }
        //    builder.Append("</xml>");

        //    #endregion 拼接QueryString

        //    return builder.ToString();
        //}

        //public static string ToQueryString(this object obj, bool needEncode = true)
        //{
        //    if (obj == null)
        //        return string.Empty;

        //    #region 转换为IDictionary

        //    IDictionary parameters_temp;
        //    if (obj is IDictionary)
        //        parameters_temp = obj as IDictionary;
        //    else
        //        parameters_temp = JSON.ConvertToType<Hashtable>(obj);

        //    if (parameters_temp == null || parameters_temp.Count == 0)
        //        return string.Empty;

        //    #endregion 转换为IDictionary

        //    #region 拼接QueryString

        //    var builder = new StringBuilder();
        //    foreach (string key in parameters_temp.Keys)
        //    {
        //        if (key.IsNullOrWhiteSpace())
        //            continue;

        //        var val = parameters_temp[key].ValueOrEmpty();
        //        builder.Append($"&{key}=").Append(needEncode ? val.UrlEscape() : val);
        //    }

        //    #endregion 拼接QueryString

        //    return builder.ToString().TrimStart('&');
        //}

        //public static string ToQueryStringSorted(this object obj, bool needEncode = true)
        //{
        //    if (obj == null)
        //        return string.Empty;

        //    #region 转换为SortedDictionary<string, string>

        //    SortedDictionary<string, string> parameters_temp;
        //    if (obj is SortedDictionary<string, string>)
        //        parameters_temp = obj as SortedDictionary<string, string>;
        //    else if (obj is IDictionary)
        //    {
        //        var parameters = obj as IDictionary;
        //        parameters_temp = new SortedDictionary<string, string>(StringComparer.OrdinalIgnoreCase);   // （SortedDictionary默认构造器忽略大小写，若要大小写敏感需传入StringComparer.Ordinal）
        //        foreach (DictionaryEntry item in parameters)
        //            parameters_temp[item.Key.ValueOrEmpty()] = item.Value.ValueOrEmpty();
        //    }
        //    else
        //        parameters_temp = JSON.ConvertToType<SortedDictionary<string, string>>(obj);

        //    #endregion 转换为SortedDictionary<string, string>

        //    #region 拼接QueryString

        //    var builder = new StringBuilder();
        //    foreach (string key in parameters_temp.Keys)
        //    {
        //        if (key.IsNullOrWhiteSpace())
        //            continue;

        //        var val = parameters_temp[key].ValueOrEmpty();
        //        builder.Append($"&{key}=").Append(needEncode ? val.UrlEscape() : val);
        //    }

        //    #endregion 拼接QueryString

        //    return builder.ToString().TrimStart('&');
        //}
    }
}
