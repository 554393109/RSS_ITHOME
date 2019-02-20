﻿/************************************************************************
 * 文件标识：  41788caa-8d4c-4e9d-a1c7-ceab30e893ec
 * 项目名称：  APP.Utility.Extension  
 * 项目描述：  
 * 类 名 称：  ObjectExtensions
 * 版 本 号：  v1.0.0.0 
 * 说    明：  
 * 作    者：  尹自强
 * 创建时间：  2018/1/30 16:24:57
 * 更新时间：  2018/1/30 16:24:57
************************************************************************
 * Copyright @ 尹自强 2018. All rights reserved.
************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace APP.Utility.Extension
{
    public static class ObjectExtensions
    {
        #region 类型判断

        /// <summary>
        /// 是否为ip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(this object obj)
        {
            bool isCorrect = false;
            string ip = string.Empty;

            if (obj != null)
                ip = obj.ToString();

            isCorrect = Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
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

            isCorrect = Regex.IsMatch(url, @"^((http|https)://)(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(:[0-9]{1,4})*(/[a-zA-Z0-9\&%_\./-~-]*)?");
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
            var dt = default(DateTime);

            var val = obj.ValueOrEmpty();
            if (!val.IsNullOrWhiteSpace())
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
        /// 判断是否DateTime且是默认时间
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
        /// 判断是否Null或string.Empty
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

        #endregion 类型判断

        #region 类型转换

        public static bool ToBoolean(this object obj)
        {
            if (obj == null)
                return false;

            bool result = false;
            bool.TryParse(obj.ToString(), out result);
            return result;
        }

        #endregion 类型转换

        /// <summary>
        /// 为null时使用string.Empty
        /// obj?.ToString() ?? string.Empty;
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
        /// (obj == null || string.IsNullOrWhiteSpace(obj.ToString())) ? val.ValueOrEmpty() : obj.ToString();
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="val"></param>
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
    }
}
