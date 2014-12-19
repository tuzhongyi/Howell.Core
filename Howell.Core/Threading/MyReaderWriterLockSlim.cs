using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Security.Permissions;
using System.Reflection;
using System.Runtime.Serialization;

namespace Howell.Threading
{
    /// <summary>
    /// 自定义锁递归策略
    /// </summary>
    public enum MyLockRecursionPolicy
    {
        /// <summary>
        /// 不递归策略
        /// </summary>
        NoRecursion,
        /// <summary>
        /// 支持递归策略
        /// </summary>
        SupportsRecursion
    }
#if _NET40
    public class MyReaderWriterLockSlim : IDisposable
    {
        private ReaderWriterLockSlim locker = null;
        /// <summary>
        /// 初始化 MyReaderWriterLockSlim 该类和VS.NET3.5下的 ReaderWriterLockSlim相同  递归策略:默认是不支持递归
        /// </summary>
        public MyReaderWriterLockSlim()
        {
            locker = new ReaderWriterLockSlim();
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="recursionPolicy">递归策略 默认是不支持递归</param>
        public MyReaderWriterLockSlim(MyLockRecursionPolicy recursionPolicy)
        {
            LockRecursionPolicy policy = LockRecursionPolicy.NoRecursion;
            switch (recursionPolicy)
	        {
		        case MyLockRecursionPolicy.NoRecursion:
                    policy = LockRecursionPolicy.NoRecursion;
                break;
                case MyLockRecursionPolicy.SupportsRecursion:
                    policy = LockRecursionPolicy.SupportsRecursion;
                break;
            default:
                break;
	        }
            locker = new ReaderWriterLockSlim(policy);
        }
        /// <summary>
        /// 回收垃圾
        /// </summary>
        public void Dispose()
        {
            locker.Dispose();
        }
        /// <summary>
        /// 进入读锁 同一时间允许多个线程进入
        /// </summary>
        public void EnterReadLock()
        {
            locker.EnterReadLock();
        }
        /// <summary>
        /// 尝试进入可升级为写锁的读锁 同一时间只允许一个线程进入
        /// </summary>
        public void EnterUpgradeableReadLock()
        {
            locker.EnterUpgradeableReadLock();
        }
        /// <summary>
        /// 尝试进入写锁 同一时间只允许一个线程进入
        /// </summary>
        public void EnterWriteLock()
        {
            locker.EnterWriteLock();
        }/// <summary>
        /// 退出读锁
        /// </summary>
        public void ExitReadLock()
        {
            locker.ExitReadLock();
        }
        /// <summary>
        /// 退出可升级的读锁
        /// </summary>
        public void ExitUpgradeableReadLock()
        {
            locker.ExitUpgradeableReadLock();
        }
        /// <summary>
        /// 退出写锁
        /// </summary>
        public void ExitWriteLock()
        {
            this.ExitWriteLock();
        }

        /// <summary>
        /// 尝试进度写锁
        /// </summary>
        /// <param name="millisecondsTimeout">超时毫秒</param>
        /// <returns>true/false</returns>
        public bool TryEnterReadLock(int millisecondsTimeout)
        {
            return locker.TryEnterUpgradeableReadLock(millisecondsTimeout);
        }
        /// <summary>
        /// 尝试进入读锁
        /// </summary>
        /// <param name="timeout">超时时间</param>
        /// <returns>true/false</returns>
        public bool TryEnterReadLock(TimeSpan timeout)
        {
            return locker.TryEnterUpgradeableReadLock(timeout);
        }
        /// <summary>
        /// 尝试进入可升级的读锁
        /// </summary>
        /// <param name="millisecondsTimeout">超时毫秒</param>
        /// <returns>true/false</returns>
        public bool TryEnterUpgradeableReadLock(int millisecondsTimeout)
        {
            return locker.TryEnterUpgradeableReadLock(millisecondsTimeout);
        }
        /// <summary>
        /// 尝试进入可升级读锁
        /// </summary>
        /// <param name="timeout">超时时间</param>
        /// <returns>true/false</returns>
        public bool TryEnterUpgradeableReadLock(TimeSpan timeout)
        {
            return locker.TryEnterUpgradeableReadLock(timeout);
        }
        /// <summary>
        /// 尝试进入写锁
        /// </summary>
        /// <param name="millisecondsTimeout">超时毫秒</param>
        /// <returns>true/false</returns>
        public bool TryEnterWriteLock(int millisecondsTimeout)
        {
            return locker.TryEnterWriteLock(millisecondsTimeout);
        }
        /// <summary>
        /// 尝试进入写锁
        /// </summary>
        /// <param name="timeout">超时时间</param>
        /// <returns>true/false</returns>
        public bool TryEnterWriteLock(TimeSpan timeout)
        {
            return locker.TryEnterWriteLock(timeout);
        }

        // Properties
        /// <summary>
        /// 当前读锁计数器
        /// </summary>
        public int CurrentReadCount
        {
            get
            {
                return locker.CurrentReadCount;
            }
        }
        /// <summary>
        /// 当前线程是否已持有读锁
        /// </summary>
        public bool IsReadLockHeld
        {
            get
            {
                return locker.IsReadLockHeld;
            }
        }
        /// <summary>
        /// 当前线程是否已持有可升级的读锁
        /// </summary>
        public bool IsUpgradeableReadLockHeld
        {
            get
            {
                return locker.IsUpgradeableReadLockHeld;
            }
        }
        /// <summary>
        /// 当前线程是否已持有写锁
        /// </summary>
        public bool IsWriteLockHeld
        {
            get
            {
                return locker.IsWriteLockHeld;
            }
        }
        /// <summary>
        /// 递归策略
        /// </summary>
        public MyLockRecursionPolicy RecursionPolicy
        {
            get
            {
                switch (locker.RecursionPolicy)
                {
                    case LockRecursionPolicy.SupportsRecursion:
                        return MyLockRecursionPolicy.SupportsRecursion;
                    case LockRecursionPolicy.NoRecursion:
                        return MyLockRecursionPolicy.NoRecursion;
                    default:
                        return MyLockRecursionPolicy.NoRecursion;
                }
            }
        }
        /// <summary>
        /// 递归读锁持有数
        /// </summary>
        public int RecursiveReadCount
        {
            get
            {
                return locker.RecursiveReadCount;
            }
        }
        /// <summary>
        /// 递归升级读锁持有数
        /// </summary>
        public int RecursiveUpgradeCount
        {
            get
            {
                return RecursiveUpgradeCount;
            }
        }
        /// <summary>
        /// 递归写锁持有数
        /// </summary>
        public int RecursiveWriteCount
        {
            get
            {
                return locker.RecursiveWriteCount;
            }
        }
        /// <summary>
        /// 等待读锁数目
        /// </summary>
        public int WaitingReadCount
        {
            get
            {
                return locker.WaitingReadCount;
            }
        }
        /// <summary>
        /// 等待升级锁数据
        /// </summary>
        public int WaitingUpgradeCount
        {
            get
            {
                return locker.WaitingUpgradeCount;
            }
        }
        /// <summary>
        /// 等待写锁数目
        /// </summary>
        public int WaitingWriteCount
        {
            get
            {
                return locker.WaitingWriteCount;
            }
        }
    }
#else
    /// <summary>
    /// MyLockRecursionException
    /// </summary>
    [Serializable, HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
    public class MyLockRecursionException : Exception
    {
        // Methods
        /// <summary>
        /// 用序列化数据初始化 MyLockRecursionException 类的新实例。
        /// </summary>
        public MyLockRecursionException()
        {
        }
        /// <summary>
        /// 用序列化数据初始化 MyLockRecursionException 类的新实例。
        /// </summary>
        /// <param name="message">描述错误的消息</param>
        public MyLockRecursionException(string message)
            : base(message)
        {
        }
        /// <summary>
        /// 用序列化数据初始化 MyLockRecursionException 类的新实例。
        /// </summary>
        /// <param name="info">System.Runtime.Serialization.SerializationInfo，它存有有关所引发异常的序列化的对象数据。</param>
        /// <param name="context">System.Runtime.Serialization.StreamingContext，它包含有关源或目标的上下文信息。</param>
        protected MyLockRecursionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
        /// <summary>
        /// 用序列化数据初始化 MyLockRecursionException 类的新实例。
        /// </summary>
        /// <param name="message">解释异常原因的错误消息。</param>
        /// <param name="innerException">导致当前异常的异常；如果未指定内部异常，则是一个 null 引用（在 Visual Basic 中为 Nothing）。</param>
        public MyLockRecursionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
    /// <summary>
    /// 递归计数
    /// </summary>
    internal class RecursiveCounts
    {
        // Fields
        public int upgradecount;
        public int writercount;

        // Methods
        public RecursiveCounts()
        {
        }
    }
    /// <summary>
    /// 读写计数器
    /// </summary>
    internal class ReaderWriterCount
    {
        // Fields
        public ReaderWriterCount next;
        public RecursiveCounts rc;
        public int readercount;
        public int threadid = -1;

        // Methods
        public ReaderWriterCount(bool fIsReentrant)
        {
            if (fIsReentrant)
            {
                this.rc = new RecursiveCounts();
            }
        }
    }
    /// <summary>
    /// 自定义ReaderWriterLockSlim
    /// </summary>
    [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true), HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
    public class MyReaderWriterLockSlim : IDisposable
    {
        // Fields
        private bool fDisposed;
        private bool fIsReentrant;
        private bool fNoWaiters;
        private bool fUpgradeThreadHoldingRead;
        private const int hashTableSize = 0xff;
        private const int LockSleep0Count = 5;
        private const int LockSpinCount = 10;
        private const int LockSpinCycles = 20;
        private const uint MAX_READER = 0xffffffe;
        private const int MaxSpinCount = 20;
        private int myLock;
        private uint numReadWaiters;
        private uint numUpgradeWaiters;
        private uint numWriteUpgradeWaiters;
        private uint numWriteWaiters;
        private uint owners;
        private const uint READER_MASK = 0xfffffff;
        private EventWaitHandle readEvent;
        private ReaderWriterCount[] rwc;
        private EventWaitHandle upgradeEvent;
        private int upgradeLockOwnerId;
        private const uint WAITING_UPGRADER = 0x20000000;
        private const uint WAITING_WRITERS = 0x40000000;
        private EventWaitHandle waitUpgradeEvent;
        private EventWaitHandle writeEvent;
        private int writeLockOwnerId;
        private const uint WRITER_HELD = 0x80000000;

        // Methods
        /// <summary>
        /// 初始化 MyReaderWriterLockSlim 该类和VS.NET3.5下的 ReaderWriterLockSlim相同  递归策略:默认是不支持递归
        /// </summary>
        public MyReaderWriterLockSlim()
            : this(MyLockRecursionPolicy.NoRecursion)
        {
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="recursionPolicy">递归策略 默认是不支持递归</param>
        public MyReaderWriterLockSlim(MyLockRecursionPolicy recursionPolicy)
        {
            if (recursionPolicy == MyLockRecursionPolicy.SupportsRecursion)
            {
                this.fIsReentrant = true;
            }
            this.InitializeThreadCounts();
        }

        private void ClearUpgraderWaiting()
        {
            this.owners &= 0xdfffffff;
        }

        private void ClearWriterAcquired()
        {
            this.owners &= 0x7fffffff;
        }

        private void ClearWritersWaiting()
        {
            this.owners &= 0xbfffffff;
        }
        /// <summary>
        /// 回收垃圾
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.fDisposed)
                {
                    throw new ObjectDisposedException(null);
                }
                if (((this.WaitingReadCount > 0) || (this.WaitingUpgradeCount > 0)) || (this.WaitingWriteCount > 0))
                {
                    throw new SynchronizationLockException("SynchronizationLockException_IncorrectDispose");
                }
                if ((this.IsReadLockHeld || this.IsUpgradeableReadLockHeld) || this.IsWriteLockHeld)
                {
                    throw new SynchronizationLockException("SynchronizationLockException_IncorrectDispose");
                }
                if (this.writeEvent != null)
                {
                    this.writeEvent.Close();
                    this.writeEvent = null;
                }
                if (this.readEvent != null)
                {
                    this.readEvent.Close();
                    this.readEvent = null;
                }
                if (this.upgradeEvent != null)
                {
                    this.upgradeEvent.Close();
                    this.upgradeEvent = null;
                }
                if (this.waitUpgradeEvent != null)
                {
                    this.waitUpgradeEvent.Close();
                    this.waitUpgradeEvent = null;
                }
                this.fDisposed = true;
            }
        }

        private void EnterMyLock()
        {
            if (Interlocked.CompareExchange(ref this.myLock, 1, 0) != 0)
            {
                this.EnterMyLockSpin();
            }
        }

        private void EnterMyLockSpin()
        {
            int processorCount = Environment.ProcessorCount;
            int num2 = 0;
            while (true)
            {
                if ((num2 < 10) && (processorCount > 1))
                {
                    Thread.SpinWait(20 * (num2 + 1));
                }
                else if (num2 < 15)
                {
                    Thread.Sleep(0);
                }
                else
                {
                    Thread.Sleep(1);
                }
                if ((this.myLock == 0) && (Interlocked.CompareExchange(ref this.myLock, 1, 0) == 0))
                {
                    return;
                }
                num2++;
            }
        }
        /// <summary>
        /// 进入读锁 同一时间允许多个线程进入
        /// </summary>
        public void EnterReadLock()
        {
            this.TryEnterReadLock(-1);
        }
        /// <summary>
        /// 尝试进入可升级为写锁的读锁 同一时间只允许一个线程进入
        /// </summary>
        public void EnterUpgradeableReadLock()
        {
            this.TryEnterUpgradeableReadLock(-1);
        }
        /// <summary>
        /// 尝试进入写锁 同一时间只允许一个线程进入
        /// </summary>
        public void EnterWriteLock()
        {
            this.TryEnterWriteLock(-1);
        }
        private void ExitAndWakeUpAppropriateWaiters()
        {
            if (this.fNoWaiters)
            {
                this.ExitMyLock();
            }
            else
            {
                this.ExitAndWakeUpAppropriateWaitersPreferringWriters();
            }
        }
        private void ExitAndWakeUpAppropriateWaitersPreferringWriters()
        {
            bool flag = false;
            bool flag2 = false;
            uint numReaders = this.GetNumReaders();
            if ((this.fIsReentrant && (this.numWriteUpgradeWaiters > 0)) && (this.fUpgradeThreadHoldingRead && (numReaders == 2)))
            {
                this.ExitMyLock();
                this.waitUpgradeEvent.Set();
            }
            else if ((numReaders == 1) && (this.numWriteUpgradeWaiters > 0))
            {
                this.ExitMyLock();
                this.waitUpgradeEvent.Set();
            }
            else if ((numReaders == 0) && (this.numWriteWaiters > 0))
            {
                this.ExitMyLock();
                this.writeEvent.Set();
            }
            else if (numReaders >= 0)
            {
                if ((this.numReadWaiters == 0) && (this.numUpgradeWaiters == 0))
                {
                    this.ExitMyLock();
                }
                else
                {
                    if (this.numReadWaiters != 0)
                    {
                        flag2 = true;
                    }
                    if ((this.numUpgradeWaiters != 0) && (this.upgradeLockOwnerId == -1))
                    {
                        flag = true;
                    }
                    this.ExitMyLock();
                    if (flag2)
                    {
                        this.readEvent.Set();
                    }
                    if (flag)
                    {
                        this.upgradeEvent.Set();
                    }
                }
            }
            else
            {
                this.ExitMyLock();
            }
        }
        private void ExitMyLock()
        {
            this.myLock = 0;
        }
        /// <summary>
        /// 退出读锁
        /// </summary>
        public void ExitReadLock()
        {
            int managedThreadId = Thread.CurrentThread.ManagedThreadId;
            ReaderWriterCount threadRWCount = null;
            this.EnterMyLock();
            threadRWCount = this.GetThreadRWCount(managedThreadId, true);
            if (!this.fIsReentrant)
            {
                if (threadRWCount == null)
                {
                    this.ExitMyLock();
                    throw new SynchronizationLockException("SynchronizationLockException_MisMatchedRead");
                }
            }
            else
            {
                if ((threadRWCount == null) || (threadRWCount.readercount < 1))
                {
                    this.ExitMyLock();
                    throw new SynchronizationLockException("SynchronizationLockException_MisMatchedRead");
                }
                if (threadRWCount.readercount > 1)
                {
                    threadRWCount.readercount--;
                    this.ExitMyLock();
                    return;
                }
                if (managedThreadId == this.upgradeLockOwnerId)
                {
                    this.fUpgradeThreadHoldingRead = false;
                }
            }
            this.owners--;
            threadRWCount.readercount--;
            this.ExitAndWakeUpAppropriateWaiters();
        }
        /// <summary>
        /// 退出可升级的读锁
        /// </summary>
        public void ExitUpgradeableReadLock()
        {
            int managedThreadId = Thread.CurrentThread.ManagedThreadId;
            if (!this.fIsReentrant)
            {
                if (managedThreadId != this.upgradeLockOwnerId)
                {
                    throw new SynchronizationLockException("SynchronizationLockException_MisMatchedUpgrade");
                }
                this.EnterMyLock();
            }
            else
            {
                this.EnterMyLock();
                ReaderWriterCount threadRWCount = this.GetThreadRWCount(managedThreadId, true);
                if (threadRWCount == null)
                {
                    this.ExitMyLock();
                    throw new SynchronizationLockException("SynchronizationLockException_MisMatchedUpgrade");
                }
                RecursiveCounts rc = threadRWCount.rc;
                if (rc.upgradecount < 1)
                {
                    this.ExitMyLock();
                    throw new SynchronizationLockException("SynchronizationLockException_MisMatchedUpgrade");
                }
                rc.upgradecount--;
                if (rc.upgradecount > 0)
                {
                    this.ExitMyLock();
                    return;
                }
                this.fUpgradeThreadHoldingRead = false;
            }
            this.owners--;
            this.upgradeLockOwnerId = -1;
            this.ExitAndWakeUpAppropriateWaiters();
        }
        /// <summary>
        /// 退出写锁
        /// </summary>
        public void ExitWriteLock()
        {
            int managedThreadId = Thread.CurrentThread.ManagedThreadId;
            if (!this.fIsReentrant)
            {
                if (managedThreadId != this.writeLockOwnerId)
                {
                    throw new SynchronizationLockException("SynchronizationLockException_MisMatchedWrite");
                }
                this.EnterMyLock();
            }
            else
            {
                this.EnterMyLock();
                ReaderWriterCount threadRWCount = this.GetThreadRWCount(managedThreadId, false);
                if (threadRWCount == null)
                {
                    this.ExitMyLock();
                    throw new SynchronizationLockException("SynchronizationLockException_MisMatchedWrite");
                }
                RecursiveCounts rc = threadRWCount.rc;
                if (rc.writercount < 1)
                {
                    this.ExitMyLock();
                    throw new SynchronizationLockException("SynchronizationLockException_MisMatchedWrite");
                }
                rc.writercount--;
                if (rc.writercount > 0)
                {
                    this.ExitMyLock();
                    return;
                }
            }
            this.ClearWriterAcquired();
            this.writeLockOwnerId = -1;
            this.ExitAndWakeUpAppropriateWaiters();
        }
        private uint GetNumReaders()
        {
            return (this.owners & 0xfffffff);
        }
        private ReaderWriterCount GetThreadRWCount(int id, bool DontAllocate)
        {
            int index = id & 0xff;
            ReaderWriterCount count = null;
            if (this.rwc[index].threadid == id)
            {
                return this.rwc[index];
            }
            if (IsRWEntryEmpty(this.rwc[index]) && !DontAllocate)
            {
                if (this.rwc[index].next == null)
                {
                    this.rwc[index].threadid = id;
                    return this.rwc[index];
                }
                count = this.rwc[index];
            }
            ReaderWriterCount next = this.rwc[index].next;
            while (next != null)
            {
                if (next.threadid == id)
                {
                    return next;
                }
                if ((count == null) && IsRWEntryEmpty(next))
                {
                    count = next;
                }
                next = next.next;
            }
            if (DontAllocate)
            {
                return null;
            }
            if (count == null)
            {
                next = new ReaderWriterCount(this.fIsReentrant);
                next.threadid = id;
                next.next = this.rwc[index].next;
                this.rwc[index].next = next;
                return next;
            }
            count.threadid = id;
            return count;
        }
        private void InitializeThreadCounts()
        {
            this.rwc = new ReaderWriterCount[0x100];
            for (int i = 0; i < this.rwc.Length; i++)
            {
                this.rwc[i] = new ReaderWriterCount(this.fIsReentrant);
            }
            this.upgradeLockOwnerId = -1;
            this.writeLockOwnerId = -1;
        }
        private static bool IsRWEntryEmpty(ReaderWriterCount rwc)
        {
            return ((rwc.threadid == -1) || (((rwc.readercount == 0) && (rwc.rc == null)) || (((rwc.readercount == 0) && (rwc.rc.writercount == 0)) && (rwc.rc.upgradecount == 0))));
        }
        private static bool IsRwHashEntryChanged(ReaderWriterCount lrwc, int id)
        {
            return (lrwc.threadid != id);
        }
        private bool IsWriterAcquired()
        {
            return ((this.owners & 0xbfffffff) == 0);
        }
        private void LazyCreateEvent(ref EventWaitHandle waitEvent, bool makeAutoResetEvent)
        {
            EventWaitHandle handle;
            this.ExitMyLock();
            if (makeAutoResetEvent)
            {
                handle = new AutoResetEvent(false);
            }
            else
            {
                handle = new ManualResetEvent(false);
            }
            this.EnterMyLock();
            if (waitEvent == null)
            {
                waitEvent = handle;
            }
            else
            {
                handle.Close();
            }
        }
        private void SetUpgraderWaiting()
        {
            this.owners |= 0x20000000;
        }
        private void SetWriterAcquired()
        {
            this.owners |= 0x80000000;
        }
        private void SetWritersWaiting()
        {
            this.owners |= 0x40000000;
        }
        private static void SpinWait(int SpinCount)
        {
            if ((SpinCount < 5) && (Environment.ProcessorCount > 1))
            {
                Thread.SpinWait(20 * SpinCount);
            }
            else if (SpinCount < 0x11)
            {
                Thread.Sleep(0);
            }
            else
            {
                Thread.Sleep(1);
            }
        }
        /// <summary>
        /// 尝试进度写锁
        /// </summary>
        /// <param name="millisecondsTimeout">超时毫秒</param>
        /// <returns>true/false</returns>
        public bool TryEnterReadLock(int millisecondsTimeout)
        {
            if (millisecondsTimeout < -1)
            {
                throw new ArgumentOutOfRangeException("millisecondsTimeout");
            }
            if (this.fDisposed)
            {
                throw new ObjectDisposedException(null);
            }
            ReaderWriterCount lrwc = null;
            int managedThreadId = Thread.CurrentThread.ManagedThreadId;
            if (!this.fIsReentrant)
            {
                if (managedThreadId == this.writeLockOwnerId)
                {
                    throw new MyLockRecursionException("MyLockRecursionException_ReadAfterWriteNotAllowed");
                }
                this.EnterMyLock();
                lrwc = this.GetThreadRWCount(managedThreadId, false);
                if (lrwc.readercount > 0)
                {
                    this.ExitMyLock();
                    throw new MyLockRecursionException("MyLockRecursionException_RecursiveReadNotAllowed");
                }
                if (managedThreadId == this.upgradeLockOwnerId)
                {
                    lrwc.readercount++;
                    this.owners++;
                    this.ExitMyLock();
                    return true;
                }
            }
            else
            {
                this.EnterMyLock();
                lrwc = this.GetThreadRWCount(managedThreadId, false);
                if (lrwc.readercount > 0)
                {
                    lrwc.readercount++;
                    this.ExitMyLock();
                    return true;
                }
                if (managedThreadId == this.upgradeLockOwnerId)
                {
                    lrwc.readercount++;
                    this.owners++;
                    this.ExitMyLock();
                    this.fUpgradeThreadHoldingRead = true;
                    return true;
                }
                if (managedThreadId == this.writeLockOwnerId)
                {
                    lrwc.readercount++;
                    this.owners++;
                    this.ExitMyLock();
                    return true;
                }
            }
            bool flag = true;
            int spinCount = 0;
        Label_013D:
            if (this.owners < 0xffffffe)
            {
                this.owners++;
                lrwc.readercount++;
            }
            else
            {
                if (spinCount < 20)
                {
                    this.ExitMyLock();
                    if (millisecondsTimeout == 0)
                    {
                        return false;
                    }
                    spinCount++;
                    SpinWait(spinCount);
                    this.EnterMyLock();
                    if (IsRwHashEntryChanged(lrwc, managedThreadId))
                    {
                        lrwc = this.GetThreadRWCount(managedThreadId, false);
                    }
                }
                else if (this.readEvent == null)
                {
                    this.LazyCreateEvent(ref this.readEvent, false);
                    if (IsRwHashEntryChanged(lrwc, managedThreadId))
                    {
                        lrwc = this.GetThreadRWCount(managedThreadId, false);
                    }
                }
                else
                {
                    flag = this.WaitOnEvent(this.readEvent, ref this.numReadWaiters, millisecondsTimeout);
                    if (!flag)
                    {
                        return false;
                    }
                    if (IsRwHashEntryChanged(lrwc, managedThreadId))
                    {
                        lrwc = this.GetThreadRWCount(managedThreadId, false);
                    }
                }
                goto Label_013D;
            }
            this.ExitMyLock();
            return flag;
        }
        /// <summary>
        /// 尝试进入读锁
        /// </summary>
        /// <param name="timeout">超时时间</param>
        /// <returns>true/false</returns>
        public bool TryEnterReadLock(TimeSpan timeout)
        {
            long totalMilliseconds = (long)timeout.TotalMilliseconds;
            if ((totalMilliseconds < -1L) || (totalMilliseconds > 0x7fffffffL))
            {
                throw new ArgumentOutOfRangeException("timeout");
            }
            int millisecondsTimeout = (int)timeout.TotalMilliseconds;
            return this.TryEnterReadLock(millisecondsTimeout);
        }
        /// <summary>
        /// 尝试进入可升级的读锁
        /// </summary>
        /// <param name="millisecondsTimeout">超时毫秒</param>
        /// <returns>true/false</returns>
        public bool TryEnterUpgradeableReadLock(int millisecondsTimeout)
        {
            ReaderWriterCount threadRWCount;
            if (millisecondsTimeout < -1)
            {
                throw new ArgumentOutOfRangeException("millisecondsTimeout");
            }
            if (this.fDisposed)
            {
                throw new ObjectDisposedException(null);
            }
            int managedThreadId = Thread.CurrentThread.ManagedThreadId;
            if (!this.fIsReentrant)
            {
                if (managedThreadId == this.upgradeLockOwnerId)
                {
                    throw new MyLockRecursionException("MyLockRecursionException_RecursiveUpgradeNotAllowed");
                }
                if (managedThreadId == this.writeLockOwnerId)
                {
                    throw new MyLockRecursionException("MyLockRecursionException_UpgradeAfterWriteNotAllowed");
                }
                this.EnterMyLock();
                threadRWCount = this.GetThreadRWCount(managedThreadId, true);
                if ((threadRWCount != null) && (threadRWCount.readercount > 0))
                {
                    this.ExitMyLock();
                    throw new MyLockRecursionException("MyLockRecursionException_UpgradeAfterReadNotAllowed");
                }
            }
            else
            {
                this.EnterMyLock();
                threadRWCount = this.GetThreadRWCount(managedThreadId, false);
                if (managedThreadId == this.upgradeLockOwnerId)
                {
                    threadRWCount.rc.upgradecount++;
                    this.ExitMyLock();
                    return true;
                }
                if (managedThreadId == this.writeLockOwnerId)
                {
                    this.owners++;
                    this.upgradeLockOwnerId = managedThreadId;
                    threadRWCount.rc.upgradecount++;
                    if (threadRWCount.readercount > 0)
                    {
                        this.fUpgradeThreadHoldingRead = true;
                    }
                    this.ExitMyLock();
                    return true;
                }
                if (threadRWCount.readercount > 0)
                {
                    this.ExitMyLock();
                    throw new MyLockRecursionException("MyLockRecursionException_UpgradeAfterReadNotAllowed");
                }
            }
            int spinCount = 0;
        Label_0139:
            if ((this.upgradeLockOwnerId == -1) && (this.owners < 0xffffffe))
            {
                this.owners++;
                this.upgradeLockOwnerId = managedThreadId;
            }
            else
            {
                if (spinCount < 20)
                {
                    this.ExitMyLock();
                    if (millisecondsTimeout == 0)
                    {
                        return false;
                    }
                    spinCount++;
                    SpinWait(spinCount);
                    this.EnterMyLock();
                    goto Label_0139;
                }
                if (this.upgradeEvent == null)
                {
                    this.LazyCreateEvent(ref this.upgradeEvent, true);
                    goto Label_0139;
                }
                if (this.WaitOnEvent(this.upgradeEvent, ref this.numUpgradeWaiters, millisecondsTimeout))
                {
                    goto Label_0139;
                }
                return false;
            }
            if (this.fIsReentrant)
            {
                if (IsRwHashEntryChanged(threadRWCount, managedThreadId))
                {
                    threadRWCount = this.GetThreadRWCount(managedThreadId, false);
                }
                threadRWCount.rc.upgradecount++;
            }
            this.ExitMyLock();
            return true;
        }
        /// <summary>
        /// 尝试进入可升级读锁
        /// </summary>
        /// <param name="timeout">超时时间</param>
        /// <returns>true/false</returns>
        public bool TryEnterUpgradeableReadLock(TimeSpan timeout)
        {
            long totalMilliseconds = (long)timeout.TotalMilliseconds;
            if ((totalMilliseconds < -1L) || (totalMilliseconds > 0x7fffffffL))
            {
                throw new ArgumentOutOfRangeException("timeout");
            }
            int millisecondsTimeout = (int)timeout.TotalMilliseconds;
            return this.TryEnterUpgradeableReadLock(millisecondsTimeout);
        }
        /// <summary>
        /// 尝试进入写锁
        /// </summary>
        /// <param name="millisecondsTimeout">超时毫秒</param>
        /// <returns>true/false</returns>
        public bool TryEnterWriteLock(int millisecondsTimeout)
        {
            ReaderWriterCount threadRWCount;
            if (millisecondsTimeout < -1)
            {
                throw new ArgumentOutOfRangeException("millisecondsTimeout");
            }
            if (this.fDisposed)
            {
                throw new ObjectDisposedException(null);
            }
            int managedThreadId = Thread.CurrentThread.ManagedThreadId;
            bool flag = false;
            if (!this.fIsReentrant)
            {
                if (managedThreadId == this.writeLockOwnerId)
                {
                    throw new MyLockRecursionException("MyLockRecursionException_RecursiveWriteNotAllowed");
                }
                if (managedThreadId == this.upgradeLockOwnerId)
                {
                    flag = true;
                }
                this.EnterMyLock();
                threadRWCount = this.GetThreadRWCount(managedThreadId, true);
                if ((threadRWCount != null) && (threadRWCount.readercount > 0))
                {
                    this.ExitMyLock();
                    throw new MyLockRecursionException("MyLockRecursionException_WriteAfterReadNotAllowed");
                }
            }
            else
            {
                this.EnterMyLock();
                threadRWCount = this.GetThreadRWCount(managedThreadId, false);
                if (managedThreadId == this.writeLockOwnerId)
                {
                    threadRWCount.rc.writercount++;
                    this.ExitMyLock();
                    return true;
                }
                if (managedThreadId == this.upgradeLockOwnerId)
                {
                    flag = true;
                }
                else if (threadRWCount.readercount > 0)
                {
                    this.ExitMyLock();
                    throw new MyLockRecursionException("MyLockRecursionException_WriteAfterReadNotAllowed");
                }
            }
            int spinCount = 0;
        Label_00EC:
            if (this.IsWriterAcquired())
            {
                this.SetWriterAcquired();
            }
            else
            {
                if (flag)
                {
                    uint numReaders = this.GetNumReaders();
                    if (numReaders == 1)
                    {
                        this.SetWriterAcquired();
                        goto Label_01DD;
                    }
                    if ((numReaders == 2) && (threadRWCount != null))
                    {
                        if (IsRwHashEntryChanged(threadRWCount, managedThreadId))
                        {
                            threadRWCount = this.GetThreadRWCount(managedThreadId, false);
                        }
                        if (threadRWCount.readercount > 0)
                        {
                            this.SetWriterAcquired();
                            goto Label_01DD;
                        }
                    }
                }
                if (spinCount < 20)
                {
                    this.ExitMyLock();
                    if (millisecondsTimeout == 0)
                    {
                        return false;
                    }
                    spinCount++;
                    SpinWait(spinCount);
                    this.EnterMyLock();
                    goto Label_00EC;
                }
                if (flag)
                {
                    if (this.waitUpgradeEvent != null)
                    {
                        if (!this.WaitOnEvent(this.waitUpgradeEvent, ref this.numWriteUpgradeWaiters, millisecondsTimeout))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        this.LazyCreateEvent(ref this.waitUpgradeEvent, true);
                    }
                    goto Label_00EC;
                }
                if (this.writeEvent == null)
                {
                    this.LazyCreateEvent(ref this.writeEvent, true);
                    goto Label_00EC;
                }
                if (this.WaitOnEvent(this.writeEvent, ref this.numWriteWaiters, millisecondsTimeout))
                {
                    goto Label_00EC;
                }
                return false;
            }
        Label_01DD:
            if (this.fIsReentrant)
            {
                if (IsRwHashEntryChanged(threadRWCount, managedThreadId))
                {
                    threadRWCount = this.GetThreadRWCount(managedThreadId, false);
                }
                threadRWCount.rc.writercount++;
            }
            this.ExitMyLock();
            this.writeLockOwnerId = managedThreadId;
            return true;
        }
        /// <summary>
        /// 尝试进入写锁
        /// </summary>
        /// <param name="timeout">超时时间</param>
        /// <returns>true/false</returns>
        public bool TryEnterWriteLock(TimeSpan timeout)
        {
            long totalMilliseconds = (long)timeout.TotalMilliseconds;
            if ((totalMilliseconds < -1L) || (totalMilliseconds > 0x7fffffffL))
            {
                throw new ArgumentOutOfRangeException("timeout");
            }
            int millisecondsTimeout = (int)timeout.TotalMilliseconds;
            return this.TryEnterWriteLock(millisecondsTimeout);
        }

        private bool WaitOnEvent(EventWaitHandle waitEvent, ref uint numWaiters, int millisecondsTimeout)
        {
            waitEvent.Reset();
            numWaiters++;
            this.fNoWaiters = false;
            if (this.numWriteWaiters == 1)
            {
                this.SetWritersWaiting();
            }
            if (this.numWriteUpgradeWaiters == 1)
            {
                this.SetUpgraderWaiting();
            }
            bool flag = false;
            this.ExitMyLock();
            try
            {
                flag = waitEvent.WaitOne(millisecondsTimeout, false);
            }
            finally
            {
                this.EnterMyLock();
                numWaiters--;
                if (((this.numWriteWaiters == 0) && (this.numWriteUpgradeWaiters == 0)) && ((this.numUpgradeWaiters == 0) && (this.numReadWaiters == 0)))
                {
                    this.fNoWaiters = true;
                }
                if (this.numWriteWaiters == 0)
                {
                    this.ClearWritersWaiting();
                }
                if (this.numWriteUpgradeWaiters == 0)
                {
                    this.ClearUpgraderWaiting();
                }
                if (!flag)
                {
                    this.ExitMyLock();
                }
            }
            return flag;
        }

        // Properties
        /// <summary>
        /// 当前读锁计数器
        /// </summary>
        public int CurrentReadCount
        {
            get
            {
                int numReaders = (int)this.GetNumReaders();
                if (this.upgradeLockOwnerId != -1)
                {
                    return (numReaders - 1);
                }
                return numReaders;
            }
        }
        /// <summary>
        /// 当前线程是否已持有读锁
        /// </summary>
        public bool IsReadLockHeld
        {
            get
            {
                return (this.RecursiveReadCount > 0);
            }
        }
        /// <summary>
        /// 当前线程是否已持有可升级的读锁
        /// </summary>
        public bool IsUpgradeableReadLockHeld
        {
            get
            {
                return (this.RecursiveUpgradeCount > 0);
            }
        }
        /// <summary>
        /// 当前线程是否已持有写锁
        /// </summary>
        public bool IsWriteLockHeld
        {
            get
            {
                return (this.RecursiveWriteCount > 0);
            }
        }
        /// <summary>
        /// 递归策略
        /// </summary>
        public MyLockRecursionPolicy RecursionPolicy
        {
            get
            {
                if (this.fIsReentrant)
                {
                    return MyLockRecursionPolicy.SupportsRecursion;
                }
                return MyLockRecursionPolicy.NoRecursion;
            }
        }
        /// <summary>
        /// 递归读锁持有数
        /// </summary>
        public int RecursiveReadCount
        {
            get
            {
                int managedThreadId = Thread.CurrentThread.ManagedThreadId;
                int readercount = 0;
                this.EnterMyLock();
                ReaderWriterCount threadRWCount = this.GetThreadRWCount(managedThreadId, true);
                if (threadRWCount != null)
                {
                    readercount = threadRWCount.readercount;
                }
                this.ExitMyLock();
                return readercount;
            }
        }
        /// <summary>
        /// 递归升级读锁持有数
        /// </summary>
        public int RecursiveUpgradeCount
        {
            get
            {
                int managedThreadId = Thread.CurrentThread.ManagedThreadId;
                if (this.fIsReentrant)
                {
                    int upgradecount = 0;
                    this.EnterMyLock();
                    ReaderWriterCount threadRWCount = this.GetThreadRWCount(managedThreadId, true);
                    if (threadRWCount != null)
                    {
                        upgradecount = threadRWCount.rc.upgradecount;
                    }
                    this.ExitMyLock();
                    return upgradecount;
                }
                if (managedThreadId == this.upgradeLockOwnerId)
                {
                    return 1;
                }
                return 0;
            }
        }
        /// <summary>
        /// 递归写锁持有数
        /// </summary>
        public int RecursiveWriteCount
        {
            get
            {
                int managedThreadId = Thread.CurrentThread.ManagedThreadId;
                int writercount = 0;
                if (this.fIsReentrant)
                {
                    this.EnterMyLock();
                    ReaderWriterCount threadRWCount = this.GetThreadRWCount(managedThreadId, true);
                    if (threadRWCount != null)
                    {
                        writercount = threadRWCount.rc.writercount;
                    }
                    this.ExitMyLock();
                    return writercount;
                }
                if (managedThreadId == this.writeLockOwnerId)
                {
                    return 1;
                }
                return 0;
            }
        }
        /// <summary>
        /// 等待读锁数目
        /// </summary>
        public int WaitingReadCount
        {
            get
            {
                return (int)this.numReadWaiters;
            }
        }
        /// <summary>
        /// 等待升级锁数据
        /// </summary>
        public int WaitingUpgradeCount
        {
            get
            {
                return (int)this.numUpgradeWaiters;
            }
        }
        /// <summary>
        /// 等待写锁数目
        /// </summary>
        public int WaitingWriteCount
        {
            get
            {
                return (int)this.numWriteWaiters;
            }
        }

    }
#endif
}
