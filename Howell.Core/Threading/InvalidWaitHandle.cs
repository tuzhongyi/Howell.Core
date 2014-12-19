using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Howell.Threading
{
    /// <summary>
    /// 无效的等待句柄
    /// </summary>
    /// <remarks>用于System.Threading.Timer.Dispose(WaitHandle)的方法</remarks>
    public class InvalidWaitHandle : WaitHandle
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public InvalidWaitHandle()
        {
            this.SafeWaitHandle = null;
        }
        ///// <summary>
        ///// 等待句柄
        ///// </summary>
        //[Obsolete]
        //public override IntPtr Handle
        //{
        //    get
        //    {
        //        return WaitHandle.InvalidHandle;
        //    }
        //    set
        //    {
        //        throw new InvalidOperationException();
        //    }
        //}
        
    }
}
