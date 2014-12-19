using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Howell.Schedules
{
    /// <summary>
    /// 节目项
    /// </summary>
    public class ProgrammeItem : IDisposable
    {
        /// <summary>
        /// 创建 Howell.Schedules.ProgrammeItem 对象
        /// </summary>
        /// <param name="key">建</param>
        /// <param name="plannedTime">计划时间</param>
        public ProgrammeItem(String key,PlanTime plannedTime)
            : this(key, plannedTime, String.Empty)
        {
        }
        /// <summary>
        /// 创建 Howell.Schedules.ProgrammeItem 对象
        /// </summary>
        /// <param name="key">建</param>
        /// <param name="plannedTime">计划时间</param>
        /// <param name="content">节目内容</param>
        public ProgrammeItem(String key, PlanTime plannedTime, String content)
        {
            Plan = new Plan(key, plannedTime);
            Plan.Content = content;
            Plan.Reminding += new EventHandler<EventArgs>(Plan_Reminding);
        }
        void Plan_Reminding(object sender, EventArgs e)
        {
            try
            {
                EventHandler<EventArgs> eventHandler = ProgrammeItemReminding;
                if (eventHandler != null)
                {
                    eventHandler(this, e);
                }
            }
            catch { }
        }
        /// <summary>
        /// 提示事件
        /// </summary>
        internal event EventHandler<EventArgs> ProgrammeItemReminding;
        /// <summary>
        /// 计划对象
        /// </summary>
        private Plan Plan { get; set; }
        /// <summary>
        /// 键值
        /// </summary>
        public virtual String Key
        {
            get
            {
                return Plan.Id;
            }
        }
        /// <summary>
        /// 计划时间
        /// </summary>
        public virtual PlanTime PlannedTime
        {
            get
            {
                return Plan.PlannedTime;
            }
        }
        /// <summary>
        /// 节目内容
        /// </summary>
        public virtual String Content
        {
            get
            {
                return Plan.Content;
            }
            set
            {
                Plan.Content = value;
            }
        }
        #region IDisposable 成员
        void IDisposable.Dispose()
        {
            if (Plan != null)
            {
                Plan.Reminding -= new EventHandler<EventArgs>(Plan_Reminding);
                Plan.Dispose();
                Plan = null;
            }
        }
        #endregion
    }
}
