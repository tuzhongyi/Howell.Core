//
// SynchronizedCollection.cs
//
// Author:
//	Atsushi Enomoto <atsushi@ximian.com>
//
// Copyright (C) 2005 Novell, Inc.  http://www.novell.com
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;

namespace Howell.Collections.Generic
{
    /// <summary>
    /// 提供一个线程安全集合，其中包含泛型参数所指定类型的对象作为元素。
    /// </summary>
    /// <typeparam name="T">包含在线程安全集合中作为项的对象的类型。</typeparam>
	[ComVisibleAttribute (false)] 
	public class SynchronizedCollection<T> : IList<T>, ICollection<T>, 
		IEnumerable<T>, IList, ICollection, IEnumerable
	{
		object root;
		List<T> list;
        /// <summary>
        /// 初始化 Howell.Collections.Generic.SynchronizedCollection&lt;T&gt; 类的新实例。
        /// </summary>
		public SynchronizedCollection ()
			: this (new object (), null, false)
		{
		}
        /// <summary>
        /// 通过用于对线程安全集合的访问进行同步的对象来初始化 Howell.Collections.Generic.SynchronizedCollection&lt;T&gt; 类的新实例。
        /// </summary>
        /// <param name="syncRoot">用于对线程安全集合的访问进行同步的对象。</param>
        /// <exception cref="System.ArgumentNullException">syncRoot 为 null。</exception>
		public SynchronizedCollection (object syncRoot)
			: this (syncRoot, null, false)
		{
		}
        /// <summary>
        /// 使用指定的可枚举元素列表和用于对线程安全集合的访问进行同步的对象来初始化 Howell.Collections.Generic.SynchronizedCollection&lt;T&gt; 类的新实例。
        /// </summary>
        /// <param name="syncRoot">用于对线程安全集合的访问进行同步的对象。</param>
        /// <param name="list">用于初始化线程安全集合的元素的 System.Collections.Generic.IEnumerable&lt;T&gt; 集合。</param>
        /// <exception cref="System.ArgumentNullException">syncRoot 或 list 为 null。</exception>
		public SynchronizedCollection (object syncRoot,
			IEnumerable<T> list)
			: this (syncRoot, new List<T> (list), false)
		{
		}
        /// <summary>
        /// 使用指定的元素数组和用于对线程安全集合的访问进行同步的对象来初始化 Howell.Collections.Generic.SynchronizedCollection&lt;T&gt; 类的新实例。
        /// </summary>
        /// <param name="syncRoot">用于对线程安全集合的访问进行同步的对象。</param>
        /// <param name="list">用于初始化线程安全集合的 T 类型元素的 System.Array。</param>
        /// <exception cref="System.ArgumentNullException">syncRoot 或 list 为 null。</exception>
		public SynchronizedCollection (object syncRoot,
			params T [] list)
			: this (syncRoot, new List<T> (list), false)
		{
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="syncRoot"></param>
        /// <param name="list"></param>
        /// <param name="makeCopy"></param>
		public SynchronizedCollection (object syncRoot,
			List<T> list, bool makeCopy)
		{
			if (syncRoot == null)
				syncRoot = new object ();
			root = syncRoot;
			if (list == null)
				this.list = new List<T> ();
			else if (makeCopy)
				this.list = new List<T> (list);
			else
				this.list = list;
		}
        /// <summary>
        /// 获取线程安全集合中所含元素的数目。
        /// </summary>
		public int Count {
			get {
				lock (root) {
					return list.Count;
				}
			}
		}
        /// <summary>
        /// 获取线程安全集合中具有指定索引的元素。
        /// </summary>
        /// <param name="index">要从集合中检索的元素的从零开始的索引。</param>
        /// <returns>集合中具有指定 index 的对象。</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">指定的 index 小于零或大于集合中的项数。</exception>
        public T this [int index] {
			get {
				lock (root) {
					return list [index];
				}
			}
			set {
				SetItem (index, value);
			}
		}
        /// <summary>
        /// 获取用于对线程安全集合的访问进行同步的对象。
        /// </summary>
		public object SyncRoot {
			get { return root; }
		}
        /// <summary>
        /// 获取线程安全集合中所含元素的列表。
        /// </summary>
		protected List<T> Items {
			get { return list; }
		}
        /// <summary>
        /// 将项添加到线程安全只读集合中。
        /// </summary>
        /// <param name="item">要添加到集合的元素。</param>
        /// <exception cref="System.ArgumentException">设置的值为 null，或者不是集合的正确泛型类型 T。</exception>
		public void Add (T item)
		{
			InsertItem (list.Count, item);
		}
        /// <summary>
        /// 从集合中移除所有项。
        /// </summary>
		public void Clear ()
		{
			ClearItems ();
		}
        /// <summary>
        /// 确定集合是否包含具有特定值的元素。
        /// </summary>
        /// <param name="item">要在集合中定位的对象。</param>
        /// <returns>如果在集合中找到元素值，则为 true；否则为 false。</returns>
		public bool Contains (T item)
		{
			lock (root) {
				return list.Contains (item);
			}
		}
        /// <summary>
        /// 从特定索引处开始，将集合中的元素复制到指定的数组。
        /// </summary>
        /// <param name="array">从集合中复制的 T 类型元素的目标 System.Array。</param>
        /// <param name="index"> 数组中从零开始的索引，在此处开始复制。</param>
		public void CopyTo (T [] array, int index)
		{
			lock (root) {
				list.CopyTo (array, index);
			}
		}
        /// <summary>
        /// 返回一个循环访问同步集合的枚举数。
        /// </summary>
        /// <returns>一个 System.Collections.Generic.IEnumerator&lt;T&gt;，用于集合中存储的类型的对象。</returns>
		public IEnumerator<T> GetEnumerator ()
		{
			lock (root) {
				return list.GetEnumerator ();
			}
		}
        /// <summary>
        /// 返回某个值在集合中的第一个匹配项的索引。
        /// </summary>
        /// <param name="item"> 从集合中移除所有项。</param>
        /// <returns>  该值在集合中的第一个匹配项的从零开始的索引。</returns>
		public int IndexOf (T item)
		{
			lock (root) {
				return list.IndexOf (item);
			}
		}
        /// <summary>
        /// 将某个项插入到集合中的指定索引处。
        /// </summary>
        /// <param name="index">要从集合中检索的元素的从零开始的索引。</param>
        /// <param name="item"> 要作为元素插入到集合中的对象。</param>
		public void Insert (int index, T item)
		{
			InsertItem (index, item);
		}
        /// <summary>
        /// 从集合中移除指定项的第一个匹配项。
        /// </summary>
        /// <param name="item">要从集合中移除的对象。</param>
        /// <returns>如果从集合中成功移除了项，则为 true；否则为 false。</returns>
		public bool Remove (T item)
		{
			int index = IndexOf (item);
			if (index < 0)
				return false;
			RemoveAt (index);
			return true;
		}
        /// <summary>
        /// 从集合中移除指定索引处的项。
        /// </summary>
        /// <param name="index">要从集合中检索的元素的从零开始的索引。</param>
		public void RemoveAt (int index)
		{
			RemoveItem (index);
		}
        /// <summary>
        /// 从集合中移除所有项。
        /// </summary>
		protected virtual void ClearItems ()
		{
			lock (root) {
				list.Clear ();
			}
		}
        /// <summary>
        ///  将某个项插入到集合中的指定索引处。
        /// </summary>
        /// <param name="index">集合中从零开始的索引，在此处插入对象。</param>
        /// <param name="item">要插入到集合中的对象。</param>
		protected virtual void InsertItem (int index, T item)
		{
			lock (root) {
				list.Insert (index, item);
			}
		}
        /// <summary>
        /// 从集合中移除指定 index 处的项。
        /// </summary>
        /// <param name="index">要从集合中检索的元素的从零开始的索引。</param>
		protected virtual void RemoveItem (int index)
		{
			lock (root) {
				list.RemoveAt (index);                
			}
		}
        /// <summary>
        ///  使用另一项替换指定索引处的项。
        /// </summary>
        /// <param name="index">要替换的对象的从零开始的索引。</param>
        /// <param name="item">要替换的对象。</param>
		protected virtual void SetItem (int index, T item)
		{
			lock (root) {
				list [index] = item;
			}
		}

        #region Explicit interface implementations

        void ICollection.CopyTo(Array array, int index)
        {
            CopyTo((T[])array, index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        int IList.Add(object value)
        {
            lock (root)
            {
                Add((T)value);
                return list.Count - 1;
            }
        }

        bool IList.Contains(object value)
        {
            return Contains((T)value);
        }

        int IList.IndexOf(object value)
        {
            return IndexOf((T)value);
        }

        void IList.Insert(int index, object value)
        {
            Insert(index, (T)value);
        }

        void IList.Remove(object value)
        {
            Remove((T)value);
        }

        bool ICollection<T>.IsReadOnly
        {
            get { return false; }
        }

        bool ICollection.IsSynchronized
        {
            get { return true; }
        }

        object ICollection.SyncRoot
        {
            get { return root; }
        }

        bool IList.IsFixedSize
        {
            get { return false; }
        }

        bool IList.IsReadOnly
        {
            get { return false; }
        }

        object IList.this[int index]
        {
            get { return this[index]; }
            set { this[index] = (T)value; }
        }

        #endregion
	}
}

