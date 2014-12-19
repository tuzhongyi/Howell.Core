using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Howell.Data
{
    /// <summary>
    /// 数据库类型约束
    /// </summary>
    public struct DateTimeConstraints
    {
        private static readonly DateTime m_Min = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);
        private static readonly DateTime m_Max = new DateTime(2999, 12, 31, 23, 59, 59, 500, DateTimeKind.Local);
        private static readonly DateTime m_Default = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);
        /// <summary>
        /// 最小时间
        /// </summary>
        public static DateTime Min
        {
            get { return m_Min; }
        }
        /// <summary>
        /// 默认时间值
        /// </summary>
        public static DateTime Default
        {
            get { return m_Default; }
        }
        /// <summary>
        /// 最大时间
        /// </summary>
        public static DateTime Max
        {
            get { return m_Max; }
        }
    }
}
