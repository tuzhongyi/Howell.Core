using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Howell.Time
{
    /// <summary>
    /// ISO8601 日期和时间格式
    /// </summary>
    public enum ISO8601DateTimeFormats
    {
        /// <summary>
        /// 基本日历日期和基本时间格式
        /// </summary>
        BasicCalendarDateAndLocalTime = 0,
        /// <summary>
        /// 扩展日历日期和基本时间格式
        /// </summary>
        ExtendedCalendarDateAndLocalTime = 1,
        /// <summary>
        /// 基本序列日期和UTC时间格式
        /// </summary>
        BasicOrdinalDateAndUTCTime = 2,
        /// <summary>
        /// 扩展序列日期和UTC时间格式
        /// </summary>
        ExtendedOrdinalDateAndUTCTime = 3,
        /// <summary>
        /// 基本周日期和基本时间格式
        /// </summary>
        BasicWeekDateAndLocalTime = 4,
        /// <summary>
        /// 扩展周日期和时间格式
        /// </summary>
        ExtendedWeekDateAndLocalTime = 5
    };

    /// <summary>
    /// ISO8601标准的日期和时间组合
    /// </summary>
    public struct ISO8601DateTime
    {
        /// <summary>
        /// 创建ISO8601DateTime对象
        /// </summary>
        /// <param name="datetime">日期和时间数据</param>
        public ISO8601DateTime(DateTime datetime)
            : this(datetime, ISO8601DateTimeFormats.BasicCalendarDateAndLocalTime)
        { }
        /// <summary>
        /// 创建ISO8601DateTime对象
        /// </summary>
        /// <param name="datetime">日期和时间数据</param>
        /// <param name="format">格式化方式</param>
        public ISO8601DateTime(DateTime datetime, ISO8601DateTimeFormats format)
        {
            m_Value = datetime;
            m_Format = format;
        }








        private static String[][] MyRegularExpressions = new string[][] {
            new String[] {"BasicCalendarDateAndLocalTime","^((?<year>[0-9]{4})(?<month>1[0-2]|0[1-9])(?<day>3[0-1]|0[1-9]|[1-2][0-9])T(?<hour>2[0-3]|[01][0-9])(?<minute>[0-5][0-9])(?<second>[0-5][0-9]))$","{0:D4}{1:D2}{2:D2}T{3:D2}{4:D2}{5:D2}"},
            new String[] {"ExtendedCalendarDateAndLocalTime","^((?<year>[0-9]{4})-(?<month>1[0-2]|0[1-9])-(?<day>3[0-1]|0[1-9]|[1-2][0-9])T(?<hour>2[0-3]|[01][0-9]):(?<minute>[0-5][0-9]):(?<second>[0-5][0-9]))$","{0:D4}-{1:D2}-{2:D2}T{3:D2}:{4:D2}:{5:D2}"},
            new String[] {"BasicOrdinalDateAndUTCTime","^((?<year>[0-9]{4})(?<dayofyear>3[0-5][0-9]|36[0-6]|[0-2][0-9][0-9])T(?<hour>2[0-3]|[01][0-9])(?<minute>[0-5][0-9])(?<second>[0-5][0-9])Z)$","{0:D4}{1:D3}T{2:D2}{3:D2}{4:D2}Z"},
            new String[] {"ExtendedOrdinalDateAndUTCTime","^((?<year>[0-9]{4})-(?<dayofyear>3[0-5][0-9]|36[0-6]|[0-2][0-9][0-9])T(?<hour>2[0-3]|[01][0-9]):(?<minute>[0-5][0-9]):(?<second>[0-5][0-9])Z)$","{0:D4}-{1:D3}T{2:D2}:{3:D2}:{4:D2}Z"},
            new String[] {"BasicWeekDateAndLocalTime","^((?<year>[0-9]{4})W(?<weekofyear>5[0-3]|[1-4][0-9]|0[1-9])(?<dayofweek>[1-7])T(?<hour>2[0-3]|[01][0-9])(?<minute>[0-5][0-9])(?<second>[0-5][0-9]))$","{0:D4}W{1:D2}{2:D1}T{3:D2}{4:D2}{5:D2}"}, //dayofweek 1-Monday
            new String[] {"ExtendedWeekDateAndLocalTime","^((?<year>[0-9]{4})-W(?<weekofyear>5[0-3]|[1-4][0-9]|0[1-9])-(?<dayofweek>[1-7])T(?<hour>2[0-3]|[01][0-9]):(?<minute>[0-5][0-9]):(?<second>[0-5][0-9]))$","{0:D4}-W{1:D2}-{2:D1}T{3:D2}:{4:D2}:{5:D2}"}, //dayofweek 1-Monday
        };

        /// <summary>
        /// 解析日期和时间字符串
        /// </summary>
        /// <param name="s">日期和时间字符串</param>
        /// <returns>返回ISO8601DateTime实例</returns>
        public static ISO8601DateTime Parse(String s)
        {
            ISO8601DateTime datetime;
            if (true == TryParse(s, out datetime)) return datetime;
            throw new ArgumentException("Invalid argument,illegal date format.");
        }
        /// <summary>
        /// 尝试解析日期和时间字符串
        /// </summary>
        /// <param name="s">日期和时间字符串</param>
        /// <param name="datetime">输出ISO8601DateTime实例</param>
        /// <returns>解析成功返回true,失败返回false.</returns>
        public static Boolean TryParse(String s, out ISO8601DateTime datetime)
        {
            if (s == null || s == String.Empty)
                throw new ArgumentNullException("Argument s is null");
            for (Int32 i = 0; i < MyRegularExpressions.GetLength(0); ++i)
            {
                Regex reg = new Regex(MyRegularExpressions[i][1]);
                Match m = reg.Match(s);
                if (m.Success == true)
                {
                    datetime = new ISO8601DateTime(Match2DateTime(m, (ISO8601DateTimeFormats)i), (ISO8601DateTimeFormats)i);
                    return true;
                }
            };
            datetime = default(ISO8601DateTime);
            return false;
        }


        /// <summary>
        /// Regular.Match Group 2 DateTime
        /// </summary>
        /// <param name="m">Regular.Match</param>
        /// <param name="format">ISO8601DateTimeFormats</param>
        /// <returns>return DateTime</returns>
        internal static DateTime Match2DateTime(Match m, ISO8601DateTimeFormats format)
        {
            DateTime datetime = default(DateTime);
            switch (format)
            {
                case ISO8601DateTimeFormats.BasicCalendarDateAndLocalTime:
                case ISO8601DateTimeFormats.ExtendedCalendarDateAndLocalTime:
                    datetime = new DateTime
                    (
                        Convert.ToInt32(m.Groups["year"].Value),
                        Convert.ToInt32(m.Groups["month"].Value),
                        Convert.ToInt32(m.Groups["day"].Value),
                        Convert.ToInt32(m.Groups["hour"].Value),
                        Convert.ToInt32(m.Groups["minute"].Value),
                        Convert.ToInt32(m.Groups["second"].Value)
                    );
                    break;
                case ISO8601DateTimeFormats.BasicOrdinalDateAndUTCTime:
                case ISO8601DateTimeFormats.ExtendedOrdinalDateAndUTCTime:
                    datetime = new DateTime
                    (
                        Convert.ToInt32(m.Groups["year"].Value),
                        1, 1,
                        Convert.ToInt32(m.Groups["hour"].Value),
                        Convert.ToInt32(m.Groups["minute"].Value),
                        Convert.ToInt32(m.Groups["second"].Value)
                    );
                    datetime = datetime.AddDays(Convert.ToInt32(m.Groups["dayofyear"].Value) - 1);
                    break;
                case ISO8601DateTimeFormats.BasicWeekDateAndLocalTime:
                case ISO8601DateTimeFormats.ExtendedWeekDateAndLocalTime:
                    datetime = new DateTime
                    (
                        Convert.ToInt32(m.Groups["year"].Value),
                        1, 1,
                        Convert.ToInt32(m.Groups["hour"].Value),
                        Convert.ToInt32(m.Groups["minute"].Value),
                        Convert.ToInt32(m.Groups["second"].Value)
                    );
                    Int32 dayofyear = ISO8601DateTimeHelper.GetDayOfYearByWeek(Convert.ToInt32(m.Groups["year"].Value), Convert.ToInt32(m.Groups["weekofyear"].Value), Convert.ToInt32(m.Groups["dayofweek"].Value));
                    datetime = datetime.AddDays(dayofyear - 1);
                    break;
                default:
                    break;
            }
            return datetime;
        }



        DateTime m_Value;
        /// <summary>
        /// 日期和时间数值
        /// </summary>
        public DateTime Value
        {
            get { return m_Value; }
            set { m_Value = value; }
        }
        ISO8601DateTimeFormats m_Format;
        /// <summary>
        /// 日期和时间格式
        /// </summary>
        public ISO8601DateTimeFormats Format
        {
            get { return m_Format; }
            set { m_Format = value; }
        }

        /// <summary>
        /// 转换为ISO8601规范的日期和时间字符串 默认格式YYYYMMDDThhmmss
        /// </summary>
        /// <returns>返回ISO8601规范的日期和时间字符串</returns>
        public override string ToString()
        {
            Int32 no = (Int32)Format;
            switch (Format)
            {
                case ISO8601DateTimeFormats.BasicCalendarDateAndLocalTime:
                    return String.Format(MyRegularExpressions[no][2], Value.Year, Value.Month, Value.Day, Value.Hour, Value.Minute, Value.Second);
                case ISO8601DateTimeFormats.ExtendedCalendarDateAndLocalTime:
                    return String.Format(MyRegularExpressions[no][2], Value.Year, Value.Month, Value.Day, Value.Hour, Value.Minute, Value.Second);
                case ISO8601DateTimeFormats.BasicOrdinalDateAndUTCTime:
                    return String.Format(MyRegularExpressions[no][2], Value.Year, Value.DayOfYear, Value.Hour, Value.Minute, Value.Second);
                case ISO8601DateTimeFormats.ExtendedOrdinalDateAndUTCTime:
                    return String.Format(MyRegularExpressions[no][2], Value.Year, Value.DayOfYear, Value.Hour, Value.Minute, Value.Second);
                case ISO8601DateTimeFormats.BasicWeekDateAndLocalTime:
                    return String.Format(MyRegularExpressions[no][2], Value.Year, ISO8601DateTimeHelper.GetWeekOfYear(Value), ISO8601DateTimeHelper.GetDayOfWeek(Value.DayOfWeek), Value.Hour, Value.Minute, Value.Second);
                case ISO8601DateTimeFormats.ExtendedWeekDateAndLocalTime:
                    return String.Format(MyRegularExpressions[no][2], Value.Year, ISO8601DateTimeHelper.GetWeekOfYear(Value), ISO8601DateTimeHelper.GetDayOfWeek(Value.DayOfWeek), Value.Hour, Value.Minute, Value.Second);
                default:
                    return "";
            }
        }
    }
}
