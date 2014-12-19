using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.Runtime.Serialization;

namespace Howell
{
    /// <summary>
    /// 表示运行时发生的版本不匹配的错误
    /// </summary>
    public class VersionMismatchedException : Exception
    {
        /// <summary>
        /// 初始化 Howell.VersionMismatchedException 类实例
        /// </summary>
        public VersionMismatchedException()
            : base()
        {
        }
        /// <summary>
        /// 使用指定的错误消息初始化 Howell.VersionMismatchedException 类的新实例。
        /// </summary>
        /// <param name="message">描述错误的消息。</param>
        public VersionMismatchedException(String message)
            : base(message)
        {
        }
        /// <summary>
        /// 使用指定错误消息和对作为此异常原因的内部异常的引用来初始化 Howell.VersionMismatchedException 类的新实例。
        /// </summary>
        /// <param name="message">描述错误的消息。</param>
        /// <param name="innerException">导致当前异常的异常；如果未指定内部异常，则是一个 null 引用（在 Visual Basic 中为 Nothing）。</param>
        public VersionMismatchedException(String message,Exception innerException)
            : base(message,innerException)
        {

        }
        /// <summary>
        /// 用序列化数据初始化 Howell.VersionMismatchedException 类的新实例。
        /// </summary>
        /// <param name="info">System.Runtime.Serialization.SerializationInfo，它存有有关所引发异常的序列化的对象数据。</param>
        /// <param name="context">System.Runtime.Serialization.StreamingContext，它包含有关源或目标的上下文信息。</param>
        /// <exception cref="System.ArgumentNullException"> info 参数为 null。</exception>
        /// <exception cref="System.Runtime.Serialization.SerializationException">类名为 null 或 System.Exception.HResult 为零 (0)。</exception>
        [SecuritySafeCritical]
        protected VersionMismatchedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}
