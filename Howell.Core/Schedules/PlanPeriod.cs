using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Howell.Threading;
using System.Threading;
using Howell.Conditions;
using Howell.Time;
using Microsoft.Win32;
using System.ComponentModel;

namespace Howell.Schedules
{
    /// <summary>
    /// .NET 时间段计划对象
    /// </summary>
    public class PlanPeriod : IDisposable
    {
        private const Int32 TimeSpanEpsilon = 200;
        private PlanTime m_BeginTime;
        private PlanTime m_EndTime;
        private WrappedTimer m_RaiseTimer;
        private Boolean m_Begun;
        private SortedSet<DateTime> m_BegunSet = new SortedSet<DateTime>();
        private SortedSet<DateTime> m_EndedSet = new SortedSet<DateTime>();
        private Object Mutex = new Object();

        private static List<PlanPeriod> PlanPeriods = new List<PlanPeriod>();
        /// <summary>
        /// 静态构造函数
        /// </summary>
        static PlanPeriod()
        {
            //注册系统时间被修改事件
            SystemEvents.TimeChanged += new EventHandler(SystemEvents_TimeChanged);
        }
        /// <summary>
        /// 系统时间被修改后触发的事件，主要用于修改系统时间后会出现计划任务时间不正确的问题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void SystemEvents_TimeChanged(object sender, EventArgs e)
        {
            lock (typeof(PlanPeriod))
            {
                foreach (var item in PlanPeriod.PlanPeriods)
                {
                    try
                    {
                        item.Reset();
                    }
                    catch (Exception ex) { Console.WriteLine(String.Format("SystemEvents_TimeChanged planPeriod reset error. {0}", ex.Message)); }
                }
            }
        }
        /// <summary>
        /// 创建每日重复的时间段计划对象
        /// </summary>
        /// <param name="beginHour">开始时间的小时值</param>
        /// <param name="beginMinute">开始时间的分钟值</param>
        /// <param name="beginSecond">开始时间的秒值</param>
        /// <param name="endHour">结束时间的小时值</param>
        /// <param name="endMinute">结束时间的分钟值</param>
        /// <param name="endSecond">结束时间的秒值</param>
        public PlanPeriod(Int32 beginHour, Int32 beginMinute, Int32 beginSecond, Int32 endHour, Int32 endMinute, Int32 endSecond)
            : this(null, null, null, beginHour, beginMinute, beginSecond, null, null, null, null, endHour, endMinute, endSecond, null)
        {
        }
        /// <summary>
        /// 创建每周重复的时间段计划对象
        /// </summary>
        /// <param name="beginHour">开始时间的小时值</param>
        /// <param name="beginMinute">开始时间的分钟值</param>
        /// <param name="beginSecond">开始时间的秒值</param>
        /// <param name="beginDayOfWeek">开始时间的星期几</param>
        /// <param name="endHour">结束时间的小时值</param>
        /// <param name="endMinute">结束时间的分钟值</param>
        /// <param name="endSecond">结束时间的秒值</param>
        /// <param name="endDayOfWeek">结束时间的星期几</param>
        public PlanPeriod(Int32 beginHour, Int32 beginMinute, Int32 beginSecond,DayOfWeek beginDayOfWeek,
             Int32 endHour, Int32 endMinute, Int32 endSecond,DayOfWeek endDayOfWeek)
            : this(null, null, null, beginHour, beginMinute, beginSecond, beginDayOfWeek, null, null, null, endHour, endMinute, endSecond, endDayOfWeek)
        {
        }
        /// <summary>
        /// 创建一次性的时间段计划对象
        /// </summary>
        /// <param name="beginYear">开始时间的年</param>
        /// <param name="beginMonth">开始时间的月</param>
        /// <param name="beginDay">开始时间的日</param>
        /// <param name="beginHour">开始时间的小时</param>
        /// <param name="beginMinute">开始时间的分钟</param>
        /// <param name="beginSecond">开始时间的秒</param>
        /// <param name="endYear">结束时间的年</param>
        /// <param name="endMonth">结束时间的月</param>
        /// <param name="endDay">结束时间的日</param>
        /// <param name="endHour">结束时间的小时</param>
        /// <param name="endMinute">结束时间的分钟</param>
        /// <param name="endSecond">结束时间的秒</param>
        public PlanPeriod(Int32 beginYear,Int32 beginMonth,Int32 beginDay, Int32 beginHour, Int32 beginMinute, Int32 beginSecond,
            Int32 endYear, Int32 endMonth, Int32 endDay, Int32 endHour, Int32 endMinute, Int32 endSecond)
            : this(beginYear, beginMonth, beginDay, beginHour, beginMinute, beginSecond, null, endYear, endMonth, endDay, endHour, endMinute, endSecond, null)
        {
        }
        /// <summary>
        /// 创建一次性的时间段计划对象
        /// </summary>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        public PlanPeriod(DateTime beginTime,DateTime endTime)
            : this(beginTime.Year,beginTime.Month,beginTime.Day,beginTime.Hour,beginTime.Minute,beginTime.Second,null,
            endTime.Year, endTime.Month, endTime.Day, endTime.Hour, endTime.Minute, endTime.Second, null)
        {           

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="beginYear"></param>
        /// <param name="beginMonth"></param>
        /// <param name="beginDay"></param>
        /// <param name="beginHour"></param>
        /// <param name="beginMinute"></param>
        /// <param name="beginSecond"></param>
        /// <param name="beginDayOfWeek"></param>
        /// <param name="endYear"></param>
        /// <param name="endMonth"></param>
        /// <param name="endDay"></param>
        /// <param name="endHour"></param>
        /// <param name="endMinute"></param>
        /// <param name="endSecond"></param>
        /// <param name="endDayOfWeek"></param>
        internal PlanPeriod(Nullable<Int32> beginYear,Nullable<Int32> beginMonth,Nullable<Int32> beginDay, Int32 beginHour, Int32 beginMinute, Int32 beginSecond,Nullable<DayOfWeek> beginDayOfWeek,
            Nullable<Int32> endYear,Nullable<Int32> endMonth,Nullable<Int32> endDay, Int32 endHour, Int32 endMinute, Int32 endSecond,Nullable<DayOfWeek> endDayOfWeek)
            : this(Guid.NewGuid().ToString(),beginYear,beginMonth,beginDay,beginHour,beginMinute,beginSecond,beginDayOfWeek,
            endYear, endMonth, endDay, endHour, endMinute, endSecond, endDayOfWeek)
        {
        }
        /// <summary>
        /// 创建时间段计划对象
        /// </summary>
        /// <param name="id"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        internal PlanPeriod(String id,PlanTime beginTime,PlanTime endTime)
            : this(id, beginTime.Year, beginTime.Month, beginTime.Day, beginTime.Hour, beginTime.Minute, beginTime.Second, beginTime.DayOfWeek, endTime.Year, endTime.Month, endTime.Day, endTime.Hour, endTime.Minute, endTime.Second, endTime.DayOfWeek)
        {
        }
        /// <summary>
        /// 创建时间段计划对象
        /// </summary>
        /// <param name="id"></param>
        /// <param name="beginYear"></param>
        /// <param name="beginMonth"></param>
        /// <param name="beginDay"></param>
        /// <param name="beginHour"></param>
        /// <param name="beginMinute"></param>
        /// <param name="beginSecond"></param>
        /// <param name="beginDayOfWeek"></param>
        /// <param name="endYear"></param>
        /// <param name="endMonth"></param>
        /// <param name="endDay"></param>
        /// <param name="endHour"></param>
        /// <param name="endMinute"></param>
        /// <param name="endSecond"></param>
        /// <param name="endDayOfWeek"></param>
        internal PlanPeriod(String id, Nullable<Int32> beginYear, Nullable<Int32> beginMonth, Nullable<Int32> beginDay, Int32 beginHour, Int32 beginMinute, Int32 beginSecond, Nullable<DayOfWeek> beginDayOfWeek,
            Nullable<Int32> endYear, Nullable<Int32> endMonth, Nullable<Int32> endDay, Int32 endHour, Int32 endMinute, Int32 endSecond, Nullable<DayOfWeek> endDayOfWeek)
        {
            this.Tag = null;
            this.Id = id;
            m_BeginTime = new PlanTime(beginYear, beginMonth, beginDay, beginHour, beginMinute, beginSecond, beginDayOfWeek);
            m_EndTime = new PlanTime(endYear, endMonth, endDay, endHour, endMinute, endSecond, endDayOfWeek);
            m_Begun = false;
            if(m_BeginTime.Type  != m_EndTime.Type)
            {
                throw new ArgumentException("PlanPeriod constructor error, the begin time type must be the same as the end time.");
            }

            if (m_BeginTime.ToDateTime() >= m_EndTime.ToDateTime())
            {
                throw new ArgumentException("PlanPeriod constructor error, the begin time must be less than the end time.");
            }
            lock (typeof(PlanPeriod))
            {
                PlanPeriod.PlanPeriods.Add(this);
            }
            this.Enabled = true;
        }
        /// <summary>
        /// 计划全局唯一ID
        /// </summary>
        public String Id { get; private set; }
        /// <summary>
        /// 是否启用计划提示功能
        /// 注意：如果在提示期间Enabled的值被修改，将不再继续RepeatTimes的后续提示，而是转而等待下次计划的触发。
        /// </summary>
        public Boolean Enabled
        {
            get
            {
                lock (Mutex)
                {
                    return (m_RaiseTimer != null);
                }
            }
            set
            {
                lock (Mutex)
                {
                    if (value == true && m_RaiseTimer == null)
                    {
                        m_RaiseTimer = new WrappedTimer(PlanTimerCallBack, null, GetNextRaisePeriod(), TimeSpan.FromDays(1));
                    }
                    if (value == false && m_RaiseTimer != null)
                    {
                        m_RaiseTimer.Dispose();
                        m_RaiseTimer = null;
                        m_Begun = false;
                    }
                }
            }
        }
        /// <summary>
        /// 计划类型
        /// </summary>
        public PlanType Type
        {
            get
            {
                return m_BeginTime.Type;
            }
        }               
        /// <summary>
        /// 获取当前的计划开始时间
        /// </summary>
        public PlanTime BeginTime
        {
            get
            {
                return m_BeginTime;
            }
        }
        /// <summary>
        /// 获取当前的计划结束时间
        /// </summary>
        public PlanTime EndTime
        {
            get
            {
                return m_EndTime;
            }
        }
        /// <summary>
        /// 标记
        /// </summary>
        public Object Tag { get; set; }
        /// <summary>
        /// 计划内容
        /// </summary>
        public String Content { get; set; }
        /// <summary>
        /// 时间段计划开始提示
        /// </summary>
        public event EventHandler<EventArgs> Beginning;
        /// <summary>
        /// 时间段计划结束提示
        /// </summary>
        public event EventHandler<EventArgs> Ending;
        /// <summary>
        /// 重置计划
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Reset()
        {
            lock (Mutex)
            {
                if (m_RaiseTimer != null)
                {
                    m_RaiseTimer.Dispose();
                    m_RaiseTimer = null;
                    m_RaiseTimer = new WrappedTimer(PlanTimerCallBack, null, GetNextRaisePeriod(), TimeSpan.FromDays(1));
                }
            }
        }
        #region IDisposable 成员
        /// <summary>
        /// 销毁由PlanPeriod占用的非托管资源，还可以另外再释放托管资源
        /// </summary>
        public void Dispose()
        {
            try
            {
                lock (Mutex)
                {
                    if (m_RaiseTimer != null)
                    {
                        m_RaiseTimer.Dispose();
                        m_RaiseTimer = null;
                    }
                    m_Begun = false;
                    m_BegunSet.Clear();
                    m_EndedSet.Clear();
                }
                Dispose(true);
            }
            finally
            {
                lock (typeof(PlanPeriod))
                {
                    PlanPeriod.PlanPeriods.Remove(this);
                }
            }
        }
        /// <summary>
        /// 释放由 PlanPeriod 占用的非托管资源，还可以另外再释放托管资源。
        /// </summary>
        /// <param name="disposing">为 true，则释放托管资源和非托管资源；为 false，则仅释放非托管资源。</param>
        protected virtual void Dispose(bool disposing)
        {
            return;
        }
        #endregion
        /// <summary>
        /// 触发计划开始事件
        /// </summary>
        protected virtual void RaiseBeginning()
        {
            EventHandler<EventArgs> eventHandler = Beginning;
            if (eventHandler != null)
            {
                eventHandler(this, new EventArgs());
            }
        }
        /// <summary>
        /// 触发计划结束事件
        /// </summary>
        protected virtual void RaiseEnding()
        {
            EventHandler<EventArgs> eventHandler = Ending;
            if (eventHandler != null)
            {
                eventHandler(this, new EventArgs());
            }
        }
        /// <summary>
        /// 获取下次触发事件的时间间隔
        /// </summary>
        /// <returns>返回时间间隔</returns>
        private TimeSpan GetNextRaisePeriod()
        {
            DateTime begin = m_BeginTime.ToDateTime();
            DateTime beginNext = m_BeginTime.GetNextTime();
            DateTime end = m_EndTime.ToDateTime();
            DateTime endNext = m_EndTime.GetNextTime();
            DateTime now = DateTime.Now;
            TimeSpan result;
            if (m_Begun == false)
            {                
                //此次的时间段已过期，需要使用下次的时间段
                if (now > end)
                {
                    //Console.WriteLine("Begun false, now > end. {0} > {1}", now, end);
                    result = beginNext - now;
                }
                //此次的时间段尚未抵达
                else if (now < begin)
                {
                    //Console.WriteLine("Begun false, now < begin. {0} < {1}", now, begin);
                    result = begin - now;
                }
                //在时间段内
                else
                {
                    //Console.WriteLine("Begun false, in range. {0} in {1}/{2}", now, begin, end);
                    result = TimeSpan.FromMilliseconds(100);//立即触发 
                }
            }
            else
            {
                //已结束
                if (now >= end)
                {
                    //Console.WriteLine("Begun true, now >= end. {0} >= {1}", now, end);
                    result = TimeSpan.FromMilliseconds(100);//立即触发 
                }
                //尚未开始，但是标志位已不正确
                else if (now < begin)
                {
                    //开始重置标记为
                    //Console.WriteLine("PlanPeriod begun flag error, reset flag now.");
                    m_Begun = false;
                    result = begin - now;
                }
                //在时间段内
                else
                {
                    //Console.WriteLine("Begun true, in range. {0} in {1}/{2}", now, begin, end);
                    result = end - now;
                }
            }
            //如果时间间隔过长则使用一个简短的时间来做校正
            if (result > TimeSpan.FromDays(10))
                return TimeSpan.FromDays(1);
            else if (result < TimeSpan.FromMilliseconds(100))
                return TimeSpan.FromMilliseconds(100);
            else
                return result;
        }
        /// <summary>
        /// 添加计划触发的时间
        /// </summary>
        private void AddRemindedTime()
        {
            lock (this)
            {
                if (m_Begun == true)
                {
                    if (m_BegunSet.Contains(this.BeginTime.ToDateTime()) == false)
                        m_BegunSet.Add(this.BeginTime.ToDateTime());
                }
                else
                {
                    if (m_EndedSet.Contains(this.EndTime.ToDateTime()) == false)
                        m_EndedSet.Add(this.EndTime.ToDateTime());
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        private void PlanTimerCallBack(Object state)
        {
            m_RaiseTimer.Change(Timeout.Infinite, Timeout.Infinite);
            try
            {
                if (m_Begun == false)
                {
                    //过滤那些应为触发时间过长，而无法使用线程时钟的时间间隔，转而使用1天的间隔所触发事件
                    if (m_BeginTime.ToDateTime().Date == DateTime.Now.Date)
                    {
                        m_Begun = true;
                        try
                        {
                            RaiseBeginning();
                        }catch{}
                        AddRemindedTime();
                    }
                }
                else
                {
                    if (m_EndTime.ToDateTime().Date == DateTime.Now.Date)
                    {
                        m_Begun = false;
                        try
                        {
                            RaiseEnding();
                        }
                        catch { }
                        AddRemindedTime();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format("PlanPeriod.Reminding error, {0}", ex.Message));
            }
            finally
            {
                Thread.Sleep(TimeSpanEpsilon);//由于线程时钟含有几百毫秒的误差，此处的Sleep用于调整可能出现的误差问题
                m_RaiseTimer.Change(GetNextRaisePeriod(), TimeSpan.FromDays(1));
            }
        }
    }
}
