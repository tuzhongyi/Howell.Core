using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Howell.Interops
{
    /// <summary>
    /// 创建结构指针
    /// </summary>
    /// <typeparam name="T">结果类型</typeparam>
    public class StructPointer<T> : IDisposable
        where T : struct
    {
        private GCHandle m_Handle;
        private T m_OriginalValue;
        /// <summary>
        /// 创建结构指针
        /// </summary>
        /// <param name="value"></param>
        public StructPointer(T value)
        {
            m_OriginalValue = value;
            m_Handle = GCHandle.Alloc(m_OriginalValue, GCHandleType.Pinned);
        }
        /// <summary>
        /// 数值
        /// </summary>
        public T Value
        {
            get
            {
                return (T)Marshal.PtrToStructure(m_Handle.AddrOfPinnedObject(), typeof(T));
            }
        }
        /// <summary>
        /// 指针
        /// </summary>
        public IntPtr Pointer
        {
            get
            {
                return m_Handle.AddrOfPinnedObject();
            }
        }
        /// <summary>
        /// 销毁对象
        /// </summary>
        public void Dispose()
        {
            m_Handle.Free();
        }
    }
    /// <summary>
    /// 创建指针结构，将指针转换为结构
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Pointer2Struct<T> : IDisposable
        where T : struct
    {
        /// <summary>
        /// 创建指针结构
        /// </summary>
        /// <param name="pointer"></param>
        public Pointer2Struct(IntPtr pointer)
        {
            this.Pointer = pointer;
            this.Value = (T)Marshal.PtrToStructure(this.Pointer, typeof(T));
        }
        /// <summary>
        /// 数值
        /// </summary>
        public T Value { get; private set; }
        /// <summary>
        /// 指针
        /// </summary>
        public IntPtr Pointer { get; private set; }
        /// <summary>
        /// 销毁对象
        /// </summary>
        public void Dispose()
        {
        }
    }
}
