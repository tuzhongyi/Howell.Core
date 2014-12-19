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
    /// �ṩһ���̰߳�ȫֻ�����ϣ��ü��ϰ������Ͳ�����ָ�������͵Ķ�����ΪԪ�ء�
    /// </summary>
    /// <typeparam name="T">�������̰߳�ȫֻ����������Ϊ��Ķ�������͡�</typeparam>
	[ComVisible (false)]
	public class SynchronizedReadOnlyCollection<T>
		: IList<T>, ICollection<T>, IEnumerable<T>, IList, ICollection, IEnumerable
	{
		List<T> l;
		object sync_root;
        /// <summary>
        /// ��ʼ�� Howell.Collections.Generic.SynchronizedReadOnlyCollection&lt;T&gt; �����ʵ����
        /// </summary>
		public SynchronizedReadOnlyCollection ()
			: this (new object ())
		{
		}
        /// <summary>
        /// ʹ�ö��̰߳�ȫֻ�����ϵķ��ʽ���ͬ���Ķ��󣬳�ʼ�� Howell.Collections.Generic.SynchronizedReadOnlyCollection&lt;T&gt; �����ʵ����
        /// </summary>
        /// <param name="syncRoot"> ���ڶ��̰߳�ȫֻ�����ϵķ��ʽ���ͬ���Ķ���</param>
        public SynchronizedReadOnlyCollection(object syncRoot)
            : this(syncRoot, new List<T>())
		{
		}
        /// <summary>
        /// ʹ��ָ���Ŀ�ö��Ԫ���б�����ڶ��̰߳�ȫֻ�����ϵķ��ʽ���ͬ���Ķ��󣬳�ʼ�� Howell.Collections.Generic.SynchronizedReadOnlyCollection&lt;T&gt; �����ʵ����
        /// </summary>
        /// <param name="syncRoot">���ڶ��̰߳�ȫֻ�����ϵķ��ʽ���ͬ���Ķ���</param>
        /// <param name="list">Ԫ�ص� System.Collections.Generic.IEnumerable&lt;T&gt; ���ϣ����ڳ�ʼ���̰߳�ȫֻ�����ϡ�</param>
        /// <exception cref="System.ArgumentNullException">syncRoot �� list Ϊ null��</exception>
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
        /// ʹ��ָ����Ԫ����������ڶ��̰߳�ȫֻ�����ϵķ��ʽ���ͬ���Ķ��󣬳�ʼ�� Howell.Collections.Generic.SynchronizedReadOnlyCollection&lt;T&gt;�����ʵ����
        /// </summary>
        /// <param name="syncRoot">���ڶ��̰߳�ȫֻ�����ϵķ��ʽ���ͬ���Ķ���</param>
        /// <param name="list">���� T Ԫ�ص� System.Array�����ڳ�ʼ���̰߳�ȫֻ�����ϡ�</param>
        /// <exception cref="System.ArgumentNullException">syncRoot �� list Ϊ null��</exception>
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
        /// ȷ�������Ƿ���������ض�ֵ��Ԫ�ء�
        /// </summary>
        /// <param name="value">Ҫ�ڼ����ж�λ�Ķ���</param>
        /// <returns>����ڼ������ҵ�Ԫ�� value����Ϊ true������Ϊ false��</returns>
        /// <exception cref="System.ArgumentException">value ���Ǽ����������͵Ķ���</exception>
		public bool Contains (T value)
		{
			bool retval;

			lock (sync_root) {
				retval = l.Contains (value);
			}

			return retval;
		}
        /// <summary>
        /// ���ض���������ʼ���������е�Ԫ�ظ��Ƶ�ָ�������顣
        /// </summary>
        /// <param name="array">System.Array�����ǴӼ����и��Ƶ�Ԫ�ص�Ŀ��λ�á�</param>
        /// <param name="index">�����д��㿪ʼ���������ڴ˴���ʼ���ơ�</param>
		public void CopyTo (T [] array, int index)
		{
			lock (sync_root) {
				l.CopyTo (array, index);
			}
		}
        /// <summary>
        /// ����һ��ö��������ö����ѭ������ͬ��ֻ�����ϡ�
        /// </summary>
        /// <returns>һ�� System.Collections.Generic.IEnumerator&lt;T&gt;�����ڼ����д洢�����͵Ķ���</returns>
		public IEnumerator<T> GetEnumerator ()
		{
			IEnumerator<T> retval;

			lock (sync_root) {
				retval = l.GetEnumerator ();
			}

			return retval;
		}
        /// <summary>
        /// ����ĳ��ֵ�ڼ����еĵ�һ��ƥ�����������
        /// </summary>
        /// <param name="value">Ҫ������������Ԫ�ء�</param>
        /// <returns>value �ڼ����еĵ�һ��ƥ����Ĵ��㿪ʼ��������</returns>
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
        /// ��ȡ�̰߳�ȫֻ������������Ԫ�ص���Ŀ��
        /// </summary>
        /// <returns>�̰߳�ȫֻ������������Ԫ�ص���Ŀ��</returns>
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
        ///  ��ȡ�̰߳�ȫֻ�������о���ָ��������Ԫ�ء�
        /// </summary>
        /// <param name="index">Ҫ�Ӽ����м�����Ԫ�صĴ��㿪ʼ��������</param>
        /// <returns>�����о���ָ�� index �Ķ���</returns>
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
        /// ��ȡ�̰߳�ȫֻ������������Ԫ�ص��б�
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
