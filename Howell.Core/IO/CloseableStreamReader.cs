using System;
using System.Collections.Generic;
using System.Text;

namespace Howell.IO
{
    /// <summary>
    /// 实现一个 System.IO.TextReader，使其以一种特定的编码从字节流中读取字符。
    /// </summary>
    public class CloseableStreamReader : System.IO.StreamReader
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="path"></param>
        public CloseableStreamReader(String path)
            : base(path)
        {
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="stream"></param>
        public CloseableStreamReader(System.IO.Stream stream)
            : base(stream)
        {
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="path"></param>
        /// <param name="encoding"></param>
        public CloseableStreamReader(String path,Encoding encoding)
            : base(path, encoding)
        {
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="encoding"></param>
        public CloseableStreamReader(System.IO.Stream stream, Encoding encoding)
            : base(stream,encoding)
        {
        }

        /// <summary>
        /// 是否在退出时关闭Stream
        /// </summary>
        public Boolean Closeable { get; set; }
        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(Closeable);
        }
    }
}
