using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Howell.Text.RegularExpressions
{
    /// <summary>
    /// 正则表达式工具
    /// </summary>
    public static class RegexHelper 
    {

        /// <summary>
        /// 正则表达式常量
        /// </summary>
        public static class Constants
        {
            /// <summary>
            /// 整数的正则表达式
            /// </summary>
            public const String Integer = @"^[+-]?\d+$";
            /// <summary>
            /// Float正则表达式
            /// </summary>
            public const String Float = @"^[+-]?(?:\d+)(?:\.\d+)?$";
            /// <summary>
            /// IPv4地址正则表达式
            /// </summary>
            public const String IPv4Address = @"^(((\d{1,2})|(1\d{2})|(2[0-4]\d)|(25[0-5]))\.){3}((\d{1,2})|(1\d{2})|(2[0-4]\d)|(25[0-5]))$";
            /// <summary>
            /// IPv6地址正则表达式
            /// </summary>
            public const String IPv6Address = @"^\s*((([0-9A-Fa-f]{1,4}:){7}([0-9A-Fa-f]{1,4}|:))|(([0-9A-Fa-f]{1,4}:){6}(:[0-9A-Fa-f]{1,4}|((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3})|:))|(([0-9A-Fa-f]{1,4}:){5}(((:[0-9A-Fa-f]{1,4}){1,2})|:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3})|:))|(([0-9A-Fa-f]{1,4}:){4}(((:[0-9A-Fa-f]{1,4}){1,3})|((:[0-9A-Fa-f]{1,4})?:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){3}(((:[0-9A-Fa-f]{1,4}){1,4})|((:[0-9A-Fa-f]{1,4}){0,2}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){2}(((:[0-9A-Fa-f]{1,4}){1,5})|((:[0-9A-Fa-f]{1,4}){0,3}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){1}(((:[0-9A-Fa-f]{1,4}){1,6})|((:[0-9A-Fa-f]{1,4}){0,4}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(:(((:[0-9A-Fa-f]{1,4}){1,7})|((:[0-9A-Fa-f]{1,4}){0,5}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:)))(%.+)?\s*$";
            /// <summary>
            /// 电子邮件地址正则表达式
            /// </summary>
            public const String EmailAddress = @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$";
            /// <summary>
            /// 中国的手机号码正则表达式
            /// </summary>
            public const String MobileNumber = @"^((\+86)|(86))?1\d{10}$";
            /// <summary>
            /// 中国移动的手机号码正则表达式
            /// </summary>
            public const String ChinaMobileNumber = @"^((\+86)|(86))?((1(3[5-9]|5[012789]|8[78]|47)\d{8})|(134[0-8]\d{7}))$";
            /// <summary>
            /// 中国联通的手机号码正则表达式
            /// </summary>
            public const String ChinaUnicomNumber = @"^((\+86)|(86))?1(3[0-2]|5[56]|8[56]|45)\d{8}$";
            /// <summary>
            /// 中国电信的手机号码正则表达式
            /// </summary>
            public const String ChinaTelecomNumber = @"^((\+86)|(86))?((1(33|53|8[09])\d{8})|(1349\d{7}))$";
            /// <summary>
            /// 
            /// </summary>
            public const String PhysicalAddress = @"^([0-9a-fA-F]{2})(([\-][0-9a-fA-F]{2}){5})$";
        }
        /// <summary>
        /// 验证是否符合正则表达式
        /// </summary>
        /// <param name="pattern">验证格式</param>
        /// <param name="input">输入待验证字符串</param>
        /// <returns>如果符合格式返回true,否则返回false</returns>
        public static bool IsRegex(string pattern, string input)
        {
            Regex reg = new Regex(pattern, RegexOptions.Compiled);
            return reg.IsMatch(input);
        }
        /// <summary>
        /// 判断字符串是可以符合整数类型的规则。
        /// </summary>
        /// <param name="value">字符串值。</param>
        /// <returns>返回是可以符合整数类型的规则。</returns>
        public static Boolean IsInteger(String value)
        {
            return IsRegex(RegexHelper.Constants.Integer, value);
        }
        /// <summary>
        /// 判断字符串是可以符合浮点数类型的规则。
        /// </summary>
        /// <param name="value">字符串值。</param>
        /// <returns>返回是可以符合浮点数类型的规则。</returns>
        public static Boolean IsFloat(String value)
        {
            return IsRegex(RegexHelper.Constants.Float, value);
        }
        /// <summary>
        /// 判断字符串是否可以符合IPv4Address的规则。
        /// </summary>
        /// <param name="value">字符串值。</param>
        /// <returns>返回是可以符合IPv4Address的规则。</returns>
        public static Boolean IsIPv4Address(String value)
        {
            return IsRegex(RegexHelper.Constants.IPv4Address, value);
        }
        /// <summary>
        /// 判断字符串是否可以符合IPv6Address的规则。
        /// </summary>
        /// <param name="value">字符串值。</param>
        /// <returns>返回是可以符合IPv6Address的规则。</returns>
        public static Boolean IsIPv6Address(String value)
        {
            return IsRegex(RegexHelper.Constants.IPv6Address, value);
        }        
        /// <summary>
        /// 验证是否电子邮件地址
        /// </summary>
        /// <param name="value">字符串值。</param>
        /// <returns>返回是可以符合电子邮件地址的规则。</returns>
        public static bool IsEmailAddress(string value)
        {
            return IsRegex(RegexHelper.Constants.EmailAddress, value);
        }
        /// <summary>
        /// 验证是否中国的手机号码。
        /// </summary>
        /// <param name="value">字符串值。</param>
        /// <returns>如果是返回true,否则返回false</returns>
        public static bool IsMobileNumber(string value)
        {
            return IsRegex(RegexHelper.Constants.MobileNumber, value);
        }
        /// <summary>
        /// 验证是否是中国移动的手机号码段
        /// </summary>
        /// <param name="value">字符串值。</param>
        /// <returns>如果是返回true,否则返回false</returns>
        public static bool IsChinaMobileNumber(string value)
        {
            //1340-1348 135 136 137 138 139 150 151 152 157 158 159 187 188 147
            return IsRegex(RegexHelper.Constants.ChinaMobileNumber, value);
        }
        /// <summary>
        /// 验证是否是中国联通的手机号码段
        /// </summary>
        /// <param name="value">字符串值。</param>
        /// <returns>如果是返回true,否则返回false</returns>
        public static bool IsChinaUnicomNumber(string value)
        {
            //130 131 132 155 156 185 186 145
            return IsRegex(RegexHelper.Constants.ChinaUnicomNumber, value);
        }
        /// <summary>
        /// 验证是否是中国电信的手机号码段
        /// </summary>
        /// <param name="value">字符串值。</param>
        /// <returns>如果是返回true,否则返回false</returns>
        public static bool IsChinaTelecomNumber(string value)
        {
            //133 1349 153 180 189
            return IsRegex(RegexHelper.Constants.ChinaTelecomNumber, value);
        }
        /// <summary>
        /// 验证是否物理地址
        /// </summary>
        /// <param name="value">字符串值。</param>
        /// <returns>如果是返回true,否则返回false</returns>
        public static bool IsPhysicalAddress(string value)
        {
            return IsRegex(RegexHelper.Constants.PhysicalAddress, value);
        }
    }
}
