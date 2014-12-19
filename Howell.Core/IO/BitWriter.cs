using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Howell.IO
{
    /// <summary>
    /// 以二进制形式将基元类型写入流，并支持用特定的编码写入字符串。
    /// </summary>
    public class BitWriter : IDisposable
    {
        private Boolean _isDisposed = false;

        /// <summary>
        /// 基于所提供的流，用 UTF-8 作为字符串编码来初始化 Howell.IO.BitWriter 类的新实例。
        /// </summary>
        /// <param name="output">输出流。</param>
        public BitWriter(Stream output)
            : this(output, System.Text.UTF8Encoding.UTF8)
        {
        }
        /// <summary>
        /// 基于所提供的流，用 UTF-8 作为字符串编码来初始化 Howell.IO.BitWriter 类的新实例。
        /// </summary>
        /// <param name="output">输出流。</param>
        /// <param name="encoding">编码格式</param>
        public BitWriter(Stream output, Encoding encoding)
            : this(output, encoding, ByteOrder.Network)
        {
        }
        /// <summary>
        /// 基于所提供的流，用 UTF-8 作为字符串编码来初始化 Howell.IO.BitWriter 类的新实例。
        /// </summary>
        /// <param name="output">输出流。</param>
        /// <param name="order">字节序</param>
        public BitWriter(Stream output, ByteOrder order)
            : this(output, Encoding.UTF8, order)
        {
            
        }
        /// <summary>
        /// 基于所提供的流，用 UTF-8 作为字符串编码来初始化 Howell.IO.BitWriter 类的新实例。
        /// </summary>
        /// <param name="output">输出流。</param>
        /// <param name="encoding">编码格式</param>
        /// <param name="order">字节序</param>
        public BitWriter(Stream output, Encoding encoding, ByteOrder order)
        {
            this.BaseStream = new BitStream(output, order);
            this.Encoding = encoding;
        }
        /// <summary>
        /// 公开对 Howell.IO.BitWriter 的基础流的访问。
        /// </summary>
        public virtual BitStream BaseStream { get; private set; }
        /// <summary>
        /// 公开的编码格式
        /// </summary>
        public Encoding Encoding { get; private set; }
        /// <summary>
        /// 关闭当前的 Howell.IO.BitWriter 和基础流。
            /// </summary>
        public virtual void Close()
        {
            CheckDisposed();
            if (this.BaseStream != null)
            {
                this.BaseStream.Close();
            }
        }
        /// <summary>
        /// 释放由 Howell.IO.BitWriter 类的当前实例占用的所有资源。
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
        /// 将一位数据的值写入当前流，其中 0 表示 false，1 表示 true。
        /// </summary>
        /// <param name="value">位数据的值</param>
        /// <exception cref="System.ObjectDisposedException">流已关闭。</exception>
        public void WriteBit(Int32 value)
        {
            CheckDisposed();
            this.BaseStream.WriteBit(value > 0);
        }
        /// <summary>
        /// 将n位数据的值写入当前流，其中 0 表示 false，1 表示 true。
        /// </summary>
        /// <param name="value">n位数据的值</param>
        /// <param name="count">位的数目</param>
        /// <exception cref="System.ObjectDisposedException">流已关闭。</exception>
        public void WriteBits(Int32 value,Int32 count)
        {
            CheckDisposed();
            if (count < 1 || count > 32) throw new ArgumentOutOfRangeException("count", "count is not in range[1,32]");
            Boolean[] bitArray = new Boolean[count];
            for (Int32 i = 0; i < count; ++i)
            {
                bitArray[i] = ((value >> (count - i - 1)) & 0x1) > 0;
            }
            this.BaseStream.Write(bitArray, 0, count);
        }
        /// <summary>
        /// 将n位数据的值写入当前流，其中 0 表示 false，1 表示 true。
        /// </summary>
        /// <param name="value">n位数据的值</param>
        /// <param name="count">位的数目</param>
        /// <exception cref="System.ObjectDisposedException">流已关闭。</exception>
        public void WriteBits(Int64 value, Int32 count)
        {
            CheckDisposed();
            if (count < 1 || count > 64) throw new ArgumentOutOfRangeException("count", "count is not in range[1,64]");
            Boolean[] bitArray = new Boolean[count];
            for (Int32 i = 0; i < count; ++i)
            {
                bitArray[i] = ((value >> (count - i - 1)) & 0x1) > 0;
            }
            this.BaseStream.Write(bitArray, 0, count);
        }
        /// <summary>
        /// 将一个无符号字节写入当前流，并将流的位置提升 1 个字节。
        /// </summary>
        /// <param name="value">要写入的无符号字节。</param>
        /// <exception cref="System.ObjectDisposedException">流已关闭。</exception>
        public void Write(Byte value)
        {
            CheckDisposed();
            this.BaseStream.WriteByte(value);
        }
        /// <summary>
        /// 将字节数组写入基础流。
        /// </summary>
        /// <param name="buffer">包含要写入的数据的字节数组。</param>
        /// <exception cref="System.ObjectDisposedException">流已关闭。</exception>
        public void Write(byte[] buffer)
        {
            Write(buffer, 0, buffer.Length);
        }
        /// <summary>
        /// 将字节数组部分写入当前流。
        /// </summary>
        /// <param name="buffer">包含要写入的数据的字节数组。</param>
        /// <param name="index">buffer 中开始写入的起始点。</param>
        /// <param name="count">要写入的字节数。</param>
        public void Write(byte [] buffer,Int32 index,Int32 count)
        {
            if (buffer == null) throw new ArgumentNullException("buffer");
            //if (count <= 0) return;
            //if (index < 0 || index >= buffer.Length) throw new ArgumentOutOfRangeException("index");
            //if (count <= 0 || index + count >= buffer.Length) throw new ArgumentOutOfRangeException("count");
            CheckDisposed();
            this.BaseStream.Write(buffer, index, count);
        }
        /// <summary>
        /// 将 2 字节带符号整数写入当前流，并将流的位置提升 2 个字节。
        /// </summary>
        /// <param name="value">要写入的 2 字节带符号整数。</param>
        /// <exception cref="System.ObjectDisposedException">流已关闭。</exception>
        public void Write(Int16 value)
        {
            this.Write((Byte)(value >> 8));
            this.Write((Byte)(value));
        }
        /// <summary>
        /// 将 4 字节带符号整数写入当前流，并将流的位置提升 4 个字节。
        /// </summary>
        /// <param name="value">要写入的 4 字节带符号整数。</param>
        /// <exception cref="System.ObjectDisposedException">流已关闭。</exception>
        public void Write(Int32 value)
        {
            this.Write((Byte)(value >> 24));
            this.Write((Byte)(value >> 16));
            this.Write((Byte)(value >> 8));
            this.Write((Byte)(value));
        }
        /// <summary>
        /// 将 8 字节带符号整数写入当前流，并将流的位置提升 8 个字节。
        /// </summary>
        /// <param name="value">要写入的 8 字节带符号整数。</param>
        /// <exception cref="System.ObjectDisposedException">流已关闭。</exception>
        public void Write(Int64 value)
        {
            this.Write((Byte)(value >> 56));
            this.Write((Byte)(value >> 48));
            this.Write((Byte)(value >> 40));
            this.Write((Byte)(value >> 32));
            this.Write((Byte)(value >> 24));
            this.Write((Byte)(value >> 16));
            this.Write((Byte)(value >> 8));
            this.Write((Byte)(value));
        }
        /// <summary>
        /// 将 2 字节无符号整数写入当前流，并将流的位置提升 2 个字节。
        /// </summary>
        /// <param name="value">要写入的 2 字节无符号整数。</param>
        /// <exception cref="System.ObjectDisposedException">流已关闭。</exception>
        public void Write(UInt16 value)
        {
            CheckDisposed();
            this.Write((Byte)(value >> 8));
            this.Write((Byte)(value));
        }
        /// <summary>
        /// 将 4 字节无符号整数写入当前流，并将流的位置提升 4 个字节。
        /// </summary>
        /// <param name="value">要写入的 4 字节无符号整数。</param>
        /// <exception cref="System.ObjectDisposedException">流已关闭。</exception>
        public void Write(UInt32 value)
        {
            this.Write((Byte)(value >> 24));
            this.Write((Byte)(value >> 16));
            this.Write((Byte)(value >> 8));
            this.Write((Byte)(value));
        }
        /// <summary>
        /// 将 8 字节无符号整数写入当前流，并将流的位置提升 8 个字节。
        /// </summary>
        /// <param name="value">要写入的 8 字节无符号整数。</param>
        /// <exception cref="System.ObjectDisposedException">流已关闭。</exception>
        public void Write(UInt64 value)
        {
            this.Write((Byte)(value >> 56));
            this.Write((Byte)(value >> 48));
            this.Write((Byte)(value >> 40));
            this.Write((Byte)(value >> 32));
            this.Write((Byte)(value >> 24));
            this.Write((Byte)(value >> 16));
            this.Write((Byte)(value >> 8));
            this.Write((Byte)(value));
        }
        /// <summary>
        /// 将字符串写入当前流
        /// </summary>
        /// <param name="value">字符串值</param>
        public void Write(String value)
        {
            Byte [] result = this.Encoding.GetBytes(value);
            this.Write(result);
        }
        /// <summary>
        /// 将字符串写入当前流
        /// </summary>
        /// <param name="value">字符串值</param>
        /// <param name="length">字符串长度</param>
        public void Write(String value,Int32 length)
        {
            Byte[] bytes = new Byte[length];
            Array.Clear(bytes,0,length);
            Byte [] result = this.Encoding.GetBytes(value);
            Array.Copy(result, bytes, System.Math.Min(length, result.Length));
            this.Write(bytes);
        }
        /// <summary>
        /// 写入无符号指数哥伦布编码
        /// </summary>
        /// <param name="value"></param>
        public void WriteGolombUE32(UInt32 value)
        {
            String binString = Convert.ToString(value + 1, 2);
            if (value == 0)
            {
                WriteBit(1);
            }
            else
            {
                //写入0
                for (int i = 0; i < binString.Length - 1; ++i)
                {
                    WriteBit(0);
                }
                WriteBits(value + 1, binString.Length);
            }
        }
        /// <summary>
        /// 写入有符号指数哥伦布编码
        /// </summary>
        /// <param name="value"></param>
        public void WriteGolombSE32(Int32 value)
        {
            UInt32 ue32 = 0;
            if (value <= 0)
            {
                ue32 = (UInt32)((-1) * value * 2);
            }
            else
            {
                ue32 = (UInt32)(value * 2 - 1);
            }
            WriteGolombUE32(ue32);
        }
        private void CheckDisposed()
        {
            if (_isDisposed) throw new ObjectDisposedException(this.GetType().Name);
        }
    }
}
