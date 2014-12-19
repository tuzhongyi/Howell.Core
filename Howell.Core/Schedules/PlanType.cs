using System;
using System.Collections.Generic;
using System.Text;

namespace Howell.Schedules
{
    /// <summary>
    /// 计划类型
    /// </summary>
    public enum PlanType
    {
        /// <summary>
        /// 一次性的
        /// </summary>
        OneOff = 0,
        /// <summary>
        /// 每周重复
        /// </summary>
        Weekly = 1,
        /// <summary>
        /// 每日重复
        /// </summary>
        Daily = 2,
    }
}
