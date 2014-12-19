using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Howell.Time
{
    /// <summary>
    /// ISO8601 时间段格式
    /// </summary>
    public enum ISO8601TimeIntervalFormats
    {
        /// <summary>
        /// 跨度在同一年中的基本日历格式
        /// （开始和结束日期）
        /// YYYYMMDD/MMDD
        /// </summary>
        BasicDateByStartAndEndInSameYear = 0,
        /// <summary>
        /// 跨度在同一年中的扩展日历格式
        /// （开始和结束日期）
        /// YYYY-MM-DD/MM-DD
        /// </summary>
        ExtendedDateByStartAndEndInSameYear = 1,
        /// <summary>
        /// 基本日历和时间格式
        /// （开始和结束日期和时间）
        /// YYYYMMDDThhmmss/YYYYMMDDThhmmss
        /// </summary>
        BasicDateTimeByStartAndEnd = 2,
        /// <summary>
        /// 扩展日历和时间格式
        /// （开始和结束日期和时间）
        /// YYYY-MM-DDThh:mm:ss/YYYY-MM-DDThh:mm:ss
        /// </summary>
        ExtendedDateTimeByStartAndEnd = 3,
        /// <summary>
        /// 日期和时间格式（符号表示）
        /// （时间段）
        /// 例：跨度为2年10月15天10小时20分30秒
        ///   ：P2Y10M15DT10H20M30S
        /// </summary>
        SymbolicDateTimeByDuration = 4,
        /// <summary>
        /// 基本日历和时间格式
        /// （时间段）
        /// PYYYYMMDDThhmmss
        /// </summary>
        BasicDateTimeByDuration = 5,
        /// <summary>
        /// 扩展日历和时间格式
        /// （时间段）
        /// PYYYY-MM-DDThh:mm:ss
        /// </summary>
        ExtendedDateTimeByDuration = 6,
        /// <summary>
        /// 基本日历和时间格式
        /// （开始和持续时间）
        /// 例：1990年3月15日5时6分57秒开始持续2年10个月15天10小时20分钟30秒
        ///   ：19900315T050657/P2Y10M15DT10H20M30S
        /// </summary>
        BasicDateTimeByStartAndDuration = 7,
        /// <summary>
        /// 扩展日历和时间格式
        /// （开始和持续时间）
        /// 例：1990年3月15日5时6分57秒开始持续2年10个月15天10小时20分钟30秒
        ///   ：1990-03-15T05:06:57/P2Y10M15DT10H20M30S
        /// </summary>
        ExtendedDateTimeByStartAndDuration = 8,
        /// <summary>
        /// 基本日历和时间格式
        /// （持续时间和结束时间）
        /// 例：持续2年10个月15天10小时20分钟30秒至1990年3月15日5时6分57秒结束
        ///   ：P2Y10M15DT10H20M30S/19900315T050657
        /// </summary>
        BasicDateTimeByDurationAndEnd = 9,
        /// <summary>
        /// 扩展日历和时间格式
        /// （持续时间结束时间）
        /// 例：持续2年10个月15天10小时20分钟30秒至1990年3月15日5时6分57秒结束
        ///   ：P2Y10M15DT10H20M30S/1990-03-15T05:06:57
        /// </summary>
        ExtendedDateTimeByDurationAndEnd = 10,
        /// <summary>
        /// 扩展日期格式
        /// （时间段）
        /// 例：持续2年6个月
        ///   ：P0002-06
        /// </summary>
        ExtendedDateByDuration,
        /// <summary>
        /// 时间格式（符号表示）
        /// （时间段）
        /// 例：持续72小时
        ///   ：PT72H
        /// </summary>
        SymbolicHourByDuration
    }
    /// <summary>
    /// ISO8601标准的时长
    /// </summary>
    public struct ISO8601TimeInterval
    {
        /// <summary>
        /// 创建ISO8601TimeInterval对象
        /// </summary>
        /// <param name="interval">时间段</param>
        public ISO8601TimeInterval(TimeSpan interval)
            : this(DateTime.MinValue, interval, ISO8601TimeIntervalFormats.SymbolicDateTimeByDuration)
        { }
        /// <summary>
        /// 创建ISO8601TimeInterval对象
        /// </summary>
        /// <param name="interval">时间段</param>
        /// <param name="format">ISO8601TimeInterval格式</param>
        public ISO8601TimeInterval(TimeSpan interval, ISO8601TimeIntervalFormats format)
            : this(DateTime.MinValue, interval, format)
        { }
        /// <summary>
        /// 创建ISO8601TimeInterval对象
        /// 默认格式：YYYYMMDDThhmmss
        /// </summary>
        /// <param name="interval">时间段</param>
        /// <param name="end">结束时间</param>
        public ISO8601TimeInterval(TimeSpan interval, DateTime end)
            : this(end.Subtract(interval), interval, end, ISO8601TimeIntervalFormats.BasicDateTimeByDurationAndEnd)
        { }
        /// <summary>
        /// 创建ISO8601TimeInterval对象
        /// </summary>
        /// <param name="interval">时间段</param>
        /// <param name="end">结束时间</param>
        /// <param name="format">ISO8601TimeInterval格式</param>
        public ISO8601TimeInterval(TimeSpan interval, DateTime end, ISO8601TimeIntervalFormats format)
            : this(end.Subtract(interval), interval, end, format)
        { }
        /// <summary>
        /// 创建ISO8601TimeInterval对象
        /// 默认格式：YYYYMMDDThhmmss
        /// </summary>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        public ISO8601TimeInterval(DateTime start, DateTime end)
            : this(start, end.Subtract(start), end, ISO8601TimeIntervalFormats.BasicDateTimeByStartAndEnd)
        { }
        /// <summary>
        /// 创建ISO8601TimeInterval对象
        /// </summary>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="format">ISO8601TimeInterval格式</param>
        public ISO8601TimeInterval(DateTime start, DateTime end, ISO8601TimeIntervalFormats format)
            : this(start, end.Subtract(start), end, format)
        { }
        /// <summary>
        /// 创建ISO8601TimeInterval对象
        /// 默认格式：YYYYMMDDThhmmss
        /// </summary>
        /// <param name="start">开始时间</param>
        /// <param name="interval">时间段</param>
        public ISO8601TimeInterval(DateTime start, TimeSpan interval)
            : this(start, interval, start.Subtract(-interval), ISO8601TimeIntervalFormats.BasicDateTimeByStartAndDuration)
        { }
        /// <summary>
        /// 创建ISO8601TimeInterval对象
        /// </summary>
        /// <param name="start">开始时间</param>
        /// <param name="interval">时间段</param>
        /// <param name="format">ISO8601TimeInterval格式</param>
        public ISO8601TimeInterval(DateTime start, TimeSpan interval, ISO8601TimeIntervalFormats format)
            : this(start, interval, start.Subtract(-interval), format)
        { }
        /// <summary>
        /// 创建ISO8601TimeInterval对象
        /// </summary>
        /// <param name="start">开始时间</param>
        /// <param name="interval">时间段</param>
        /// <param name="end">结束时间</param>
        /// <param name="format">ISO8601TimeInterval格式</param>
        private ISO8601TimeInterval(DateTime start, TimeSpan interval, DateTime end, ISO8601TimeIntervalFormats format)
        {
            m_StartValue = start;
            m_IntervalValue = interval;
            m_EndValue = end;
            m_Format = format;
        }

        private static String[][] MyRegularExpressions = new string[][] {
            new String[]{"BasicDateByStartAndEndInSameYear","^((?<year>[0-9]{4})(?<startmonth>1[0-2]|0[1-9])(?<startday>3[0-1]|0[1-9]|[1-2][0-9])/(?<endmonth>1[0-2]|0[1-9])(?<endday>3[0-1]|0[1-9]|[1-2][0-9]))$","{0:D4}{1:D2}{2:D2}/{3:D2}{4:D2}"},
            new String[]{"ExtendedDateByStartAndEndInSameYear","^((?<year>[0-9]{4})-(?<startmonth>1[0-2]|0[1-9])-(?<startday>3[0-1]|0[1-9]|[1-2][0-9])/(?<endmonth>1[0-2]|0[1-9])-(?<endday>3[0-1]|0[1-9]|[1-2][0-9]))$","{0:D4}-{1:D2}-{2:D2}/{3:D2}-{4:D2}"},
            new String[]{"BasicDateTimeByStartAndEnd","^((?<startyear>[0-9]{4})(?<startmonth>1[0-2]|0[1-9])(?<startday>3[0-1]|0[1-9]|[1-2][0-9])T(?<starthour>2[0-3]|[01][0-9])(?<startminute>[0-5][0-9])(?<startsecond>[0-5][0-9])/(?<endyear>[0-9]{4})(?<endmonth>1[0-2]|0[1-9])(?<endday>3[0-1]|0[1-9]|[1-2][0-9])T(?<endhour>2[0-3]|[01][0-9])(?<endminute>[0-5][0-9])(?<endsecond>[0-5][0-9]))$","{0:D4}{1:D2}{2:D2}T{3:D2}{4:D2}{5:D2}/{6:D4}{7:D2}{8:D2}T{9:D2}{10:D2}{11:D2}"},
            new String[]{"ExtendedDateTimeByStartAndEnd","^((?<startyear>[0-9]{4})-(?<startmonth>1[0-2]|0[1-9])-(?<startday>3[0-1]|0[1-9]|[1-2][0-9])T(?<starthour>2[0-3]|[01][0-9]):(?<startminute>[0-5][0-9]):(?<startsecond>[0-5][0-9])/(?<endyear>[0-9]{4})-(?<endmonth>1[0-2]|0[1-9])-(?<endday>3[0-1]|0[1-9]|[1-2][0-9])T(?<endhour>2[0-3]|[01][0-9]):(?<endminute>[0-5][0-9]):(?<endsecond>[0-5][0-9]))$","{0:D4}-{1:D2}-{2:D2}T{3:D2}:{4:D2}:{5:D2}/{6:D4}-{7:D2}-{8:D2}T{9:D2}:{10:D2}:{11:D2}"},
            new String[]{"SymbolicDateTimeByDuration","^(P((?<year>[0-9]{1,4})Y)?((?<month>1[0-2]|[1-9])M)?((?<day>3[0-1]|[1-9]|[1-2][0-9])D)?(T((?<hour>2[0-3]|[1][0-9]|[0-9])H)?((?<minute>[1-5][0-9]|[0-9])M)?((?<second>[1-5][0-9]|[0-9])S)?)?)$","P{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}"},
            new String[]{"BasicDateTimeByDuration","^(P(?<year>[0-9]{4})(?<month>1[0-2]|0[1-9])(?<day>3[0-1]|0[1-9]|[1-2][0-9])T(?<hour>2[0-3]|[01][0-9])(?<minute>[0-5][0-9])(?<second>[0-5][0-9]))$","P{0:D4}{1:D2}{2:D2}T{3:D2}{4:D2}{5:D2}"},
            new String[]{"ExtendedDateTimeByDuration","^(P(?<year>[0-9]{4})-(?<month>1[0-2]|0[1-9])-(?<day>3[0-1]|0[1-9]|[1-2][0-9])T(?<hour>2[0-3]|[01][0-9]):(?<minute>[0-5][0-9]):(?<second>[0-5][0-9]))$","P{0:D4}-{1:D2}-{2:D2}T{3:D2}:{4:D2}:{5:D2}"},
            new String[]{"BasicDateTimeByStartAndDuration","^((?<startyear>[0-9]{4})(?<startmonth>1[0-2]|0[1-9])(?<startday>3[0-1]|0[1-9]|[1-2][0-9])T(?<starthour>2[0-3]|[01][0-9])(?<startminute>[0-5][0-9])(?<startsecond>[0-5][0-9])/P((?<year>[0-9]{1,4})Y)?((?<month>1[0-2]|[1-9])M)?((?<day>3[0-1]|[1-9]|[1-2][0-9])D)?(T((?<hour>2[0-3]|[1][0-9]|[0-9])H)?((?<minute>[1-5][0-9]|[0-9])M)?((?<second>[1-5][0-9]|[0-9])S)?)?)$","{0:D4}{1:D2}{2:D2}T{3:D2}{4:D2}{5:D2}/P{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}"},
            new String[]{"ExtendedDateTimeByStartAndDuration","^((?<startyear>[0-9]{4})-(?<startmonth>1[0-2]|0[1-9])-(?<startday>3[0-1]|0[1-9]|[1-2][0-9])T(?<starthour>2[0-3]|[01][0-9]):(?<startminute>[0-5][0-9]):(?<startsecond>[0-5][0-9])/P((?<year>[0-9]{1,4})Y)?((?<month>1[0-2]|[1-9])M)?((?<day>3[0-1]|[1-9]|[1-2][0-9])D)?(T((?<hour>2[0-3]|[1][0-9]|[0-9])H)?((?<minute>[1-5][0-9]|[0-9])M)?((?<second>[1-5][0-9]|[0-9])S)?)?)$","{0:D4}-{1:D2}-{2:D2}T{3:D2}:{4:D2}:{5:D2}/P{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}"},
            new String[]{"BasicDateTimeByDurationAndEnd","^(P((?<year>[0-9]{1,4})Y)?((?<month>1[0-2]|[1-9])M)?((?<day>3[0-1]|[1-9]|[1-2][0-9])D)?(T((?<hour>2[0-3]|[1][0-9]|[0-9])H)?((?<minute>[1-5][0-9]|[0-9])M)?((?<second>[1-5][0-9]|[0-9])S)?)?/(?<endyear>[0-9]{4})(?<endmonth>1[0-2]|0[1-9])(?<endday>3[0-1]|0[1-9]|[1-2][0-9])T(?<endhour>2[0-3]|[01][0-9])(?<endminute>[0-5][0-9])(?<endsecond>[0-5][0-9]))$","P{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}/{13:D4}{14:D2}{15:D2}T{16:D2}{17:D2}{18:D2}"},
            new String[]{"ExtendedDateTimeByDurationAndEnd","^(P((?<year>[0-9]{1,4})Y)?((?<month>1[0-2]|[1-9])M)?((?<day>3[0-1]|[1-9]|[1-2][0-9])D)?(T((?<hour>2[0-3]|[1][0-9]|[0-9])H)?((?<minute>[1-5][0-9]|[0-9])M)?((?<second>[1-5][0-9]|[0-9])S)?)?/(?<endyear>[0-9]{4})-(?<endmonth>1[0-2]|0[1-9])-(?<endday>3[0-1]|0[1-9]|[1-2][0-9])T(?<endhour>2[0-3]|[01][0-9]):(?<endminute>[0-5][0-9]):(?<endsecond>[0-5][0-9]))$","P{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}/{13:D4}-{14:D2}-{15:D2}T{16:D2}:{17:D2}:{18:D2}"},
            new String[]{"ExtendedDateByDuration","^(P(?<year>[0-9]{4})-(?<month>1[0-2]|0[1-9])?)$","P{0:D4}-{1:D2}"},
            new String[]{"SymbolicHourByDuration","^(PT(?<hour>[0-9]{1,})H)$","PT{0}H"}
        };


        /// <summary>
        /// 解析时间段字符串
        /// </summary>
        /// <param name="s">时间段字符串</param>
        /// <returns>ISO8601TimeInterval实例</returns>
        public static ISO8601TimeInterval Parse(String s)
        {
            ISO8601TimeInterval datetime;
            if (true == TryParse(s, out datetime)) return datetime;
            throw new ArgumentException("Invalid argument,illegal TimeInterval format.");
        }
        /// <summary>
        /// 尝试解析时间段字符串
        /// </summary>
        /// <param name="s">时间段字符串</param>
        /// <param name="timeInterval">输出ISO8601TimeInterval实例</param>
        /// <returns>解析成功返回true,失败返回false.</returns>
        public static Boolean TryParse(String s, out ISO8601TimeInterval timeInterval)
        {
            if (s == null || s == String.Empty)
                throw new ArgumentNullException("Argument s is null");
            for (Int32 i = 0; i < MyRegularExpressions.GetLength(0); ++i)
            {
                Regex reg = new Regex(MyRegularExpressions[i][1]);
                Match m = reg.Match(s);
                if (m.Success == true)
                {
                    DateTime start;
                    DateTime end;
                    TimeSpan interval = Match2DateTime(m, (ISO8601TimeIntervalFormats)i, out start, out end);
                    timeInterval = new ISO8601TimeInterval(start, interval, end, (ISO8601TimeIntervalFormats)i);
                    return true;
                }
            };
            timeInterval = default(ISO8601TimeInterval);
            return false;
        }


        /// <summary>
        /// 生成由正则表达式解析的数据
        /// </summary>
        /// <param name="m">正则表达式解析类</param>
        /// <param name="format">ISO8601TimeInterval格式</param>
        /// <param name="start">所需要时间段的开始时间</param>
        /// <param name="end">所需要时间段的结束时间</param>
        /// <returns>时间段</returns>
        internal static TimeSpan Match2DateTime(Match m, ISO8601TimeIntervalFormats format, out DateTime start, out DateTime end)
        {
            TimeSpan interval = default(TimeSpan);
            switch (format)
            {
                case ISO8601TimeIntervalFormats.BasicDateByStartAndEndInSameYear:
                case ISO8601TimeIntervalFormats.ExtendedDateByStartAndEndInSameYear:
                    start = new DateTime(Convert.ToInt32(m.Groups["year"].Value), Convert.ToInt32(m.Groups["startmonth"].Value), Convert.ToInt32(m.Groups["startday"].Value));
                    end = new DateTime(Convert.ToInt32(m.Groups["year"].Value), Convert.ToInt32(m.Groups["endmonth"].Value), Convert.ToInt32(m.Groups["endday"].Value));
                    interval = ISO8601TimeIntervalHelper.GetInterval(start, end);
                    break;
                case ISO8601TimeIntervalFormats.BasicDateTimeByStartAndEnd:
                case ISO8601TimeIntervalFormats.ExtendedDateTimeByStartAndEnd:
                    start = new DateTime
                    (
                        Convert.ToInt32(m.Groups["startyear"].Value),
                        Convert.ToInt32(m.Groups["startmonth"].Value),
                        Convert.ToInt32(m.Groups["startday"].Value),
                        Convert.ToInt32(m.Groups["starthour"].Value),
                        Convert.ToInt32(m.Groups["startminute"].Value),
                        Convert.ToInt32(m.Groups["startsecond"].Value)
                    );
                    end = new DateTime
                    (
                        Convert.ToInt32(m.Groups["endyear"].Value),
                        Convert.ToInt32(m.Groups["endmonth"].Value),
                        Convert.ToInt32(m.Groups["endday"].Value),
                        Convert.ToInt32(m.Groups["endhour"].Value),
                        Convert.ToInt32(m.Groups["endminute"].Value),
                        Convert.ToInt32(m.Groups["endsecond"].Value)
                    );
                    interval = ISO8601TimeIntervalHelper.GetInterval(start, end);
                    break;
                case ISO8601TimeIntervalFormats.SymbolicDateTimeByDuration:
                case ISO8601TimeIntervalFormats.BasicDateTimeByDuration:
                case ISO8601TimeIntervalFormats.ExtendedDateTimeByDuration:
                    interval = ISO8601TimeIntervalHelper.GetInterval
                    (
                        m.Groups["year"].Value,
                        m.Groups["month"].Value,
                        m.Groups["day"].Value,
                        m.Groups["hour"].Value,
                        m.Groups["minute"].Value,
                        m.Groups["second"].Value
                    );
                    start = default(DateTime);
                    end = default(DateTime);
                    break;
                case ISO8601TimeIntervalFormats.BasicDateTimeByStartAndDuration:
                case ISO8601TimeIntervalFormats.ExtendedDateTimeByStartAndDuration:
                    start = new DateTime
                    (
                        Convert.ToInt32(m.Groups["startyear"].Value),
                        Convert.ToInt32(m.Groups["startmonth"].Value),
                        Convert.ToInt32(m.Groups["startday"].Value),
                        Convert.ToInt32(m.Groups["starthour"].Value),
                        Convert.ToInt32(m.Groups["startminute"].Value),
                        Convert.ToInt32(m.Groups["startsecond"].Value)
                    );
                    interval = ISO8601TimeIntervalHelper.GetInterval
                    (
                       m.Groups["year"].Value,
                       m.Groups["month"].Value,
                       m.Groups["day"].Value,
                       m.Groups["hour"].Value,
                       m.Groups["minute"].Value,
                       m.Groups["second"].Value
                    );
                    end = ISO8601TimeIntervalHelper.GetEndDate(start, interval);
                    break;
                case ISO8601TimeIntervalFormats.BasicDateTimeByDurationAndEnd:
                case ISO8601TimeIntervalFormats.ExtendedDateTimeByDurationAndEnd:
                    interval = ISO8601TimeIntervalHelper.GetInterval
                    (
                       m.Groups["year"].Value,
                       m.Groups["month"].Value,
                       m.Groups["day"].Value,
                       m.Groups["hour"].Value,
                       m.Groups["minute"].Value,
                       m.Groups["second"].Value
                    );
                    end = new DateTime
                    (
                        Convert.ToInt32(m.Groups["endyear"].Value),
                        Convert.ToInt32(m.Groups["endmonth"].Value),
                        Convert.ToInt32(m.Groups["endday"].Value),
                        Convert.ToInt32(m.Groups["endhour"].Value),
                        Convert.ToInt32(m.Groups["endminute"].Value),
                        Convert.ToInt32(m.Groups["endsecond"].Value)
                    );
                    start = ISO8601TimeIntervalHelper.GetStartDate(interval, end);
                    break;
                case ISO8601TimeIntervalFormats.ExtendedDateByDuration:
                    interval = ISO8601TimeIntervalHelper.GetInterval
                    (
                        m.Groups["year"].Value,
                        m.Groups["month"].Value,
                        m.Groups["day"].Value
                    );
                    start = default(DateTime);
                    end = default(DateTime);
                    break;
                case ISO8601TimeIntervalFormats.SymbolicHourByDuration:
                    interval = ISO8601TimeIntervalHelper.GetNewInterval
                    (
                        m.Groups["hour"].Value,
                        m.Groups["minute"].Value,
                        m.Groups["second"].Value
                    );
                    start = default(DateTime);
                    end = default(DateTime);
                    break;
                default:
                    start = default(DateTime);
                    end = default(DateTime);
                    break;
            }

            return interval;
        }

        /// <summary>
        /// 转换为ISO8601规范的时间段字符串 默认格式YYYYMMDD
        /// </summary>
        /// <returns>返回ISO8601规范的时间段字符串</returns>
        public override string ToString()
        {
            Int32 no = (Int32)Format;
            string signTime = string.Empty;
            string signYear = string.Empty;
            string signMonth = string.Empty;
            string signDay = string.Empty;
            string signHour = string.Empty;
            string signMinute = string.Empty;
            string signSecond = string.Empty;
            int intervalYear = ISO8601TimeIntervalHelper.GetIntervalYear(IntervalValue);
            int intervalMonth = ISO8601TimeIntervalHelper.GetIntervalMonth(IntervalValue);
            int intervalDay = ISO8601TimeIntervalHelper.GetIntervalDay(IntervalValue);
            bool allNull = true;
            if (intervalYear != 0)
            {
                signYear = "Y";
            }
            if (intervalMonth != 0)
            {
                signMonth = "M";
            }
            if (intervalDay != 0)
            {
                signDay = "D";
            }
            if (IntervalValue.Hours != 0)
            {
                signHour = "H";
                allNull = false;
            }
            if (IntervalValue.Minutes != 0)
            {
                signMinute = "M";
                allNull = false;
            }
            if (IntervalValue.Seconds != 0)
            {
                signSecond = "S";
                allNull = false;
            }
            if (false == allNull)
                signTime = "T";
            switch (Format)
            {
                case ISO8601TimeIntervalFormats.BasicDateByStartAndEndInSameYear:
                    return String.Format(MyRegularExpressions[no][2], StartValue.Year, StartValue.Month, StartValue.Day, EndValue.Month, EndValue.Day);
                case ISO8601TimeIntervalFormats.ExtendedDateByStartAndEndInSameYear:
                    return String.Format(MyRegularExpressions[no][2], StartValue.Year, StartValue.Month, StartValue.Day, EndValue.Month, EndValue.Day);
                case ISO8601TimeIntervalFormats.BasicDateTimeByStartAndEnd:
                    return String.Format(MyRegularExpressions[no][2], StartValue.Year, StartValue.Month, StartValue.Day, StartValue.Hour, StartValue.Minute, StartValue.Second, EndValue.Year, EndValue.Month, EndValue.Day, EndValue.Hour, EndValue.Minute, EndValue.Second);
                case ISO8601TimeIntervalFormats.ExtendedDateTimeByStartAndEnd:
                    return String.Format(MyRegularExpressions[no][2], StartValue.Year, StartValue.Month, StartValue.Day, StartValue.Hour, StartValue.Minute, StartValue.Second, EndValue.Year, EndValue.Month, EndValue.Day, EndValue.Hour, EndValue.Minute, EndValue.Second);
                case ISO8601TimeIntervalFormats.SymbolicDateTimeByDuration:

                    return String.Format(MyRegularExpressions[no][2], ISO8601TimeIntervalHelper.ZeroToNull(intervalYear), signYear, ISO8601TimeIntervalHelper.ZeroToNull(intervalMonth), signMonth, ISO8601TimeIntervalHelper.ZeroToNull(intervalDay), signDay, signTime, ISO8601TimeIntervalHelper.ZeroToNull(IntervalValue.Hours), signHour, ISO8601TimeIntervalHelper.ZeroToNull(IntervalValue.Minutes), signMinute, ISO8601TimeIntervalHelper.ZeroToNull(IntervalValue.Seconds), signSecond);

                case ISO8601TimeIntervalFormats.BasicDateTimeByDuration:
                    return String.Format(MyRegularExpressions[no][2], intervalYear, intervalMonth, intervalDay, IntervalValue.Hours, IntervalValue.Minutes, IntervalValue.Seconds);
                case ISO8601TimeIntervalFormats.ExtendedDateTimeByDuration:
                    return String.Format(MyRegularExpressions[no][2], intervalYear, intervalMonth, intervalDay, IntervalValue.Hours, IntervalValue.Minutes, IntervalValue.Seconds);

                case ISO8601TimeIntervalFormats.BasicDateTimeByStartAndDuration:

                    return String.Format(MyRegularExpressions[no][2], StartValue.Year, StartValue.Month, StartValue.Day, StartValue.Hour, StartValue.Minute, StartValue.Second, ISO8601TimeIntervalHelper.ZeroToNull(intervalYear), signYear, ISO8601TimeIntervalHelper.ZeroToNull(intervalMonth), signMonth, ISO8601TimeIntervalHelper.ZeroToNull(intervalDay), signDay, signTime, ISO8601TimeIntervalHelper.ZeroToNull(IntervalValue.Hours), signHour, ISO8601TimeIntervalHelper.ZeroToNull(IntervalValue.Minutes), signMinute, ISO8601TimeIntervalHelper.ZeroToNull(IntervalValue.Seconds), signSecond);
                case ISO8601TimeIntervalFormats.ExtendedDateTimeByStartAndDuration:
                    return String.Format(MyRegularExpressions[no][2], StartValue.Year, StartValue.Month, StartValue.Day, StartValue.Hour, StartValue.Minute, StartValue.Second, ISO8601TimeIntervalHelper.ZeroToNull(intervalYear), signYear, ISO8601TimeIntervalHelper.ZeroToNull(intervalMonth), signMonth, ISO8601TimeIntervalHelper.ZeroToNull(intervalDay), signDay, signTime, ISO8601TimeIntervalHelper.ZeroToNull(IntervalValue.Hours), signHour, ISO8601TimeIntervalHelper.ZeroToNull(IntervalValue.Minutes), signMinute, ISO8601TimeIntervalHelper.ZeroToNull(IntervalValue.Seconds), signSecond);
                case ISO8601TimeIntervalFormats.BasicDateTimeByDurationAndEnd:
                    return String.Format(MyRegularExpressions[no][2], ISO8601TimeIntervalHelper.ZeroToNull(intervalYear), signYear, ISO8601TimeIntervalHelper.ZeroToNull(intervalMonth), signMonth, ISO8601TimeIntervalHelper.ZeroToNull(intervalDay), signDay, signTime, ISO8601TimeIntervalHelper.ZeroToNull(IntervalValue.Hours), signHour, ISO8601TimeIntervalHelper.ZeroToNull(IntervalValue.Minutes), signMinute, ISO8601TimeIntervalHelper.ZeroToNull(IntervalValue.Seconds), signSecond, EndValue.Year, EndValue.Month, EndValue.Day, EndValue.Hour, EndValue.Minute, EndValue.Second);
                case ISO8601TimeIntervalFormats.ExtendedDateTimeByDurationAndEnd:
                    return String.Format(MyRegularExpressions[no][2], ISO8601TimeIntervalHelper.ZeroToNull(intervalYear), signYear, ISO8601TimeIntervalHelper.ZeroToNull(intervalMonth), signMonth, ISO8601TimeIntervalHelper.ZeroToNull(intervalDay), signDay, signTime, ISO8601TimeIntervalHelper.ZeroToNull(IntervalValue.Hours), signHour, ISO8601TimeIntervalHelper.ZeroToNull(IntervalValue.Minutes), signMinute, ISO8601TimeIntervalHelper.ZeroToNull(IntervalValue.Seconds), signSecond, EndValue.Year, EndValue.Month, EndValue.Day, EndValue.Hour, EndValue.Minute, EndValue.Second);
                case ISO8601TimeIntervalFormats.ExtendedDateByDuration:
                    return String.Format(MyRegularExpressions[no][2], intervalYear, intervalMonth);
                case ISO8601TimeIntervalFormats.SymbolicHourByDuration:
                    string strHour = "";
                    int hour = IntervalValue.Hours;
                    if (IntervalValue.Days > 0)
                        hour = IntervalValue.Days * 24 + IntervalValue.Hours;
                    if (hour > 0)
                        strHour = String.Format("{0}", hour);
                    return String.Format(MyRegularExpressions[no][2], strHour);
                default:
                    return "";
            }
        }

        /// <summary>
        /// 开始时间+标准格式的持续时间
        /// </summary>
        /// <param name="start">开始时间</param>
        /// <param name="interval">时间段</param>
        /// <returns>带开始时间的时间段</returns>
        public static ISO8601TimeInterval operator +(DateTime start, ISO8601TimeInterval interval)
        {
            return new ISO8601TimeInterval(start, interval.IntervalValue);
        }

        /// <summary>
        /// 结束时间-标准格式的持续时间
        /// </summary>
        /// <param name="end">结束时间</param>
        /// <param name="interval">时间段</param>
        /// <returns>带结束时间的时间段</returns>
        public static ISO8601TimeInterval operator -(DateTime end, ISO8601TimeInterval interval)
        {
            return new ISO8601TimeInterval(interval.IntervalValue, end);
        }

        private DateTime m_StartValue;
        /// <summary>
        /// 起始日期
        /// </summary>
        public DateTime StartValue
        {
            get { return m_StartValue; }
            set { m_StartValue = value; }
        }
        private DateTime m_EndValue;
        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime EndValue
        {
            get { return m_EndValue; }
            set { m_EndValue = value; }
        }
        private TimeSpan m_IntervalValue;
        /// <summary>
        /// 间隔时间
        /// </summary>
        public TimeSpan IntervalValue
        {
            get { return m_IntervalValue; }
            set { m_IntervalValue = value; }
        }

        ISO8601TimeIntervalFormats m_Format;

        /// <summary>
        /// 时间间隔的格式
        /// </summary>
        public ISO8601TimeIntervalFormats Format
        {
            get { return m_Format; }
            set { m_Format = value; }
        }
    }
}
