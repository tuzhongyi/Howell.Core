using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Howell
{
    /// <summary>
    /// 异常事件参数
    /// </summary>
    public class ExceptionEventArgs : EventArgs
    {
        /// <summary>
        /// 创建异常事件参数
        /// </summary>
        /// <param name="ex">错误信息</param>
        public ExceptionEventArgs(Exception ex)
            : base()
        {
            this.Exception = ex;
        }
        /// <summary>
        /// 异常信息
        /// </summary>
        public Exception Exception { get; private set; }
    }
}
