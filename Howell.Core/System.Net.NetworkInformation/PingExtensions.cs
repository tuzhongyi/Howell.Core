using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace System.Net.NetworkInformation
{
    /// <summary>
    /// Ping命令的扩展函数
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class PingExtensions 
    {
        /// <summary>
        /// 转换为字符串格式
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static String ToContentString(this PingReply value)
        {            
            return String.Format("{0} bytes from {1} reply: Time={2}ms TTL={3} Status={4}", value.Buffer.Length, value.Address.ToString(), value.RoundtripTime, value.Options.Ttl, value.Status);
        }
    }
}
