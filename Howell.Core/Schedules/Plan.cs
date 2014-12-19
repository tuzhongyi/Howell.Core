using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Howell.Conditions;
using System.Text.RegularExpressions;
using System.Threading;
using Howell.Numeric;
using Howell.Threading;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using System.ComponentModel;

namespace Howell.Schedules
{
    /// <summary>
    /// .NET 计划对象
    /// </summary>
    public class Plan : IDisposable
    {
        private const Int32 TimeSpanEpsilon = 200;
        private TimeSpan m_RepeatInterval;
        private Int32 m_RepeatTimes;
        private WrappedTimer m_RaiseTimer;
        private ThresholdNumber<Int32> m_RepeatThreshold;
        private PlanTime m_Time;
        private SortedSet<DateTime> m_RemindedSet = new SortedSet<DateTime>();
        private Object Mutex = new Object();
        private static List<Plan> Plans = new List<Plan>();
        /// <summary>
        /// 静态构造函数
        /// </summary>
        static Plan()
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
            lock (typeof(Plan))
            {
                foreach (var item in Plan.Plans)
                {
                    try
                    {
                        item.Reset();
                    }
                    catch (Exception ex) { Console.WriteLine(String.Format("SystemEvents_TimeChanged plan reset error. {0}", ex.Message)); }
                }
            }
        }
        /// <summary>
        /// 创建每日重复计划
        /// </summary>
        /// <param name="hour">时</param>
        /// <param name="minute">分</param>
        /// <param name="second">秒</param>
        public Plan(Int32 hour, Int32 minute, Int32 second)
            : this(null, null, null, hour, minute, second, null)
        {
        }
        /// <summary>
        /// 创建每周重复计划
        /// </summary>
        /// <param name="hour">时</param>
        /// <param name="minute">分</param>
        /// <param name="second">秒</param>
        /// <param name="dayOfWeek">星期几</param>
        public Plan(Int32 hour, Int32 minute, Int32 second, DayOfWeek dayOfWeek)
            : this(null, null, null, hour, minute, second, dayOfWeek)
        {
        }
        /// <summary>
        /// 创建一次性的计划
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="day">日</param>
        /// <param name="hour">时</param>
        /// <param name="minute">分</param>
        /// <param name="second">秒</param>
        public Plan(Int32 year, Int32 month, Int32 day, Int32 hour, Int32 minute, Int32 second)
            : this(year, month, day, hour, minute, second, null)
        {
        }
        /// <summary>
        ///  创建计划
        /// </summary>
        /// <param name="time"></param>
        public Plan(PlanTime time)
            : this(time.Year, time.Month, time.Day, time.Hour, time.Minute, time.Second, time.DayOfWeek)
        {
        }
        /// <summary>
        /// 创建一次性的计划
        /// </summary>
        /// <param name="time">一次性的计划触发时间</param>
        public Plan(DateTime time)
            : this(time.Year, time.Month, time.Day, time.Hour, time.Minute, time.Second, null)
        {
        }
        /// <summary>
        /// 创建PlanTime对象
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="day">日</param>
        /// <param name="hour">时</param>
        /// <param name="minute">分</param>
        /// <param name="second">秒</param>
        /// <param name="dayOfWeek">星期几</param>
        internal Plan(Nullable<Int32> year, Nullable<Int32> month, Nullable<Int32> day, Int32 hour, Int32 minute, Int32 second, Nullable<DayOfWeek> dayOfWeek)
            : this( year, month, day, hour, minute, second, dayOfWeek, 1, TimeSpan.FromSeconds(30), true)
        {

        }
        /// <summary>
        /// 创建PlanTime对象
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="day">日</param>
        /// <param name="hour">时</param>
        /// <param name="minute">分</param>
        /// <param name="second">秒</param>
        /// <param name="dayOfWeek">星期几</param>
        /// <param name="repeatTimes">重复提示次数,取值范围：[1,5]，默认为1次</param>
        /// <param name="repeatInterval">重复提示的间隔时间,取值范围: (5秒至30分钟)，默认为30秒.</param>
        /// <param name="enabled">是否启用计划，注意：如果在提示期间Enabled的值被修改，将不再继续RepeatTimes的后续提示，而是转而等待下次计划的触发。</param>
        internal Plan(Nullable<Int32> year, Nullable<Int32> month, Nullable<Int32> day, Int32 hour, Int32 minute, Int32 second, Nullable<DayOfWeek> dayOfWeek, Int32 repeatTimes, TimeSpan repeatInterval, Boolean enabled)
            : this(Guid.NewGuid().ToString(), year, month, day, hour, minute, second, dayOfWeek, repeatTimes, repeatInterval, enabled)
        {
        }
        /// <summary>
        /// 创建PlanTime对象
        /// </summary>
        /// <param name="id"></param>
        /// <param name="time">计划时间</param>
        internal Plan(String id, PlanTime time)
            : this(id, time.Year, time.Month, time.Day, time.Hour, time.Minute, time.Second, time.DayOfWeek, 1, TimeSpan.FromSeconds(30), true)
        {

        }
        /// <summary>
        /// 创建PlanTime对象
        /// </summary>
        /// <param name="id"></param>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="day">日</param>
        /// <param name="hour">时</param>
        /// <param name="minute">分</param>
        /// <param name="second">秒</param>
        /// <param name="dayOfWeek">星期几</param>
        /// <param name="repeatTimes">重复提示次数,取值范围：[1,5]，默认为1次</param>
        /// <param name="repeatInterval">重复提示的间隔时间,取值范围: (5秒至30分钟)，默认为30秒.</param>
        /// <param name="enabled">是否启用计划，注意：如果在提示期间Enabled的值被修改，将不再继续RepeatTimes的后续提示，而是转而等待下次计划的触发。</param>
        internal Plan(String id, Nullable<Int32> year, Nullable<Int32> month, Nullable<Int32> day, Int32 hour, Int32 minute, Int32 second, Nullable<DayOfWeek> dayOfWeek, Int32 repeatTimes, TimeSpan repeatInterval, Boolean enabled)
        {
            this.Tag = null;
            this.Id = id;
            this.m_Time = new PlanTime(year, month, day, hour, minute, second, dayOfWeek);
            this.RepeatTimes = repeatTimes;
            this.RepeatInterval = repeatInterval;
            m_RaiseTimer = null;
            this.Enabled = enabled;
            lock (typeof(Plan))
            {
                Plan.Plans.Add(this);
            }
        }
        /// <summary>
        /// 计划全局唯一ID
        /// </summary>
        public String Id { get; private set; }
        /// <summary>
        /// 重复提示的间隔时间,取值范围: (5秒至30分钟)，默认为30秒.
        /// </summary>
        public TimeSpan RepeatInterval
        {
            get
            {
                return m_RepeatInterval;
            }
            set
            {
                Condition.Requires(value.TotalSeconds).IsInRange(5, 30 * 60);
                m_RepeatInterval = value;
            }
        }
        /// <summary>
        /// 重复提示次数,取值范围：[1,5]，默认为1次
        /// </summary>
        public Int32 RepeatTimes
        {
            get
            {
                return m_RepeatTimes;
            }
            set
            {
                Condition.Requires(value).IsInRange(1, 5);
                m_RepeatTimes = value;
                m_RepeatThreshold = new ThresholdNumber<int>(value - 1, 0, 0);
            }
        }
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
                        m_RaiseTimer = new WrappedTimer(PlanTimerCallBack, null, GetNextRaisePeriod(false), RepeatInterval);
                    }
                    if (value == false && m_RaiseTimer != null)
                    {
                        m_RaiseTimer.Dispose();
                        m_RaiseTimer = null;
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
                return m_Time.Type;
            }
        }
        /// <summary>
        /// 获取计划时间
        /// </summary>
        public PlanTime PlannedTime
        {
            get
            {
                return m_Time;
            }
        }
        /// <summary>
        /// 计划内容
        /// </summary>
        public String Content { get; set; }
        /// <summary>
        /// 获取已通过计划提醒过的时间记录
        /// </summary>
        public ReadOnlyCollection<DateTime> RemindedRecords
        {
            get
            {
                return m_RemindedSet.ToList().AsReadOnly();
            }
        }
        /// <summary>
        /// 标记
        /// </summary>
        public Object Tag { get; set; }
        /// <summary>
        /// 重置计划
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Reset()
        {
            lock(Mutex)
            {
                if (m_RaiseTimer != null)
                {
                    m_RaiseTimer.Dispose();
                    m_RaiseTimer = null;
                    m_RaiseTimer = new WrappedTimer(PlanTimerCallBack, null, GetNextRaisePeriod(false), RepeatInterval);
                }
            }
        }
        /// <summary>
        /// 计划提示事件
        /// </summary>
        public event EventHandler<EventArgs> Reminding;        
        #region IDisposable 成员
        /// <summary>
        /// 销毁计划对象
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
                    m_RemindedSet.Clear();
                }
                Dispose(true);
            }
            finally
            {
                lock (typeof(Plan))
                {
                    Plan.Plans.Remove(this);
                }
            }
        }
        /// <summary>
        /// 释放由 Plan 占用的非托管资源，还可以另外再释放托管资源。
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
        protected virtual void RaiseReminding()
        {
            EventHandler<EventArgs> eventHandler = Reminding;
            if (eventHandler != null)
            {
                eventHandler(this, new EventArgs());
            }
        }        
        /// <summary>
        /// 添加计划触发的时间
        /// </summary>
        private void AddRemindedTime()
        {
            lock (this)
            {
                if (m_RemindedSet.Contains(this.PlannedTime.ToDateTime()) == false)
                {
                    m_RemindedSet.Add(this.PlannedTime.ToDateTime());
                }
            }
        }
        /// <summary>
        /// 获取下次触发事件的时间间隔
        /// </summary>
        /// <param name="needRepeat"></param>
        /// <returns>返回时间间隔</returns>
        private TimeSpan GetNextRaisePeriod(Boolean needRepeat)
        {
            TimeSpan ts = needRepeat ? RepeatInterval : (m_Time.GetNextTime() - DateTime.Now);
            //如果时间间隔过长则使用一个简短的时间来做校正
            if (ts > TimeSpan.FromDays(10))
                return TimeSpan.FromDays(1);
            else if (ts < TimeSpan.FromMilliseconds(100))
                return TimeSpan.FromMilliseconds(100);
            else
                return ts;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        private void PlanTimerCallBack(Object state)
        {
            Boolean needRepeat = false;
            m_RaiseTimer.Change(Timeout.Infinite, Timeout.Infinite);
            try
            {
                //过滤那些应为触发时间过长，而无法使用线程时钟的时间间隔，转而使用1天的间隔所触发事件
                if (m_Time.ToDateTime().Date == DateTime.Now.Date)
                {
                    try
                    {
                        RaiseReminding();
                    }
                    catch { }
                    AddRemindedTime();
                    Int32 newValue = 0;
                    needRepeat = m_RepeatThreshold.TryIncrement(out newValue);
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format("Plan.Reminding error, {0}", ex.Message));
            }
            finally
            {
                Thread.Sleep(TimeSpanEpsilon);//由于线程时钟含有几百毫秒的误差，此处的Sleep用于调整可能出现的误差问题
                m_RaiseTimer.Change(GetNextRaisePeriod(needRepeat), RepeatInterval);
            }
        }
    }
}
