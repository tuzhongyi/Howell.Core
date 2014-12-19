using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;

namespace Howell.Security
{

    /// <summary>
    /// 带上下文信息的会话对象
    /// </summary>
    /// <typeparam name="T">上下文信息类型</typeparam>
    public class Session<T> : IExpiration, IDisposable
    {
        private Timer m_ExpiredTimer = null;
        /// <summary>
        /// 
        /// </summary>
        private Boolean m_Disposed = false;

        /// <summary>
        /// Session构造
        /// </summary>
        /// <param name="id">会话Id</param>
        /// <param name="timeout">超时时间，单位（秒）.</param>
        /// <param name="remoteEP">远程终结点</param>
        /// <param name="context">上下文信息</param>
        public Session(String id, Int32 timeout, IPEndPoint remoteEP, T context)
        {
            this.Id = id;
            this.CreationTime = DateTime.Now;
            this.Timeout = timeout;
            this.RemoteEndPoint = remoteEP;
            this.Context = context;
            m_Disposed = false;
            m_ExpiredTimer = new Timer(ExpiredTimerCallback, null, System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);            
            Renewal();
        }
        /// <summary>
        /// 是否已过期
        /// </summary>
        /// <returns></returns>
        public virtual Boolean IsExpired()
        {
            lock (this)
            {
                if (m_Disposed == true) throw new ObjectDisposedException(this.GetType().FullName);
                return DateTime.Now > ExpiredTime;
            }
        }
        /// <summary>
        /// 续租会话
        /// </summary>
        public virtual void Renewal()
        {
            lock (this)
            {
                if (m_Disposed == true) throw new ObjectDisposedException(this.GetType().FullName);
                this.RenewalTime = DateTime.Now;
                this.ExpiredTime = this.RenewalTime.AddSeconds(Timeout);
                m_ExpiredTimer.Change(Timeout * 1000, System.Threading.Timeout.Infinite);
            }
        }
        /// <summary>
        /// 会话Id
        /// </summary>
        public String Id { get; private set; }
        /// <summary>
        /// 会话创建时间
        /// </summary>
        public DateTime CreationTime { get; private set; }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime RenewalTime { get; private set; }
        /// <summary>
        /// 会话过期时间
        /// </summary>
        public DateTime ExpiredTime { get; set; }
        /// <summary>
        /// 心跳超时时间 （单位：秒）
        /// </summary>
        public Int32 Timeout { get; private set; }
        /// <summary>
        /// 远程终结点
        /// </summary>
        public IPEndPoint RemoteEndPoint { get; private set; }
        /// <summary>
        /// 会话期满事件
        /// </summary>
        public event EventHandler<EventArgs> Expired;
        private void ExpiredTimerCallback(Object state)
        {
            m_ExpiredTimer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
            try
            {
                EventHandler<EventArgs> eventExpired = this.Expired;
                if (eventExpired != null)
                {
                    eventExpired(this, new EventArgs());
                }
            }
            catch { }
            finally
            {
            }
        }
        /// <summary>
        /// 上下文信息数据
        /// </summary>
        public T Context { get; private set; }
        #region IDisposable 成员
        /// <summary>
        /// 销毁Session对象
        /// </summary>
        public virtual void Dispose()
        {
            if (m_Disposed == true) throw new ObjectDisposedException(this.GetType().FullName);
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
        /// 销毁Session对象
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            try
            {
                if (!m_Disposed)
                {
                    if (disposing)
                    {
                        if (m_ExpiredTimer != null)
                        {
                            m_ExpiredTimer.Dispose();
                            m_ExpiredTimer = null;
                        }
                        if (Context is IDisposable)
                        {
                            (Context as IDisposable).Dispose();
                        }
                    }
                }
            }
            finally
            {
                m_Disposed = true;
            }
        }
        #endregion
    }
    /// <summary>
    /// 同步的Session容器
    /// </summary>
    /// <typeparam name="T">上下文信息类型</typeparam>
    public class SynchronizedSessionCollection<T> : System.Collections.Generic.SynchronizedKeyedCollection<String, Session<T>>
    {
        /// <summary>
        /// 在派生类中重写时，获取指定项的键。
        /// </summary>
        /// <param name="item">要检索其键的类型 T 的项。</param>
        /// <returns>类型 K 的键，用于类型 T 的指定 item。</returns>
        protected override String GetKeyForItem(Session<T> item)
        {
            return item.Id;
        }
    }
}
