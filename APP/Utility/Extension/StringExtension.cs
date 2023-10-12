namespace APP.Utility.Extension
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    public static class StringExtension
    {
        public static string Left(this string value, int length)
        {
            if (String.IsNullOrEmpty(value))
            {
                return value;
            }
            if (length < 1)
            {
                return String.Empty;
            }
            value = value.Length > length ? value.Substring(0, length) : value;
            return value;
        }

        public static string Right(this string value, int length)
        {
            if (String.IsNullOrEmpty(value))
            {
                return value;
            }
            if (length < 1)
            {
                return String.Empty;
            }
            value = value.Length > length ? value.Substring(value.Length - length, length) : value;
            return value;
        }

        public static string IsNullOrEmpty(string value, string defaultValue)
        {
            if (String.IsNullOrEmpty(value))
            {
                return defaultValue;
            }
            return value;
        }

        public static bool ObjIsNull(object obj)
        {
            try
            {
                return string.IsNullOrEmpty(obj.ToString());
            }
            catch
            {

                return true;
            }
        }

        public static bool ObjIsNullOrWhiteSpace(object obj)
        {
            try
            {
                return string.IsNullOrWhiteSpace(obj.ToString());
            }
            catch
            {
                return true;
            }
        }

        public static bool IsEmpty(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// a.Equals(b)
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static bool Same(this string a, string b, bool ignoreCase = true)
        {
            var result = false;
            try
            {
                if ((a == null && b == null)
                    || a.Equals(b, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal))
                    result = true;
            }
            catch { }

            return result;
        }

        /// <summary>
        /// a.IndexOf(b)
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static bool Exists(this string a, string b, bool ignoreCase = true)
        {
            var result = false;
            try
            {
                if (a != null
                    && b != null
                    && a.IndexOf(b, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) != -1)
                    result = true;
            }
            catch { }

            return result;
        }

        public static string ToFormat(this string value, params object[] args)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return value;
            }
            return string.Format(value, args);
        }

        public static string ToBase64String(this string value, string charset = "utf-8")
        {
            if (string.IsNullOrWhiteSpace(value))
                return value;

            return Convert.ToBase64String(value.GetBytes(charset));
        }

        public static string FromBase64String(this string value, string charset = "utf-8")
        {
            if (string.IsNullOrWhiteSpace(value))
                return value;

            return Encoding.GetEncoding(charset).GetString(Convert.FromBase64String(value));
        }

        public static string Join(this string[] array, string separator)
        {
            if (array != null || array.Length > 0)
            {
                return string.Join(separator, array);
            }
            return string.Empty;
        }

        public static string Join<TSource>(this IEnumerable<TSource> source, Func<TSource, string> keySelector, string separator)
        {
            string result = String.Empty;
            if (source != null && source.Count() > 0)
            {
                string[] value = source.Select(keySelector).ToArray();
                result = String.Join(separator, value);
            }
            return result;
        }

        /// <summary>
        /// 数组任一内容
        /// item.Equals(value)
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        /// <param name="ignoreCase">忽略大小写【默认：true】</param>
        /// <returns></returns>
        public static bool Contains(this string[] array, string value, bool ignoreCase = true)
        {
            if (array != null && array.Count() > 0
                && value.IsNotEmpty())
            {
                foreach (string item in array)
                {
                    if (item.Equals(value, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal))
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 数组任一内容
        /// item.Equals(value)
        /// 忽略大小写
        /// </summary>
        /// <param name="value"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        public static bool Contains(this string value, params string[] array)
        {
            if (array == null || array.Length == 0 || value.IsNullOrWhiteSpace())
                return false;

            foreach (string item in array)
            {
                if (item.Equals(value, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// 数组任一内容是否在字符串中
        /// value.IndexOf(array[item])
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static bool InContent(this string[] array, string value, bool ignoreCase = true)
        {
            if (array == null || array.Length == 0 || value.IsNullOrWhiteSpace())
                return false;

            foreach (string item in array)
            {
                if (value.IndexOf(item, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) != -1)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// 名单全部内容是否在字符串中
        /// value.IndexOf(array[item])
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static bool AllInContent(this string[] array, string value, bool ignoreCase = true)
        {
            if (array == null || array.Length == 0 || value.IsNullOrWhiteSpace())
                return false;

            foreach (string item in array)
            {
                if (value.IndexOf(item, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) == -1)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// 名单任一内容是否在字符串中
        /// value.IndexOf(array[item])
        /// 忽略大小写
        /// </summary>
        /// <param name="value"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        public static bool InContent(this string value, params string[] array)
        {
            if (array == null || array.Length == 0 || value.IsNullOrWhiteSpace())
                return false;

            foreach (string item in array)
            {
                if (value.IndexOf(item, StringComparison.OrdinalIgnoreCase) != -1)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// 名单全部内容是否在字符串中
        /// value.IndexOf(array[item])
        /// 忽略大小写
        /// </summary>
        /// <param name="value"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        public static bool AllInContent(this string value, params string[] array)
        {
            if (array == null || array.Length == 0 || value.IsNullOrWhiteSpace())
                return false;

            foreach (string item in array)
            {
                if (value.IndexOf(item, StringComparison.OrdinalIgnoreCase) == -1)
                    return false;
            }
            return true;
        }

        public static int GetBytesLength(this string value)
        {
            return GetBytesLength(value, Encoding.Default);
        }

        public static int GetBytesLength(this string value, Encoding encoding)
        {
            if (String.IsNullOrEmpty(value))
            {
                return 0;
            }
            if (encoding == null)
            {
                encoding = Encoding.Default;
            }
            return encoding.GetBytes(value).Length;
        }






        /// <summary>
        /// 截取字符串
        /// 减3位 拼省略号【...】
        /// </summary>
        /// <param name="value"></param>
        /// <param name="len">最大长度</param>
        /// <param name="withSuffix">是否拼接【...】</param>
        /// <returns></returns>
        public static string CutString(this string value, int len, bool withSuffix = true)
        {
            if (!string.IsNullOrWhiteSpace(value)
                && value.Length > len)
            {
                if (withSuffix && value.Length > 3)
                    return value.Remove(len - 3) + "...";
                else
                    return value.Remove(len);
            }
            else
                return value;
        }

        /// <summary>
        /// 切割字符串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="separator">分割符号</param>
        /// <param name="maxCount"></param>
        /// <param name="options">设置省略返回的数组中的空数组元素</param>
        /// <returns></returns>
        public static string[] GetArray(this string value, string separator, int maxCount = int.MaxValue, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
        {
            return value.ValueOrEmpty().Split(new string[1] { separator }, maxCount, options);
        }

        /// <summary>
        /// 切割字符串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="separator">分割符号</param>
        /// <param name="maxCount"></param>
        /// <param name="options">设置省略返回的数组中的空数组元素</param>
        /// <returns></returns>
        public static List<string> GetArrayList(this string value, string separator, int maxCount = int.MaxValue, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
        {
            return value.GetArray(separator, maxCount, options).ToList<string>();
        }

        /// <summary>
        /// 字符串按长度分页
        /// </summary>
        /// <param name="value"></param>
        /// <param name="pageSize">页尺寸</param>
        /// <returns></returns>
        public static List<string> ToArrayList(this string value, int pageSize)
        {
            var list = new List<string>();
            if (value.IsNotEmpty())
            {
                var pageCount = Math.Ceiling(1M * value.Length / pageSize).ToInt32();
                for (int page = 0; page < pageCount; page++)
                {
                    var arr_char = value.Skip(page * pageSize).Take(pageSize);
                    list.Add(string.Join(null, arr_char));
                }
            }

            return list;
        }






        /// <summary>
        /// String转Unicode
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string StringToUnicode(this string source)
        {
            if (string.IsNullOrWhiteSpace(source))
                return string.Empty;

            byte[] bytes = Encoding.Unicode.GetBytes(source);
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i += 2)
            {
                stringBuilder.AppendFormat("\\u{0}{1}", bytes[i + 1].ToString("x").PadLeft(2, '0'), bytes[i].ToString("x").PadLeft(2, '0'));
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Unicode转String
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string UnicodeToString(this string source)
        {
            if (string.IsNullOrWhiteSpace(source))
                return string.Empty;

            return new Regex(@"\\u([0-9A-F]{4})", RegexOptions.IgnoreCase | RegexOptions.Compiled).Replace(source, x => string.Empty + Convert.ToChar(Convert.ToUInt16(x.Result("$1"), 16)));
        }

        public static byte[] HexStrToByte(this string source)
        {
            source = source.Replace(" ", "");
            if ((source.Length % 2) != 0)
                source += " ";
            byte[] returnBytes = new byte[source.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(source.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        public static string ByteToHexStr(this byte[] source, string format = "X2")
        {
            var returnStr = new StringBuilder();
            if (source != null)
            {
                if (!"X2".Equals(format, StringComparison.OrdinalIgnoreCase))
                    format = "X2";

                for (int i = 0; i < source.Length; i++)
                {
                    //returnStr.Append(source[i].ToString("X2"));            //大写
                    //returnStr.Append(source[i].ToString("x2"));              //小写
                    returnStr.Append(source[i].ToString(format));              //小写
                }
            }
            return returnStr.ToString();
        }

        public static byte[] GetBytes(this string sourece, string charset = "utf-8")
        {
            if (null == sourece)
                return null;

            return Encoding.GetEncoding(charset).GetBytes(sourece);
        }

        public static string GetString(this byte[] sourece, string charset = "utf-8")
        {
            if (null == sourece)
                return null;

            return Encoding.GetEncoding(charset).GetString(sourece);
        }



        /// <summary>
        /// Uri.EscapeDataString
        /// 【超过50000时分段处理】
        /// Uri.EscapeDataString入参最大长度：65519
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string UrlEscape(this string source)
        {
            if (source == null)
                return source;

            var limit = 50000;
            if (source.Length > limit)
            {
                var sb = new StringBuilder();
                var loops = source.Length / limit;
                for (int i = 0; i <= loops; i++)
                {
                    var tmp = i < loops
                        ? source.Substring(limit * i, limit)
                        : source.Substring(limit * i);

                    sb.Append(Uri.EscapeDataString(tmp));
                }

                return sb.ToString();
            }
            else
                return Uri.EscapeDataString(source);
        }

        /// <summary>
        /// Uri.UnescapeDataString
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string UrlUnescape(this string source)
        {
            if (source == null)
                return source;

            return Uri.UnescapeDataString(source);
        }

        //[Obsolete("此方法对加号处理不友好，建议使用CySoft.Utility.StringExtension.UrlEscape()方法；但有部分第三方接口需要使用不同编码进行UrlEncode，所以仍需保留")]
        //public static string UrlEncode(this string source) => UrlEncode(source, Encoding.UTF8);

        //[Obsolete("此方法对加号处理不友好，建议使用CySoft.Utility.StringExtension.UrlEscape()方法；但有部分第三方接口需要使用不同编码进行UrlEncode，所以仍需保留")]
        //public static string UrlEncode(this string source, Encoding encoding)
        //{
        //    if (source == null)
        //        return source;

        //    var limit = 50000;
        //    if (source.Length > limit)
        //    {
        //        var sb = new StringBuilder();
        //        var loops = source.Length / limit;
        //        for (int i = 0; i <= loops; i++)
        //        {
        //            var tmp = i < loops
        //                ? source.Substring(limit * i, limit)
        //                : source.Substring(limit * i);

        //            sb.Append(System.Web.HttpUtility.UrlEncode(str: tmp, e: encoding));
        //        }

        //        return sb.ToString();
        //    }
        //    else
        //        return System.Web.HttpUtility.UrlEncode(str: source, e: encoding);
        //}

        //[Obsolete("此方法对加号处理不友好，建议使用CySoft.Utility.StringExtension.UrlUnescape()方法；但有部分第三方接口需要使用不同编码进行UrlDecode，所以仍需保留")]
        //public static string UrlDecode(this string source) => UrlDecode(source, Encoding.UTF8);

        //[Obsolete("此方法对加号处理不友好，建议使用CySoft.Utility.StringExtension.UrlUnescape()方法；但有部分第三方接口需要使用不同编码进行UrlDecode，所以仍需保留")]
        //public static string UrlDecode(this string source, Encoding encoding)
        //{
        //    if (source == null)
        //        return source;

        //    return System.Web.HttpUtility.UrlDecode(str: source, e: encoding);
        //}



        /// <summary>
        /// 移除字符串
        /// 默认：移除空格
        /// <code>source.Replace(oldValue, string.Empty)</code>
        /// </summary>
        /// <param name="source"></param>
        /// <param name="oldValue"></param>
        /// <returns></returns>
        public static string TrimAny(this string source, string oldValue = " ")
        {
            return source == null
                ? source
                : source.Replace(oldValue, string.Empty);
        }

        /// <summary>
        /// 移除字符串数组
        /// <code>source.Replace(arr_oldValue, string.Empty);</code>
        /// </summary>
        /// <param name="source"></param>
        /// <param name="arr_oldValue">为空则替换空格</param>
        /// <returns></returns>
        public static string TrimAny(this string source, params string[] arr_oldValue)
        {
            if (source == null)
                return null;

            if (arr_oldValue == null || arr_oldValue.Length == 0)
                arr_oldValue = new string[1] { " " };

            if (arr_oldValue?.Length > 0)
            {
                foreach (string oldValue in arr_oldValue)
                    source = source.Replace(oldValue, string.Empty);
            }

            return source;
        }

        /// <summary>
        /// 移除字符串数组
        /// </summary>
        /// <param name="source"></param>
        /// <param name="arr_oldValueRegex">为空则替换空格</param>
        /// <returns></returns>
        public static string TrimAny(this string source, params Regex[] arr_oldValueRegex)
        {
            if (source == null)
                return null;

            if (arr_oldValueRegex == null || arr_oldValueRegex.Length == 0)
                arr_oldValueRegex = new Regex[1] { new Regex(pattern: @" ", options: RegexOptions.IgnoreCase | RegexOptions.Compiled) };

            if (arr_oldValueRegex?.Length > 0)
            {
                foreach (Regex valueRegex in arr_oldValueRegex)
                    source = valueRegex.Replace(source, string.Empty);
            }

            return source;
        }

        /// <summary>
        /// 将多个字符串替换为新字符串
        /// </summary>
        /// <param name="source"></param>
        /// <param name="arr_oldValue"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        public static string ReplaceAny(this string source, string[] arr_oldValue, string newValue = "")
        {
            if (source == null)
                return null;

            if (arr_oldValue != null && arr_oldValue.Length > 0)
            {
                foreach (string oldValue in arr_oldValue)
                    source = source.Replace(oldValue, newValue);
            }

            return source;
        }

        /// <summary>
        /// 将多个字符串替换为新字符串
        /// </summary>
        /// <param name="source"></param>
        /// <param name="arr_oldValueRegex"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        public static string ReplaceAny(this string source, string newValue = "", params Regex[] arr_oldValueRegex)
        {
            if (source == null)
                return null;

            if (arr_oldValueRegex != null && arr_oldValueRegex.Length > 0)
            {
                foreach (Regex valueRegex in arr_oldValueRegex)
                    source = valueRegex.Replace(source, newValue);
            }

            return source;
        }

        public static Hashtable QueryStringToHashtable(this string sourece, bool NeedUrlDecode = true)
        {
            var hash = new Hashtable();
            if (!sourece.IsNullOrWhiteSpace())
            {
                var list_item = sourece
                    .Trim()
                    .Trim('?')
                    .Trim('&')
                    .GetArray("&").ToList();

                foreach (var item in list_item)
                {
                    if (item.IsNullOrWhiteSpace())
                        continue;

                    var k_v = item.Split(new char[] { '=' }, count: 2, options: StringSplitOptions.None).ToList();
                    if (k_v.Count == 1)
                        k_v.Add(string.Empty);

                    if (k_v[0].IsNullOrWhiteSpace())
                        continue;

                    hash[k_v[0]] = NeedUrlDecode
                        ? k_v[1].ValueOrEmpty().UrlUnescape()
                        : k_v[1].ValueOrEmpty();
                }
            }
            return hash;
        }
    }
}
