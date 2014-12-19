using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Howell.IO
{
    /// <summary>
    /// 位流
    /// </summary>
    public class BitStream : Stream
    {
        /// <summary>
        /// 
        /// </summary>
        private Stream _innerStream;
        /// <summary>
        /// 0-7的偏移值
        /// </summary>
        private RangeInt32 _offset = new RangeInt32(0, 7);
        /// <summary>
        /// 
        /// </summary>
        private Int32 _currentByte = 0;
        /// <summary>
        /// 
        /// </summary>
        private ByteOrder _order = ByteOrder.Network;

        /// <summary>
        /// 
        /// </summary>
        public BitStream()
            : base()
        {
            _innerStream = new MemoryStream();
            _offset.Exchange(0);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"> 从中创建当前流的无符号字节数组。</param>
        /// <exception cref="System.ArgumentNullException">buffer 为 null。</exception>
        public BitStream(byte[] buffer)
            : this(buffer, true)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer">从中创建该流的无符号字节数组。</param>
        /// <param name="writable">Howell.IO.BitStream.CanWrite 属性的设置，确定流是否支持写入。</param>
        /// <exception cref="System.ArgumentNullException">buffer 为 null。</exception>
        public BitStream(byte[] buffer, bool writable)
            : this(buffer, 0, buffer.Length, writable)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer">从中创建该流的无符号字节数组。</param>
        /// <param name="index">buffer 内的索引，流从此处开始。</param>
        /// <param name="count">流的长度（以字节为单位）。</param>
        /// <exception cref="System.ArgumentNullException">buffer 为 null。</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">index 或 count 小于零。</exception>
        /// <exception cref="System.ArgumentException">index 与 count 的和大于 buffer 的长度。</exception>
        public BitStream(byte[] buffer, int index, int count)
            : this(buffer, index,count, true)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer">从中创建该流的无符号字节数组。</param>
        /// <param name="index">buffer 内的索引，流从此处开始。</param>
        /// <param name="count">流的长度（以字节为单位）。</param>
        /// <param name="writable">Howell.IO.BitStream.CanWrite 属性的设置，确定流是否支持写入。</param>
        /// <exception cref="System.ArgumentNullException">buffer 为 null。</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">index 或 count 小于零。</exception>
        /// <exception cref="System.ArgumentException">index 与 count 的和大于 buffer 的长度。</exception>
        public BitStream(byte[] buffer, int index, int count, bool writable)
            : this(buffer, index, count, writable, ByteOrder.Network)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer">从中创建该流的无符号字节数组。</param>
        /// <param name="index">buffer 内的索引，流从此处开始。</param>
        /// <param name="count">流的长度（以字节为单位）。</param>
        /// <param name="writable">Howell.IO.BitStream.CanWrite 属性的设置，确定流是否支持写入。</param>
        /// <param name="order">字节序</param>
        /// <exception cref="System.ArgumentNullException">buffer 为 null。</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">index 或 count 小于零。</exception>
        /// <exception cref="System.ArgumentException">index 与 count 的和大于 buffer 的长度。</exception>
        public BitStream(byte[] buffer, int index, int count, bool writable,ByteOrder order)
        {
            this._innerStream = new MemoryStream(buffer, index, count, writable);
            _offset.Exchange(0);
            _order = order;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream">流</param>
        public BitStream(Stream stream)
            : this(stream, ByteOrder.Network)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream">流</param>
        /// <param name="order">字节序</param>
        public BitStream(Stream stream, ByteOrder order)
        {
            this._innerStream = stream;
            _offset.Exchange(0);
            _order = order;
        }
        /// <summary>
        /// 获取一个值，该值指示当前流是否支持读取。
        /// </summary>
        /// <returns>如果流是打开的，则为 true。</returns>
        public override bool CanRead
        {
            get { return _innerStream.CanRead; }
        }
        /// <summary>
        /// 获取一个值，该值指示当前流是否支持查找。
        /// </summary>
        /// <returns>如果流是打开的，则为 true。</returns>
        public override bool CanSeek
        {
            get { return _innerStream.CanSeek; }
        }
        /// <summary>
        /// 获取一个值，该值指示当前流是否支持写入。
        /// </summary>
        /// <returns>如果流支持写入，为 true；否则为 false。</returns>
        public override bool CanWrite
        {
            get { return _innerStream.CanWrite; }
        }
        /// <summary>
        ///  获取用字节表示的流长度。
        /// </summary>
        /// <returns>流的长度（以位为单位）。</returns>
        public override long Length
        {
            get { return _innerStream.Length * 8; }
        }
        /// <summary>
        /// 当在派生类中重写时，获取或设置当前流中的位置。
        /// </summary>
        /// <returns>流中的当前位置。</returns>
        public override long Position
        {
            get
            {
                return (this._innerStream.Position * 8 - _offset);
            }
            set
            {                
                this._innerStream.Position = Convert.ToInt64(System.Math.Ceiling((double)value / 8));
                _offset.Exchange(Convert.ToInt32(8 - value % 8) % 8);
            }
        }
        /// <summary>
        /// 将当前流中的位置设置为指定值。
        /// </summary>
        /// <param name="offset"> 流内的新位置。它是相对于 loc 参数的位置，而且可正可负。</param>
        /// <param name="origin">类型 System.IO.SeekOrigin 的值，它用作查找参考点。</param>
        /// <returns>流内的新位置，通过将初始参考点和偏移量合并计算而得。</returns>
        public override long Seek(long offset, SeekOrigin origin)
        {
            if(origin == SeekOrigin.Current)
            {
                this.Position = this.Position + offset;
            }
            else if(origin == SeekOrigin.Begin)
            {
                this.Position = offset;
            }
            else
            {
                this.Position = this.Length - offset;
            }
            return this.Position;
        }
        /// <summary>
        /// 从当前流中读取字节块并将数据写入 buffer 中。
        /// </summary>
        /// <param name="buffer">当此方法返回时，包含指定的字节数组，该数组中从 offset 到 (offset + count -1) 之间的值由从当前流中读取的字符替换。</param>
        /// <param name="offset">buffer 中的字节偏移量，从此处开始读取。</param>
        /// <param name="count">最多读取的字节数。</param>
        /// <returns>写入缓冲区中的总字节数。如果当前可用字节数不到所请求的字节数，则这一总字节数可能小于所请求的字节数，或者如果在读取任何字节前已到达流的末尾，则为零。</returns>
        public override int Read(byte[] buffer, int offset, int count)
        {
            if (this.IsByteAligned == true)
            {
                return _innerStream.Read(buffer, offset, count);
            }
            else
            {
                int i;
                for (i = 0; i < count; ++i)
                {
                    Boolean[] bitArray = new Boolean[8];
                    buffer[offset + i] = 0;
                    if (this.Read(bitArray, 0, 8) == 8)
                    {
                        for (int j = 0; j < 8; ++j)
                        {
                            buffer[offset + i] |= Convert.ToByte((bitArray[j] ? 1 : 0) << (7 - j));
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                return i;
            }
        }
        
        /// <summary>
        /// 使用从缓冲区读取的数据将字节块写入当前流。
        /// </summary>
        /// <param name="buffer">从中写入数据的缓冲区。</param>
        /// <param name="offset"> buffer 中的字节偏移量，从此处开始写入。</param>
        /// <param name="count">最多写入的字节数。</param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            if(this.IsByteAligned == true)
            {
                _innerStream.Write(buffer, offset, count);
            }
            else
            {
                for(int i =0;i < count;++i)
                {                    
                    Byte value = buffer[offset + i];
                    for (int j = 0; j < 8; ++j)
                    {
                        Boolean bit = ((buffer[offset + i] >>(7 - j)) & 0x1) > 0;
                        this.WriteBit(bit);
                    }
                }
            }
        }
        /// <summary>
        /// 从当前流中读取一个位。
        /// </summary>
        /// <returns> 强制转换为 System.Boolean  的位；或者如果已到达流的末尾，则为 null。</returns>
        public Nullable<Boolean> ReadBit()
        {
            Boolean[] result = new Boolean[1];
            if(Read(result,0,1)<=0)
                return null;
            return result[0];
        }
        /// <summary>
        /// 将一个位写入当前流中的当前位置。
        /// </summary>
        /// <param name="value">写入的位。</param>
        public void WriteBit(Boolean value)
        {
            Write(new Boolean[1] { value }, 0, 1);
        }
        /// <summary>
        /// 从当前流中读取位块并将数据写入 buffer 中。
        /// </summary>
        /// <param name="buffer">当此方法返回时，包含指定的字节数组，该数组中从 offset 到 (offset + count -1) 之间的值由从当前流中读取的字符替换。</param>
        /// <param name="offset">buffer 中的字节偏移量，从此处开始读取。</param>
        /// <param name="count">最多读取的位数。</param>
        /// <returns>写入缓冲区中的总位数。如果当前可用字节数不到所请求的位数，则这一总位数可能小于所请求的位数，或者如果在读取任何位前已到达流的末尾，则为零。</returns>
        public Int32 Read(Boolean[] buffer, int offset, int count)
        {
            int i;
            for(i =0;i < count;++i)
            {
                if(this.IsByteAligned == true)
                {
                    _currentByte = this.ReadByte();
                    if(_currentByte == -1) break;
                }
                _offset.Decrement();//0->7
                buffer[i + offset] = (_currentByte >> _offset & 0x1) > 0;
            }
            return i;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        public void Write(Boolean[] buffer, int offset, int count)
        {
            int i;
            for(i = 0;i < count;++i)
            {
                Boolean firstBitOfByte = false;
                if(this.IsByteAligned == true)
                {
                    _currentByte = 0;
                    firstBitOfByte = true;
                }
                _offset.Decrement();
                _currentByte |= (buffer[i + offset] ? 1 : 0) << _offset;
                if (firstBitOfByte == false) _innerStream.Seek(-1, SeekOrigin.Current);
                _innerStream.WriteByte((Byte)_currentByte);
            }            
        }
        
        /// <summary>
        /// 重写 System.IO.Stream.Flush() 以便不执行任何操作。
        /// </summary>
        public override void Flush()
        {
            this._innerStream.Flush();
        }
        /// <summary>
        /// 将当前流的长度设为指定值。
        /// </summary>
        /// <param name="value">值，通过该值设置长度。</param>
        /// <exception cref="System.NotSupportedException">当前流无法调整大小，而且 value 大于当前容量。- 或 -当前流不支持写入。</exception>
        public override void SetLength(long value)
        {
            throw new NotSupportedException("Current Stream does not support set length.");
        }
        /// <summary>
        /// 判断当期的位置是否是字节对齐位
        /// </summary>
        public Boolean IsByteAligned
        {
            get
            {
                return (_offset == 0);
            }
        }
        /// <summary>
        /// 关闭流
        /// </summary>
        public override void Close()
        {
            this._innerStream.Close();
            base.Close();
        }
        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                this._innerStream.Dispose();
            }
            base.Dispose(disposing);
        }
        /// <summary>
        /// 将流转换为数组
        /// </summary>
        /// <returns></returns>
        public Byte [] ToArray()
        {
            Int64 currentPos = this.Position;
            try
            {
                MemoryStream stream = new MemoryStream();

                this.Position = 0;
                this._innerStream.CopyTo(stream);
                return stream.ToArray();
            }
            finally
            {
                this.Position = currentPos;
            }
        }
    }
}
