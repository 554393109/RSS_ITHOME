﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace APP.Utility.Extension
{
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

        public static string ToFormat(this string value, params object[] args)
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                return value;
            }
            return String.Format(value, args);
        }

        public static string Join(this string[] array, string separator)
        {
            if (array != null || array.Length > 0)
            {
                return String.Join(separator, array);
            }
            return String.Empty;
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

        public static bool Contains(this string[] array, string value)
        {
            if (array != null && array.Count() > 0)
            {
                foreach (string item in array)
                {
                    if (item.Equals(value))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool Contains(this string[] array, string value, StringComparison comparisonType)
        {
            if (array != null && array.Count() > 0)
            {
                foreach (string item in array)
                {
                    if (item.Equals(value, comparisonType))
                    {
                        return true;
                    }
                }
            }
            return false;
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

        public static int ToInt32(this string value)
        {
            return Convert.ToInt32(value);
        }

        public static long ToInt64(this string value)
        {
            return Convert.ToInt64(value);
        }

        public static bool ToBoolean(this string value)
        {
            return Convert.ToBoolean(value);
        }

        public static T ToEnum<T>(this string value)
        {
            var @enum = (T)Enum.Parse(typeof(T), value, true);
            return @enum;
        }

        public static bool ToTryEnum<T>(this string value, out T result)
            where T : struct
        {
            var success = Enum.TryParse<T>(value, true, out result);
            return success;
        }



        /// <summary>
        /// 半角转全角(SBC case)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToSBC(this string value)
        {
            //半角转全角：
            char[] c = value.ToCharArray();
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
        public static string ToDBC(this string value)
        {
            char[] c = value.ToCharArray();
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
            if (!string.IsNullOrWhiteSpace(value) && value.Length > len)
                return value.Remove(len - 3) + (withSuffix ? "..." : string.Empty);
            else
                return value;
        }

        /// <summary>
        /// 切割字符串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="separator">分割符号</param>
        /// <param name="options">设置省略返回的数组中的空数组元素</param>
        /// <returns></returns>
        public static string[] GetArray(this string value, string separator, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
        {
            if (value.IsNullOrWhiteSpace())
                return new string[0] { };
            return value.Split(new string[] { separator }, options);
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

        /// <summary>
        /// <para>将 URL 中的参数名称/值编码为合法的格式。</para>
        /// <para>可以解决类似这样的问题：假设参数名为 tvshow, 参数值为 Tom&Jerry，如果不编码，可能得到的网址： http://a.com/?tvshow=Tom&Jerry&year=1965 编码后则为：http://a.com/?tvshow=Tom%26Jerry&year=1965 </para>
        /// <para>实践中经常导致问题的字符有：'&', '?', '=' 等</para>
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string UrlEscape(this string source)
        {
            if (source == null)
                return null;

            return Uri.EscapeDataString(source);
        }

        public static string UrlUnescape(this string source)
        {
            if (source == null)
                return null;

            return Uri.UnescapeDataString(source);
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

        //        /// <summary>
        //        /// 封装System.Web.HttpUtility.UrlEncode
        //        /// </summary>
        //        /// <param name="url"></param>
        //        /// <returns></returns>
        //#if DEBUG
        //        [Obsolete("此方法已过期，建议使用CySoft.Utility.StringExtension.UrlEscape()方法")]
        //#endif
        //        public static string UrlEncode(this string url)
        //        {
        //            if (url == null)
        //                return string.Empty;

        //            return System.Web.HttpUtility.UrlEncode(url);
        //        }


        ///// <summary>
        ///// 封装System.Web.HttpUtility.UrlDecode
        ///// </summary>
        ///// <param name="url"></param>
        ///// <returns></returns>
        //public static string UrlDecode(this string url)
        //{
        //    if (url == null)
        //        return string.Empty;

        //    return System.Web.HttpUtility.UrlDecode(url);
        //}

        /// <summary>
        /// 移除字符串
        /// </summary>
        /// <param name="source"></param>
        /// <param name="oldValue"></param>
        /// <returns></returns>
        public static string TrimAny(this string source, string oldValue = " ")
        {
            if (source == null)
                return null;

            return source.Replace(oldValue, string.Empty);
        }

        /// <summary>
        /// 移除字符串数组
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

            if (arr_oldValue?.Length > 0)
            {
                foreach (string oldValue in arr_oldValue)
                    source = source.Replace(oldValue, newValue);
            }

            return source;
        }
    }
}
