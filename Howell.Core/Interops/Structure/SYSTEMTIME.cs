using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Howell.Interops
{
    /// <summary>
    /// 指定一周的某天。
    /// </summary>
    [ComVisible(true)]
    public enum DayOfWeek : ushort
    {
        /// <summary>
        /// 表示星期日。
        /// </summary>
        Sunday = 0,
        /// <summary>
        /// 表示星期一。
        /// </summary>
        Monday = 1,
        /// <summary>
        /// 表示星期二。
        /// </summary>
        Tuesday = 2,
        /// <summary>
        /// 表示星期三。
        /// </summary>
        Wednesday = 3,
        /// <summary>
        /// 表示星期四。
        /// </summary>
        Thursday = 4,
        /// <summary>
        /// 表示星期五。
        /// </summary>
        Friday = 5,
        /// <summary>
        /// 表示星期六。
        /// </summary>
        Saturday = 6,
    }
    /// <summary>
    /// 系统时间
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 16)]
    public struct SYSTEMTIME
    {
        /// <summary>
        /// 年
        /// </summary>
        [FieldOffset(0)]
        public UInt16 Year;
        /// <summary>
        /// 月
        /// </summary>
        [FieldOffset(2)]
        public UInt16 Month;
        /// <summary>
        /// 星期几
        /// </summary>
        [FieldOffset(4)]
        public DayOfWeek DayOfWeek;
        /// <summary>
        /// 日
        /// </summary>
        [FieldOffset(6)]
        public UInt16 Day;
        /// <summary>
        /// 时
        /// </summary>
        [FieldOffset(8)]
        public UInt16 Hour;
        /// <summary>
        /// 分
        /// </summary>
        [FieldOffset(10)]
        public UInt16 Minute;
        /// <summary>
        /// 秒
        /// </summary>
        [FieldOffset(12)]
        public UInt16 Second;
        /// <summary>
        /// 毫秒
        /// </summary>
        [FieldOffset(14)]
        public UInt16 Milliseconds;
    }
        
}
