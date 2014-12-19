using System;
using System.Collections.Generic;
using System.Text;

namespace Howell.Time
{
    /// <summary>
    /// ISO8601TimeInterval助手
    /// </summary>
    public static class ISO8601TimeIntervalHelper
    {
        /// <summary>
        /// 根据给定的日期数据字符串返回一个时间段对象
        /// </summary>
        /// <param name="hour">小时数据</param>
        /// <param name="minute">分钟数据</param>
        /// <param name="second">秒钟数据</param>
        /// <returns>时间段对象</returns>
        public static TimeSpan GetNewInterval(string hour, string minute, string second)
        {
            return new TimeSpan
            (
                Convert.ToInt32(NullStringAddZero(hour)),
                Convert.ToInt32(NullStringAddZero(minute)),
                Convert.ToInt32(NullStringAddZero(second))
            );
        }
        /// <summary>
        /// 直接生成获得时间段
        /// </summary>
        /// <param name="year">年数据</param>
        /// <param name="month">月数据</param>
        /// <param name="day">天数据</param>
        /// <returns>时间段</returns>
        public static TimeSpan GetInterval(string year, string month, string day)
        {
            return GetInterval(year, month, day, "", "", "");
        }
        /// <summary>
        /// 直接生成获得时间段
        /// </summary>
        /// <param name="year">年数据</param>
        /// <param name="month">月数据</param>
        /// <param name="day">天数据</param>
        /// <param name="hour">小时数据</param>
        /// <param name="minute">分钟数据</param>
        /// <param name="second">秒数据</param>
        /// <returns>时间段</returns>
        public static TimeSpan GetInterval(string year, string month, string day, string hour, string minute, string second)
        {

            DateTime datetime = DateTime.MinValue;

            datetime = datetime.AddYears(Convert.ToInt32(NullStringAddZero(year)));

            datetime = datetime.AddMonths(Convert.ToInt32(NullStringAddZero(month)));

            datetime = datetime.AddDays(Convert.ToInt32(NullStringAddZero(day)));

            datetime = datetime.AddHours(Convert.ToInt32(NullStringAddZero(hour)));

            datetime = datetime.AddMinutes(Convert.ToInt32(NullStringAddZero(minute)));

            datetime = datetime.AddSeconds(Convert.ToInt32(NullStringAddZero(second)));

            return datetime.Subtract(DateTime.MinValue);
        }
        /// <summary>
        /// 为字符串补零
        /// 用于生成日期或时间格式时，转换字符串为空时
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>"0"或原来的字符串</returns>
        public static string NullStringAddZero(string str)
        {
            if (str == "" || str == null || str == string.Empty)
                return "0";
            return str;
        }
        /// <summary>
        /// 把0转换为空字符串
        /// 用于对外输出时，不显示的数据
        /// </summary>
        /// <param name="isZero">是否为0的字符串</param>
        /// <returns>数据或空字符串</returns>
        public static string ZeroToNull(string isZero)
        {
            return isZero == "0" ? "" : isZero;
        }
        /// <summary>
        /// 把0转换为空字符串
        /// 用于对外输出时，不显示的数据
        /// </summary>
        /// <param name="isZero">是否为0</param>
        /// <returns>数据或空字符串</returns>
        public static string ZeroToNull(int isZero)
        {
            return isZero == 0 ? "" : String.Format("{0}", isZero);
        }

        /// <summary>
        /// 从开始和结束的日期获得时间段
        /// </summary>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <returns>时间段</returns>
        public static TimeSpan GetInterval(DateTime start, DateTime end)
        {
            return end.Subtract(start);
        }
        /// <summary>
        /// 从结束时间和时间段获得开始时间
        /// </summary>
        /// <param name="interval">时间段</param>
        /// <param name="end">结束时间</param>
        /// <returns>开始日期</returns>
        public static DateTime GetStartDate(TimeSpan interval, DateTime end)
        {
            return end.Subtract(interval);
        }
        /// <summary>
        /// 从开始时间和时间段获得结束时间
        /// </summary>
        /// <param name="start">开始时间</param>
        /// <param name="interval">时间段</param>
        /// <returns>结束日期</returns>
        public static DateTime GetEndDate(DateTime start, TimeSpan interval)
        {
            return start.Subtract(-interval);
        }
        /// <summary>
        /// 把时间段转换成日期的格式
        /// </summary>
        /// <param name="interval">时间段</param>
        /// <returns>日期#这里的日期多了1年1月1日</returns>
        private static DateTime TimeToDate(TimeSpan interval)
        {
            return DateTime.MinValue.Subtract(-interval);
        }
        /// <summary>
        /// 提取时间段有几年
        /// </summary>
        /// <param name="interval">时间段</param>
        /// <returns>年数据</returns>
        public static int GetIntervalYear(TimeSpan interval)
        {
            return TimeToDate(interval).Year-1;
        }
        /// <summary>
        /// 提取时间段有几年中的几个月
        /// </summary>
        /// <param name="interval">时间段</param>
        /// <returns>月数据</returns>
        public static int GetIntervalMonth(TimeSpan interval)
        {
            return TimeToDate(interval).Month-1;
        }
        /// <summary>
        /// 提取时间段有几年中的几个月的零几天
        /// </summary>
        /// <param name="interval">时间段</param>
        /// <returns>天数据</returns>
        public static int GetIntervalDay(TimeSpan interval)
        {
            return TimeToDate(interval).Day-1;
        }
    }
}
