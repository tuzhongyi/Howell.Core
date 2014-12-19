using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Howell.Industry
{
    /// <summary>
    /// 解码通道状态
    /// </summary>
    public enum DecodingChannelState
    {
        /// <summary>
        /// 关闭
        /// </summary>
        Disabled = 0,
        /// <summary>
        /// 休眠
        /// </summary>
        Dormant  = 1,
        /// <summary>
        /// 正在连接
        /// </summary>
        Connecting = 2,
        /// <summary>
        /// 已连接
        /// </summary>
        Connected = 3,
        /// <summary>
        /// 正在解码
        /// </summary>
        Decoding = 4,
        /// <summary>
        /// 异常
        /// </summary>
        Abnormal = 5,
    }
}
