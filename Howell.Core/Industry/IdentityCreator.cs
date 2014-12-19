using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Howell.Industry
{
    /// <summary>
    /// 唯一标识符创建器
    /// </summary>
    public class IdentityCreator
    {
        /// <summary>
        /// 创建唯一标识符
        /// </summary>
        /// <returns>返回新的唯一标识符</returns>
        public static String Create()
        {
            return Guid.NewGuid().ToString("N").ToLower();
        }
        /// <summary>
        /// 创建唯一标识符创建器
        /// </summary>
        public IdentityCreator()
        {
        }
    }
}
