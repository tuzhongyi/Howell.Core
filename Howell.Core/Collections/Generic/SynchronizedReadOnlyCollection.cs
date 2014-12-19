//
// System.ServiceModel.SynchronizedReadOnlyCollection.cs
//
// Author: Duncan Mak (duncan@novell.com)
//
// Copyright (C) 2005 Novell, Inc (http://www.novell.com)
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
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace Howell.Collections.Generic
{
    /// <summary>
    /// 提供一个线程安全只读集合，该集合包含泛型参数所指定的类型的对象作为元素。
    /// </summary>
    /// <typeparam name="T">包含在线程安全只读集合中作为项的对象的类型。</typeparam>
	[ComVisible (false)]
	public class SynchronizedReadOnlyCollection<T>
		: IList<T>, ICollection<T>, IEnumerable<T>, IList, ICollection, IEnumerable
	{
		List<T> l;
		object sync_root;
        /// <summary>
        /// 初始化 Howell.Collections.Generic.SynchronizedReadOnlyCollection&lt;T&gt; 类的新实例。
        /// </summary>
		public SynchronizedReadOnlyCollection ()
			: this (new object ())
		{
		}
        /// <summary>
        /// 使用对线程安全只读集合的访问进行同步的对象，初始化 Howell.Collections.Generic.SynchronizedReadOnlyCollection&lt;T&gt; 类的新实例。
        /// </summary>
        /// <param name="syncRoot"> 用于对线程安全只读集合的访问进行同步的对象。</param>
        public SynchronizedReadOnlyCollection(object syncRoot)
            : this(syncRoot, new List<T>())
		{
		}
        /// <summary>
        /// 使用指定的可枚举元素列表和用于对线程安全只读集合的访问进行同步的对象，初始化 Howell.Collections.Generic.SynchronizedReadOnlyCollection&lt;T&gt; 类的新实例。
        /// </summary>
        /// <param name="syncRoot">用于对线程安全只读集合的访问进行同步的对象。</param>
        /// <param name="list">元素的 System.Collections.Generic.IEnumerable&lt;T&gt; 集合，用于初始化线程安全只读集合。</param>
        /// <exception cref="System.ArgumentNullException">syncRoot 或 list 为 null。</exception>
        public SynchronizedReadOnlyCollection(object syncRoot, IEnumerable<T> list)
		{
            if (syncRoot == null)
                throw new ArgumentNullException("syncRoot");

			if (list == null)
				throw new ArgumentNullException ("list");

            this.sync_root = syncRoot;
			this.l = new List<T> (list);
		}
        /// <summary>
        /// 使用指定的元素数组和用于对线程安全只读集合的访问进行同步的对象，初始化 Howell.Collections.Generic.SynchronizedReadOnlyCollection&lt;T&gt;类的新实例。
        /// </summary>
        /// <param name="syncRoot">用于对线程安全只读集合的访问进行同步的对象。</param>
        /// <param name="list">类型 T 元素的 System.Array，用于初始化线程安全只读集合。</param>
        /// <exception cref="System.ArgumentNullException">syncRoot 或 list 为 null。</exception>
        public SynchronizedReadOnlyCollection(object syncRoot, params T[] list)
            : this(syncRoot, (IEnumerable<T>)list)
		{
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="syncRoot"></param>
        /// <param name="list"></param>
        /// <param name="makeCopy"></param>
        public SynchronizedReadOnlyCollection(object syncRoot, List<T> list, bool makeCopy)
            : this(syncRoot,
                list == null ? null : makeCopy ? new List<T>(list) : list)
		{
		}
        /// <summary>
        /// 确定集合是否包含具有特定值的元素。
        /// </summary>
        /// <param name="value">要在集合中定位的对象。</param>
        /// <returns>如果在集合中找到元素 value，则为 true；否则为 false。</returns>
        /// <exception cref="System.ArgumentException">value 不是集合所含类型的对象。</exception>
		public bool Contains (T value)
		{
			bool retval;

			lock (sync_root) {
				retval = l.Contains (value);
			}

			return retval;
		}
        /// <summary>
        /// 从特定索引处开始，将集合中的元素复制到指定的数组。
        /// </summary>
        /// <param name="array">System.Array，它是从集合中复制的元素的目标位置。</param>
        /// <param name="index">数组中从零开始的索引，在此处开始复制。</param>
		public void CopyTo (T [] array, int index)
		{
			lock (sync_root) {
				l.CopyTo (array, index);
			}
		}
        /// <summary>
        /// 返回一个枚举数，该枚举数循环访问同步只读集合。
        /// </summary>
        /// <returns>一个 System.Collections.Generic.IEnumerator&lt;T&gt;，用于集合中存储的类型的对象。</returns>
		public IEnumerator<T> GetEnumerator ()
		{
			IEnumerator<T> retval;

			lock (sync_root) {
				retval = l.GetEnumerator ();
			}

			return retval;
		}
        /// <summary>
        /// 返回某个值在集合中的第一个匹配项的索引。
        /// </summary>
        /// <param name="value">要检索其索引的元素。</param>
        /// <returns>value 在集合中的第一个匹配项的从零开始的索引。</returns>
		public int IndexOf (T value)
		{
			int retval;

			lock (sync_root) {
				retval = l.IndexOf (value);
			}

			return retval;
		}

		void ICollection<T>.Add (T value) { throw new NotSupportedException (); }
		void ICollection<T>.Clear () { throw new NotSupportedException (); }
		bool ICollection<T>.Remove (T value) { throw new NotSupportedException (); }

		void IList<T>.Insert (int index, T value) { throw new NotSupportedException (); }
		void IList<T>.RemoveAt (int index) { throw new NotSupportedException (); }

		void ICollection.CopyTo (Array array, int index)
		{
			ICollection<T> a = array as ICollection<T>;

			if (a == null)
				throw new ArgumentException ("The array type is not compatible.");

			lock (sync_root) {
				((ICollection) l).CopyTo (array, index);
			}
		}

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return GetEnumerator ();
		}

		int IList.Add (object value) { throw new NotSupportedException (); }
		void IList.Clear () { throw new NotSupportedException (); }

		bool IList.Contains (object value)
		{
			if (typeof (T).IsValueType)
				throw new ArgumentException ("This is a collection of ValueTypes.");

			// null always gets thru
			if (value is T == false && value != null)
				throw new ArgumentException ("value is not of the same type as this collection.");

			bool retval;
			T val = (T) value;
			lock (sync_root) {
				retval = l.Contains (val);
			}

			return retval;
		}

		int IList.IndexOf (object value)
		{
			if (typeof (T).IsValueType)
				throw new ArgumentException ("This is a collection of ValueTypes.");

			if (value is T == false)
				throw new ArgumentException ("value is not of the same type as this collection.");

			int retval;
			T val = (T) value;
			lock (sync_root) {
				retval = l.IndexOf (val);
			}

			return retval;
		}

		void IList.Insert (int index, object value) { throw new NotSupportedException (); }
		void IList.Remove (object value) { throw new NotSupportedException (); }
		void IList.RemoveAt (int index) { throw new NotSupportedException (); }
        /// <summary>
        /// 获取线程安全只读集合中所含元素的数目。
        /// </summary>
        /// <returns>线程安全只读集合中所含元素的数目。</returns>
		public int Count {
			get {
				int retval;
				lock (sync_root) {
					retval = l.Count;
				}
				return retval;
			}
		}
        /// <summary>
        ///  获取线程安全只读集合中具有指定索引的元素。
        /// </summary>
        /// <param name="index">要从集合中检索的元素的从零开始的索引。</param>
        /// <returns>集合中具有指定 index 的对象。</returns>
		public T this [int index] {
			get {
				T retval;
				lock (sync_root) {
					retval = l [index];
				}
				return retval;
			}
		}
        /// <summary>
        /// 获取线程安全只读集合中所含元素的列表。
        /// </summary>
		protected IList<T> Items {
			get { return l; }
		}


		bool ICollection<T>.IsReadOnly { get { return true; }}

		bool ICollection.IsSynchronized { get { return true; }}
		object ICollection.SyncRoot { get { return sync_root; }}

		bool IList.IsFixedSize { get { return true; }}
		bool IList.IsReadOnly { get { return true; }}

		T IList<T>.this [int index] {
			get { return this [index]; }
			set { throw new NotSupportedException (); }
		}

		object IList.this [int index] {
			get { return this [index]; }
			set { throw new NotSupportedException (); }
		}
	}
}
