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
    /// �ṩһ���̰߳�ȫ���ϣ����а������Ͳ�����ָ�����͵Ķ�����ΪԪ�ء�
    /// </summary>
    /// <typeparam name="T">�������̰߳�ȫ��������Ϊ��Ķ�������͡�</typeparam>
	[ComVisibleAttribute (false)] 
	public class SynchronizedCollection<T> : IList<T>, ICollection<T>, 
		IEnumerable<T>, IList, ICollection, IEnumerable
	{
		object root;
		List<T> list;
        /// <summary>
        /// ��ʼ�� Howell.Collections.Generic.SynchronizedCollection&lt;T&gt; �����ʵ����
        /// </summary>
		public SynchronizedCollection ()
			: this (new object (), null, false)
		{
		}
        /// <summary>
        /// ͨ�����ڶ��̰߳�ȫ���ϵķ��ʽ���ͬ���Ķ�������ʼ�� Howell.Collections.Generic.SynchronizedCollection&lt;T&gt; �����ʵ����
        /// </summary>
        /// <param name="syncRoot">���ڶ��̰߳�ȫ���ϵķ��ʽ���ͬ���Ķ���</param>
        /// <exception cref="System.ArgumentNullException">syncRoot Ϊ null��</exception>
		public SynchronizedCollection (object syncRoot)
			: this (syncRoot, null, false)
		{
		}
        /// <summary>
        /// ʹ��ָ���Ŀ�ö��Ԫ���б�����ڶ��̰߳�ȫ���ϵķ��ʽ���ͬ���Ķ�������ʼ�� Howell.Collections.Generic.SynchronizedCollection&lt;T&gt; �����ʵ����
        /// </summary>
        /// <param name="syncRoot">���ڶ��̰߳�ȫ���ϵķ��ʽ���ͬ���Ķ���</param>
        /// <param name="list">���ڳ�ʼ���̰߳�ȫ���ϵ�Ԫ�ص� System.Collections.Generic.IEnumerable&lt;T&gt; ���ϡ�</param>
        /// <exception cref="System.ArgumentNullException">syncRoot �� list Ϊ null��</exception>
		public SynchronizedCollection (object syncRoot,
			IEnumerable<T> list)
			: this (syncRoot, new List<T> (list), false)
		{
		}
        /// <summary>
        /// ʹ��ָ����Ԫ����������ڶ��̰߳�ȫ���ϵķ��ʽ���ͬ���Ķ�������ʼ�� Howell.Collections.Generic.SynchronizedCollection&lt;T&gt; �����ʵ����
        /// </summary>
        /// <param name="syncRoot">���ڶ��̰߳�ȫ���ϵķ��ʽ���ͬ���Ķ���</param>
        /// <param name="list">���ڳ�ʼ���̰߳�ȫ���ϵ� T ����Ԫ�ص� System.Array��</param>
        /// <exception cref="System.ArgumentNullException">syncRoot �� list Ϊ null��</exception>
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
        /// ��ȡ�̰߳�ȫ����������Ԫ�ص���Ŀ��
        /// </summary>
		public int Count {
			get {
				lock (root) {
					return list.Count;
				}
			}
		}
        /// <summary>
        /// ��ȡ�̰߳�ȫ�����о���ָ��������Ԫ�ء�
        /// </summary>
        /// <param name="index">Ҫ�Ӽ����м�����Ԫ�صĴ��㿪ʼ��������</param>
        /// <returns>�����о���ָ�� index �Ķ���</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">ָ���� index С�������ڼ����е�������</exception>
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
        /// ��ȡ���ڶ��̰߳�ȫ���ϵķ��ʽ���ͬ���Ķ���
        /// </summary>
		public object SyncRoot {
			get { return root; }
		}
        /// <summary>
        /// ��ȡ�̰߳�ȫ����������Ԫ�ص��б�
        /// </summary>
		protected List<T> Items {
			get { return list; }
		}
        /// <summary>
        /// ������ӵ��̰߳�ȫֻ�������С�
        /// </summary>
        /// <param name="item">Ҫ��ӵ����ϵ�Ԫ�ء�</param>
        /// <exception cref="System.ArgumentException">���õ�ֵΪ null�����߲��Ǽ��ϵ���ȷ�������� T��</exception>
		public void Add (T item)
		{
			InsertItem (list.Count, item);
		}
        /// <summary>
        /// �Ӽ������Ƴ������
        /// </summary>
		public void Clear ()
		{
			ClearItems ();
		}
        /// <summary>
        /// ȷ�������Ƿ���������ض�ֵ��Ԫ�ء�
        /// </summary>
        /// <param name="item">Ҫ�ڼ����ж�λ�Ķ���</param>
        /// <returns>����ڼ������ҵ�Ԫ��ֵ����Ϊ true������Ϊ false��</returns>
		public bool Contains (T item)
		{
			lock (root) {
				return list.Contains (item);
			}
		}
        /// <summary>
        /// ���ض���������ʼ���������е�Ԫ�ظ��Ƶ�ָ�������顣
        /// </summary>
        /// <param name="array">�Ӽ����и��Ƶ� T ����Ԫ�ص�Ŀ�� System.Array��</param>
        /// <param name="index"> �����д��㿪ʼ���������ڴ˴���ʼ���ơ�</param>
		public void CopyTo (T [] array, int index)
		{
			lock (root) {
				list.CopyTo (array, index);
			}
		}
        /// <summary>
        /// ����һ��ѭ������ͬ�����ϵ�ö������
        /// </summary>
        /// <returns>һ�� System.Collections.Generic.IEnumerator&lt;T&gt;�����ڼ����д洢�����͵Ķ���</returns>
		public IEnumerator<T> GetEnumerator ()
		{
			lock (root) {
				return list.GetEnumerator ();
			}
		}
        /// <summary>
        /// ����ĳ��ֵ�ڼ����еĵ�һ��ƥ�����������
        /// </summary>
        /// <param name="item"> �Ӽ������Ƴ������</param>
        /// <returns>  ��ֵ�ڼ����еĵ�һ��ƥ����Ĵ��㿪ʼ��������</returns>
		public int IndexOf (T item)
		{
			lock (root) {
				return list.IndexOf (item);
			}
		}
        /// <summary>
        /// ��ĳ������뵽�����е�ָ����������
        /// </summary>
        /// <param name="index">Ҫ�Ӽ����м�����Ԫ�صĴ��㿪ʼ��������</param>
        /// <param name="item"> Ҫ��ΪԪ�ز��뵽�����еĶ���</param>
		public void Insert (int index, T item)
		{
			InsertItem (index, item);
		}
        /// <summary>
        /// �Ӽ������Ƴ�ָ����ĵ�һ��ƥ���
        /// </summary>
        /// <param name="item">Ҫ�Ӽ������Ƴ��Ķ���</param>
        /// <returns>����Ӽ����гɹ��Ƴ������Ϊ true������Ϊ false��</returns>
		public bool Remove (T item)
		{
			int index = IndexOf (item);
			if (index < 0)
				return false;
			RemoveAt (index);
			return true;
		}
        /// <summary>
        /// �Ӽ������Ƴ�ָ�����������
        /// </summary>
        /// <param name="index">Ҫ�Ӽ����м�����Ԫ�صĴ��㿪ʼ��������</param>
		public void RemoveAt (int index)
		{
			RemoveItem (index);
		}
        /// <summary>
        /// �Ӽ������Ƴ������
        /// </summary>
		protected virtual void ClearItems ()
		{
			lock (root) {
				list.Clear ();
			}
		}
        /// <summary>
        ///  ��ĳ������뵽�����е�ָ����������
        /// </summary>
        /// <param name="index">�����д��㿪ʼ���������ڴ˴��������</param>
        /// <param name="item">Ҫ���뵽�����еĶ���</param>
		protected virtual void InsertItem (int index, T item)
		{
			lock (root) {
				list.Insert (index, item);
			}
		}
        /// <summary>
        /// �Ӽ������Ƴ�ָ�� index �����
        /// </summary>
        /// <param name="index">Ҫ�Ӽ����м�����Ԫ�صĴ��㿪ʼ��������</param>
		protected virtual void RemoveItem (int index)
		{
			lock (root) {
				list.RemoveAt (index);                
			}
		}
        /// <summary>
        ///  ʹ����һ���滻ָ�����������
        /// </summary>
        /// <param name="index">Ҫ�滻�Ķ���Ĵ��㿪ʼ��������</param>
        /// <param name="item">Ҫ�滻�Ķ���</param>
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

