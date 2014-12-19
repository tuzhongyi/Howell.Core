using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections;

namespace Howell.Schedules
{
    /// <summary>
    /// 日重复工作表
    /// </summary>
    public class WorkSheet : IDisposable
    {
        /// <summary>
        /// 创建Programme对象
        /// </summary>
        public WorkSheet()
        {
            Items = new WorkSheetItemCollection(this);
            Items.WorkSheetItemBeginning += new EventHandler<WorkSheetItemBeginningEventArgs>(Items_WorkSheetItemBeginning);
            Items.WorkSheetItemEnding += new EventHandler<WorkSheetItemEndingEventArgs>(Items_WorkSheetItemEnding);
        }
        void Items_WorkSheetItemEnding(object sender, WorkSheetItemEndingEventArgs e)
        {
            try
            {
                EventHandler<WorkSheetItemEndingEventArgs> eventHandler = WorkSheetItemEnding;
                if (eventHandler != null)
                {
                    eventHandler(this, e);
                }
            }catch {}
        }
        void Items_WorkSheetItemBeginning(object sender, WorkSheetItemBeginningEventArgs e)
        {
            try
            {
                EventHandler<WorkSheetItemBeginningEventArgs> eventHandler = WorkSheetItemBeginning;
                if (eventHandler != null)
                {
                    eventHandler(this, e);
                }
            }
            catch { }
        }  
        /// <summary>
        /// 获取包含节目表中所有项的集合。
        /// </summary>
        public WorkSheetItemCollection Items { get; private set; }
        /// <summary>
        /// 工作表项开始提醒事件
        /// </summary>
        public event EventHandler<WorkSheetItemBeginningEventArgs> WorkSheetItemBeginning;
        /// <summary>
        /// 工作表项结束提醒事件
        /// </summary>
        public event EventHandler<WorkSheetItemEndingEventArgs> WorkSheetItemEnding;
        #region IDisposable 成员
        /// <summary>
        /// 释放由 WorkSheet 占用的非托管资源，还可以另外再释放托管资源。
        /// </summary>
        public void Dispose()
        {
            Items.Clear();
        }
        #endregion

        /// <summary>
        /// 工作表项的集合
        /// </summary>
        public class WorkSheetItemCollection : IList<WorkSheetItem>
        {
            private List<WorkSheetItem> m_Items = new List<WorkSheetItem>();
            private WorkSheet m_Owner = null;
            /// <summary>
            /// 构造
            /// </summary>
            /// <param name="owner"></param>
            public WorkSheetItemCollection(WorkSheet owner)
            {
                m_Owner = owner;
            }
            void WorkSheetItem_Ending(object sender, EventArgs e)
            {
                try
                {
                    EventHandler<WorkSheetItemEndingEventArgs> eventHandler = WorkSheetItemEnding;
                    if (eventHandler != null)
                    {
                        eventHandler(this, new WorkSheetItemEndingEventArgs(IndexOf(sender as WorkSheetItem)));
                    }
                }
                catch { }
            }
            void WorkSheetItem_Beginning(object sender, EventArgs e)
            {

                try
                {
                    EventHandler<WorkSheetItemBeginningEventArgs> eventHandler = WorkSheetItemBeginning;
                    if (eventHandler != null)
                    {
                        eventHandler(this, new WorkSheetItemBeginningEventArgs(IndexOf(sender as WorkSheetItem)));
                    }
                }
                catch { }
            }
            #region IList<WorkSheetItem> 成员
            /// <summary>
            /// 返回指定的项在集合中的索引
            /// </summary>
            /// <param name="item">Howell.Schedules.WorkSheetItem，表示要在集合中查找的项。</param>
            /// <returns>项在集合中的位置的从零开始的索引；如果项不在集合中，则为 -1。</returns>
            public int IndexOf(WorkSheetItem item)
            {
                lock (this)
                {
                    for (int i = 0; i < m_Items.Count; ++i)
                    {
                        if (m_Items[i].Id == item.Id)
                        {
                            return i;
                        }
                    }
                    return -1;
                }
            }
            /// <summary>
            /// 将现有的 Howell.Schedules.WorkSheetItem 插入到集合中的指定索引处。
            /// </summary>
            /// <param name="index">插入项的从零开始的索引位置。</param>
            /// <param name="item">Howell.Schedules.WorkSheetItem，表示要插入的项。</param>
            public void Insert(int index, WorkSheetItem item)
            {
                lock (this)
                {
                    m_Items.Insert(index, item);                    
                    item.Beginning += new EventHandler<EventArgs>(WorkSheetItem_Beginning);
                    item.Ending += new EventHandler<EventArgs>(WorkSheetItem_Ending);
                }
            }
            /// <summary>
            /// 移除集合中指定索引处的项。
            /// </summary>
            /// <param name="index">从零开始的索引（属于要移除的项）。</param>
            public void RemoveAt(int index)
            {
                lock (this)
                {
                    if (IsValidIndex(index) == false) return;
                    WorkSheetItem item = m_Items[index];
                    m_Items.RemoveAt(index);
                    item.Beginning -= new EventHandler<EventArgs>(WorkSheetItem_Beginning);
                    item.Ending -= new EventHandler<EventArgs>(WorkSheetItem_Ending);
                    if (item is IDisposable)
                    {
                        (item as IDisposable).Dispose();
                    }
                }
            }
            /// <summary>
            /// 获取或设置集合中指定索引处的项。
            /// </summary>
            /// <param name="index">集合中要获取或设置的项的索引。</param>
            /// <returns>Howell.Schedules.WorkSheetItem，表示位于集合内指定索引处的项。</returns>
            public WorkSheetItem this[int index]
            {
                get
                {
                    lock (this)
                    {
                        return m_Items[index];
                    }
                }
                set
                {
                    throw new NotSupportedException();
                }
            }
            #endregion
            #region ICollection<WorkSheetItem> 成员
            /// <summary>
            /// 将现有的 Howell.Schedules.WorkSheetItem 添加到集合中。
            /// </summary>
            /// <param name="item">要添加到集合中的 Howell.Schedules.WorkSheetItem。</param>
            public void Add(WorkSheetItem item)
            {
                Insert(this.Count, item);
            }
            /// <summary>
            /// 从集合中移除所有项。
            /// </summary>
            public void Clear()
            {
                lock (this)
                {
                    while (this.Count > 0)
                    {
                        RemoveAt(0);
                    }
                }
            }
            /// <summary>
            /// 确定指定项是否位于集合内。
            /// </summary>
            /// <param name="item">Howell.Schedules.WorkSheetItem，表示要在集合中查找的项。</param>
            /// <returns>如果集合中包含该项，则为 true；否则为 false。</returns>
            public bool Contains(WorkSheetItem item)
            {
                lock (this)
                {
                    return IsValidIndex(IndexOf(item));
                }
            }
            /// <summary>
            /// 将整个集合复制到现有数组中，从该数组内的指定位置开始复制。
            /// </summary>
            /// <param name="array">System.Array，表示要将该集合的内容复制到的数组。</param>
            /// <param name="arrayIndex"> 集合中的项将复制到的目标数组中的位置。</param>
            public void CopyTo(WorkSheetItem[] array, int arrayIndex)
            {
                lock (this)
                {
                    m_Items.CopyTo(array, arrayIndex);
                }
            }
            /// <summary>
            /// 获取集合中项的数目。
            /// </summary>
            [Browsable(false)]
            public int Count
            {
                get { return m_Items.Count; }
            }
            /// <summary>
            /// 获取一个值，该值指示集合是否为只读。
            /// </summary>
            public bool IsReadOnly
            {
                get { return false; }
            }
            /// <summary>
            /// 从集合中移除指定的项。
            /// </summary>
            /// <param name="item">Howell.Schedules.WorkSheetItem，表示要从集合中移除的项。</param>
            /// <returns>移除成功返回true,否则返回false。</returns>
            public bool Remove(WorkSheetItem item)
            {
                lock (this)
                {
                    for (int i = 0; i < m_Items.Count; ++i)
                    {
                        if (m_Items[i].Id == item.Id)
                        {
                            RemoveAt(i);
                            return true;
                        }
                    }
                }
                return false;
            }
            #endregion
            #region IEnumerable<WorkSheetItem> 成员
            /// <summary>
            /// 返回一个枚举数，将使用该枚举数循环访问项集合。
            /// </summary>
            /// <returns> System.Collections.IEnumerator，表示项集合。</returns>
            public IEnumerator<WorkSheetItem> GetEnumerator()
            {
                lock (this)
                {
                    return m_Items.GetEnumerator();
                }
            }
            #endregion
            #region IEnumerable 成员
            /// <summary>
            /// 返回一个枚举数，将使用该枚举数循环访问项集合
            /// </summary>
            /// <returns> System.Collections.IEnumerator，表示项集合。</returns>
            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }
            #endregion
            /// <summary>
            /// 工作表项开始事件
            /// </summary>
            public event EventHandler<WorkSheetItemBeginningEventArgs> WorkSheetItemBeginning;
            /// <summary>
            /// 工作表项结束事件
            /// </summary>
            public event EventHandler<WorkSheetItemEndingEventArgs> WorkSheetItemEnding;
            private bool IsValidIndex(Int32 index)
            {
                if (index < 0 || index >= m_Items.Count) return false;
                return true;
            }
        }
    }
}
