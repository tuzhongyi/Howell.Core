using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Howell.Industry
{
    /// <summary>
    /// 报警触发方式
    /// </summary>
    public enum IOInputTriggeringType
    {
        /// <summary>
        /// 低电平
        /// </summary>
        Low = 0,
        /// <summary>
        /// 高电平
        /// </summary>
        High = 1,
        /// <summary>
        /// 电平下降
        /// </summary>
        Falling = 2,
        /// <summary>
        /// 电平上升
        /// </summary>
        Rasing = 3,
    }
    /// <summary>
    /// 继电器输出触发方式
    /// </summary>
    public enum IOOutputTriggeringType
    {
        /// <summary>
        /// 低电平
        /// </summary>
        Low = 0,
        /// <summary>
        /// 高电平
        /// </summary>
        High = 1,
    }
}
