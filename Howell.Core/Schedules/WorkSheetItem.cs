using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.ComponentModel;

namespace Howell.Schedules
{
    /// <summary>
    /// 工作表项结束事件参数
    /// </summary>
    public class WorkSheetItemEndingEventArgs :EventArgs
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="index">工作表项索引</param>
        public WorkSheetItemEndingEventArgs(Int32 index)
        {
            this.Index = index;
        }
        /// <summary>
        /// 工作表项索引
        /// </summary>
        public Int32 Index { get; private set; }
    }
    /// <summary>
    /// 工作表项开始事件参数
    /// </summary>
    public class WorkSheetItemBeginningEventArgs : EventArgs
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="index">工作表项索引</param>
        public WorkSheetItemBeginningEventArgs(Int32 index)
        {
            this.Index = index;
        }
        /// <summary>
        /// 工作表项索引
        /// </summary>
        public Int32 Index { get; private set; }
    }
    /// <summary>
    /// 工作表项
    /// </summary>
    public class WorkSheetItem : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        protected WorkSheetItem()
        {

        }
        /// <summary>
        /// 创建Howell.Schedules.WorkSheetItem对象
        /// </summary>
        /// <param name="beginTime">工作表项的开始时间</param>
        /// <param name="endTime">工作表项的结束时间</param>
        public WorkSheetItem(PlanTime beginTime,PlanTime endTime)
            : this(beginTime, endTime, String.Empty, true)
        {
        }
        /// <summary>
        /// 创建Howell.Schedules.WorkSheetItem对象
        /// </summary>
        /// <param name="beginTime">工作表项的开始时间</param>
        /// <param name="endTime">工作表项的结束时间</param>
        /// <param name="content">工作表项的内容</param>
        /// <param name="enabled">是否启用提示功能</param>
        public WorkSheetItem(PlanTime beginTime,PlanTime endTime,String content,Boolean enabled)
        {
            this.Plan = new PlanPeriod(Guid.NewGuid().ToString(), beginTime, endTime);
            this.Plan.Beginning += new EventHandler<EventArgs>(Plan_Beginning);
            this.Plan.Ending += new EventHandler<EventArgs>(Plan_Ending);
            this.Plan.Content = content;
            this.Plan.Enabled = enabled;
        }
        private PlanPeriod Plan { get; set; }
        /// <summary>
        /// 工作表项的开始时间
        /// </summary>
        public virtual PlanTime BeginTime
        {
            get
            {
                return this.Plan.BeginTime;
            }
        }
        /// <summary>
        /// 工作表项的结束时间
        /// </summary>
        public virtual PlanTime EndTime
        {
            get
            {
                return this.Plan.EndTime;
            }
        }
        /// <summary>
        /// 工作表项的内容
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
        /// 是否启用提示功能
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
        /// 
        /// </summary>
        void IDisposable.Dispose()
        {
            if(this.Plan != null)
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
