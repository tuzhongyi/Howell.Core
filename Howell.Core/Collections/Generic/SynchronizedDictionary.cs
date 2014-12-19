using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;

namespace Howell.Collections.Generic
{
    /// <summary>
    /// Dictionary that uses ReaderWriterLockSlim to syncronize all read and writes to the underlying Dictionary
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    [Serializable]
    public class SynchronizedDictionary<TKey, TValue> : IDictionary<TKey, TValue>, IDisposable
    {

        Dictionary<TKey, TValue> _dictionary;

        [NonSerialized]
        ReaderWriterLockSlim _lock;
        /// <summary>
        /// 
        /// </summary>
        public SynchronizedDictionary()
        {
            _dictionary = new Dictionary<TKey, TValue>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dictionary"></param>
        public SynchronizedDictionary(IDictionary<TKey, TValue> dictionary)
        {
            _dictionary = new Dictionary<TKey, TValue>(dictionary);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="comparer"></param>
        public SynchronizedDictionary(IEqualityComparer<TKey> comparer)
        {
            _dictionary = new Dictionary<TKey, TValue>(comparer);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="capacity"></param>
        public SynchronizedDictionary(int capacity)
        {
            _dictionary = new Dictionary<TKey, TValue>(capacity);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="comparer"></param>
        public SynchronizedDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
        {
            _dictionary = new Dictionary<TKey, TValue>(dictionary, comparer);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="capacity"></param>
        /// <param name="comparer"></param>
        public SynchronizedDictionary(int capacity, IEqualityComparer<TKey> comparer)
        {
            _dictionary = new Dictionary<TKey, TValue>(capacity, comparer);
        }
        /// <summary>
        /// 
        /// </summary>
        protected ReaderWriterLockSlim Lock
        {
            get
            {
                if (_lock == null)
                {
                    Interlocked.CompareExchange(ref _lock, new ReaderWriterLockSlim(), null);
                }
                return _lock;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void EnterReadLock()
        {
            Lock.EnterReadLock();
        }
        /// <summary>
        /// 
        /// </summary>
        public void ExitReadLock()
        {
            Lock.ExitReadLock();
        }
        /// <summary>
        /// 
        /// </summary>
        public void EnterWriteLock()
        {
            Lock.EnterWriteLock();
        }
        /// <summary>
        /// 
        /// </summary>
        public void ExitWriteLock()
        {
            Lock.ExitWriteLock();
        }
        /// <summary>
        /// 
        /// </summary>
        public void EnterUpgradeableReadLock()
        {
            Lock.EnterUpgradeableReadLock();
        }
        /// <summary>
        /// 
        /// </summary>
        public void ExitUpgradeableReadLock()
        {
            Lock.ExitUpgradeableReadLock();
        }
        /// <summary>
        /// 
        /// </summary>
        protected Dictionary<TKey, TValue> Dictionary
        {
            get
            {
                return _dictionary;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="addfunction"></param>
        /// <returns></returns>
        public TValue GetAdd(TKey key, Func<TValue> addfunction)
        {

            if (addfunction == null)
                throw new ArgumentNullException("Func<TValue> addfunction");

            try
            {
                EnterUpgradeableReadLock();

                if (!_dictionary.ContainsKey(key))
                {
                    try
                    {
                        EnterWriteLock();

                        TValue value = addfunction();

                        _dictionary.Add(key, value);
                        return value;
                    }
                    finally
                    {
                        ExitWriteLock();
                    }
                }
                else
                {
                    return _dictionary[key];
                }
            }
            finally
            {
                ExitUpgradeableReadLock();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(TKey key, TValue value)
        {
            try
            {
                EnterWriteLock();
                _dictionary.Add(key, value);
            }
            finally
            {
                ExitWriteLock();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            try
            {
                EnterWriteLock();
                _dictionary.Add(item.Key, item.Value);
            }
            finally
            {
                ExitWriteLock();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="throwOnNotFound"></param>
        /// <returns></returns>
        public bool Add(TKey key, TValue value, bool throwOnNotFound)
        {
            try
            {
                EnterUpgradeableReadLock();
                if (!_dictionary.ContainsKey(key))
                {
                    try
                    {
                        EnterWriteLock();
                        _dictionary.Add(key, value);
                        return true;
                    }
                    finally
                    {
                        ExitWriteLock();
                    }
                }
                else
                {
                    if (throwOnNotFound)
                        throw new ArgumentNullException();
                    else
                        return false;
                }
            }
            finally
            {
                ExitUpgradeableReadLock();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(TKey key)
        {
            try
            {
                EnterReadLock();
                return _dictionary.ContainsKey(key);
            }
            finally
            {
                ExitReadLock();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            try
            {
                EnterReadLock();
                return _dictionary.Contains(item);
            }
            finally
            {
                ExitReadLock();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public TKey[] KeysToArray()
        {
            try
            {
                EnterReadLock();
                return _dictionary.Keys.ToArray();
            }
            finally
            {
                ExitReadLock();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public ICollection<TKey> Keys
        {
            get
            {
                return _dictionary.Keys;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(TKey key)
        {
            try
            {
                EnterWriteLock();
                return _dictionary.Remove(key);
            }
            finally
            {
                ExitWriteLock();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            try
            {
                EnterWriteLock();
                return _dictionary.Remove(item.Key);
            }
            finally
            {
                ExitWriteLock();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            try
            {
                EnterReadLock();
                return _dictionary.TryGetValue(key, out value);
            }
            finally
            {
                ExitReadLock();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public TValue[] ValuesToArray()
        {
            try
            {
                EnterReadLock();
                return _dictionary.Values.ToArray();
            }
            finally
            {
                ExitReadLock();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public ICollection<TValue> Values
        {
            get
            {
                return _dictionary.Values;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TValue this[TKey key]
        {
            get
            {
                try
                {
                    EnterReadLock();
                    return _dictionary[key];
                }
                finally
                {
                    ExitReadLock();
                }
            }
            set
            {
                try
                {
                    EnterWriteLock();
                    _dictionary[key] = value;
                }
                finally
                {
                    ExitWriteLock();
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            try
            {
                EnterWriteLock();
                _dictionary.Clear();
            }
            finally
            {
                ExitWriteLock();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            try
            {
                EnterReadLock();

                for (int i = 0; i < _dictionary.Count; i++)
                {
                    array.SetValue(_dictionary.ElementAt(i), arrayIndex + i);
                }
            }
            finally
            {
                ExitReadLock();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Count
        {
            get
            {
                try
                {
                    EnterReadLock();
                    return _dictionary.Count;
                }
                finally
                {
                    ExitReadLock();
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsSynchronized
        {
            get
            {
                return true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            EnterReadLock();
            try
            {
                return _dictionary.GetEnumerator();
            }
            finally
            {
                ExitReadLock();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        /// <summary>
        /// 
        /// </summary>
        protected bool IsDisposed { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual void Dispose()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(this.GetType().Name);
            try
            {
                this.Dispose(true);
            }
            finally
            {
                GC.SuppressFinalize(this);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            try
            {
                if (!IsDisposed)
                {
                    if (disposing)
                    {
                        if (_lock != null)
                        {
                            _lock.Dispose();
                            _lock = null;
                        }
                    }
                }
            }
            finally
            {
                this.IsDisposed = true;
            }
        }
        
        //Only add Finalizer in you need to dispose of resources with out call Dispose() directly
        /// <summary>
        /// 
        /// </summary>
        ~SynchronizedDictionary()
        {
            Dispose(!IsDisposed);
        }
    }
}
