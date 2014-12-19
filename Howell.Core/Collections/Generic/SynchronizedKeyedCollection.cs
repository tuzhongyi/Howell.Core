//
// SynchronizedKeyedCollection.cs
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
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Howell.Collections.Generic
{

    /// <summary>
    /// �ṩһ���̰߳�ȫ���ϣ��ü������������������һ�����Ͳ���ָ�������Ҽ��ϸ��ݼ����з��顣
    /// </summary>
    /// <typeparam name="K"> ���ڶԼ�������������з���ļ������͡�</typeparam>
    /// <typeparam name="T">�̰߳�ȫ���ؼ�������������������͡�</typeparam>
	[ComVisibleAttribute (false)] 
	public abstract class SynchronizedKeyedCollection<K, T>
		: SynchronizedCollection<T>
	{
		Dictionary<K, T> dict;
        /// <summary>
        /// ��ʼ�� Howell.Collections.Generic.SynchronizedKeyedCollection&lt;K,T&gt; �����ʵ����
        /// </summary>
		protected SynchronizedKeyedCollection ()
			: this (new object ())
		{
		}
        /// <summary>
        /// ʹ������ʽָ���Ķ������ͬ���ķ��ʳ�ʼ�� Howell.Collections.Generic.SynchronizedKeyedCollection&lt;K,T&gt;�����ʵ����
        /// </summary>
        /// <param name="syncRoot">���ڶ��̰߳�ȫ���ϵķ��ʽ���ͬ���Ķ���</param>
        /// <exception cref="System.ArgumentNullException">syncRoot Ϊ null��</exception>
		protected SynchronizedKeyedCollection (object syncRoot)
			: base (syncRoot)
		{
			dict = new Dictionary<K, T> ();
		}
        /// <summary>
        /// ʹ������ʽָ���Ķ������ͬ���ķ��ʺ���ָ����ʽ���бȽϵļ�����ʼ�� System.Collections.Generic.SynchronizedKeyedCollection&lt;K,T&gt;�����ʵ����
        /// </summary>
        /// <param name="syncRoot">���ڶ��̰߳�ȫ���ϵķ��ʽ���ͬ���Ķ���</param>
        /// <param name="comparer">���� K �� System.Collections.Generic.IEqualityComparer&lt;T&gt;�����ڱȽ����� K �ļ������Ƿ���ȡ�</param>
        /// <exception cref="System.ArgumentNullException">syncRoot Ϊ null �� comparer Ϊ null��</exception>
		protected SynchronizedKeyedCollection (object syncRoot,
			IEqualityComparer<K> comparer)
			: base (syncRoot)
		{
			dict = new Dictionary<K, T> (comparer);
		}
        /// <summary>
        /// ʹ������ʽָ���Ķ������ͬ���ķ��ʺ���ָ����ʽ���бȽϵļ�����ʼ�� System.Collections.Generic.SynchronizedKeyedCollection&lt;K,T&gt;�����ʵ����
        /// </summary>
        /// <param name="syncRoot">���ڶ��̰߳�ȫ���ϵķ��ʽ���ͬ���Ķ���</param>
        /// <param name="comparer">���� K �� System.Collections.Generic.IEqualityComparer&lt;T&gt;�����ڱȽ����� K �ļ������Ƿ���ȡ�</param>
        /// <param name="capacity">Ϊ���ϴ����ֵ�����������Ŀ��</param>
        /// <exception cref="System.ArgumentNullException">syncRoot Ϊ null �� comparer Ϊ null��</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">capacity С�� -1��</exception>
		protected SynchronizedKeyedCollection (object syncRoot,
			IEqualityComparer<K> comparer, int capacity)
			: base (syncRoot)
		{
			dict = new Dictionary<K, T> (capacity, comparer);
		}

        /// <summary>
        /// ��ȡ�����о���ָ�������
        /// </summary>
        /// <param name="key">Ҫ��������ļ���</param>
        /// <returns>�����о���ָ���������� T ���</returns>
		public T this [K key] {
			get {
				lock (SyncRoot) {
					return dict [key];
				}
			}
		}
        /// <summary>
        /// ��ȡ�뼯�Ϲ������ֵ䡣
        /// </summary>
		protected IDictionary<K, T> Dictionary {
			get { return dict; }
		}
        /// <summary>
        /// ����һ��ֵ����ֵָʾ�����Ƿ��������ָ�������
        /// </summary>
        /// <param name="key">Ҫ���Ե����� K �ļ���</param>
        /// <returns>������ϰ�������ָ���������Ϊ true������Ϊ false��</returns>
		public bool Contains (K key)
		{
			lock (SyncRoot) {
				return dict.ContainsKey (key);
			}
		}
        /// <summary>
        /// �Ӽ������Ƴ�����ָ�����������һ��ֵ����ֵָʾ���Ƿ����Ƴ���
        /// </summary>
        /// <param name="key">Ҫ�Ƴ�����ļ���</param>
        /// <returns>����Ƴ��˾���ָ���������Ϊ true������Ϊ false��</returns>
		public bool Remove (K key)
		{
			lock (SyncRoot) {
				return dict.Remove (key);                
			}
		}
        /// <summary>
        /// ����ͬ��������ĳ��ָ����ļ���
        /// </summary>
        /// <param name="item">Ҫ����������</param>
        /// <param name="newKey">ָ������¼���</param>
		protected void ChangeItemKey (T item, K newKey)
		{
			lock (SyncRoot) {
				K old = GetKeyForItem (item);
				dict [old] = default (T);
				dict [newKey] = item;
			}
		}
        /// <summary>
        /// ��������е������
        /// </summary>
		protected override void ClearItems ()
		{
			base.ClearItems ();
			lock (SyncRoot) {
				dict.Clear ();
			}
		}
        /// <summary>
        /// ������������дʱ����ȡָ����ļ���
        /// </summary>
        /// <param name="item">Ҫ������������� T ���</param>
        /// <returns>���� K �ļ����������� T ��ָ�� item��</returns>
		protected abstract K GetKeyForItem (T item);
        /// <summary>
        /// ��ĳ������뼯���е�ָ��λ�á�
        /// </summary>
        /// <param name="index">���㿪ʼ����������ָ�����ڼ����еĲ���λ�á�</param>
        /// <param name="item">Ҫ���뼯�ϵ����� T ���</param>
		protected override void InsertItem (int index, T item)
		{
			base.InsertItem (index, item);
			dict.Add (GetKeyForItem (item), item);
		}
        /// <summary>
        /// �Ӽ������Ƴ�ָ��λ�ô����
        /// </summary>
        /// <param name="index">���㿪ʼ����������ָ���Ӽ����Ƴ������λ�á�</param>
		protected override void RemoveItem (int index)
		{
			K key = GetKeyForItem (base [index]);
			base.RemoveItem (index);
			dict.Remove (key);
		}
        /// <summary>
        /// �������滻������ָ��λ�ô����
        /// </summary>
        /// <param name="index">���㿪ʼ����������ָ�����ڼ����еĲ���λ�á�</param>
        /// <param name="item">Ҫ���뼯�ϵ����� T ���</param>
		protected override void SetItem (int index, T item)
		{
			base.SetItem (index, item);
			dict [GetKeyForItem (item)] = item;
		}
	}
}

