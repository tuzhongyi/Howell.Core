using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Globalization;

namespace Howell.Threading
{
    /// <summary>
    /// 线程包装类
    /// </summary>
    public class WrappedThread 
    {
        private Thread m_Thread = null;
        private EventWaitHandle m_WaitHandle = null;
        private Boolean m_WaitFortExit = false;
        /// <summary>
        /// 见System.Threading.Thread的构造函数说明
        /// </summary>
        /// <param name="start"></param>
        public WrappedThread(ParameterizedThreadStart start)
        {
            m_ParameterizedThreadCallback = start;
            m_Thread = new Thread(new ParameterizedThreadStart(CurrentThreadCallback));
        }
        /// <summary>
        /// 见System.Threading.Thread的构造函数说明
        /// </summary>
        /// <param name="start"></param>
        public WrappedThread(ThreadStart start)
        {
            m_ThreadCallback = start;
            m_Thread = new Thread(new ThreadStart(CurrentThreadCallback));
        }
        /// <summary>
        /// 见System.Threading.Thread的构造函数说明
        /// </summary>
        /// <param name="start"></param>
        /// <param name="maxStackSize"></param>
        public WrappedThread(ParameterizedThreadStart start, int maxStackSize)
        {
            m_ParameterizedThreadCallback = start;
            m_Thread = new Thread(new ParameterizedThreadStart(CurrentThreadCallback), maxStackSize);
        }
        /// <summary>
        /// 见System.Threading.Thread的构造函数说明
        /// </summary>
        /// <param name="start"></param>
        /// <param name="maxStackSize"></param>
        public WrappedThread(ThreadStart start, int maxStackSize)
        {
            m_ThreadCallback = start;
            m_Thread = new Thread(new ThreadStart(CurrentThreadCallback), maxStackSize);
        }

        private ParameterizedThreadStart m_ParameterizedThreadCallback;
        private ThreadStart m_ThreadCallback;
        #region Properties
        /// <summary>
        /// 见System.Threading.Thread.CurrentCulture
        /// </summary>
        public CultureInfo CurrentCulture 
        {
            get { return m_Thread.CurrentCulture; }
            set { m_Thread.CurrentCulture = value; }
        }
        /// <summary>
        /// 见System.Threading.Thread.CurrentUICulture
        /// </summary>
        public CultureInfo CurrentUICulture 
        {
            get { return m_Thread.CurrentUICulture; }
            set { m_Thread.CurrentUICulture = value; }
        }
        /// <summary>
        /// 见System.Threading.Thread.ExecutionContext
        /// </summary>
        public ExecutionContext ExecutionContext 
        {
            get { return m_Thread.ExecutionContext; }
        }
        /// <summary>
        /// 见System.Threading.Thread.IsAlive
        /// </summary>
        public bool IsAlive  
        {
            get { return m_Thread.IsAlive; }
        }
        /// <summary>
        /// 见System.Threading.Thread.IsBackground
        /// </summary>
        public bool IsBackground
        {
            get { return m_Thread.IsBackground; }
            set { m_Thread.IsBackground = value; }
        }
        /// <summary>
        /// 见System.Threading.Thread.IsThreadPoolThread
        /// </summary>
        public bool IsThreadPoolThread 
        {
            get { return m_Thread.IsThreadPoolThread; }
        }
        /// <summary>
        /// 见System.Threading.Thread.ManagedThreadId
        /// </summary>
        public int ManagedThreadId 
        {
            get { return m_Thread.ManagedThreadId; }
        }
        /// <summary>
        /// 见System.Threading.Thread.Name
        /// </summary>
        public string Name 
        {
            get { return m_Thread.Name; }
            set { m_Thread.Name = value; }
        }
        /// <summary>
        /// 见System.Threading.Thread.Priority
        /// </summary>
        public ThreadPriority Priority
        {
            get { return m_Thread.Priority; }
            set { m_Thread.Priority = value; }
        }
        /// <summary>
        /// 见System.Threading.Thread.ThreadState
        /// </summary>
        public ThreadState ThreadState
        {
            get { return m_Thread.ThreadState; }
        }
        #endregion
        #region Method
        /// <summary>
        /// 见System.Threading.Thread.Abort说明
        /// </summary>
        public void Abort()
        {
            m_Thread.Abort();
        }
        /// <summary>
        /// 见System.Threading.Thread.Abort说明
        /// </summary>
        /// <param name="stateInfo"></param>
        public void Abort(object stateInfo)
        {
            m_Thread.Abort(stateInfo);
        }
        /// <summary>
        /// 见System.Threading.Thread.Start说明
        /// </summary>
        public void Start()
        {
            m_Thread.Start();            
            m_WaitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
            m_WaitFortExit = false;
        }
        /// <summary>
        /// 见System.Threading.Thread.Start说明
        /// </summary>
        /// <param name="parameter"></param>
        public void Start(object parameter)
        {
            m_Thread.Start(parameter);
            m_WaitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
            m_WaitFortExit = false;
        }
        /// <summary>
        /// 见System.Threading.Thread.Interrupt说明
        /// </summary>
        public void Interrupt()
        {
            m_Thread.Interrupt();
        }
        /// <summary>
        /// 见System.Threading.Thread.Join说明
        /// </summary>
        public void Join()
        {
            m_Thread.Join();
        }
        /// <summary>
        /// 见System.Threading.Thread.Join说明
        /// </summary>
        /// <param name="millisecondsTimeout"></param>
        /// <returns></returns>
        public bool Join(int millisecondsTimeout)
        {
            return m_Thread.Join(millisecondsTimeout);
        }
        /// <summary>
        /// 见System.Threading.Thread.Join说明
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public bool Join(TimeSpan timeout)
        {
            return m_Thread.Join(timeout);
        }
        /// <summary>
        /// 停止Thread
        /// </summary>
        public void Stop()
        {
            if (m_WaitFortExit == false && m_WaitHandle != null)
            {
                m_WaitFortExit = true;
                m_WaitHandle.WaitOne(Timeout.Infinite);
                m_WaitHandle.Close();
                m_WaitHandle = null;
                m_WaitFortExit = false;
            }
            return;
        }
        #endregion
        private void CurrentThreadCallback()
        {
            if(m_WaitFortExit == false)
            {
                m_ThreadCallback();
                return;
            }
            m_WaitHandle.Set();
        }
        private void CurrentThreadCallback(object stateObject)
        {
            if (m_WaitFortExit == false)
            {
                m_ParameterizedThreadCallback(stateObject);
                return;
            }
            m_WaitHandle.Set();
        }
    }
}
