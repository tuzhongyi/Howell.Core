using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Howell.Threading
{
    /// <summary>
    /// 基于线程时钟的包装类（主要用于解决线程时钟的释放问题）
    /// </summary>
    public class WrappedTimer : IDisposable
    {
        private System.Threading.Timer m_Timer = null;
        private Boolean disposed = false;
        private Boolean disposing = false;
        private ReaderWriterLockSlim locker = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

        /// <summary>
        /// 使用新创建的 System.Threading.Timer 对象作为状态对象，用一个无限周期和一个无限到期时间初始化 System.Threading.Timer类的新实例。
        /// </summary>
        /// <param name="callback"></param>
        public WrappedTimer(TimerCallback callback)
        {
            m_Timer = new System.Threading.Timer(callback);
        }
        /// <summary>
        /// 使用 32 位的有符号整数指定时间间隔，初始化 Timer 类的新实例。
        /// </summary>
        /// <param name="callback">一个 System.Threading.TimerCallback 委托，表示要执行的方法。</param>
        /// <param name="state">一个包含回调方法要使用的信息的对象，或者为 null。</param>
        /// <param name="dueTime">调用 callback 之前延迟的时间量（以毫秒为单位）。指定 System.Threading.Timeout.Infinite 可防止启动计时器。指定零(0) 可立即启动计时器。</param>
        /// <param name="period">调用 callback 的时间间隔（以毫秒为单位）。指定 System.Threading.Timeout.Infinite 可以禁用定期终止。</param>
        public WrappedTimer(TimerCallback callback, object state, int dueTime, int period)
        {
            m_Timer = new System.Threading.Timer(callback, state, dueTime, period);
        }
        /// <summary>
        /// 初始化 Timer 类的新实例
        /// </summary>
        /// <param name="callback">一个 System.Threading.TimerCallback 委托，表示要执行的方法。</param>
        /// <param name="state">一个包含回调方法要使用的信息的对象，或者为 null。</param>
        /// <param name="dueTime">调用 callback 之前延迟的时间量（以毫秒为单位）。指定 System.Threading.Timeout.Infinite 可防止启动计时器。指定零(0) 可立即启动计时器。</param>
        /// <param name="period">调用 callback 的时间间隔（以毫秒为单位）。指定 System.Threading.Timeout.Infinite 可以禁用定期终止。</param>
        public WrappedTimer(TimerCallback callback, object state, long dueTime, long period)
        {
            m_Timer = new System.Threading.Timer(callback, state, dueTime, period);
        }
        /// <summary>
        /// 初始化 Timer 类的新实例，使用 System.TimeSpan 值来度量时间间隔。
        /// </summary>
        /// <param name="callback">一个 System.Threading.TimerCallback 委托，表示要执行的方法。</param>
        /// <param name="state">一个包含回调方法要使用的信息的对象，或者为 null。</param>
        /// <param name="dueTime">System.TimeSpan，表示在 callback 参数调用它的方法之前延迟的时间量。指定 -1 毫秒以防止启动计时器。指定零 (0) 可立即启动计时器。</param>
        /// <param name="period">在调用 callback 所引用的方法之间的时间间隔。指定 -1 毫秒可以禁用定期终止。</param>
        public WrappedTimer(TimerCallback callback, object state, TimeSpan dueTime, TimeSpan period)
        {
            m_Timer = new System.Threading.Timer(callback, state, dueTime, period);
        }
        /// <summary>
        /// 用 32 位无符号整数来度量时间间隔，以初始化 Timer 类的新实例。
        /// </summary>
        /// <param name="callback"> 一个 System.Threading.TimerCallback 委托，表示要执行的方法。</param>
        /// <param name="state">一个包含回调方法要使用的信息的对象，或者为 null。</param>
        /// <param name="dueTime">调用 callback 之前延迟的时间量（以毫秒为单位）。指定 System.Threading.Timeout.Infinite 可防止启动计时器。指定零(0) 可立即启动计时器。</param>
        /// <param name="period">调用 callback 的时间间隔（以毫秒为单位）。指定 System.Threading.Timeout.Infinite 可以禁用定期终止。</param>
        public WrappedTimer(TimerCallback callback, object state, uint dueTime, uint period)
        {
            m_Timer = new System.Threading.Timer(callback, state, dueTime, period);
        }
        /// <summary>
        /// 更改计时器的启动时间和方法调用之间的间隔，用 32 位有符号整数度量时间间隔。
        /// </summary>
        /// <param name="dueTime">在调用构造 System.Threading.Timer 时指定的回调方法之前的延迟时间量（以毫秒为单位）。指定 System.Threading.Timeout.Infinite可防止重新启动计时器。指定零 (0) 可立即重新启动计时器。</param>
        /// <param name="period">调用构造 System.Threading.Timer 时指定的回调方法的时间间隔（以毫秒为单位）。指定 System.Threading.Timeout.Infinite可以禁用定期终止。</param>
        /// <returns>如果尚未释放当前实例，则为 true；否则为 false。</returns>
        public bool Change(int dueTime, int period)
        {
            using (ReaderWriterLockSlimAutoController write = new ReaderWriterLockSlimAutoController(locker, AutoLockPolicy.Write))
            {
                if (disposing == true) return false;
                if (disposed == true) throw new ObjectDisposedException(typeof(WrappedTimer).ToString());
                return m_Timer.Change(dueTime, period);
            }
        }
        /// <summary>
        /// 更改计时器的启动时间和方法调用之间的间隔，用 64 位有符号整数度量时间间隔。
        /// </summary>
        /// <param name="dueTime">在调用构造 System.Threading.Timer 时指定的回调方法之前的延迟时间量（以毫秒为单位）。指定 System.Threading.Timeout.Infinite可防止重新启动计时器。指定零 (0) 可立即重新启动计时器。</param>
        /// <param name="period">调用构造 System.Threading.Timer 时指定的回调方法的时间间隔（以毫秒为单位）。指定 System.Threading.Timeout.Infinite可以禁用定期终止。</param>
        /// <returns>如果尚未释放当前实例，则为 true；否则为 false。</returns>
        public bool Change(long dueTime, long period)
        {
            using (ReaderWriterLockSlimAutoController write = new ReaderWriterLockSlimAutoController(locker, AutoLockPolicy.Write))
            {
                if (disposing == true) return false;
                if (disposed == true) throw new ObjectDisposedException(typeof(WrappedTimer).ToString());
                return m_Timer.Change(dueTime, period);
            }
        }
        /// <summary>
        /// 更改计时器的启动时间和方法调用之间的时间间隔，使用 System.TimeSpan 值度量时间间隔。
        /// </summary>
        /// <param name="dueTime">一个 System.TimeSpan，表示在调用构造 System.Threading.Timer 时指定的回调方法之前的延迟时间量。指定负 -1毫秒以防止计时器重新启动。指定零 (0) 可立即重新启动计时器。</param>
        /// <param name="period">在构造 System.Threading.Timer 时指定的回调方法调用之间的时间间隔。指定 -1 毫秒可以禁用定期终止。</param>
        /// <returns>如果尚未释放当前实例，则为 true；否则为 false。</returns>
        public bool Change(TimeSpan dueTime, TimeSpan period)
        {
            using (ReaderWriterLockSlimAutoController write = new ReaderWriterLockSlimAutoController(locker, AutoLockPolicy.Write))
            {
                if (disposing == true) return false;
                if (disposed == true) throw new ObjectDisposedException(typeof(WrappedTimer).ToString());
                return m_Timer.Change(dueTime, period);
            }
        }
        /// <summary>
        /// 更改计时器的启动时间和方法调用之间的间隔，用 32 位无符号整数度量时间间隔。
        /// </summary>
        /// <param name="dueTime">在调用构造 System.Threading.Timer 时指定的回调方法之前的延迟时间量（以毫秒为单位）。指定 System.Threading.Timeout.Infinite可防止重新启动计时器。指定零 (0) 可立即重新启动计时器。</param>
        /// <param name="period">调用构造 System.Threading.Timer 时指定的回调方法的时间间隔（以毫秒为单位）。指定 System.Threading.Timeout.Infinite可以禁用定期终止。</param>
        /// <returns>如果尚未释放当前实例，则为 true；否则为 false。</returns>
        public bool Change(uint dueTime, uint period)
        {
            using (ReaderWriterLockSlimAutoController write = new ReaderWriterLockSlimAutoController(locker, AutoLockPolicy.Write))
            {
                if (disposing == true) return false;
                if (disposed == true) throw new ObjectDisposedException(typeof(WrappedTimer).ToString());
                return m_Timer.Change(dueTime, period);
            }
        }

        #region IDisposable 成员
        /// <summary>
        /// 释放由 System.Threading.Timer 的当前实例使用的所有资源。
        /// </summary>
        public void Dispose()
        {
            //WaitHandle handle = new InvalidWaitHandle();
            WaitHandle handle = new EventWaitHandle(false, EventResetMode.ManualReset);
            try
            {
                Dispose( handle);
            }
            finally
            {
                handle.Close();
            }
        }

        #endregion
        /// <summary>
        /// 释放 System.Threading.Timer 的当前实例使用的所有资源并在释放完计时器时发出信号。
        /// </summary>
        /// <param name="notifyObject">释放完 Timer 时要发出其信号的 System.Threading.WaitHandle。</param>
        /// <returns>如果函数成功，则为 true；否则为 false。</returns>
        public bool Dispose(WaitHandle notifyObject)
        {
            using (ReaderWriterLockSlimAutoController write = new ReaderWriterLockSlimAutoController(locker, AutoLockPolicy.Write))
            {
                if (disposing == true) return false;
                disposing = true;
                if (disposed == true) throw new ObjectDisposedException(typeof(WrappedTimer).ToString());
                try
                {
                    bool result = false;
                    locker.ExitWriteLock();
                    try
                    {
                        result = m_Timer.Dispose(notifyObject);
                        notifyObject.WaitOne();
                    }
                    finally
                    {
                        locker.EnterWriteLock();
                    }
                    m_Timer = null;
                    return result;
                }
                finally
                {
                    disposing = false;
                    disposed = true;
                }
            }
        }
    }
}
