using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Howell.Net
{
    /// <summary>
    /// 套接字协议类型
    /// </summary>
    public enum SocketProtocolType
    {
        /// <summary>
        /// 传输控制协议
        /// </summary>
        Tcp = 0,
        /// <summary>
        /// 用户数据报协议
        /// </summary>
        Udp = 1,
    }
}
