using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using Howell.Numeric;
using System.ComponentModel;

namespace Howell.Time
{
    /// <summary>
    /// DateTime type's extensions function class
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Get age by date of birth. The default reference date is today.
        /// </summary>
        /// <param name="dateOfBirth">Date of birth.</param>
        /// <returns>return the age calculate by date of birth. </returns>
        public static Int32 GetAge(this DateTime dateOfBirth)
        {
            return GetAge(dateOfBirth, DateTime.Today);
        }
        /// <summary>
        /// Calculate age by date of birth.
        /// </summary>
        /// <param name="dateOfBirth">Date of birth</param>
        /// <param name="referenceDate">Reference date which is used to calculate age.</param>
        /// <returns>return the age calculate by date of birth and reference date. </returns>
        public static Int32 GetAge(this DateTime dateOfBirth, DateTime referenceDate)
        {
            int years = referenceDate.Year - dateOfBirth.Year;
            if (referenceDate.Month < dateOfBirth.Month || (referenceDate.Month == dateOfBirth.Month && referenceDate.Day < dateOfBirth.Day)) --years;
            return years;
        }
        /// <summary>
        /// Get the days of the month.
        /// </summary>
        /// <param name="date">The date value.</param>
        /// <returns>return the total number of days of the month.</returns>
        public static Int32 GetDaysOfMonth(this DateTime date) 
        {            
            var nextMonth = date.AddMonths(1);
            return new DateTime(nextMonth.Year, nextMonth.Month, 1).AddDays(-1).Day;
        }
        /// <summary>
        /// Get the first day of the month.
        /// </summary>
        /// <param name="date">The date value.</param>
        /// <returns>return the date of first day of the month.</returns>
        public static DateTime GetFirstDayOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }       
        /// <summary>
        /// Get the first day of the month, and the day of week is equals to the parameter.
        /// </summary>
        /// <param name="date">The date value.</param>
        /// <param name="dayOfWeek">Day of week.</param>
        /// <returns>return the date of first day of the month, and the day of week is equals to the parameter.</returns>
        public static DateTime GetFirstDayOfMonth(this DateTime date, DayOfWeek dayOfWeek)
        {
            var dt = date.GetFirstDayOfMonth();
            while (dt.DayOfWeek != dayOfWeek)
            {
                dt = dt.AddDays(1);
            }
            return dt;
        }
        /// <summary>
        /// Get the last day of the month.
        /// </summary>
        /// <param name="date">The date value.</param>
        /// <returns>return the last day of the month.</returns>
        public static DateTime GetLastDayOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, GetDaysOfMonth(date));
        }
        /// <summary>
        /// Get the last day of the month, and the day of week is equals to the parameter.
        /// </summary>
        /// <param name="date">The date value.</param>
        /// <param name="dayOfWeek">Day of week.</param>
        /// <returns>return the last day of the month, and the day of week is equals to the parameter.</returns>
        public static DateTime GetLastDayOfMonth(this DateTime date, DayOfWeek dayOfWeek)
        {
            var dt = date.GetLastDayOfMonth();
            while (dt.DayOfWeek != dayOfWeek)
            {
                dt = dt.AddDays(-1);
            }
            return dt;
        }
        /// <summary>
        /// Indicates whether  this instance of DateTime is today.
        /// </summary>
        /// <param name="date">The date value.</param>
        /// <returns>If the date is today,then returns true, otherwise it returns false.</returns>
        public static bool IsToday(this DateTime date)
        {
            return (date.Date == DateTime.Today);
        }
        /// <summary>
        /// Indicates whether  this instance of DateTime is today.
        /// </summary>
        /// <param name="dateTimeOffset">The date time offset value.</param>
        /// <returns>If the date is today,then returns true, otherwise it returns false.</returns>
        public static bool IsToday(this DateTimeOffset dateTimeOffset)
        {
            return (dateTimeOffset.Date.IsToday());
        }
        /// <summary>
        /// Get the first day of this week
        /// </summary>
        /// <param name="date">The date value to demonstrate week value.</param>
        /// <returns>return the first day's date of this week.</returns>
        public static DateTime GetFirstDayOfWeek(this DateTime date)
        {
            return date.GetFirstDayOfWeek(null);
        }
        /// <summary>
        /// Get the first day of this week
        /// </summary>
        /// <param name="date">The date value to demonstrate week value.</param>
        /// <param name="cultureInfo"></param>
        /// <returns>return the first day's date of this week.</returns>
        public static DateTime GetFirstDayOfWeek(this DateTime date, CultureInfo cultureInfo)
        {            
            cultureInfo = (cultureInfo ?? CultureInfo.CurrentCulture);
            var firstDayOfWeek = cultureInfo.DateTimeFormat.FirstDayOfWeek;
            while (date.DayOfWeek != firstDayOfWeek) date = date.AddDays(-1);
            return date;
        }
        /// <summary>
        /// Get the last day of this week
        /// </summary>
        /// <param name="date">The date value to demonstrate week value.</param>
        /// <returns>return the last day's date of this week.</returns>
        public static DateTime GetLastDayOfWeek(this DateTime date)
        {
            return date.GetLastDayOfWeek(null);
        }
        /// <summary>
        /// Get the last day of this week
        /// </summary>
        /// <param name="date">The date value to demonstrate week value.</param>
        /// <param name="cultureInfo"></param>
        /// <returns>return the last day's date of this week.</returns>
        public static DateTime GetLastDayOfWeek(this DateTime date, CultureInfo cultureInfo)
        {
            return date.GetFirstDayOfWeek(cultureInfo).AddDays(6);
        }
        /// <summary>
        /// Get the week of year.
        /// </summary>
        /// <param name="date">The date value</param>
        /// <returns>return the week of year.</returns>
        public static int GetWeekOfYear(this DateTime date)
        {
            System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
            System.Globalization.CalendarWeekRule rule = dateInfo.CalendarWeekRule;
            DayOfWeek firstDayOfWeek = dateInfo.FirstDayOfWeek;
            return GetWeekOfYear(date, rule, firstDayOfWeek);
        }
        /// <summary>
        /// Get the week of year.
        /// </summary>
        /// <param name="date">The date value.</param>
        /// <param name="rule">The calendar week rule.</param>
        /// <returns>return the week of year.</returns>
        public static int GetWeekOfYear(this DateTime date, System.Globalization.CalendarWeekRule rule)
        {
            System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
            DayOfWeek firstDayOfWeek = dateInfo.FirstDayOfWeek;
            return GetWeekOfYear(date, rule, firstDayOfWeek);
        }
        /// <summary>
        /// Get the week of year.
        /// </summary>
        /// <param name="date">The date value.</param>
        /// <param name="firstDayOfWeek">The day of week for the first date in week.</param>
        /// <returns>return the week of year.</returns>
        public static int GetWeekOfYear(this DateTime date, DayOfWeek firstDayOfWeek)
        {
            System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
            System.Globalization.CalendarWeekRule rule = dateInfo.CalendarWeekRule;
            return GetWeekOfYear(date, rule, firstDayOfWeek);
        }
        /// <summary>
        /// Get the week of year.
        /// </summary>
        /// <param name="date">The date value.</param>
        /// <param name="rule">The calendar week rule.</param>
        /// <param name="firstDayOfWeek">The day of week for the first date in week.</param>
        /// <returns>return the week of year.</returns>
        public static int GetWeekOfYear(this DateTime date, System.Globalization.CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
        {
            System.Globalization.CultureInfo ciCurr = System.Globalization.CultureInfo.CurrentCulture;
            return ciCurr.Calendar.GetWeekOfYear(date, rule, firstDayOfWeek);
        }
        /// <summary>
        /// Indicates whether this instance of DateTime is weekday.
        /// </summary>
        /// <param name="date">Instance of date.</param>
        /// <returns>If the instance of date is weekday, then returns true, otherwise returns false.</returns>
        public static bool IsWeekDay(this DateTime date)
        {
            return !date.IsWeekend();
        }
        /// <summary>
        /// Indicates whether this instance of DateTime is weekend.
        /// </summary>
        /// <param name="date">Instance of date.</param>
        /// <returns>If the instance of date is weekend, then returns true, otherwise returns false.</returns>
        public static bool IsWeekend(this DateTime date)
        {
            return (date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday);
        }
        /// <summary>
        /// Indicates whether this instance of DateTime is in time range. The default Intervals is all closed.
        /// </summary>
        /// <param name="time">The instance of DateTime.</param>
        /// <param name="beginTime">The instance of begin DateTime.</param>
        /// <param name="endTime">The instance of end DateTime.</param>
        /// <returns>If the instance of date is in time range, then returns true, otherwise returns false.</returns>
        public static bool IsInRange(this DateTime time,DateTime beginTime,DateTime endTime)
        {
            return IsInRange(time, beginTime, endTime, Intervals.LeftClosed | Intervals.RightClosed);
        }
        /// <summary>
        /// Indicates whether this instance of DateTime is in time range.
        /// </summary>
        /// <param name="time">The instance of DateTime.</param>
        /// <param name="beginTime">The instance of begin DateTime.</param>
        /// <param name="endTime">The instance of end DateTime.</param>
        /// <param name="intervals">Intervals of DateTime range.</param>
        /// <returns>If the instance of date is in time range, then returns true, otherwise returns false.</returns>
        public static bool IsInRange(this DateTime time, DateTime beginTime, DateTime endTime, Intervals intervals)
        {            
            Boolean leftResult = false;
            Boolean rightResult = false;
            if ((intervals | Intervals.LeftClosed) != Intervals.None)
            {
                if(time >= beginTime)
                {
                    leftResult = true;
                }
            }
            else
            {
                if(time > beginTime)
                {
                    leftResult = true;
                }
            }
            if((intervals | Intervals.RightClosed) != Intervals.None)
            {
                if(time <= endTime)
                {
                    rightResult = true;
                }
            }
            else
            {
                if(time < endTime)
                {
                    rightResult = true;
                }
            }
            return (leftResult && rightResult);
        }
    }
}
