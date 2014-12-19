using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Howell.Schedules
{
    /// <summary>
    /// 星期几的工作表项开始提示事件参数
    /// </summary>
    public class DayOfWeekWorkSheetItemBeginningEventArgs : EventArgs
    {
        /// <summary>
        /// 创建  Howell.Schedules.DayOfWeekWorkSheetItemBeginningEventArgs 对象
        /// </summary>
        /// <param name="index">项的索引值</param>
        /// <param name="dayOfWeek">星期几</param>
        public DayOfWeekWorkSheetItemBeginningEventArgs(Int32 index,DayOfWeek dayOfWeek)
        {
            this.Index = index;
            this.DayOfWeek = dayOfWeek;
        }
        /// <summary>
        /// 项的索引值
        /// </summary>
        public Int32 Index { get; private set; }
        /// <summary>
        /// 星期几
        /// </summary>
        public DayOfWeek DayOfWeek { get; private set;  }
    }
    /// <summary>
    /// 星期几的工作表项结束提示事件参数
    /// </summary>
    public class DayOfWeekWorkSheetItemEndingEventArgs : EventArgs
    {
        /// <summary>
        /// 创建  Howell.Schedules.DayOfWeekWorkSheetItemEndingEventArgs 对象
        /// </summary>
        /// <param name="index">项的索引值</param>
        /// <param name="dayOfWeek">星期几</param>
        public DayOfWeekWorkSheetItemEndingEventArgs(Int32 index, DayOfWeek dayOfWeek)
        {
            this.Index = index;
            this.DayOfWeek = dayOfWeek;
        }
        /// <summary>
        /// 项的索引值
        /// </summary>
        public Int32 Index { get; private set; }
        /// <summary>
        /// 星期几
        /// </summary>
        public DayOfWeek DayOfWeek { get; private set; }
    }
    /// <summary>
    /// 指定星期几的工作表
    /// </summary>
    public class DayOfWeekWorkSheet : IDisposable
    {
        /// <summary>
        /// 创建 Howell.Schedules.DayOfWeekWorkSheet 对象
        /// </summary>
        /// <param name="dayOfWeek">星期几</param>
        public DayOfWeekWorkSheet(DayOfWeek dayOfWeek)
        {
            this.AllowConflict = false;
            this.DayOfWeek = dayOfWeek;
            this.Items = new DayOfWeekWorkSheetItemCollection(this);
            this.Items.DayOfWeekWorkSheetItemBeginning += new EventHandler<DayOfWeekWorkSheetItemBeginningEventArgs>(Items_DayOfWeekWorkSheetItemBeginning);
            this.Items.DayOfWeekWorkSheetItemEnding += new EventHandler<DayOfWeekWorkSheetItemEndingEventArgs>(Items_DayOfWeekWorkSheetItemEnding);
        }
        void Items_DayOfWeekWorkSheetItemBeginning(object sender, DayOfWeekWorkSheetItemBeginningEventArgs e)
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
        void Items_DayOfWeekWorkSheetItemEnding(object sender, DayOfWeekWorkSheetItemEndingEventArgs e)
        {
            try
            {
                EventHandler<DayOfWeekWorkSheetItemEndingEventArgs> eventHandler = DayOfWeekWorkSheetItemEnding;
                if(eventHandler != null)
                {
                    eventHandler(this, e);
                }
            }catch {}
        }
        /// <summary>
        /// 开始提示事件
        /// </summary>
        internal event EventHandler<DayOfWeekWorkSheetItemBeginningEventArgs> DayOfWeekWorkSheetItemBeginning;
        /// <summary>
        /// 结束提示事件
        /// </summary>
        internal event EventHandler<DayOfWeekWorkSheetItemEndingEventArgs> DayOfWeekWorkSheetItemEnding;
        /// <summary>
        /// 是否允许时间段冲突，默认是 false即不允许。
        /// </summary>
        internal Boolean AllowConflict { get; set; }
        /// <summary>
        /// 星期几
        /// </summary>
        public DayOfWeek DayOfWeek { get; private set; }
        /// <summary>
        /// 获取包含控件中所有项的集合。
        /// </summary>
        public DayOfWeekWorkSheetItemCollection Items { get; private set; }        
        #region IDisposable 成员
        /// <summary>
        /// 销毁 Howell.Schedules.DayOfWeekWorkSheet 对象
        /// </summary>
        public void Dispose()
        {
            this.Items.Clear();
        }
        #endregion
        /// <summary>
        /// 星期几的工作表项容器
        /// </summary>
        public class DayOfWeekWorkSheetItemCollection : IEnumerable<DayOfWeekWorkSheetItem>
        {
            private DayOfWeekWorkSheet m_Owner = null;
            private List<DayOfWeekWorkSheetItem> m_Items = new List<DayOfWeekWorkSheetItem>();
            /// <summary>
            /// 创建工作表项容器
            /// </summary>
            /// <param name="owner">容器拥有者</param>
            public DayOfWeekWorkSheetItemCollection(DayOfWeekWorkSheet owner)
            {
                m_Owner = owner;
            }
            /// <summary>
            /// 将现有的 Howell.Schedules.DayOfWeekWorkSheetItem 添加到集合中。
            /// </summary>
            /// <param name="beginTime">开始时间</param>
            /// <param name="endTime">结束时间</param>
            /// <returns>已添加到集合中的 Howell.Schedules.DayOfWeekWorkSheetItem。</returns>
            public virtual DayOfWeekWorkSheetItem Add(DateTime beginTime, DateTime endTime)
            {
                return this.Add(beginTime.Hour, beginTime.Minute, beginTime.Second, endTime.Hour, endTime.Minute, endTime.Second);
            }
            /// <summary>
            /// 将现有的 Howell.Schedules.DayOfWeekWorkSheetItem 添加到集合中。
            /// </summary>
            /// <param name="beginTime">开始时间</param>
            /// <param name="endTime">结束时间</param>
            /// <param name="enabled">是否启用</param>
            /// <returns>已添加到集合中的 Howell.Schedules.DayOfWeekWorkSheetItem。</returns>
            public virtual DayOfWeekWorkSheetItem Add(DateTime beginTime, DateTime endTime, Boolean enabled)
            {
                return this.Add(beginTime.Hour, beginTime.Minute, beginTime.Second, endTime.Hour, endTime.Minute, endTime.Second, enabled);
            }
            /// <summary>
            ///  将现有的 Howell.Schedules.DayOfWeekWorkSheetItem 添加到集合中。
            /// </summary>
            /// <param name="beginHour">开始时间的小时值</param>
            /// <param name="beginMinute">开始时间的分钟值</param>
            /// <param name="beginSecond">开始时间的秒值</param>
            /// <param name="endHour">结束时间的小时值</param>
            /// <param name="endMinute">结束时间的分钟值</param>
            /// <param name="endSecond">结束时间的秒值</param>
            /// <returns>已添加到集合中的 Howell.Schedules.DayOfWeekWorkSheetItem。</returns>
            public virtual DayOfWeekWorkSheetItem Add(Int32 beginHour,Int32 beginMinute,Int32 beginSecond,Int32 endHour,Int32 endMinute,Int32 endSecond)
            {
                return this.Add(beginHour, beginMinute, beginSecond, endHour, endMinute, endSecond, true);
            }
            /// <summary>
            /// 将现有的 Howell.Schedules.DayOfWeekWorkSheetItem 添加到集合中。
            /// </summary>
            /// <param name="beginHour">开始时间的小时值</param>
            /// <param name="beginMinute">开始时间的分钟值</param>
            /// <param name="beginSecond">开始时间的秒值</param>
            /// <param name="endHour">结束时间的小时值</param>
            /// <param name="endMinute">结束时间的分钟值</param>
            /// <param name="endSecond">结束时间的秒值</param>
            /// <param name="enabled">是否启用</param>
            /// <returns>已添加到集合中的 Howell.Schedules.DayOfWeekWorkSheetItem。</returns>
            public virtual DayOfWeekWorkSheetItem Add(Int32 beginHour, Int32 beginMinute, Int32 beginSecond, Int32 endHour, Int32 endMinute, Int32 endSecond, Boolean enabled)
            {
                return this.Add(beginHour, beginMinute, beginSecond, endHour, endMinute, endSecond, enabled, String.Empty);
            }
            /// <summary>
            /// 将现有的 Howell.Schedules.DayOfWeekWorkSheetItem 添加到集合中。
            /// </summary>
            /// <param name="beginHour">开始时间的小时值</param>
            /// <param name="beginMinute">开始时间的分钟值</param>
            /// <param name="beginSecond">开始时间的秒值</param>
            /// <param name="endHour">结束时间的小时值</param>
            /// <param name="endMinute">结束时间的分钟值</param>
            /// <param name="endSecond">结束时间的秒值</param>
            /// <param name="enabled">是否启用</param>
            /// <param name="content">项内容</param>
            /// <returns>已添加到集合中的 Howell.Schedules.DayOfWeekWorkSheetItem。</returns>
            public virtual DayOfWeekWorkSheetItem Add(Int32 beginHour, Int32 beginMinute, Int32 beginSecond, Int32 endHour, Int32 endMinute, Int32 endSecond, Boolean enabled, String content)
            {
                lock (this)
                {
                    if (m_Owner.AllowConflict == false && IsConflict(beginHour, beginMinute, beginSecond, endHour, endMinute, endSecond) == true)
                    {
                        throw new InvalidOperationException("Not allow time conflict.");
                    }
                    DayOfWeekWorkSheetItem item = new DayOfWeekWorkSheetItem(beginHour, beginMinute, beginSecond, endHour, endMinute, endSecond, m_Owner.DayOfWeek);
                    m_Items.Add(item);
                    item.Beginning += new EventHandler<EventArgs>(Item_Beginning);
                    item.Ending += new EventHandler<EventArgs>(Item_Ending);
                    item.Content = content;
                    item.Enabled = enabled;
                    return item;
                }
            }
            /// <summary>
            /// 从集合中移除指定的项。
            /// </summary>
            /// <param name="item">Howell.Schedules.DayOfWeekWorkSheetItem，表示要从集合中移除的项。</param>
            public virtual void Remove(DayOfWeekWorkSheetItem item)
            {
                RemoveAt(IndexOf(item));
            }
            /// <summary>
            /// 移除集合中指定索引处的项。
            /// </summary>
            /// <param name="index">从零开始的索引（属于要移除的项）。</param>
            /// <exception cref="System.ArgumentOutOfRangeException">
            /// index 参数小于零或大于等于 Howell.Schedules.DayOfWeekWorkSheet.DayOfWeekWorkSheetItemCollection 的 Howell.Schedules.DayOfWeekWorkSheet.DayOfWeekWorkSheetItemCollection.Count
            /// </exception>
            public virtual void RemoveAt(int index)
            {
                lock (this)
                {
                    if (IsValidIndex(index) == false) throw new System.ArgumentOutOfRangeException("index", "Argument index is less than 0 or lager than DayOfWeekWorkSheetItemCollection.Count.");
                    DayOfWeekWorkSheetItem item = m_Items[index];
                    m_Items.RemoveAt(index);
                    item.Beginning -= new EventHandler<EventArgs>(Item_Beginning);
                    item.Ending -= new EventHandler<EventArgs>(Item_Ending);
                    (item as IDisposable).Dispose();
                }
            }
            /// <summary>
            /// 从集合中移除所有项。
            /// </summary>
            public virtual void Clear()
            {
                lock(this)
                {
                    while (m_Items.Count > 0)
                    {
                        RemoveAt(0);
                    }
                }
            }
            /// <summary>
            /// 集合中元素的数量
            /// </summary>
            public Int32 Count
            {
                get
                {
                    lock (this)
                    {
                        return m_Items.Count;
                    }
                }
            }
            /// <summary>
            /// 返回指定的项在集合中的索引。
            /// </summary>
            /// <param name="item">Howell.Schedules.DayOfWeekWorkSheetItem，表示要在集合中查找的项。</param>
            /// <returns>项在集合中的位置的从零开始的索引；如果项不在集合中，则为 -1。</returns>
            public int IndexOf(DayOfWeekWorkSheetItem item)
            {
                lock(this)
                {
                    for(Int32 i = 0;i < m_Items.Count;++i)
                    {
                        if(m_Items[i].Id == item.Id)
                        {
                            return i;
                        }
                    }
                }
                return -1;
            }
            /// <summary>
            /// 确定指定项是否位于集合内。
            /// </summary>
            /// <param name="item">Howell.Schedules.DayOfWeekWorkSheetItem，表示要在集合中查找的项。</param>
            /// <returns>如果集合中包含该项，则为 true；否则为 false。</returns>
            public Boolean Contains(DayOfWeekWorkSheetItem item)
            {
                return IsValidIndex(IndexOf(item));
            }
            /// <summary>
            /// 判断时间段是否和集合内的时间段有冲突
            /// </summary>
            /// <param name="beginTime">需要判断的时间段开始时间</param>
            /// <param name="endTime">需要判断的时间段结束时间</param>
            /// <returns>如果和集合内的时间段有冲突则返回true，否则返回false。</returns>
            public Boolean IsConflict(DateTime beginTime,DateTime endTime)
            {
                return IsConflict(beginTime.Hour, beginTime.Minute, beginTime.Second, endTime.Hour, endTime.Minute, endTime.Second);
            }
            /// <summary>
            /// 判断时间段是否和集合内的时间段有冲突
            /// </summary>
            /// <param name="beginHour">需要判断的时间段开始时间的小时值</param>
            /// <param name="beginMinute">需要判断的时间段开始时间的分钟值</param>
            /// <param name="beginSecond">需要判断的时间段开始时间的秒值</param>
            /// <param name="endHour">需要判断的时间段结束时间的小时值</param>
            /// <param name="endMinute">需要判断的时间段结束时间的分钟值</param>
            /// <param name="endSecond">需要判断的时间段结束时间的秒值</param>
            /// <returns>如果和集合内的时间段有冲突则返回true，否则返回false。</returns>
            public Boolean IsConflict(Int32 beginHour, Int32 beginMinute, Int32 beginSecond, Int32 endHour, Int32 endMinute, Int32 endSecond)
            {
                PlanTime beginTime = new PlanTime(beginHour, beginMinute, beginSecond,m_Owner.DayOfWeek);
                PlanTime endTime = new PlanTime(endHour, endMinute, endSecond, m_Owner.DayOfWeek);
                for (int i = 0; i < m_Items.Count; ++i)
                {
                    if (beginTime.IsInRange(m_Items[i].BeginTime, m_Items[i].EndTime) || endTime.IsInRange(m_Items[i].BeginTime, m_Items[i].EndTime))
                    {
                        return true;
                    }
                }
                return false;
            }
            /// <summary>
            /// 获取集合中指定索引处的项。
            /// </summary>
            /// <param name="index">集合中要获取或设置的项的索引。</param>
            /// <returns>Howell.Schedules.DayOfWeekWorkSheetItem，表示位于集合内指定索引处的项。</returns>
            public virtual DayOfWeekWorkSheetItem this[int index]
            {
                get
                {
                    lock(this)
                    {
                        return m_Items[index];
                    }
                }
            }
            #region IEnumerable<DayOfWeekWorkSheetItem> 成员
            /// <summary>
            /// 返回一个枚举数，将使用该枚举数循环访问项集合。
            /// </summary>
            /// <returns> System.Collections.IEnumerator，表示项集合。</returns>
            public IEnumerator<DayOfWeekWorkSheetItem> GetEnumerator()
            {
                lock (this)
                {
                    return m_Items.GetEnumerator();
                }
            }
            #endregion
            #region IEnumerable 成员

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            #endregion
            /// <summary>
            /// 开始提示事件
            /// </summary>
            internal event EventHandler<DayOfWeekWorkSheetItemBeginningEventArgs> DayOfWeekWorkSheetItemBeginning;
            /// <summary>
            /// 结束提示事件
            /// </summary>
            internal event EventHandler<DayOfWeekWorkSheetItemEndingEventArgs> DayOfWeekWorkSheetItemEnding;
            void Item_Beginning(object sender, EventArgs e)
            {
                try
                {
                    EventHandler<DayOfWeekWorkSheetItemBeginningEventArgs> eventHandler = DayOfWeekWorkSheetItemBeginning;
                    if (eventHandler != null)
                    {
                        eventHandler(this, new DayOfWeekWorkSheetItemBeginningEventArgs(IndexOf(sender as DayOfWeekWorkSheetItem), m_Owner.DayOfWeek));
                    }
                }
                catch { }
            }
            void Item_Ending(object sender, EventArgs e)
            {
                try
                {
                    EventHandler<DayOfWeekWorkSheetItemEndingEventArgs> eventHandler = DayOfWeekWorkSheetItemEnding;
                    if (eventHandler != null)
                    {
                        eventHandler(this, new DayOfWeekWorkSheetItemEndingEventArgs(IndexOf(sender as DayOfWeekWorkSheetItem), m_Owner.DayOfWeek));
                    }
                }
                catch { }
            }
            private bool IsValidIndex(Int32 index)
            {
                if (index < 0 || index >= m_Items.Count) return false;
                return true;
            }
        }

    }
}
