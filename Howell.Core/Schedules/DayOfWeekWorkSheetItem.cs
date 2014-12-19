using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Howell.Schedules
{
    /// <summary>
    /// 星期几的工作表项对象
    /// </summary>
    public  class DayOfWeekWorkSheetItem : IDisposable
    {
        /// <summary>
        /// 创建 Howell.Schedules.DayOfWeekWorkSheetItem 对象
        /// </summary>
        /// <param name="beginHour">开始时间的小时值</param>
        /// <param name="beginMinute">开始时间的分钟值</param>
        /// <param name="beginSecond">开始时间的秒值</param>
        /// <param name="endHour">结束时间的小时值</param>
        /// <param name="endMinute">结束时间的分钟值</param>
        /// <param name="endSecond">结束时间的秒值</param>
        /// <param name="dayOfWeek">星期几</param>
        internal DayOfWeekWorkSheetItem(Int32 beginHour, Int32 beginMinute, Int32 beginSecond, Int32 endHour, Int32 endMinute, Int32 endSecond, DayOfWeek dayOfWeek)
        {
            this.Plan = new PlanPeriod(Guid.NewGuid().ToString(), new PlanTime(beginHour, beginMinute, beginSecond, dayOfWeek), new PlanTime(endHour, endMinute, endSecond, dayOfWeek));
            this.Plan.Beginning += new EventHandler<EventArgs>(Plan_Beginning);
            this.Plan.Ending +=new EventHandler<EventArgs>(Plan_Ending);
        }
        private PlanPeriod Plan { get; set; } 
        /// <summary>
        /// 开始时间段
        /// </summary>
        public virtual PlanTime BeginTime
        {
            get
            {
                return this.Plan.BeginTime;
            }
        }
        /// <summary>
        /// 结束时间段
        /// </summary>
        public virtual PlanTime EndTime
        {
            get
            {
                return this.Plan.EndTime;
            }
        }
        /// <summary>
        /// 时间段内容
        /// </summary>
        public virtual String Content
        {
            get
            {
                return this.Plan.Content;
            }
            set
            {
                this.Plan.Content = value;
            }
        }
        /// <summary>
        /// 是否启用该工作表项
        /// </summary>
        public virtual Boolean Enabled
        {
            get
            {
                return this.Plan.Enabled;
            }
            set
            {
                this.Plan.Enabled = value;
            }
        }
        /// <summary>
        /// 唯一ID
        /// </summary>
        internal virtual String Id
        {
            get
            {
                return this.Plan.Id;
            }
        }
        /// <summary>
        /// 时间段计划开始提示
        /// </summary>
        internal event EventHandler<EventArgs> Beginning;
        /// <summary>
        /// 时间段计划结束提示
        /// </summary>
        internal event EventHandler<EventArgs> Ending;
        /// <summary>
        /// 触发开始事件
        /// </summary>
        protected void RaiseBeginning()
        {
            try
            {
                EventHandler<EventArgs> eventHandler = this.Beginning;
                if (eventHandler != null)
                {
                    eventHandler(this, new EventArgs());
                }
            }
            catch { }
        }
        /// <summary>
        /// 触发结束事件
        /// </summary>
        protected void RaiseEnding()
        {
            try
            {
                EventHandler<EventArgs> eventHandler = this.Ending;
                if (eventHandler != null)
                {
                    eventHandler(this, new EventArgs());
                }
            }
            catch { }
        }
        void Plan_Ending(object sender, EventArgs e)
        {
            RaiseEnding();
        }
        void Plan_Beginning(object sender, EventArgs e)
        {
            RaiseBeginning();
        }
        #region IDisposable 成员
        /// <summary>
        /// 销毁 Howell.Schedules.DayOfWeekWorkSheetItem 对象
        /// </summary>
        void IDisposable.Dispose()
        {
            if (this.Plan != null)
            {
                this.Plan.Beginning -= new EventHandler<EventArgs>(Plan_Beginning);
                this.Plan.Ending -= new EventHandler<EventArgs>(Plan_Ending);
                this.Plan.Dispose();
                this.Plan = null;
            }
        }
        #endregion
    }
}
