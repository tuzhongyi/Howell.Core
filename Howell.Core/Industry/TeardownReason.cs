using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Howell.Industry
{
    /// <summary>
    /// 设备注销原因
    /// </summary>
    public enum TeardownReason
    {
        /// <summary>
        /// 未知或无信息
        /// </summary>
        None = 0,        
        /// <summary>
        /// 本地手动的
        /// </summary>
        Manual = 1,
        /// <summary>
        /// 网络手动的
        /// </summary>
        Network = 2,
        /// <summary>
        /// 系统异常
        /// </summary>
        SystemAbnormal = 3,
        /// <summary>
        /// 存储介质异常
        /// </summary>
        StorageMediumAbnormal = 4,
        /// <summary>
        /// 看门狗
        /// </summary>
        WatchDog = 5,

    }

}
