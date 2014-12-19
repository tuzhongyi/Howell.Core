using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Howell.Schedules
{
    /// <summary>
    /// 周工作表对象
    /// </summary>
    public class WeeklyWorkSheet : IDisposable
    {
        private List<DayOfWeekWorkSheet> m_Items = new List<DayOfWeekWorkSheet>();
        private Boolean m_AllowConflict = false;
        /// <summary>
        /// 创建 Howell.Schedules.WeeklyWorkSheet 对象
        /// </summary>
        public WeeklyWorkSheet()
        {            
            this.Sunday = new DayOfWeekWorkSheet(DayOfWeek.Sunday);
            this.Monday = new DayOfWeekWorkSheet(DayOfWeek.Monday);
            this.Tuesday = new DayOfWeekWorkSheet(DayOfWeek.Tuesday);
            this.Wednesday = new DayOfWeekWorkSheet(DayOfWeek.Wednesday);
            this.Thursday = new DayOfWeekWorkSheet(DayOfWeek.Thursday);
            this.Friday = new DayOfWeekWorkSheet(DayOfWeek.Friday);
            this.Saturday = new DayOfWeekWorkSheet(DayOfWeek.Saturday);
            m_Items.Add(this.Sunday);
            m_Items.Add(this.Monday);
            m_Items.Add(this.Tuesday);
            m_Items.Add(this.Wednesday);
            m_Items.Add(this.Thursday);
            m_Items.Add(this.Friday);
            m_Items.Add(this.Saturday);
            for(int i = 0;i < m_Items.Count;++i)
            {
                m_Items[i].DayOfWeekWorkSheetItemBeginning += new EventHandler<DayOfWeekWorkSheetItemBeginningEventArgs>(WeeklyWorkSheet_DayOfWeekWorkSheetItemBeginning);
                m_Items[i].DayOfWeekWorkSheetItemEnding += new EventHandler<DayOfWeekWorkSheetItemEndingEventArgs>(WeeklyWorkSheet_DayOfWeekWorkSheetItemEnding);
            }
        }
        void WeeklyWorkSheet_DayOfWeekWorkSheetItemEnding(object sender, DayOfWeekWorkSheetItemEndingEventArgs e)
        {
            try
            {
                EventHandler<DayOfWeekWorkSheetItemEndingEventArgs> eventHandler = DayOfWeekWorkSheetItemEnding;
                if (eventHandler != null)
                {
                    eventHandler(this, e);
                }
            }
            catch { }
        }
        void WeeklyWorkSheet_DayOfWeekWorkSheetItemBeginning(object sender, DayOfWeekWorkSheetItemBeginningEventArgs e)
        {
            try
            {
                EventHandler<DayOfWeekWorkSheetItemBeginningEventArgs> eventHandler = DayOfWeekWorkSheetItemBeginning;
                if (eventHandler != null)
                {
                    eventHandler(this, e);
                }
            }
            catch { }
        }
        /// <summary>
        /// 是否允许时间段冲突，默认是 false即不允许。
        /// </summary>
        public Boolean AllowConflict
        {
            get
            {
                return m_AllowConflict;
            }
            set
            {                
                m_AllowConflict = value;
                foreach (var index in Enum.GetValues(typeof(DayOfWeek)))
                {
                    this[(Int32)index].AllowConflict = value;
                }
            }
        }
        /// <summary>
        /// 星期日的工作表项信息
        /// </summary>
        public DayOfWeekWorkSheet Sunday { get; private set; }
        /// <summary>
        /// 星期一的工作表项信息
        /// </summary>
        public DayOfWeekWorkSheet Monday { get; private set; }
        /// <summary>
        /// 星期二的工作表项信息
        /// </summary>
        public DayOfWeekWorkSheet Tuesday { get; private set; }
        /// <summary>
        /// 星期三的工作表项信息
        /// </summary>
        public DayOfWeekWorkSheet Wednesday { get; private set; }
        /// <summary>
        /// 星期四的工作表项信息
        /// </summary>
        public DayOfWeekWorkSheet Thursday { get; private set; }
        /// <summary>
        /// 星期五的工作表项信息
        /// </summary>
        public DayOfWeekWorkSheet Friday { get; private set; }
        /// <summary>
        /// 星期六的工作表项信息
        /// </summary>
        public DayOfWeekWorkSheet Saturday { get; private set; }
        /// <summary>
        /// 获取星期几的工作表项值
        /// </summary>
        /// <param name="index">获取项的的项的索引，该值与System.DayOfWeek的枚举值相同</param>
        /// <returns>返回对应一周内某一天的工作表项信息。</returns>
        public DayOfWeekWorkSheet this[Int32 index]
        {
            get
            {
                lock(this)
                {
                    return m_Items[index];
                }
            }
        }
        /// <summary>
        /// 获取星期几的工作表项值
        /// </summary>
        /// <param name="dayOfWeek">获取项的的项的索引，该值与System.DayOfWeek的枚举值相同</param>
        /// <returns>返回对应一周内某一天的工作表项信息。</returns>
        public DayOfWeekWorkSheet this[DayOfWeek dayOfWeek]
        {
            get
            {
                lock(this)
                {
                    return m_Items[(Int32)dayOfWeek];
                }
            }
        }
        /// <summary>
        /// 开始提示事件
        /// </summary>
        public event EventHandler<DayOfWeekWorkSheetItemBeginningEventArgs> DayOfWeekWorkSheetItemBeginning;
        /// <summary>
        /// 结束提示事件
        /// </summary>
        public event EventHandler<DayOfWeekWorkSheetItemEndingEventArgs> DayOfWeekWorkSheetItemEnding;
        /// <summary>
        /// 将一周内的某一天的工作表项，拷贝到一周内的其他日期中。
        /// </summary>
        /// <param name="dayOfWeek">星期几</param>
        public void CopyToAll(DayOfWeek dayOfWeek)
        {
            lock (this)
            {
                DayOfWeekWorkSheet ws = this[(Int32)dayOfWeek];
                var items = (from i in ws.Items
                            select new { i.BeginTime, i.EndTime, i.Enabled, i.Content, i.Id }).ToArray();
                
                for (int i = 0; i < m_Items.Count; ++i)
                {
                    m_Items[i].Items.Clear();
                    
                    foreach (var it in items)
                    {
                        m_Items[i].Items.Add(it.BeginTime.Hour, it.BeginTime.Minute, it.BeginTime.Second, it.EndTime.Hour, it.EndTime.Minute, it.EndTime.Second, it.Enabled, it.Content);
                    }
                }
            }
        }
        #region IDisposable 成员
        /// <summary>
        /// 销毁 Howell.Schedules.WeeklyWorkSheet 对象
        /// </summary>
        public void Dispose()
        {
            lock (this)
            {
                foreach (var item in m_Items)
                {
                    item.Dispose();
                }
            }
        }
        #endregion
    }
    
}
