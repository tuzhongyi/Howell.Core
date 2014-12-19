using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Howell.IO
{
    /// <summary>
    /// 字节序
    /// </summary>
    public enum ByteOrder
    {
        /// <summary>
        /// 网络字节序 Big-Endian
        /// </summary>
        Network,
        /// <summary>
        /// 主机字节序 Little-Endian
        /// </summary>
        Host,
    }
}
