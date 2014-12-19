using System;
using System.Collections.Generic;
using System.Text;
using Howell.Time;
using System.Text.RegularExpressions;
using Howell.Conditions;
using System.Xml.Serialization;
using Howell.Numeric;

namespace Howell.Schedules
{
    /// <summary>
    /// 计划时间, 注意PlanTime不等同于DateTime不可以直接来比较大小，必须转换为DateTime以后才可以。
    /// </summary>
    [Serializable()]
    public struct PlanTime : IXmlSerializable, IEquatable<PlanTime>
    {
        private const String DateRegularExpression = @"^(?:(?!0000)[0-9]{4}-(?:(?:0[1-9]|1[0-2])-(?:0[1-9]|1[0-9]|2[0-8])|(?:0[13-9]|1[0-2])-(?:29|30)|(?:0[13578]|1[02])-31)|(?:[0-9]{2}(?:0[48]|[2468][048]|[13579][26])|(?:0[48]|[2468][048]|[13579][26])00)-02-29)$";
        private const String TimeRegularExpression = @"^(([0-1][0-9])|([1-2][0-3])):([0-5][0-9]):([0-5][0-9])$";
        private const String DateTimeRegularExpression = @"^((?:(?!0000)[0-9]{4}-(?:(?:0[1-9]|1[0-2])-(?:0[1-9]|1[0-9]|2[0-8])|(?:0[13-9]|1[0-2])-(?:29|30)|(?:0[13578]|1[02])-31)|(?:[0-9]{2}(?:0[48]|[2468][048]|[13579][26])|(?:0[48]|[2468][048]|[13579][26])00)-02-29)\s(([0-1][0-9])|([1-2][0-3])):([0-5][0-9]):([0-5][0-9]))$";
        private const String WeeklyTimeRegularExpression = @"^((([0-1][0-9])|([1-2][0-3])):([0-5][0-9]):([0-5][0-9])\s(Sunday|Monday|Tuesday|Wednesday|Thursday|Friday|Saturday))$";

        private Nullable<Int32> m_Year;
        private Nullable<Int32> m_Month;
        private Nullable<Int32> m_Day;
        private Nullable<DayOfWeek> m_DayOfWeek;
        private Int32 m_Hour;
        private Int32 m_Minute;
        private Int32 m_Second;

        /// <summary>
        /// 创建日重复的PlanTime对象
        /// </summary>
        /// <param name="hour">时</param>
        /// <param name="minute">分</param>
        /// <param name="second">秒</param>
        public PlanTime(Int32 hour, Int32 minute, Int32 second)
            : this(null, null, null, hour, minute, second, null)
        {
        }
        /// <summary>
        /// 创建周重复的PlanTime对象
        /// </summary>
        /// <param name="hour">时</param>
        /// <param name="minute">分</param>
        /// <param name="second">秒</param>
        /// <param name="dayOfWeek">星期几</param>
        public PlanTime(Int32 hour, Int32 minute, Int32 second, DayOfWeek dayOfWeek)
            : this(null, null, null, hour, minute, second, dayOfWeek)
        {
        }
        /// <summary>
        /// 创建一次性的PlanTime对象
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="day">日</param>
        /// <param name="hour">时</param>
        /// <param name="minute">分</param>
        /// <param name="second">秒</param>
        public PlanTime(Int32 year, Int32 month, Int32 day, Int32 hour, Int32 minute, Int32 second)
            : this(year, month, day, hour, minute, second, null)
        {
        }
        /// <summary>
        /// 创建一次性的PlanTime对象
        /// </summary>
        /// <param name="time">一次性计划的触发时间</param>
        public PlanTime(DateTime time)
            : this(time.Year, time.Month, time.Day, time.Hour, time.Minute, time.Second, null)
        {
        }
        /// <summary>
        /// 拷贝构造
        /// </summary>
        /// <param name="time"></param>
        internal PlanTime(PlanTime time)
            : this(time.Year, time.Month, time.Day, time.Hour, time.Minute, time.Second, time.DayOfWeek)
        {
        }
        /// <summary>
        /// 内部构造函数 
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="day">日</param>
        /// <param name="hour">时</param>
        /// <param name="minute">分</param>
        /// <param name="second">秒</param>
        /// <param name="dayOfWeek">星期几</param>
        internal PlanTime(Nullable<Int32> year, Nullable<Int32> month, Nullable<Int32> day, Int32 hour, Int32 minute, Int32 second, Nullable<DayOfWeek> dayOfWeek)
        {
            if (year != null || month != null || day != null)
            {
                Condition.Requires<Nullable<Int32>>(year, "year").IsNotNull().IsInRange(1, 9999);
                Condition.Requires<Nullable<Int32>>(month, "month").IsNotNull().IsInRange(1, 12);
                Condition.Requires<Nullable<Int32>>(day, "day").IsNotNull().IsInRange(1, DateTime.DaysInMonth((Int32)year, (Int32)month));
            }
            Condition.Requires<Int32>(hour, "hour").IsInRange(0, 23);
            Condition.Requires<Int32>(minute, "minute").IsInRange(0, 59);
            Condition.Requires<Int32>(second, "second").IsInRange(0, 59);

            m_Hour = hour;
            m_Minute = minute;
            m_Second = second;
            m_DayOfWeek = dayOfWeek;
            m_Year = year;
            m_Month = month;
            m_Day = day;
        }

        /// <summary>
        /// 年
        /// </summary>
        public Nullable<Int32> Year
        {
            get { return m_Year; }
        }
        /// <summary>
        /// 月
        /// </summary>
        public Nullable<Int32> Month
        {
            get { return m_Month; }
        }
        /// <summary>
        /// 日
        /// </summary>
        public Nullable<Int32> Day
        {
            get { return m_Day; }
        }
        /// <summary>
        /// 星期几
        /// </summary>
        public Nullable<DayOfWeek> DayOfWeek
        {
            get { return m_DayOfWeek; }
        }
        /// <summary>
        /// 时
        /// </summary>
        public Int32 Hour
        {
            get { return m_Hour; }
        }
        /// <summary>
        /// 分
        /// </summary>
        public Int32 Minute
        {
            get { return m_Minute; }
        }
        /// <summary>
        /// 秒
        /// </summary>
        public Int32 Second
        {
            get { return m_Second; }
        }
        /// <summary>
        /// 计划类型，分别为OneOff,Weekly,Daily
        /// </summary>
        public PlanType Type
        {
            get
            {
                if (Year != null && Month != null && Day != null)
                {
                    return PlanType.OneOff;
                }
                else if (DayOfWeek != null)
                {
                    return PlanType.Weekly;
                }
                else
                {
                    return PlanType.Daily;
                }
            }
        }
        /// <summary>
        /// 根据参照时间寻找计划的触发时间
        /// </summary>
        /// <param name="referenceTime">参照时间</param>
        /// <returns>返回触发时间</returns>
        public DateTime ToDateTime(DateTime referenceTime)
        {
            //一次性计划时间
            if (Type == PlanType.OneOff)
            {
                //完整时间
                return new DateTime(Year ?? 0, Month ?? 0, Day ?? 0, Hour, Minute, Second, 0);
            }
            //周计划时间
            else if (Type == PlanType.Weekly)
            {
                DateTime date = referenceTime.AddDays(Convert.ToInt32(DayOfWeek ?? 0) - Convert.ToInt32(referenceTime.DayOfWeek));
                return new DateTime(date.Year, date.Month, date.Day, Hour, Minute, Second, 0);
            }
            //日计划时间
            else
            {
                DateTime date = referenceTime;
                return new DateTime(date.Year, date.Month, date.Day, Hour, Minute, Second, 0);
            }
        }
        /// <summary>
        /// 将PlanTime转换为DateTime。
        /// 注意DateTime的值会以当前系统时间作为参照变更为当前最合适的时间
        /// </summary>
        /// <returns>返回DateTime对象</returns>        
        public DateTime ToDateTime()
        {
            return ToDateTime(DateTime.Now);
        }
        /// <summary>
        /// 获取根据当前计划时间所计算出来的下一次触发时间
        /// </summary>
        /// <returns>如果有下一次的触发时间则返回该值，否则将返回DateTime.MaxValue</returns>
        public DateTime GetNextTime()
        {
            DateTime nextTime = ToDateTime();
            if (DateTime.Now < nextTime)
            {
                return nextTime;
            }
            else
            {
                if (Type == PlanType.OneOff)
                {
                    return DateTime.MaxValue;
                }
                else if (Type == PlanType.Weekly)
                {
                    //下周的触发时间
                    return nextTime.AddDays(7);
                }
                else
                {
                    return nextTime.AddDays(1);
                }
            }
        }
        /// <summary>
        /// 继承自System.Object 将PlanTime转换为时间字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (Year != null && Month != null && Day != null)
            {
                return String.Format("{0:D4}-{1:D2}-{2:D2} {3:D2}:{4:D2}:{5:D2}", Year, Month, Day, Hour, Minute, Second);
            }
            else if (DayOfWeek != null)
            {
                return String.Format("{0:D2}:{1:D2}:{2:D2} {3}", Hour, Minute, Second, DayOfWeek);
            }
            else
            {
                return String.Format("{0:D2}:{1:D2}:{2:D2}", Hour, Minute, Second);
            }
        }
        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool Equals(object value)
        {
            if (value is PlanTime)
            {
                PlanTime time = (PlanTime)value;
                return (this == time);
            }
            return false;
        }
        /// <summary>
        /// GetHashCode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
        /// <summary>
        /// 根据字符串解析PlanTime对象
        /// </summary>
        /// <param name="planTimeString">PlanTime的字符串</param>
        /// <returns>返回PlanTime对象。</returns>
        public static PlanTime Parse(String planTimeString)
        {
            PlanTime time;
            if (!TryParse(planTimeString, out time))
                throw new FormatException(String.Format("PlanTimeString {0} is not match.", planTimeString));
            return time;
        }
        /// <summary>
        /// 尝试根据字符串解析PlanTime对象
        /// </summary>
        /// <param name="planTimeString">PlanTime的字符串</param>
        /// <param name="planTime">如果成功则输出PlanTime对象</param>
        /// <returns>如果解析成功则返回true,否则返回false.</returns>
        public static Boolean TryParse(String planTimeString, out PlanTime planTime)
        {
            if (Regex.IsMatch(planTimeString, DateTimeRegularExpression) == true)
            {
                Int32 year = Convert.ToInt32(planTimeString.Substring(0, 4));
                Int32 month = Convert.ToInt32(planTimeString.Substring(5, 2));
                Int32 day = Convert.ToInt32(planTimeString.Substring(8, 2));
                Int32 hour = Convert.ToInt32(planTimeString.Substring(11, 2));
                Int32 minute = Convert.ToInt32(planTimeString.Substring(14, 2));
                Int32 second = Convert.ToInt32(planTimeString.Substring(17, 2));
                planTime = new PlanTime(year, month, day, hour, minute, second);
            }
            else if (Regex.IsMatch(planTimeString, WeeklyTimeRegularExpression) == true)
            {
                Int32 hour = Convert.ToInt32(planTimeString.Substring(0, 2));
                Int32 minute = Convert.ToInt32(planTimeString.Substring(3, 2));
                Int32 second = Convert.ToInt32(planTimeString.Substring(6, 2));
                DayOfWeek dayOfWeek = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), planTimeString.Substring(9));
                planTime = new PlanTime(hour, minute, second, dayOfWeek);
            }
            else if (Regex.IsMatch(planTimeString, TimeRegularExpression) == true)
            {
                Int32 hour = Convert.ToInt32(planTimeString.Substring(0, 2));
                Int32 minute = Convert.ToInt32(planTimeString.Substring(3, 2));
                Int32 second = Convert.ToInt32(planTimeString.Substring(6, 2));
                planTime = new PlanTime(hour, minute, second);
            }
            else
            {
                planTime = new PlanTime();
                return false;
            }
            return true;
        }
        /// <summary>
        /// ==操作符重载
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static bool operator ==(PlanTime p1, PlanTime p2)
        {
            return (p1.Year == p2.Year && p1.Month == p2.Month && p1.Day == p2.Day
                && p1.Hour == p2.Hour && p1.Minute == p2.Minute && p1.Second == p2.Second);
        }
        /// <summary>
        /// !=操作符重载
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static bool operator !=(PlanTime p1, PlanTime p2)
        {
            return !(p1 == p2);
        }
        #region IXmlSerializable 成员
        /// <summary>
        /// 此方法是保留方法，请不要使用。在实现 IXmlSerializable 接口时，应从此方法返回 null（在 Visual Basic 中为 Nothing），如果需要指定自定义架构，应向该
        /// </summary>
        /// <returns></returns>
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        public void ReadXml(System.Xml.XmlReader reader)
        {
            reader.ReadStartElement("PlanTime");
            this.CopyFrom(PlanTime.Parse(reader.ReadString()));
            reader.ReadEndElement();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        public void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteStartElement("PlanTime");
            writer.WriteString(this.ToString());
            writer.WriteEndElement();
        }

        #endregion
        #region IEquatable<DateTime> 成员
        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Equals(PlanTime value)
        {
            return (this == value);
        }
        #endregion
        /// <summary>
        /// 将PlanTime对象的值拷贝到当前的PlanTime对象中
        /// </summary>
        /// <param name="time"></param>
        private void CopyFrom(PlanTime time)
        {
            m_Year = time.Year;
            m_Month = time.Month;
            m_Day = time.Day;
            m_Hour = time.Hour;
            m_Minute = time.Minute;
            m_Second = time.Second;
            m_DayOfWeek = time.DayOfWeek;
        }
        /// <summary>
        /// 判断计划时间是否处于某个时间段内
        /// </summary>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>如果处于某个时间段内返回true,否则返回false。</returns>
        public Boolean IsInRange(PlanTime beginTime,PlanTime endTime)
        {
            return IsInRange(beginTime, endTime, Intervals.LeftClosed | Intervals.RightClosed);
        }
        /// <summary>
        /// 判断计划时间是否处于某个时间段内
        /// </summary>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="intervals">取值区间值</param>
        /// <returns>如果处于某个时间段内返回true,否则返回false。</returns>
        public Boolean IsInRange(PlanTime beginTime, PlanTime endTime, Intervals intervals)
        {
            if (this.Type != beginTime.Type || this.Type != endTime.Type)
                throw new ArgumentException("Plan time type mismatch.");
            return this.ToDateTime().IsInRange(beginTime.ToDateTime(), endTime.ToDateTime(), intervals);
        }
    }
}
