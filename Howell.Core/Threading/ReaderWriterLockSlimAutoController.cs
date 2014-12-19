using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Howell.Threading
{    
    /// <summary>
    /// 自定义锁方式
    /// </summary>
    public enum AutoLockPolicy 
    {
        /// <summary>
        /// 只读锁
        /// </summary>
        Read,
        /// <summary>
        /// 写锁
        /// </summary>
        Write,
        /// <summary>
        /// 可升级读锁
        /// </summary>
        UpgradeableRead,
    }
    /// <summary>
    /// 读写锁自动释放控制器 设计初衷：由于使用CSharp Try/Finally 语句是可能Try前的锁和Finally内的错误的携程锁不同的2个锁或进出锁的方式不用，
    /// 所有在
    /// </summary>
    public class ReaderWriterLockSlimAutoController : IDisposable
    {
        private AutoLockPolicy m_Policy;
        private ReaderWriterLockSlim m_Locker;
        /// <summary>
        /// 创建 读写锁自动释放控制器
        /// </summary>
        /// <param name="locker">外部需要自动处理的锁</param>
        /// <param name="Policy">锁定策略<see cref="AutoLockPolicy"></see></param>
        public ReaderWriterLockSlimAutoController(ReaderWriterLockSlim locker, AutoLockPolicy Policy)
        {
            m_Policy = Policy;
            m_Locker = locker;
            EnterLock();
        }
        #region IDisposable 成员
        /// <summary>
        /// Using 作用域结束后退出锁
        /// </summary>
        public void Dispose()
        {
            ExitLock();
            m_Locker = null;
        }

        #endregion
        private void EnterLock()
        {
            switch (m_Policy)
            {
                case AutoLockPolicy.Read:
                    m_Locker.EnterReadLock();
                    break;
                case AutoLockPolicy.Write:
                    m_Locker.EnterWriteLock();
                    break;
                case AutoLockPolicy.UpgradeableRead:
                    m_Locker.EnterUpgradeableReadLock();
                    break;
                default:
                    break;
            }
        }
        private void ExitLock()
        {

            switch (m_Policy)
            {
                case AutoLockPolicy.Read:
                    m_Locker.ExitReadLock();
                    break;
                case AutoLockPolicy.Write:
                    m_Locker.ExitWriteLock();
                    break;
                case AutoLockPolicy.UpgradeableRead:
                    m_Locker.ExitUpgradeableReadLock();
                    break;
                default:
                    break;
            }
        }
    }
}
