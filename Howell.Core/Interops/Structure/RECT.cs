using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Howell.Interops
{
    /// <summary>
    /// 矩形数据结构
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 16)]
    public struct RECT
    {
        /// <summary>
        /// 左
        /// </summary>
        [FieldOffset(0)]
        public Int32 Left;
        /// <summary>
        /// 上
        /// </summary>
        [FieldOffset(4)]
        public Int32 Top;
        /// <summary>
        /// 右
        /// </summary>
        [FieldOffset(8)]
        public Int32 Right;
        /// <summary>
        /// 下
        /// </summary>
        [FieldOffset(12)]
        public Int32 Bottom;
    }
}
