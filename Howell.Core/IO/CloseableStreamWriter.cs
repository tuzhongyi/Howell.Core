using System;
using System.Collections.Generic;
using System.Text;

namespace Howell.IO
{
    /// <summary>
    /// 实现一个 System.IO.TextWriter，使其以一种特定的编码向流中写入字符。
    /// </summary>
    public class CloseableStreamWriter : System.IO.StreamWriter
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="path"></param>
        public CloseableStreamWriter(String path)
            : base(path)
        {
        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="stream"></param>
        public CloseableStreamWriter(System.IO.Stream stream)
            : base(stream)
        {
        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="path"></param>
        /// <param name="append"></param>
        public CloseableStreamWriter(String path,Boolean append)
            : base(path,append)
        {
        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="encoding"></param>
        public CloseableStreamWriter(System.IO.Stream stream,Encoding encoding)
            : base(stream,encoding)
        {
        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="path"></param>
        /// <param name="append"></param>
        /// <param name="encoding"></param>
        public CloseableStreamWriter(String path,Boolean append,Encoding encoding)
            : base(path,append,encoding)
        {
        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="encoding"></param>
        /// <param name="bufferSize"></param>
        public CloseableStreamWriter(System.IO.Stream stream,Encoding encoding,int bufferSize)
            : base(stream,encoding,bufferSize)
        {
        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="path"></param>
        /// <param name="append"></param>
        /// <param name="encoding"></param>
        /// <param name="bufferSize"></param>
        public CloseableStreamWriter(String path,Boolean append,Encoding encoding,int bufferSize)
            : base(path,append,encoding,bufferSize)
        {
        }
        /// <summary>
        /// 是否在退出时关闭Stream
        /// </summary>
        public Boolean Closeable { get; set;}
        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected override void  Dispose(bool disposing)    
        {
            base.Dispose(Closeable);
        }
    }
}
