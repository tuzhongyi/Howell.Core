using System;
using System.Collections.Generic;
using System.Text;

namespace Howell.Time
{
    /// <summary>
    /// ISO8601规范的日期时间数据工具
    /// </summary>
    /// <remarks>
    /// ISO8601 标准中 每周的第一天是Mon,最后一天是Sun
    /// Mon=1, Sun = 7    
    /// </remarks>
    public static class ISO8601DateTimeHelper
    {
        private const DayOfWeek FirstDayOfWeek = DayOfWeek.Monday;
        private const DayOfWeek LastDayOfWeek = DayOfWeek.Sunday;
        /// <summary>
        /// 将星期几转换为整型
        /// </summary>
        /// <param name="dayofweek">星期几</param>
        /// <returns>返回1-7的整型1-Mon 2-Sun</returns>
        public static Int32 GetDayOfWeek(DayOfWeek dayofweek)
        {
            return Convert.ToInt32(dayofweek) == 0 ? 7 : Convert.ToInt32(dayofweek);
        }
        /// <summary>
        /// 是否是一周的第一天
        /// </summary>
        /// <param name="dayofweek">星期几</param>
        /// <returns>如果是一周的第一天返回true,否则返回false.</returns>
        public static Boolean IsFirstDayOfWeek(DayOfWeek dayofweek)
        {
            return dayofweek == FirstDayOfWeek;
        }
        /// <summary>
        /// 是否是一周的最后一天
        /// </summary>
        /// <param name="dayofweek">星期几</param>
        /// <returns>如果是一周的最后一天返回true,否则返回false.</returns>
        public static Boolean IsLastDayOfWeek(DayOfWeek dayofweek)
        {
            return dayofweek == LastDayOfWeek;
        }
        /// <summary>
        /// 获取一年中的第几周 1-53
        /// </summary>
        /// <param name="datetime">日期时间数据</param>
        /// <returns>返回一年中的第几周1-53</returns>
        public static Int32 GetWeekOfYear(DateTime datetime)
        {
            DateTime firstdayofyear = new DateTime(datetime.Year, 1, 1);
            Int32 firstdayofyearweekday = GetDayOfWeek(firstdayofyear.DayOfWeek);
            Int32 week = Convert.ToInt32(System.Math.Floor((double)(datetime.DayOfYear + firstdayofyearweekday -2) / (double)7)) + 1;
            return week;
        }
        /// <summary>
        /// 获取一年的中第几天
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="weekofyear">第几周</param>
        /// <param name="dayofweek">星期几</param>
        /// <returns>一年的中第几天</returns>
        public static Int32 GetDayOfYearByWeek(Int32 year, Int32 weekofyear, DayOfWeek dayofweek)
        {
            DateTime firstdayofyear = new DateTime(year, 1, 1);
            //1-Monday ... 7-Sunday
            Int32 firstdayofweekday = GetDayOfWeek(firstdayofyear.DayOfWeek);
            Int32 intdayofweek = GetDayOfWeek(dayofweek);
            Int32 dayofyear = (weekofyear - 1) * 7 + intdayofweek - firstdayofweekday +1;
            return dayofyear;
        }        
        /// <summary>
        /// 获取一年的中第几天
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="weekofyear">第几周</param>
        /// <param name="dayofweek">星期几 1-Monday 7-Sunday</param>
        /// <returns>一年的中第几天</returns>
        public static Int32 GetDayOfYearByWeek(Int32 year, Int32 weekofyear, Int32 dayofweek)
        {
            DateTime firstdayofyear = new DateTime(year, 1, 1);
            //1-Monday ... 7-Sunday
            Int32 firstdayofweekday = GetDayOfWeek(firstdayofyear.DayOfWeek);
            Int32 dayofyear = (weekofyear - 1) * 7 + dayofweek - firstdayofweekday + 1;

            return dayofyear;
        }
    }
}
