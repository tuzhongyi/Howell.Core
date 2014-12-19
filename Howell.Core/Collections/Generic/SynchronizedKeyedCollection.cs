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
    /// 提供一个线程安全集合，该集合所含对象的类型由一个泛型参数指定，并且集合根据键进行分组。
    /// </summary>
    /// <typeparam name="K"> 用于对集合所含的项进行分组的键的类型。</typeparam>
    /// <typeparam name="T">线程安全键控集合中所包含的项的类型。</typeparam>
	[ComVisibleAttribute (false)] 
	public abstract class SynchronizedKeyedCollection<K, T>
		: SynchronizedCollection<T>
	{
		Dictionary<K, T> dict;
        /// <summary>
        /// 初始化 Howell.Collections.Generic.SynchronizedKeyedCollection&lt;K,T&gt; 类的新实例。
        /// </summary>
		protected SynchronizedKeyedCollection ()
			: this (new object ())
		{
		}
        /// <summary>
        /// 使用由显式指定的对象进行同步的访问初始化 Howell.Collections.Generic.SynchronizedKeyedCollection&lt;K,T&gt;类的新实例。
        /// </summary>
        /// <param name="syncRoot">用于对线程安全集合的访问进行同步的对象。</param>
        /// <exception cref="System.ArgumentNullException">syncRoot 为 null。</exception>
		protected SynchronizedKeyedCollection (object syncRoot)
			: base (syncRoot)
		{
			dict = new Dictionary<K, T> ();
		}
        /// <summary>
        /// 使用由显式指定的对象进行同步的访问和以指定方式进行比较的键，初始化 System.Collections.Generic.SynchronizedKeyedCollection&lt;K,T&gt;类的新实例。
        /// </summary>
        /// <param name="syncRoot">用于对线程安全集合的访问进行同步的对象。</param>
        /// <param name="comparer">类型 K 的 System.Collections.Generic.IEqualityComparer&lt;T&gt;，用于比较类型 K 的键对象是否相等。</param>
        /// <exception cref="System.ArgumentNullException">syncRoot 为 null 或 comparer 为 null。</exception>
		protected SynchronizedKeyedCollection (object syncRoot,
			IEqualityComparer<K> comparer)
			: base (syncRoot)
		{
			dict = new Dictionary<K, T> (comparer);
		}
        /// <summary>
        /// 使用由显式指定的对象进行同步的访问和以指定方式进行比较的键，初始化 System.Collections.Generic.SynchronizedKeyedCollection&lt;K,T&gt;类的新实例。
        /// </summary>
        /// <param name="syncRoot">用于对线程安全集合的访问进行同步的对象。</param>
        /// <param name="comparer">类型 K 的 System.Collections.Generic.IEqualityComparer&lt;T&gt;，用于比较类型 K 的键对象是否相等。</param>
        /// <param name="capacity">为集合创建字典所需的项的数目。</param>
        /// <exception cref="System.ArgumentNullException">syncRoot 为 null 或 comparer 为 null。</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">capacity 小于 -1。</exception>
		protected SynchronizedKeyedCollection (object syncRoot,
			IEqualityComparer<K> comparer, int capacity)
			: base (syncRoot)
		{
			dict = new Dictionary<K, T> (capacity, comparer);
		}

        /// <summary>
        /// 获取集合中具有指定键的项。
        /// </summary>
        /// <param name="key">要检索的项的键。</param>
        /// <returns>集合中具有指定键的类型 T 的项。</returns>
		public T this [K key] {
			get {
				lock (SyncRoot) {
					return dict [key];
				}
			}
		}
        /// <summary>
        /// 获取与集合关联的字典。
        /// </summary>
		protected IDictionary<K, T> Dictionary {
			get { return dict; }
		}
        /// <summary>
        /// 返回一个值，该值指示集合是否包含具有指定键的项。
        /// </summary>
        /// <param name="key">要测试的类型 K 的键。</param>
        /// <returns>如果集合包含具有指定键的项，则为 true；否则为 false。</returns>
		public bool Contains (K key)
		{
			lock (SyncRoot) {
				return dict.ContainsKey (key);
			}
		}
        /// <summary>
        /// 从集合中移除具有指定键的项并返回一个值，该值指示项是否已移除。
        /// </summary>
        /// <param name="key">要移除的项的键。</param>
        /// <returns>如果移除了具有指定键的项，则为 true；否则为 false。</returns>
		public bool Remove (K key)
		{
			lock (SyncRoot) {
				return dict.Remove (key);                
			}
		}
        /// <summary>
        /// 更改同步集合中某个指定项的键。
        /// </summary>
        /// <param name="item">要更改其键的项。</param>
        /// <param name="newKey">指定项的新键。</param>
		protected void ChangeItemKey (T item, K newKey)
		{
			lock (SyncRoot) {
				K old = GetKeyForItem (item);
				dict [old] = default (T);
				dict [newKey] = item;
			}
		}
        /// <summary>
        /// 清除集合中的所有项。
        /// </summary>
		protected override void ClearItems ()
		{
			base.ClearItems ();
			lock (SyncRoot) {
				dict.Clear ();
			}
		}
        /// <summary>
        /// 在派生类中重写时，获取指定项的键。
        /// </summary>
        /// <param name="item">要检索其键的类型 T 的项。</param>
        /// <returns>类型 K 的键，用于类型 T 的指定 item。</returns>
		protected abstract K GetKeyForItem (T item);
        /// <summary>
        /// 将某个项插入集合中的指定位置。
        /// </summary>
        /// <param name="index">从零开始的索引，它指定项在集合中的插入位置。</param>
        /// <param name="item">要插入集合的类型 T 的项。</param>
		protected override void InsertItem (int index, T item)
		{
			base.InsertItem (index, item);
			dict.Add (GetKeyForItem (item), item);
		}
        /// <summary>
        /// 从集合中移除指定位置处的项。
        /// </summary>
        /// <param name="index">从零开始的索引，它指定从集合移除的项的位置。</param>
		protected override void RemoveItem (int index)
		{
			K key = GetKeyForItem (base [index]);
			base.RemoveItem (index);
			dict.Remove (key);
		}
        /// <summary>
        /// 用新项替换集合中指定位置处的项。
        /// </summary>
        /// <param name="index">从零开始的索引，它指定项在集合中的插入位置。</param>
        /// <param name="item">要插入集合的类型 T 的项。</param>
		protected override void SetItem (int index, T item)
		{
			base.SetItem (index, item);
			dict [GetKeyForItem (item)] = item;
		}
	}
}

