namespace APP.Utility
{
    using System;
    using System.Text.RegularExpressions;

    public sealed class RegexHelper
    {
        #region 付款码正则已测试

        /// <summary>
        /// 建行龙支付付款码校验
        /// 【62开头，(第3位，2-4借记卡，5-7贷记卡)，（第4-7位，固定0105），总长度为19位数字】
        /// <code><![CDATA[@"^(62)([4-7]{1})(0105)(\d{12})$"]]></code>
        /// </summary>
        public static readonly Regex LONGPAY_AuthCode = new Regex(pattern: @"^(62)([2-7]{1})(0105)(\d{12})$", options: RegexOptions.Compiled, matchTimeout: TimeSpan.FromMilliseconds(20));

        /// <summary>
        /// 银联云闪付付款码校验
        /// 【62开头，总长度为19位数字】
        /// <code><![CDATA[@"^(62)(\d{17})$"]]></code>
        /// </summary>
        public static readonly Regex UNIONPAY_AuthCode = new Regex(pattern: @"^(62)(\d{17})$", options: RegexOptions.Compiled, matchTimeout: TimeSpan.FromMilliseconds(20));

        /// <summary>
        /// 京东支付付款码校验
        /// 【(18开头，总长度为18位数字)或(62开头，总长度为19位数字)】
        /// <code><![CDATA[@"^(18)(\d{16})$|^(62)(\d{17})$"]]></code>
        /// </summary>
        public static readonly Regex JDPAY_AuthCode = new Regex(pattern: @"^(18)(\d{16})$|^(62)(\d{17})$", options: RegexOptions.Compiled, matchTimeout: TimeSpan.FromMilliseconds(20));

        /// <summary>
        /// 翼支付付款码校验
        /// 【51开头，总长度为18位数字】
        /// <code><![CDATA[@"^(51)(\d{16})$"]]></code>
        /// </summary>
        public static readonly Regex BESTPAY_AuthCode = new Regex(pattern: @"^(51)(\d{16})$", options: RegexOptions.Compiled, matchTimeout: TimeSpan.FromMilliseconds(20));

        /// <summary>
        /// 数字人民币校验
        /// 【01开头，总长度为19位数字】
        /// <code><![CDATA[@"^(01)(\d{17})$"]]></code>
        /// </summary>
        public static readonly Regex ECNY_AuthCode = new Regex(pattern: @"^(01)(\d{17})$", options: RegexOptions.Compiled, matchTimeout: TimeSpan.FromMilliseconds(20));

        /// <summary>
        /// 微信支付付款码校验
        /// 【10~15开头，总长度为18位数字】
        /// <code><![CDATA[@"^(1[0-5])(\d{16})$"]]></code>
        /// </summary>
        public static readonly Regex WECHAT_AuthCode = new Regex(pattern: @"^(1[0-5])(\d{16})$", options: RegexOptions.Compiled, matchTimeout: TimeSpan.FromMilliseconds(20));

        /// <summary>
        /// 支付宝付款码校验
        /// 【25~30开头，总长度为16~24位数字】
        /// <code><![CDATA[@"^(2[5-9]|30)(\d{14,22})$"]]></code>
        /// </summary>
        public static readonly Regex ALIPAY_AuthCode = new Regex(pattern: @"^(2[5-9]|30)(\d{14,22})$", options: RegexOptions.Compiled, matchTimeout: TimeSpan.FromMilliseconds(20));

        /// <summary>
        /// 支付宝刷脸付款标识校验（正则已测试，待使用场景上线）
        /// 【fp开头，总长度35位】
        /// <code><![CDATA[@"^(fp)([A-Za-z0-9]){35}$"]]></code>
        /// </summary>
        public static readonly Regex ALIPAY_FaceAuthCode = new Regex(pattern: @"^(fp)([A-Za-z0-9]{33})$", options: RegexOptions.Compiled, matchTimeout: TimeSpan.FromMilliseconds(20));

        #endregion 付款码正则已测试




        /// <summary>
        /// 中文、英文、数字，不含符号
        /// <code><![CDATA[@"^[\u4E00-\u9FA5A-Za-z0-9]$"]]></code>
        /// </summary>
        public static readonly Regex ChnEngNum = new Regex(pattern: @"^[\u4E00-\u9FA5A-Za-z0-9]+$", options: RegexOptions.Compiled, matchTimeout: TimeSpan.FromMilliseconds(50));

        /// <summary>
        /// 纯中文
        /// <code><![CDATA[@"^[\u4E00-\u9FA5]+$"]]></code>
        /// </summary>
        public static readonly Regex Chn = new Regex(pattern: @"^[\u4E00-\u9FA5]+$", options: RegexOptions.Compiled, matchTimeout: TimeSpan.FromMilliseconds(50));

        /// <summary>
        /// 纯英文
        /// <code><![CDATA[@"^[A-Za-z]+$"]]></code>
        /// </summary>
        public static readonly Regex Eng = new Regex(pattern: @"^[A-Za-z]+$", options: RegexOptions.Compiled, matchTimeout: TimeSpan.FromMilliseconds(20));

        /// <summary>
        /// 英文、数字
        /// <code><![CDATA[@"^[A-Za-z0-9]+$"]]></code>
        /// </summary>
        public static readonly Regex EngNum = new Regex(pattern: @"^[A-Za-z0-9]+$", options: RegexOptions.Compiled, matchTimeout: TimeSpan.FromMilliseconds(20));

        /// <summary>
        /// 手机号
        /// <code><![CDATA[@"^1[2-9]\d{9}$"]]></code>
        /// </summary>
        public static readonly Regex Mobile = new Regex(pattern: @"^1[2-9]\d{9}$", options: RegexOptions.Compiled, matchTimeout: TimeSpan.FromMilliseconds(20));

        /// <summary>
        /// 邮箱
        /// <code><![CDATA[@"^[\w!#$%&'*+/=?^_`{|}~-]+(?:\.[\w!#$%&'*+/=?^_`{|}~-]+)*@(?:[\w](?:[\w-]*[\w])?\.)+[\w](?:[\w-]*[\w])?"]]></code>
        /// </summary>
        public static readonly Regex Email = new Regex(pattern: @"^[\w!#$%&'*+/=?^_`{|}~-]+(?:\.[\w!#$%&'*+/=?^_`{|}~-]+)*@(?:[\w](?:[\w-]*[\w])?\.)+[\w](?:[\w-]*[\w])?", options: RegexOptions.Compiled, matchTimeout: TimeSpan.FromMilliseconds(50));

        /// <summary>
        /// IP
        /// <code><![CDATA[@"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$"]]></code>
        /// </summary>
        public static readonly Regex IP = new Regex(pattern: @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$", options: RegexOptions.Compiled, matchTimeout: TimeSpan.FromMilliseconds(20));

        /// <summary>
        /// URL
        /// <code><![CDATA[@"^((http|https)://)(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(:[0-9]{1,4})*(/[a-zA-Z0-9\&%_\./-~-]*)?"]]></code>
        /// </summary>
        public static readonly Regex URL = new Regex(pattern: @"^((http|https)://)(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(:[0-9]{1,4})*(/[a-zA-Z0-9\&%_\./-~-]*)?", options: RegexOptions.IgnoreCase | RegexOptions.Compiled, matchTimeout: TimeSpan.FromMilliseconds(50));
        //public static readonly Regex URL = new Regex(pattern: @"^((http|https)://)(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3})|(localhost))(:[0-9]{1,4})*(/[a-zA-Z0-9\&%_\./-~-]*)?", options: RegexOptions.Compiled, matchTimeout: TimeSpan.FromMilliseconds(50));

        /// <summary>
        /// 纯Time
        /// <code><![CDATA[@"^(([0,1]?[0-9])|(2[0-3])):[0-5]?[0-9](:[0-5]?[0-9](\.\d+)?)?$"]]></code>
        /// </summary>
        public static readonly Regex Time = new Regex(pattern: @"^(([0,1]?[0-9])|(2[0-3])):[0-5]?[0-9](:[0-5]?[0-9](\.\d+)?)?$", options: RegexOptions.Compiled, matchTimeout: TimeSpan.FromMilliseconds(20));

        /// <summary>
        /// Xml头部声明
        /// <code><![CDATA[@"(<\?xml).*?(>)"]]></code>
        /// </summary>
        public static readonly Regex XmlDeclaration = new Regex(pattern: @"(<\?xml).*?(>)", options: RegexOptions.IgnoreCase | RegexOptions.Compiled, matchTimeout: TimeSpan.FromMilliseconds(50));
    }
}
