using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Howell.Threading
{
    /// <summary>
    /// 自定义ReaderWriterLockSlim 扩展 主要用于测试死锁问题
    /// </summary>
    public class MyReaderWriterLockSlimEx : MyReaderWriterLockSlim
    {
        private String m_Name;
        /// <summary>
        /// Constructor
        /// </summary>
        public MyReaderWriterLockSlimEx()
            : base(MyLockRecursionPolicy.NoRecursion)
        {
            m_Name = "";
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="recursionPolicy">递归策略 默认是不支持递归</param>
        public MyReaderWriterLockSlimEx(MyLockRecursionPolicy recursionPolicy)
            : base(recursionPolicy)
        {
            m_Name = "";
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="recursionPolicy">递归策略 默认是不支持递归</param>
        /// <param name="Name">Name</param>
        public MyReaderWriterLockSlimEx(MyLockRecursionPolicy recursionPolicy, String Name)
            : base(recursionPolicy)
        {
            m_Name = Name;
        }
        /// <summary>
        /// 进入读锁 同一时间允许多个线程进入
        /// </summary>
        public new void EnterReadLock()
        {
            try
            {
                if (m_Name != "")
                    Console.WriteLine("Name:{0} {1} EnterReadLock!", m_Name, Thread.CurrentThread.ManagedThreadId);
                base.EnterReadLock();
            }
            catch
            {
                if (m_Name != "")
                    Console.WriteLine("Exception Name:{0} {1} EnterReadLock!", m_Name, Thread.CurrentThread.ManagedThreadId);
            }
        }
        /// <summary>
        /// 退出读锁
        /// </summary>
        public new void ExitReadLock()
        {
            try
            {
                if (m_Name != "")
                    Console.WriteLine("Name:{0} {1} ExitReadLock!", m_Name, Thread.CurrentThread.ManagedThreadId);
                base.ExitReadLock();
            }
            catch
            {
                if (m_Name != "")
                    Console.WriteLine("Exception Name:{0} {1} ExitReadLock!", m_Name, Thread.CurrentThread.ManagedThreadId);
            }
        }

        /// <summary>
        /// 尝试进入可升级为写锁的读锁 同一时间只允许一个线程进入
        /// </summary>
        public new void EnterUpgradeableReadLock()
        {
            try
            {
                if (m_Name != "")
                    Console.WriteLine("Name:{0} {1} EnterUpgradeableReadLock!", m_Name, Thread.CurrentThread.ManagedThreadId);
                base.EnterUpgradeableReadLock();
            }
            catch
            {
                if (m_Name != "")
                    Console.WriteLine("Exception Name:{0} {1} EnterUpgradeableReadLock!", m_Name, Thread.CurrentThread.ManagedThreadId);
            }
        }
        /// <summary>
        /// 退出可升级的读锁
        /// </summary>
        public new void ExitUpgradeableReadLock()
        {
            try
            {
                if (m_Name != "")
                    Console.WriteLine("Name:{0} {1} ExitUpgradeableReadLock!", m_Name, Thread.CurrentThread.ManagedThreadId);
                base.ExitUpgradeableReadLock();
            }
            catch
            {
                if (m_Name != "")
                    Console.WriteLine("Exception Name:{0} {1} ExitUpgradeableReadLock!", m_Name, Thread.CurrentThread.ManagedThreadId);
            }
        }
        /// <summary>
        /// 尝试进入写锁 同一时间只允许一个线程进入
        /// </summary>
        public new void EnterWriteLock()
        {
            try
            {
                if (m_Name != "")
                    Console.WriteLine("Name:{0} {1} EnterWriteLock!", m_Name, Thread.CurrentThread.ManagedThreadId);
                base.EnterWriteLock();
            }
            catch
            {
                if (m_Name != "")
                    Console.WriteLine("Exception Name:{0} {1} EnterWriteLock!", m_Name, Thread.CurrentThread.ManagedThreadId);
            }
        }
        /// <summary>
        /// 退出写锁
        /// </summary>
        public new void ExitWriteLock()
        {

            try
            {
                if (m_Name != "")
                    Console.WriteLine("Name:{0} {1} ExitWriteLock!", m_Name, Thread.CurrentThread.ManagedThreadId);
                base.ExitWriteLock();
            }
            catch
            {
                if (m_Name != "")
                    Console.WriteLine("Exception Name:{0} {1} ExitWriteLock!", m_Name, Thread.CurrentThread.ManagedThreadId);
            }
        }
    }
}
