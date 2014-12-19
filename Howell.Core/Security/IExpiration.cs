using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Howell.Security
{
    /// <summary>
    /// 期满特性的数据接口
    /// </summary>
    public interface IExpiration
    {
        /// <summary>
        /// 判断对象是否已期满
        /// </summary>
        /// <returns>期满返回true,否则返回false。</returns>
        Boolean IsExpired();
        /// <summary>
        /// 续订
        /// </summary>
        void Renewal();
    }
}
