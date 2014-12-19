using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Howell.Time
{
    /// <summary>
    /// ISO8601 日期格式
    /// </summary>
    public enum ISO8601DateFormats
    {
        /// <summary>
        /// 基本日历日期格式
        /// </summary>
        BasicCalendarDate = 0,
        /// <summary>
        /// 扩展日历日期格式
        /// </summary>
        ExtendedCalendarDate = 1,
        /// <summary>
        /// 基本序列日期格式
        /// </summary>
        BasicOrdinalDate = 2,
        /// <summary>
        /// 扩展序列日期格式
        /// </summary>
        ExtendedOrdinalDate = 3,
        /// <summary>
        /// 基本周日期格式
        /// </summary>
        BasicWeekDate = 4,
        /// <summary>
        /// 扩展周日期格式
        /// </summary>
        ExtendedWeekDate = 5,
    }
    /// <summary>
    /// ISO8601 标准的日期
    /// </summary>
    public struct ISO8601Date
    {
        private ISO8601DateFormats m_Format;
        private DateTime m_Value;
        private static String[][] MyRegularExpressions = new string[][] {
                new String[] { "BasicCalendarDate", "^(?<year>[0-9]{4})(?<month>1[0-2]|0[1-9])(?<day>3[0-1]|0[1-9]|[1-2][0-9])$", "{0:D4}{1:D2}{2:D2}" },
                new String[] { "ExtendedCalendarDate", "^(?<year>[0-9]{4})-(?<month>1[0-2]|0[1-9])-(?<day>3[0-1]|0[1-9]|[1-2][0-9])$", "{0:D4}-{1:D2}-{2:D2}" },
                new String[] {"BasicOrdinalDate","^(?<year>[0-9]{4})(?<dayofyear>3[0-5][0-9]|36[0-6]|[0-2][0-9][0-9])$","{0:D4}{1:D3}"},
                new String[] {"ExtendedOrdinalDate","^(?<year>[0-9]{4})-(?<dayofyear>3[0-5][0-9]|36[0-6]|[0-2][0-9][0-9])$","{0:D4}-{1:D3}"},
                new String[] {"BasicWeekDate","^(?<year>[0-9]{4})W(?<weekofyear>5[0-3]|[1-4][0-9]|0[1-9])(?<dayofweek>[1-7])$","{0:D4}W{1:D2}{2:D1}"}, //dayofweek 1-Monday
                new String[] {"ExtendedWeekDate","^(?<year>[0-9]{4})-W(?<weekofyear>5[0-3]|[1-4][0-9]|0[1-9])-(?<dayofweek>[1-7])$","{0:D4}-W{1:D2}-{2:D1}"}, //dayofweek 1-Monday
        };
        /// <summary>
        /// 解析日期字符串
        /// </summary>
        /// <param name="s">日期字符串</param>
        /// <returns>返回ISO8601Date实例</returns>
        public static ISO8601Date Parse(String s)
        {            
            ISO8601Date date;
            if (true == TryParse(s, out date)) return date;
            throw new ArgumentException("Invalid argument,illegal date format.");
        }
        /// <summary>
        /// 尝试解析日期字符串
        /// </summary>
        /// <param name="s">日期字符串</param>
        /// <param name="date">输出ISO8601Date实例</param>
        /// <returns>解析成功返回true,失败返回false.</returns>
        public static Boolean TryParse(String s,out ISO8601Date date)
        {
            if (s == null || s == String.Empty)
                throw new ArgumentNullException("Argument s is null");
            for (Int32 i = 0; i < MyRegularExpressions.GetLength(0); ++i)
            {
                Regex reg = new Regex(MyRegularExpressions[i][1]);
                Match m = reg.Match(s);
                if (m.Success == true)
                {
                    date = new ISO8601Date(Match2DateTime(m, (ISO8601DateFormats)i), (ISO8601DateFormats)i);
                    return true;
                }
            };
            date = default(ISO8601Date);
            return false;
        }
        /// <summary>
        /// Regular.Match Group 2 DateTime
        /// </summary>
        /// <param name="m">Regular.Match</param>
        /// <param name="format">ISO8601DateFormats</param>
        /// <returns>return DateTime</returns>
        internal static DateTime Match2DateTime(Match m, ISO8601DateFormats format)
        {
            DateTime datetime = default(DateTime);
            switch (format)
            {
                case ISO8601DateFormats.BasicCalendarDate:
                case ISO8601DateFormats.ExtendedCalendarDate:
                    datetime = new DateTime(Convert.ToInt32(m.Groups["year"].Value), Convert.ToInt32(m.Groups["month"].Value), Convert.ToInt32(m.Groups["day"].Value));
                    break;
                case ISO8601DateFormats.BasicOrdinalDate:
                case ISO8601DateFormats.ExtendedOrdinalDate:
                    datetime = new DateTime(Convert.ToInt32(m.Groups["year"].Value), 1, 1);
                    datetime = datetime.AddDays(Convert.ToInt32(m.Groups["dayofyear"].Value) - 1);
                    break;
                case ISO8601DateFormats.BasicWeekDate:
                case ISO8601DateFormats.ExtendedWeekDate:
                    datetime = new DateTime(Convert.ToInt32(m.Groups["year"].Value), 1, 1);
                    Int32 dayofyear = ISO8601DateTimeHelper.GetDayOfYearByWeek(Convert.ToInt32(m.Groups["year"].Value), Convert.ToInt32(m.Groups["weekofyear"].Value), Convert.ToInt32(m.Groups["dayofweek"].Value));
                    datetime = datetime.AddDays(dayofyear-1);
                    break;
                default:
                    break;
            }
            return datetime;
        }
        /// <summary>
        /// 创建ISO8601Date对象
        /// </summary>
        /// <param name="datetime">日期和时间数据</param>
        public ISO8601Date(DateTime datetime)
            : this(datetime,ISO8601DateFormats.BasicCalendarDate)
        {
        }
        /// <summary>
        /// 创建ISO8601Date对象
        /// </summary>
        /// <param name="datetime">日期和时间数据</param>
        /// <param name="format">格式化方式</param>
        public ISO8601Date(DateTime datetime, ISO8601DateFormats format)
        {
            m_Value = datetime;
            m_Format = format;
        }

        /// <summary>
        /// 日期数值
        /// </summary>
        public DateTime Value 
        { 
            get{ return m_Value;}
            private set{ m_Value = value;}
        }
        /// <summary>
        /// 日期的格式
        /// </summary>
        public ISO8601DateFormats Format
        {
            get { return m_Format; }
            set { m_Format = value; }
        }
        /// <summary>
        /// 转换为ISO8601规范的日期字符串 默认格式YYYYMMDD
        /// </summary>
        /// <returns>返回ISO8601规范的日期字符串</returns>
        public override string ToString()
        {
            Int32 no = (Int32)Format;
            switch (Format)
            {
                case ISO8601DateFormats.BasicCalendarDate:
                    return String.Format(MyRegularExpressions[no][2], Value.Year, Value.Month, Value.Day);
                case ISO8601DateFormats.ExtendedCalendarDate:
                    return String.Format(MyRegularExpressions[no][2], Value.Year, Value.Month, Value.Day);
                case ISO8601DateFormats.BasicOrdinalDate:
                    return String.Format(MyRegularExpressions[no][2], Value.Year, Value.DayOfYear);
                case ISO8601DateFormats.ExtendedOrdinalDate:
                    return String.Format(MyRegularExpressions[no][2], Value.Year, Value.DayOfYear);
                case ISO8601DateFormats.BasicWeekDate:
                    return String.Format(MyRegularExpressions[no][2], Value.Year, ISO8601DateTimeHelper.GetWeekOfYear(Value), ISO8601DateTimeHelper.GetDayOfWeek(Value.DayOfWeek));
                case ISO8601DateFormats.ExtendedWeekDate:
                    return String.Format(MyRegularExpressions[no][2], Value.Year, ISO8601DateTimeHelper.GetWeekOfYear(Value), ISO8601DateTimeHelper.GetDayOfWeek(Value.DayOfWeek));
                default:
                    return "";
            }
        }

    }
}
