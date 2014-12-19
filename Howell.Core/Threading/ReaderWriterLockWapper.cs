using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Howell.Threading
{
    /// <summary>
    /// 读写锁封装类 static
    /// </summary>
    public static class ReaderWriterLockWapper
    {
        /// <summary>
        /// 申请读锁定
        /// </summary>
        /// <param name="rwlock">读写锁</param>
        /// <returns>true-申请 false-未申请</returns>
        public static Boolean ReaderLock(ReaderWriterLock rwlock)
        {
            if (false == rwlock.IsReaderLockHeld && false == rwlock.IsWriterLockHeld)
            {
                rwlock.AcquireReaderLock(Timeout.Infinite);
                return true;
            }
            return false;
        }
        /// <summary>
        /// 解除读锁定
        /// </summary>
        /// <param name="rwlock">读写锁</param>
        /// <param name="locked">是否已申请</param>
        public static void ReleaseReaderLock(ReaderWriterLock rwlock, Boolean locked)
        {
            if (locked)
            {
                rwlock.ReleaseReaderLock();
            }
        }
        /// <summary>
        /// 申请写入锁
        /// </summary>
        /// <param name="rwlock">读写锁</param>
        /// <returns>Cookie对象</returns>
        public static IntelligentLockCookie WriterLock(ReaderWriterLock rwlock)
        {
            IntelligentLockCookie ilc = new IntelligentLockCookie();
            ilc.upgrade = false;
            if (rwlock.IsWriterLockHeld)
            {
                ilc.locked = false;
                ilc.upgrade = false;
            }
            else
            {
                if (rwlock.IsReaderLockHeld)
                {
                    //注意: 在线程调用 UpgradeToWriterLock 时，不管锁计数为多少都将释放读线程锁，并且线程将转到写线程锁队列的末尾。因此，在请求升级的线程被授予写线程锁之前，其他线程可以写入资源。
                    //此操作相当危险所以暂时不使用
                    throw new AbandonedMutexException("ReaderWriterLock.UpgradeToWriterLock is dangerous method!");
                    //ilc.lc = rwlock.UpgradeToWriterLock(Timeout.Infinite);
                    //ilc.locked = true;
                    //ilc.upgrade = true;
                }
                else
                {
                    rwlock.AcquireWriterLock(Timeout.Infinite);
                    ilc.locked = true;
                    ilc.upgrade = false;
                }
            }
            return ilc;
        }
        /// <summary>
        /// 解除写入锁
        /// </summary>
        /// <param name="rwlock">读写锁</param>
        /// <param name="ilc">Cookie对象</param>
        public static void ReleaseWriterLock(ReaderWriterLock rwlock, IntelligentLockCookie ilc)
        {
            if (true == ilc.locked)
            {
                if (true == ilc.upgrade)
                {
                    LockCookie lc = ilc.lc;
                    rwlock.DowngradeFromWriterLock(ref lc);
                }
                else
                {
                    rwlock.ReleaseWriterLock();
                }
            }
        }
    }
    /// <summary>
    /// 智能LockCookie
    /// </summary>
    public class IntelligentLockCookie
    {
        private LockCookie m_lc;
        /// <summary>
        /// 锁Cookie
        /// </summary>
        public LockCookie lc
        {
            get { return m_lc; }
            set { m_lc = value; }
        }
        private Boolean m_upgrade;
        /// <summary>
        /// 是否升级
        /// </summary>
        public Boolean upgrade
        {
            get { return m_upgrade; }
            set { m_upgrade = value; }
        }
        private Boolean m_locked;
        /// <summary>
        /// 是否加锁
        /// </summary>
        public Boolean locked
        {
            get { return m_locked; }
            set { m_locked = value; }
        }
    }
}
