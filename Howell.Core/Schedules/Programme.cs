using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections;

namespace Howell.Schedules
{
    /// <summary>
    /// 节目表计划提醒事件参数对象
    /// </summary>
    public class ProgrammeRemindingEventArgs : EventArgs
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="item"></param>
        public ProgrammeRemindingEventArgs(ProgrammeItem item)
            : base()
        {
            this.Item = item;
        }
        /// <summary>
        /// 计划信息
        /// </summary>
        public ProgrammeItem Item { get; private set; }
    }
    /// <summary>
    /// 节目表
    /// </summary>
    public class Programme : IDisposable
    {
        /// <summary>
        /// 创建Programme对象
        /// </summary>
        public Programme()
        {
            Items = new ProgrammeItemCollection(this);
            Items.ProgrammeItemReminding += new EventHandler<ProgrammeRemindingEventArgs>(Items_Reminding);
        }

        void Items_Reminding(object sender, ProgrammeRemindingEventArgs e)
        {
            EventHandler<ProgrammeRemindingEventArgs> eventHandler = Reminding;
            if(eventHandler != null)
            {
                eventHandler(this, e);
            }
        }
  
        /// <summary>
        /// 获取包含节目表中所有项的集合。
        /// </summary>
        public ProgrammeItemCollection Items { get; private set; }

        /// <summary>
        /// 根据参考日期获取当天的计划信息
        /// </summary>
        /// <param name="date">参考日期</param>
        /// <returns>返回计划信息列表</returns>
        public ReadOnlyCollection<ProgrammeItem> GetDateProgrammeItems(DateTime date)
        {
            SortedList<DateTime, ProgrammeItem> result = new SortedList<DateTime, ProgrammeItem>();
            lock (this)
            {
                foreach (var p in Items)
                {
                    DateTime plannedTime = p.PlannedTime.ToDateTime(date);
                    if (plannedTime.Date == date.Date)
                    {
                        result.Add(plannedTime, p);
                    }
                }
            }
            return result.Values.ToList().AsReadOnly();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Plan_Reminding(object sender, EventArgs e)
        {
            try
            {
                EventHandler<ProgrammeRemindingEventArgs> eventHandler = Reminding;
                if (eventHandler != null)
                {
                    eventHandler(this, new ProgrammeRemindingEventArgs(sender as ProgrammeItem));
                }
            }
            catch
            {
            }
        }
        /// <summary>
        /// 节目表计划提醒事件
        /// </summary>
        public event EventHandler<ProgrammeRemindingEventArgs> Reminding;
        #region IDisposable 成员
        /// <summary>
        /// 释放由 Programme 占用的非托管资源，还可以另外再释放托管资源。
        /// </summary>
        public void Dispose()
        {
            Items.Clear();
        }
        #endregion

        /// <summary>
        /// 节目表子项的集合
        /// </summary>
        public class ProgrammeItemCollection : ICollection<ProgrammeItem>, IEnumerable<ProgrammeItem>
        {
            private List<ProgrammeItem> m_InnerList = new List<ProgrammeItem>();
            private Programme m_Owner = null;
            /// <summary>
            /// 
            /// </summary>
            /// <param name="owner"></param>
            public ProgrammeItemCollection(Programme owner)
            {
                m_Owner = owner;
            }
            /// <summary>
            ///  将现有的Howell.Schedules.ProgrammeItem 添加到集合中。
            /// </summary>
            /// <param name="item">要添加到集合中的 Howell.Schedules.ProgrammeItem。</param>
            /// <returns>已添加到集合中的 Howell.Schedules.ProgrammeItem。</returns>
            public virtual ProgrammeItem Add(ProgrammeItem item)
            {
                item.ProgrammeItemReminding += new EventHandler<EventArgs>(ProgrammeItem_Reminding);
                m_InnerList.Add(item);
                return item;
            }
            /// <summary>
            /// 将现有的Howell.Schedules.ProgrammeItem 添加到集合中。
            /// </summary>
            /// <param name="key">键值</param>
            /// <param name="time">节目时间</param>
            /// <param name="content">节目内容</param>
            /// <returns>已添加到集合中的 Howell.Schedules.ProgrammeItem。</returns>
            public virtual ProgrammeItem Add(String key, PlanTime time, String content)
            {
                lock (this)
                {
                    if (ContansKey(key) == true)
                        throw new System.InvalidOperationException(String.Format("Key {0} has already contained.", key));
                    ProgrammeItem item = new ProgrammeItem(key, time, content);
                    return this.Add(item);
                }
            }
            /// <summary>
            /// 从集合中移除指定的项。
            /// </summary>
            /// <param name="item">Howell.Schedules.ProgrammeItem，表示要从集合中移除的项。</param>
            /// <returns>如果成功移除 item，则为 true；否则为 false。如果在集合中没有找到item，该方法也会返回 false。</returns>
            public virtual Boolean Remove(ProgrammeItem item)
            {
                return RemoveByKey(item.Key);
            }
            /// <summary>
            /// 从集合中移除具有指定键的项。
            /// </summary>
            /// <param name="key">要从集合中移除的项的名称。</param>
            /// <returns>如果成功移除 item，则为 true；否则为 false。如果在集合中没有找到item，该方法也会返回 false。</returns>
            public virtual Boolean RemoveByKey(String key)
            {
                lock (this)
                {
                    for (int i = 0; i < m_InnerList.Count; ++i)
                    {
                        if (m_InnerList[i].Key == key)
                        {
                            m_InnerList[i].ProgrammeItemReminding -= new EventHandler<EventArgs>(ProgrammeItem_Reminding);
                            (m_InnerList[i] as IDisposable).Dispose();
                            m_InnerList.RemoveAt(i);
                            return true;
                        }
                    }
                }
                return false;
            }
            /// <summary>
            /// 从集合中移除所有项。
            /// </summary>
            public virtual void Clear()
            {
                lock (this)
                {
                    String[] keys = m_InnerList.Select(x => x.Key).ToArray();
                    foreach (var key in keys)
                    {
                        RemoveByKey(key);
                    }
                }
            }
            /// <summary>
            /// 确定指定项是否位于集合内。
            /// </summary>
            /// <param name="item"> Howell.Schedules.ProgrammeItem，表示要在集合中查找的项。</param>
            /// <returns>如果集合中包含该项，则为 true；否则为 false。</returns>
            public bool Contains(ProgrammeItem item)
            {
                lock (this)
                {
                    return m_InnerList.Contains(item);
                }
            }
            /// <summary>
            /// 确定集合是否包含具有指定键的项。
            /// </summary>
            /// <param name="key">要搜索的项的名称。</param>
            /// <returns>如果集合包含具有指定键的项，则为 true；否则为 false。</returns>
            public bool ContansKey(String key)
            {
                lock (this)
                {
                    for (int i = 0; i < m_InnerList.Count; ++i)
                    {
                        if (m_InnerList[i].Key == key)
                            return true;
                    }
                    return false;
                }
            }
            /// <summary>
            ///  检索具有指定键的项。
            /// </summary>
            /// <param name="key">要检索的项的名称。</param>
            /// <returns>Howell.Schedules.ProgrammeItem，其 Howell.Schedules.ProgrammeItem与指定键匹配。</returns>
            public virtual ProgrammeItem this[String key]
            {
                get
                {
                    lock (this)
                    {
                        for (int i = 0; i < m_InnerList.Count; ++i)
                        {
                            if (m_InnerList[i].Key == key)
                            {
                                return m_InnerList[i];
                            }
                        }
                        throw new ArgumentException(String.Format("Howell.Schedules.ProgrammeItemCollection does not contain key {0}.", key));
                    }
                }
            }
            /// <summary>
            /// 获取集合中项的数目。
            /// </summary>
            public int Count
            {
                get
                {
                    return m_InnerList.Count;
                }
            }
            /// <summary>
            /// 获取一个值，该值指示集合是否为只读。
            /// </summary>
            public bool IsReadOnly
            {
                get { return false; }
            }

            #region ICollection<ProgrammeItem> 成员
            /// <summary>
            /// Add
            /// </summary>
            /// <param name="item"></param>
            void ICollection<ProgrammeItem>.Add(ProgrammeItem item)
            {
                this.Add(item);
            }
            /// <summary>
            ///  将整个集合复制到现有数组中，从该数组内的指定位置开始复制。
            /// </summary>
            /// <param name="array">表示要将该集合的内容复制到的数组。</param>
            /// <param name="arrayIndex">集合中的项将复制到的目标数组中的位置。</param>
            public void CopyTo(ProgrammeItem[] array, int arrayIndex)
            {
                lock (this)
                {
                    m_InnerList.CopyTo(array, arrayIndex);
                }
            }
            /// <summary>
            /// Remove
            /// </summary>
            /// <param name="item"></param>
            /// <returns></returns>
            bool ICollection<ProgrammeItem>.Remove(ProgrammeItem item)
            {
                return this.Remove(item);
            }

            #endregion

            #region IEnumerable<ProgrammeItem> 成员
            /// <summary>
            /// 返回可用于循环访问选定索引集合的枚举数。
            /// </summary>
            /// <returns>一个表示选定的索引集合的 System.Collections.IEnumerator。</returns>
            public IEnumerator<ProgrammeItem> GetEnumerator()
            {
                lock (this)
                {
                    return m_InnerList.GetEnumerator();
                }
            }

            #endregion

            #region IEnumerable 成员
            /// <summary>
            /// GetEnumerator
            /// </summary>
            /// <returns></returns>
            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }
            #endregion

            /// <summary>
            /// 节目表计划提醒事件
            /// </summary>
            internal event EventHandler<ProgrammeRemindingEventArgs> ProgrammeItemReminding;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void ProgrammeItem_Reminding(object sender, EventArgs e)
            {
                try
                {
                    EventHandler<ProgrammeRemindingEventArgs> eventHandler = ProgrammeItemReminding;
                    if (eventHandler != null)
                        eventHandler(this, new ProgrammeRemindingEventArgs(sender as ProgrammeItem));
                }
                catch { }
            }
        }
    }
}
