using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace System.Net.NetworkInformation
{
    /// <summary>
    /// 多次Ping调用完成事件参数
    /// </summary>
    public class MultiPingCompletedEventArgs : AsyncCompletedEventArgs
    {
        /// <summary>
        /// 创建多次Ping调用完成事件参数
        /// </summary>
        /// <param name="reply">回应信息</param>
        /// <param name="error">内部错误</param>
        /// <param name="cancelled">是否取消操作标记</param>
        /// <param name="userState">用户状态值</param>
        public MultiPingCompletedEventArgs(MultiPingReply reply, Exception error, bool cancelled, object userState)
            : base(error, cancelled, userState)
        {
            this.Reply = reply;
        }
        /// <summary>
        /// 多次Ping命令回送信息
        /// </summary>
        public MultiPingReply Reply { get; private set; }
    }
    /// <summary>
    /// 多次Ping命令对象完成事件的方法
    /// </summary>
    /// <param name="sender">发送对象</param>
    /// <param name="e">事件参数</param>
    public delegate void MultiPingCompletedEventHandler(Object sender,MultiPingCompletedEventArgs e);
    /// <summary>
    /// 多次Ping命令回送信息
    /// </summary>
    public class MultiPingReply
    {
        /// <summary>
        /// 多次Ping命令回送信息
        /// </summary>
        /// <param name="address">Ping的目标地址</param>
        /// <param name="replys">每次Ping命令的数据回送数组</param>
        /// <param name="loss">丢包数</param>
        /// <param name="total">总的Ping包数</param>
        public MultiPingReply(IPAddress address,PingReply[] replys, Int32 loss, Int32 total)
        {
            this.Address = address;
            this.Replys = replys;
            this.Loss = loss;
            this.Total = total;
            this.LossRate = (Double)loss / (Double)total;
        }
        /// <summary>
        /// 获取包含数据的对象，该数据描述发送 Internet 控制消息协议 (ICMP) 回送请求消息并接受相应的 ICMP 回送答复消息的尝试数组。
        /// </summary>
        public PingReply [] Replys { get; private set; }
        /// <summary>
        /// 地址
        /// </summary>
        public IPAddress Address { get; private set; }
        /// <summary>
        /// 丢包数
        /// </summary>
        public Int32 Loss { get; private set; }
        /// <summary>
        /// 总包数
        /// </summary>
        public Int32 Total { get; private set; }
        /// <summary>
        /// 丢包率
        /// </summary>
        public Double LossRate { get; private set; }

        /// <summary>
        /// 转换为字符串格式
        /// </summary>
        /// <returns>返回字符串。</returns>
        public String ToContentString()
        {
            return String.Format("{0} ping relays from {1}: loss:{2} total:{3} loss_rate:{4}.", Total - Loss, Address, Loss, Total, LossRate);
        }
    }

    /// <summary>
    /// 允许应用程序确定是否可通过网络访问远程计算机，多次Ping调用实现。
    /// </summary>
    public class MultiPing : IDisposable
    {
        private Int32 m_Interval = 500;
        private Ping m_Ping = null;
        private Boolean m_Pinging = false;
        private Int32 m_Times = 0;
        private Object m_UserToken = null;
        
        private IList<PingReply> m_Replys = new List<PingReply>(); 
        /// <summary>
        /// 创建允许应用程序确定是否可通过网络访问远程计算机，多次Ping调用实现。
        /// </summary>
        public MultiPing()
            : this(500)
        {

        }
        /// <summary>
        /// 创建允许应用程序确定是否可通过网络访问远程计算机，多次Ping调用实现。
        /// </summary>
        /// <param name="interval">Ping命令发送间隔毫秒值，默认是500毫秒</param>
        public MultiPing(Int32 interval)
        {
            m_Interval = interval;
            m_Pinging = false;
            m_Ping = new Ping();
            m_Ping.PingCompleted += new PingCompletedEventHandler(Ping_PingCompleted);
        }
        /// <summary>
        ///  释放由 System.Net.NetworkInformation.MultiPing 对象使用的非托管资源，并可根据需要释放托管资源。
        /// </summary>
        public void Dispose()
        {
            lock(this)
            {
                Dispose(true);
            }
        }

        /// <summary>
        /// 释放由 System.Net.NetworkInformation.MultiPing 对象使用的非托管资源，并可根据需要释放托管资源。
        /// </summary>
        /// <param name="disposing">如果为 true，则释放托管资源和非托管资源；如果为 false，则仅释放非托管资源。</param>
        protected virtual void Dispose(bool disposing)
        {
            if (m_Ping != null)
            {
                m_Ping.Dispose();
                m_Ping = null;
            }
        }

        void Ping_PingCompleted(object sender, PingCompletedEventArgs e)
        {
            if(e.Cancelled == true)
            {
                try
                {
                    OnMultiPingCompleted(new MultiPingCompletedEventArgs(CreateReply(e.Reply.Address, m_Replys.ToArray()), e.Error, e.Cancelled, e.UserState));
                }
                catch { }
                finally
                {
                    m_Pinging = false;
                }
                return;
            }
            else
            {
                m_Replys.Add(e.Reply);
                m_Times--;
                try
                {
                    OnSinglePingCompleted(e);
                }
                catch { }
                if(m_Times <= 0)
                {
                    try
                    {
                        OnMultiPingCompleted(new MultiPingCompletedEventArgs(CreateReply(e.Reply.Address, m_Replys.ToArray()), e.Error, e.Cancelled, e.UserState));
                    }
                    catch { }
                    finally
                    {
                        m_Pinging = false;
                    }
                    return;
                }
                else
                {
                    Threading.Thread.Sleep(m_Interval);
                    m_Ping.SendAsync(e.Reply.Address, e.UserState);
                }
            }
        }
        /// <summary>
        /// 尝试以异步方式向指定 System.Net.IPAddress 的计算机发送 Internet 控制消息协议 (ICMP) 回送消息，并从该计算机接收相应的
        /// ICMP 回送答复消息。
        /// </summary>
        /// <param name="address">标识 ICMP 回送消息目标计算机的 System.Net.IPAddress。</param>
        /// <param name="times">Ping命令发送次数。</param>
        /// <param name="userToken">一个对象，此对象将被传递给异步操作完成后所调用的方法。</param>
        public void SendAsync(IPAddress address, Int32 times, Object userToken)
        {
            SendAsync(address.ToString(), times, userToken);
        }
        /// <summary>
        /// 尝试以异步方式向指定的计算机发送 Internet 控制消息协议 (ICMP) 回送消息，并从该计算机接收相应的 ICMP 回送答复消息。
        /// </summary>
        /// <param name="hostNameOrAddress">一个 System.String，它标识作为 ICMP 回送消息目标的计算机。为此参数指定的值可以是主机名，也可以是以字符串形式表示的 IP 地址。</param>
        /// <param name="times">Ping命令发送次数。</param>
        /// <param name="userToken">一个对象，此对象将被传递给异步操作完成后所调用的方法。</param>
        public void SendAsync(String hostNameOrAddress, Int32 times, Object userToken)
        {
            SendAsync(hostNameOrAddress, times, 3000, userToken);
        }
        /// <summary>
        /// 尝试以异步方式向指定 System.Net.IPAddress 的计算机发送 Internet 控制消息协议 (ICMP) 回送消息，并从该计算机接收相应的ICMP 回送答复消息。此重载使您可以为操作指定一个超时值。
        /// </summary>
        /// <param name="address">标识 ICMP 回送消息目标计算机的 System.Net.IPAddress。</param>
        /// <param name="times">Ping命令发送次数。</param>
        /// <param name="timeout">一个 System.Int32 值，指定（发送回送消息后）等待 ICMP 回送答复消息的最大毫秒数。</param>
        /// <param name="userToken">一个对象，此对象将被传递给异步操作完成后所调用的方法。</param>
        public void SendAsync(IPAddress address, Int32 times, Int32 timeout, Object userToken)
        {
            SendAsync(address.ToString(), times, timeout, userToken);
        }
        /// <summary>
        /// 尝试以异步方式向指定 System.Net.IPAddress 的计算机发送 Internet 控制消息协议 (ICMP) 回送消息，并从该计算机接收相应的ICMP 回送答复消息。此重载使您可以为操作指定一个超时值。
        /// </summary>
        /// <param name="hostNameOrAddress">一个 System.String，它标识作为 ICMP 回送消息目标的计算机。为此参数指定的值可以是主机名，也可以是以字符串形式表示的 IP 地址。</param>
        /// <param name="times">Ping命令发送次数。</param>
        /// <param name="timeout">一个 System.Int32 值，指定（发送回送消息后）等待 ICMP 回送答复消息的最大毫秒数。</param>
        /// <param name="userToken">一个对象，此对象将被传递给异步操作完成后所调用的方法。</param>
        public void SendAsync(String hostNameOrAddress, Int32 times, Int32 timeout, Object userToken)
        {
            SendAsync(hostNameOrAddress, times, timeout, new Byte[32], userToken);
        }
        /// <summary>
        /// 尝试用指定的数据缓冲区以异步方式将 Internet 控制消息协议 (ICMP) 回显消息发送到具有指定的 System.Net.IPAddress
        /// 的计算机，并从该计算机接收对应的 ICMP 回显回复消息。此重载使您可以为操作指定一个超时值。
        /// </summary>
        /// <param name="address">标识 ICMP 回送消息目标计算机的 System.Net.IPAddress。</param>
        /// <param name="times">Ping命令发送次数。</param>
        /// <param name="timeout">一个 System.Int32 值，指定（发送回送消息后）等待 ICMP 回送答复消息的最大毫秒数。</param>
        /// <param name="buffer">一个 System.Byte 数组，它包含要与 ICMP 回送消息一起发送并在 ICMP 回送应答消息中返回的数据。该数组包含的字节数不能超过 65,500个字节</param>
        /// <param name="userToken">一个对象，此对象将被传递给异步操作完成后所调用的方法。</param>
        public void SendAsync(IPAddress address, Int32 times, Int32 timeout, Byte[] buffer, Object userToken)
        {
            SendAsync(address.ToString(), times, timeout, buffer, userToken);
        }
        /// <summary>
        /// 尝试用指定的数据缓冲区以异步方式将 Internet 控制消息协议 (ICMP) 回显消息发送到具有指定的 System.Net.IPAddress
        /// 的计算机，并从该计算机接收对应的 ICMP 回显回复消息。此重载使您可以为操作指定一个超时值。
        /// </summary>
        /// <param name="hostNameOrAddress">一个 System.String，它标识作为 ICMP 回送消息目标的计算机。为此参数指定的值可以是主机名，也可以是以字符串形式表示的 IP 地址。</param>
        /// <param name="times">Ping命令发送次数。</param>
        /// <param name="timeout">一个 System.Int32 值，指定（发送回送消息后）等待 ICMP 回送答复消息的最大毫秒数。</param>
        /// <param name="buffer">一个 System.Byte 数组，它包含要与 ICMP 回送消息一起发送并在 ICMP 回送应答消息中返回的数据。该数组包含的字节数不能超过 65,500个字节</param>
        /// <param name="userToken">一个对象，此对象将被传递给异步操作完成后所调用的方法。</param>
        public void SendAsync(String hostNameOrAddress, Int32 times, Int32 timeout, Byte[] buffer, Object userToken)
        {
            SendAsync(hostNameOrAddress, times, timeout, buffer, new PingOptions() { Ttl = 32, DontFragment = true }, userToken);
        }
        /// <summary>
        /// 尝试用指定的数据缓冲区以异步方式将 Internet 控制消息协议 (ICMP) 回显消息发送到具有指定的 System.Net.IPAddress
        /// 的计算机，并从该计算机接收对应的 ICMP 回显回复消息。此重载允许您指定操作的超时值，并控制 ICMP 回显消息数据包的碎片和生存时间值。
        /// </summary>
        /// <param name="address">标识 ICMP 回送消息目标计算机的 System.Net.IPAddress。</param>
        /// <param name="times">Ping命令发送次数。</param>
        /// <param name="timeout">一个 System.Byte 数组，它包含要与 ICMP 回送消息一起发送并在 ICMP 回送应答消息中返回的数据。该数组包含的字节数不能超过 65,500个字节。</param>
        /// <param name="buffer">一个 System.Byte 数组，它包含要与 ICMP 回送消息一起发送并在 ICMP 回送应答消息中返回的数据。该数组包含的字节数不能超过 65,500个字节</param>
        /// <param name="options">一个 System.Net.NetworkInformation.PingOptions 对象，用于控制 ICMP 回显消息数据包的碎片和生存时间值。</param>
        /// <param name="userToken">一个对象，此对象将被传递给异步操作完成后所调用的方法。</param>
        public void SendAsync(IPAddress address, Int32 times, Int32 timeout, Byte[] buffer, PingOptions options, Object userToken)
        {
            SendAsync(address.ToString(), times, timeout, buffer, options, userToken);
        }
        /// <summary>
        /// 尝试用指定的数据缓冲区以异步方式将 Internet 控制消息协议 (ICMP) 回显消息发送到具有指定的 System.Net.IPAddress
        /// 的计算机，并从该计算机接收对应的 ICMP 回显回复消息。此重载允许您指定操作的超时值，并控制 ICMP 回显消息数据包的碎片和生存时间值。
        /// </summary>
        /// <param name="hostNameOrAddress">一个 System.String，它标识作为 ICMP 回送消息目标的计算机。为此参数指定的值可以是主机名，也可以是以字符串形式表示的 IP 地址。</param>
        /// <param name="times">Ping命令发送次数。</param>
        /// <param name="timeout">一个 System.Byte 数组，它包含要与 ICMP 回送消息一起发送并在 ICMP 回送应答消息中返回的数据。该数组包含的字节数不能超过 65,500个字节。</param>
        /// <param name="buffer">一个 System.Byte 数组，它包含要与 ICMP 回送消息一起发送并在 ICMP 回送应答消息中返回的数据。该数组包含的字节数不能超过 65,500个字节</param>
        /// <param name="options">一个 System.Net.NetworkInformation.PingOptions 对象，用于控制 ICMP 回显消息数据包的碎片和生存时间值。</param>
        /// <param name="userToken">一个对象，此对象将被传递给异步操作完成后所调用的方法。</param>
        public void SendAsync(String hostNameOrAddress, Int32 times, Int32 timeout, Byte[] buffer, PingOptions options, Object userToken)
        {
            lock (this)
            {
                if (m_Pinging != false)
                {
                    throw new InvalidOperationException("The call of SendAsync is underway.");
                }
                m_Times = times;
                m_UserToken = userToken;
                m_Replys.Clear();
                m_Ping.SendAsync(hostNameOrAddress, timeout, buffer, options, userToken);
            }
            
        }
        /// <summary>
        /// 取消所有挂起的发送 Internet 控制消息协议 (ICMP) 回送消息并接收相应 ICMP 回送答复消息的异步请求。
        /// </summary>
        public void SendAsyncCancel()
        {
            lock(this)
            {
                m_Ping.SendAsyncCancel();
            }
        }
        /// <summary>
        /// 当发送 Internet 控制消息协议 (ICMP) 回送消息并接收相应 ICMP 回送答复消息的异步操作完成时发生。
        /// </summary>
        public event PingCompletedEventHandler SinglePingCompleted;
        /// <summary>
        /// 当发送多次 Internet 控制消息协议 (ICMP) 回送消息并接收相应 ICMP 回送答复消息的异步操作完成或被取消时发生。
        /// </summary>
        public event MultiPingCompletedEventHandler MultiPingCompleted;
        /// <summary>
        /// 引发 System.Net.NetworkInformation.Ping.PingCompleted 事件。
        /// </summary>
        /// <param name="e">包含事件数据的 System.Net.NetworkInformation.PingCompletedEventArgs 对象。</param>
        protected void OnSinglePingCompleted(PingCompletedEventArgs e)
        {
            PingCompletedEventHandler handler = SinglePingCompleted;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        /// <summary>
        /// 引发 System.Net.NetworkInformation.MultiPing.MultiPingCompleted 事件。
        /// </summary>
        /// <param name="e">包含事件数据的 System.Net.NetworkInformation.MultiPingCompletedEventArgs 对象。</param>
        protected void OnMultiPingCompleted(MultiPingCompletedEventArgs e)
        {
            MultiPingCompletedEventHandler handler = MultiPingCompleted;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        /// <summary>
        /// 创建多此Ping对象的回送参数
        /// </summary>
        /// <param name="address"></param>
        /// <param name="replys"></param>
        /// <returns></returns>
        protected MultiPingReply CreateReply(IPAddress address,PingReply[] replys)
        {
            Int32 total = replys.Length;
            Int32 loss = 0;
            foreach (var item in replys)
            {
                if(item.Status != IPStatus.Success)
                {
                    ++loss;
                }
            }
            MultiPingReply result = new MultiPingReply(address, replys, loss, total);
            return result;
        }
    }
}
