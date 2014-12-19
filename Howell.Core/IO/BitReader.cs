using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Howell.IO
{
    /// <summary>
    /// 位数据阅读器
    /// </summary>    
    public class BitReader :  IDisposable
    {
        private Boolean _isDisposed = false;

        /// <summary>
        /// 基于所提供的流，用 System.Text.UTF8Encoding 初始化 Howell.IO.BitReader 类的新实例。
        /// </summary>
        /// <param name="input">流</param>
        public BitReader(Stream input)
            : this(input,System.Text.UTF8Encoding.UTF8)
        {
        }
        /// <summary>
        /// 基于所提供的流，用 System.Text.UTF8Encoding 初始化 Howell.IO.BitReader 类的新实例。
        /// </summary>
        /// <param name="input">流</param>
        /// <param name="encoding">编码格式</param>
        public BitReader(Stream input,Encoding encoding)
        {
            this.BaseStream = new BitStream(input);
            this.Encoding = encoding;      
        }
        /// <summary>
        /// 公开对 Howell.IO.BitReader 的基础流的访问。
        /// </summary>
        public virtual BitStream BaseStream { get; private set; }
        /// <summary>
        /// 公开的编码格式
        /// </summary>
        public Encoding Encoding { get; private set; }
        /// <summary>
        /// 关闭当前阅读器及基础流。
        /// </summary>
        public virtual void Close()
        {
            CheckDisposed();
            if(this.BaseStream != null)
            {
                this.BaseStream.Close();
            }
        }
        /// <summary>
        /// 释放由 Howell.IO.BitReader 类的当前实例占用的所有资源。
        /// </summary>
        public void Dispose()
        {
            try
            {
                Close();
                if (this.BaseStream != null)
                {
                    this.BaseStream.Dispose();
                    this.BaseStream = null;
                }
            }
            finally
            {
                _isDisposed = true;
            }
        }
        /// <summary>
        /// 读取一位数据
        /// </summary>
        /// <returns>转换为 Int32 的无符号字节 0 或 1；如果到达流的末尾，则为 -1。</returns>
        /// <exception cref="System.ObjectDisposedException">流已关闭。</exception>
        /// <exception cref="System.IO.EndOfStreamException">已到达流的末尾。</exception>
        public Int32 ReadBit()
        {
            CheckDisposed();
            Nullable<Boolean>  result = this.BaseStream.ReadBit();
            if(result == null) return -1;
            return ((Boolean)result) ? 1 : 0;
        }
        /// <summary>
        /// 读取一位数据
        /// </summary>
        /// <param name="count">位数目[1-32]</param>
        /// <returns>转换为 Int32 的无符号字节 0 或 1。</returns>
        /// <exception cref="System.ObjectDisposedException">流已关闭。</exception>
        /// <exception cref="System.IO.EndOfStreamException">已到达流的末尾。</exception>
        public Int32 ReadBits(Int32 count)
        {
            CheckDisposed();
            if(count < 1 || count > 32) throw new ArgumentOutOfRangeException("count","count is not in range[1,32]");
            Boolean [] bitArray = new Boolean [count];
            Int32 readCount = this.BaseStream.Read(bitArray, 0, count);
            String result = "";
            for (Int32 i = 0; i < readCount; ++i)
            {
                result += bitArray[i] ? "1" : "0";
            }
            return Convert.ToInt32(result, 2);
        }
        /// <summary>
        /// 读取一位数据
        /// </summary>
        /// <param name="count">位数目[1-64]</param>
        /// <returns>转换为 Int64 的无符号字节 0 或 1。</returns>
        /// <exception cref="System.ObjectDisposedException">流已关闭。</exception>
        /// <exception cref="System.IO.EndOfStreamException">已到达流的末尾。</exception>
        public Int64 ReadBits64(Int32 count)
        {
            CheckDisposed();
            if (count < 1 || count > 64) throw new ArgumentOutOfRangeException("count", "count is not in range[1,32]");
            Boolean[] bitArray = new Boolean[count];
            Int32 readCount = this.BaseStream.Read(bitArray, 0, count);
            String result = "";
            for (Int32 i = 0; i < readCount; ++i)
            {
                result += bitArray[i] ? "1" : "0";
            }
            return Convert.ToInt64(result, 2);
        }
        /// <summary>
        /// 读取一个字节的数据
        /// </summary>
        /// <returns>转换为 Byte 的无符号字节。</returns>
        /// <exception cref="System.ObjectDisposedException">流已关闭。</exception>
        /// <exception cref="System.IO.EndOfStreamException">已到达流的末尾。</exception>
        public Byte ReadByte()
        {
            CheckDisposed();
            Int32 result = this.BaseStream.ReadByte();
            if (result < 0) throw new System.IO.EndOfStreamException("You have already reached the end of stream");
            return (Byte)result;
        }
        /// <summary>
        ///  从当前流中读取指定的字节数以写入字节数组中，并将当前位置前移相应的字节数。
        /// </summary>
        /// <param name="count">要读取的字节数。</param>
        /// <returns>包含从基础流中读取的数据的字节数组。如果到达了流的末尾，则该字节数组可能小于所请求的字节数。</returns>
        /// <exception cref="System.ObjectDisposedException">流已关闭。</exception>
        /// <exception cref="System.IO.EndOfStreamException">已到达流的末尾。</exception>
        public Byte [] ReadBytes(Int32 count)
        {
            CheckDisposed();
            Byte [] buffer = new Byte [count];
            Int32 readCount = this.BaseStream.Read(buffer, 0, count);
            if(readCount == count) return buffer;
            Byte[] resultBuffer = new Byte[readCount];
            Array.Copy(buffer, resultBuffer, readCount);
            return resultBuffer;
        }
        /// <summary>
        /// 从当前流中读取 2 字节有符号整数，并使流的当前位置提升 2 个字节。
        /// </summary>
        /// <returns>从当前流中读取的 2 字节有符号整数。</returns>
        /// <exception cref="System.ObjectDisposedException">流已关闭。</exception>
        /// <exception cref="System.IO.EndOfStreamException">已到达流的末尾。</exception>
        public Int16 ReadInt16()
        {
            Int16 result =0;
            result += (Int16)((Int16)ReadByte() << 8);
            result += (Int16)ReadByte();
            return result;
        }
        /// <summary>
        /// 从当前流中读取 4 字节有符号整数，并使流的当前位置提升 4 个字节。
        /// </summary>
        /// <returns>从当前流中读取的 4 字节有符号整数。</returns>
        /// <exception cref="System.ObjectDisposedException">流已关闭。</exception>
        /// <exception cref="System.IO.EndOfStreamException">已到达流的末尾。</exception>
        public Int32 ReadInt32()
        {
            Int32 result = 0;
            result += (Int32)((Int32)ReadByte() << 24);
            result += (Int32)((Int32)ReadByte() << 16);
            result += (Int32)((Int32)ReadByte() << 8);
            result += (Int32)ReadByte();
            return result;
        }
        /// <summary>
        /// 从当前流中读取 8 字节有符号整数，并使流的当前位置向前移动 8 个字节。
        /// </summary>
        /// <returns>从当前流中读取的 8 字节有符号整数。</returns>
        /// <exception cref="System.ObjectDisposedException">流已关闭。</exception>
        /// <exception cref="System.IO.EndOfStreamException">已到达流的末尾。</exception>
        public Int64 ReadInt64()
        {
            Int64 result = 0;
            result += (Int64)((Int64)ReadByte() << 56);
            result += (Int64)((Int64)ReadByte() << 48);
            result += (Int64)((Int64)ReadByte() << 40);
            result += (Int64)((Int64)ReadByte() << 32);
            result += (Int64)((Int64)ReadByte() << 24);
            result += (Int64)((Int64)ReadByte() << 16);
            result += (Int64)((Int64)ReadByte() << 8);
            result += (Int64)ReadByte();
            return result;
        }
        /// <summary>
        /// 从当前流中读取 2 字节无符号整数，并使流的当前位置提升 2 个字节。
        /// </summary>
        /// <returns>从当前流中读取的 2 字节无符号整数。</returns>
        /// <exception cref="System.ObjectDisposedException">流已关闭。</exception>
        /// <exception cref="System.IO.EndOfStreamException">已到达流的末尾。</exception>
        public UInt16 ReadUInt16()
        {
            UInt16 result = 0;
            result += (UInt16)((UInt16)ReadByte() << 8);
            result += (UInt16)ReadByte();
            return result;
        }
        /// <summary>
        /// 从当前流中读取 4 字节无符号整数，并使流的当前位置提升 4 个字节。
        /// </summary>
        /// <returns>从当前流中读取的 4 字节无符号整数。</returns>
        /// <exception cref="System.ObjectDisposedException">流已关闭。</exception>
        /// <exception cref="System.IO.EndOfStreamException">已到达流的末尾。</exception>
        public UInt32 ReadUInt32()
        {
            UInt32 result = 0;
            result += (UInt32)((UInt32)ReadByte() << 24);
            result += (UInt32)((UInt32)ReadByte() << 16);
            result += (UInt32)((UInt32)ReadByte() << 8);
            result += (UInt32)ReadByte();
            return result;
        }
        /// <summary>
        /// 从当前流中读取 8 字节无符号整数，并使流的当前位置提升 8 个字节。
        /// </summary>
        /// <returns>从当前流中读取的 8 字节无符号整数。</returns>
        /// <exception cref="System.ObjectDisposedException">流已关闭。</exception>
        /// <exception cref="System.IO.EndOfStreamException">已到达流的末尾。</exception>
        public UInt64 ReadUInt64()
        {
            UInt64 result = 0;
            result += (UInt64)((UInt64)ReadByte() << 56);
            result += (UInt64)((UInt64)ReadByte() << 48);
            result += (UInt64)((UInt64)ReadByte() << 40);
            result += (UInt64)((UInt64)ReadByte() << 32);
            result += (UInt64)((UInt64)ReadByte() << 24);
            result += (UInt32)((UInt64)ReadByte() << 16);
            result += (UInt64)((UInt64)ReadByte() << 8);
            result += (UInt64)ReadByte();
            return result;
        }
        /// <summary>
        /// 从当前流中读取指定长度的字符串
        /// </summary>
        /// <param name="count">字符串字节数</param>
        /// <returns>返回指定长度的字符串，如果到达文件尾不，返回的字符串长度可能小于实际的需求。</returns>
        public String ReadString(Int32 count)
        {
            Byte [] result = this.ReadBytes(count);
            return Encoding.GetString(result);
        }        
        /// <summary>
        /// 读取无符号指数哥伦布编码
        /// </summary>
        /// <returns></returns>
        public UInt32 ReadGolombUE32()
        {
            Int32 leadingZeroBits = -1;
            for (Int32 b = 0; b == 0; leadingZeroBits++)
                b = ReadBit();
            UInt32 codeNum = (UInt32)(((UInt32)1 << leadingZeroBits) - 1 + (Int32)ReadBits(leadingZeroBits));
            return codeNum;
        }
        /// <summary>
        /// 读取有符号指数哥伦布编码
        /// </summary>
        /// <returns></returns>
        public Int32 ReadGolombSE32()
        {
            UInt32 ue32 = ReadGolombUE32();
            return (Int32)(System.Math.Pow(-1, (double)ue32 + 1) * System.Math.Ceiling((double)ue32 / 2));
        }
                
        private void CheckDisposed()
        {
            if (_isDisposed) throw new ObjectDisposedException(this.GetType().Name);
        }


        
    }
}
