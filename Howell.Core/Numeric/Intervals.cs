using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Howell.Numeric
{
    /// <summary>
    /// 数学术语 (区间)
    /// </summary>
    [FlagsAttribute()]
    public enum Intervals
    {
        /// <summary>
        /// 全开区间
        /// </summary>
        None = 0,
        /// <summary>
        /// 左区间闭合
        /// </summary>
        LeftClosed = 0x01,
        /// <summary>
        /// 右区间闭合
        /// </summary>
        RightClosed = 0x02,
    }
    ///// <summary>
    ///// 数学术语 (区间)
    ///// </summary>
    //public enum Interval
    //{
    //    /// <summary>
    //    /// 开区间
    //    /// </summary>
    //    Opened =0,
    //    /// <summary>
    //    /// 闭区间
    //    /// </summary>
    //    Closed = 1,
    //}
}
