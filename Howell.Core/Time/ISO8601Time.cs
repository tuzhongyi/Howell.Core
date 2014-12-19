using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Howell.Time
{

    /// <summary>
    /// ISO8601 时间格式
    /// </summary>
    public enum ISO8601TimeFormats
    {
        /// <summary>
        /// 基本短本地时间格式 2315
        /// </summary>
        BasicShortLocalTime = 0,
        /// <summary>
        /// 扩展短本地时间格式 23:15
        /// </summary>
        ExtendedShortLocalTime = 1,
        /// <summary>
        /// 基本本地时间格式 231520
        /// </summary>
        BasicLocalTime = 2,
        /// <summary>
        /// 扩展本地时间格式 23:15:20   
        /// </summary>
        ExtendedLocalTime = 3,
        /// <summary>
        /// 基本UTC时间格式 231520Z
        /// </summary>
        BasicUTCTime = 4,
        /// <summary>
        /// 扩展UTC时间格式 23:15:20Z   
        /// </summary>
        ExtendedUTCTime = 5,
        /// <summary>
        /// 基本本地时间加时区格式 231520+08
        /// </summary>
        BasicLocalTimeAndZoneHour = 6,
        /// <summary>
        /// 扩展本地时间加时区格式 23:15:20+08
        /// </summary>
        ExtendedLocalTimeAndZoneHour = 7,
        /// <summary>
        /// 基本本地时间加时区格式 231520+0800
        /// </summary>
        BasicLocalTimeAndZone = 8,
        /// <summary>
        /// 扩展本地时间加时区格式 23:15:20+08:00
        /// </summary>
        ExtendedLocalTimeAndZone = 9,
    }

    /// <summary>
    /// ISO8601 标准的时间
    /// </summary>
    /// <remarks>
    /// ISO8601 规范中一天的时间范围是00:00:00 - 24:00:00 
    /// 但是DateTime 不支持24:00:00这种时间，所以请转换为23:59:59
    /// </remarks>
    public struct ISO8601Time
    {


        /// <summary>
        /// 创建ISO8601Time对象
        /// </summary>
        /// <param name="datetime">日期和时间数据</param>
        public ISO8601Time(DateTime datetime)
            : this(datetime, ISO8601TimeFormats.BasicLocalTime)
        { }

        /// <summary>
        /// 创建ISO8601Time对象
        /// </summary>
        /// <param name="datetime">日期和时间数据</param>
        /// <param name="format">格式化方式</param>
        public ISO8601Time(DateTime datetime, ISO8601TimeFormats format)
            : this(datetime, format, new TimeSpan(0,0,0))
        {
        }

        /// <summary>
        /// 创建ISO8601Time对象
        /// </summary>
        /// <param name="datetime">日期和时间数据</param>
        /// <param name="format">格式化方式</param>
        /// <param name="timeZone">时差</param>
        public ISO8601Time(DateTime datetime, ISO8601TimeFormats format, TimeSpan timeZone)
        {
            m_Value = datetime;
            m_Format = format;
            m_TimeZone = timeZone;
        }


        private ISO8601TimeFormats m_Format;
        private DateTime m_Value;
        private static TimeSpan m_TimeZone;

        private static String[][] MyRegularExpressions = new string[][] {
                new String[] {"BasicShortLocalTime", "^((?<hour>2[0-3]|[01][0-9])(?<minute>[0-5][0-9]))$", "{0:D2}{1:D2}" },
                new String[] {"ExtendedShortLocalTime", "^((?<hour>2[0-3]|[01][0-9]):(?<minute>[0-5][0-9]))$", "{0:D2}:{1:D2}" },
                new String[] {"BasicLocalTime", "^((?<hour>2[0-3]|[01][0-9])(?<minute>[0-5][0-9])(?<second>[0-5][0-9]))$", "{0:D2}{1:D2}{2:D2}" },
                new String[] {"ExtendedLocalTime", "^((?<hour>2[0-3]|[01][0-9]):(?<minute>[0-5][0-9]):(?<second>[0-5][0-9]))$", "{0:D2}:{1:D2}:{2:D2}" },
                new String[] {"BasicUTCTime", "^((?<hour>2[0-3]|[01][0-9])(?<minute>[0-5][0-9])(?<second>[0-5][0-9])Z)$", "{0:D2}{1:D2}{2:D2}Z" },
                new String[] {"ExtendedUTCTime", "^((?<hour>2[0-3]|[01][0-9]):(?<minute>[0-5][0-9]):(?<second>[0-5][0-9])Z)$", "{0:D2}:{1:D2}:{2:D2}Z" },
                new String[] {"BasicLocalTimeAndZoneHour","^((?<hour>2[0-3]|[01][0-9])(?<minute>[0-5][0-9])(?<second>[0-5][0-9])(?<sign>[+-])(?<timezonehour>(2[0-3]|[01][0-9])))$","{0:D2}{1:D2}{2:D2}{3}{4:D2}"},
                new String[] {"ExtendedLocalTimeAndZoneHour","^((?<hour>2[0-3]|[01][0-9]):(?<minute>[0-5][0-9]):(?<second>[0-5][0-9])(?<sign>[+-])(?<timezonehour>(2[0-3]|[01][0-9])))$","{0:D2}:{1:D2}:{2:D2}{3}{4:D2}"},
                new String[] {"BasicLocalTimeAndZone","^((?<hour>2[0-3]|[01][0-9])(?<minute>[0-5][0-9])(?<second>[0-5][0-9])(?<sign>[+-])(?<timezonehour>(2[0-3]|[01][0-9]))(?<timezoneminute>[0-5][0-9]))$","{0:D2}{1:D2}{2:D1}{3}{4:D2}{5:D2}"},
                new String[] {"ExtendedLocalTimeAndZone","^((?<hour>2[0-3]|[01][0-9]):(?<minute>[0-5][0-9]):(?<second>[0-5][0-9])(?<sign>[+-])(?<timezonehour>(2[0-3]|[01][0-9])):(?<timezoneminute>[0-5][0-9]))$","{0:D2}:{1:D2}:{2:D2}{3}{4:D2}:{5:D2}"},
        };

        /// <summary>
        /// 解析时间字符串
        /// </summary>
        /// <param name="s">时间字符串</param>
        /// <returns>返回ISO8601Time实例</returns>
        public static ISO8601Time Parse(String s)
        {
            ISO8601Time time;
            if (true == TryParse(s, out time)) return time;
            throw new ArgumentException("Invalid argument,illegal time format.");
        }
        /// <summary>
        /// 尝试解析日期字符串
        /// </summary>
        /// <param name="s">时间字符串</param>
        /// <param name="time">输出ISO8601Time实例</param>
        /// <returns>解析成功返回true,失败返回false.</returns>
        public static Boolean TryParse(String s, out ISO8601Time time)
        {
            if (s == null || s == String.Empty)
                throw new ArgumentNullException("Argument s is null");
            for (Int32 i = 0; i < MyRegularExpressions.GetLength(0); ++i)
            {
                Regex reg = new Regex(MyRegularExpressions[i][1]);
                Match m = reg.Match(s);
                if (m.Success == true)
                {
                    time = new ISO8601Time(Match2Time(m, (ISO8601TimeFormats)i, out m_TimeZone), (ISO8601TimeFormats)i, m_TimeZone);
                    return true;
                }
            }
            time = default(ISO8601Time);
            return false;
        }

        internal static DateTime Match2Time(Match m, ISO8601TimeFormats format, out TimeSpan timeZone)
        {
            DateTime datetime = default(DateTime);
            
            timeZone = default(TimeSpan);
            switch (format)
            {
                case ISO8601TimeFormats.BasicLocalTime:
                case ISO8601TimeFormats.ExtendedLocalTime:
                    datetime = new DateTime(1, 1, 1, Convert.ToInt32(m.Groups["hour"].Value), Convert.ToInt32(m.Groups["minute"].Value), Convert.ToInt32(m.Groups["second"].Value));
                    break;
                case ISO8601TimeFormats.BasicUTCTime:
                case ISO8601TimeFormats.ExtendedUTCTime:
                    datetime = new DateTime(1, 1, 1, Convert.ToInt32(m.Groups["hour"].Value), Convert.ToInt32(m.Groups["minute"].Value), Convert.ToInt32(m.Groups["second"].Value));
                    break;
                case ISO8601TimeFormats.BasicLocalTimeAndZone:
                case ISO8601TimeFormats.ExtendedLocalTimeAndZone:
                    datetime = new DateTime(1, 1, 1, Convert.ToInt32(m.Groups["hour"].Value), Convert.ToInt32(m.Groups["minute"].Value), Convert.ToInt32(m.Groups["second"].Value));
                    timeZone = new TimeSpan(Convert.ToInt32(m.Groups["sign"].Value + m.Groups["timezonehour"].Value), Convert.ToInt32(m.Groups["timezoneminute"].Value), 0);
                    datetime = LocalToUtc(datetime,format, timeZone);
                    break;
                case ISO8601TimeFormats.BasicLocalTimeAndZoneHour:
                case ISO8601TimeFormats.ExtendedLocalTimeAndZoneHour:
                    datetime = new DateTime(1, 1, 1, Convert.ToInt32(m.Groups["hour"].Value), Convert.ToInt32(m.Groups["minute"].Value), Convert.ToInt32(m.Groups["second"].Value));
                    timeZone = new TimeSpan(Convert.ToInt32(m.Groups["sign"].Value + m.Groups["timezonehour"].Value), 0, 0);
                    datetime = LocalToUtc(datetime,format, timeZone);
                    break;
                case ISO8601TimeFormats.BasicShortLocalTime:
                case ISO8601TimeFormats.ExtendedShortLocalTime:
                    datetime = new DateTime(1, 1, 1, Convert.ToInt32(m.Groups["hour"].Value), Convert.ToInt32(m.Groups["minute"].Value), 0);
                    break;
                default:
                    break;
            }
            return datetime;
        }

        /// <summary>
        /// 转换为ISO8601规范的日期字符串 默认格式hhmmss
        /// </summary>
        /// <returns>返回ISO8601规范的时间字符串</returns>
        public override string ToString()
        {
            Int32 no = (Int32)Format;
            switch (Format)
            {
                case ISO8601TimeFormats.BasicLocalTime:
                    return String.Format(MyRegularExpressions[no][2], Value.Hour, Value.Minute, Value.Second);
                case ISO8601TimeFormats.ExtendedLocalTime:
                    return String.Format(MyRegularExpressions[no][2], Value.Hour, Value.Minute, Value.Second);
                case ISO8601TimeFormats.BasicUTCTime:
                    return String.Format(MyRegularExpressions[no][2], Value.Hour, Value.Minute, Value.Second);
                case ISO8601TimeFormats.ExtendedUTCTime:
                    return String.Format(MyRegularExpressions[no][2], Value.Hour, Value.Minute, Value.Second);
                case ISO8601TimeFormats.BasicLocalTimeAndZone:
                    DateTime datetime = UtcToLocal(Value, TimeZone);
                    
                    string sign = TimeZone.Hours < 0 ? "" : "+";
                    return String.Format(MyRegularExpressions[no][2], datetime.Hour, datetime.Minute, datetime.Second, sign, TimeZone.Hours, TimeZone.Minutes);
                case ISO8601TimeFormats.ExtendedLocalTimeAndZone:
                    datetime = UtcToLocal(Value, TimeZone);
                    sign = TimeZone.Hours < 0 ? "" : "+";
                    return String.Format(MyRegularExpressions[no][2], datetime.Hour, datetime.Minute, datetime.Second, sign, TimeZone.Hours, TimeZone.Minutes);
                case ISO8601TimeFormats.BasicLocalTimeAndZoneHour:
                    datetime = UtcToLocal(Value, TimeZone);
                    sign = TimeZone.Hours < 0 ? "" : "+";
                    return String.Format(MyRegularExpressions[no][2], datetime.Hour, datetime.Minute, datetime.Second, sign, TimeZone.Hours);
                case ISO8601TimeFormats.ExtendedLocalTimeAndZoneHour:
                    datetime = UtcToLocal(Value, TimeZone);
                    sign = TimeZone.Hours < 0 ? "" : "+";
                    return String.Format(MyRegularExpressions[no][2], datetime.Hour, datetime.Minute, datetime.Second, sign, TimeZone.Hours);
                case ISO8601TimeFormats.BasicShortLocalTime:
                    return String.Format(MyRegularExpressions[no][2], Value.Hour, Value.Minute);
                case ISO8601TimeFormats.ExtendedShortLocalTime:
                    return String.Format(MyRegularExpressions[no][2], Value.Hour, Value.Minute);
                default:
                    return "";
            }
        }

        /// <summary>
        /// 把UTC时间转回当前时间
        /// </summary>
        /// <param name="datetime">UTC时间的数据</param>
        /// <param name="timeZone">时区值</param>
        /// <returns></returns>
        private static DateTime UtcToLocal(DateTime datetime, TimeSpan timeZone)
        {
            datetime = datetime.AddHours(timeZone.Hours);
            datetime = datetime.AddMinutes(timeZone.Minutes);
            datetime = datetime.AddSeconds(timeZone.Seconds);

            return datetime;
        }

        /// <summary>
        /// 按照时区值把标准时间转成TUC时间
        /// </summary>
        /// <param name="datetime">日期和时期值</param>
        /// <param name="format">当前转换的格式，检查是否需要转换</param>
        /// <param name="timeZone">时区值</param>
        /// <returns></returns>
        private static DateTime LocalToUtc(DateTime datetime, ISO8601TimeFormats format, TimeSpan timeZone)
        {
            switch (format)
            {
                case ISO8601TimeFormats.BasicShortLocalTime:
                case ISO8601TimeFormats.ExtendedShortLocalTime:
                case ISO8601TimeFormats.BasicLocalTime:
                case ISO8601TimeFormats.ExtendedLocalTime:
                    break;
                case ISO8601TimeFormats.BasicUTCTime:
                case ISO8601TimeFormats.ExtendedUTCTime:
                case ISO8601TimeFormats.BasicLocalTimeAndZoneHour:
                case ISO8601TimeFormats.ExtendedLocalTimeAndZoneHour:
                case ISO8601TimeFormats.BasicLocalTimeAndZone:
                case ISO8601TimeFormats.ExtendedLocalTimeAndZone:
                    datetime = datetime.AddHours(-timeZone.Hours);
                    datetime = datetime.AddMinutes(-timeZone.Minutes);
                    datetime = datetime.AddSeconds(-timeZone.Seconds);
                    break;
                default:
                    break;
            }
            return datetime;
        }

        /// <summary>
        /// 时间数值
        /// </summary>
        public DateTime Value
        {
            get { return m_Value; }
            private set { m_Value = value; }
        }
        /// <summary>
        /// 时间的格式
        /// </summary>
        public ISO8601TimeFormats Format
        {
            get { return m_Format; }
            set { m_Format = value; }
        }

        /// <summary>
        /// 区域时差
        /// </summary>
        public TimeSpan TimeZone
        {
            get { return m_TimeZone; }
            set { m_TimeZone = value; }
        }
    }




}
