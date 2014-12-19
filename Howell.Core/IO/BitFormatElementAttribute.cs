using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Howell.IO
{
    /// <summary>
    /// 位格式化元素特性
    /// </summary>    
    [AttributeUsage(AttributeTargets.Property)]    
    public class BitFormatElementAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">格式化属性类型</param>
        public BitFormatElementAttribute(BitType type)
            : base()
        {
            this.IsNullable = false;
            this.Order = 0;
            this.Type = type;
            this.Length = 0;
        }
        /// <summary>
        /// 是否可以为空
        /// </summary>
        public bool IsNullable { get; set; }
        /// <summary>
        /// 序列化顺序
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public BitType Type { get; set; }
        /// <summary>
        /// 长度
        /// </summary>
        public Int32 Length { get; set; }                
    }
    /// <summary>
    ///  
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class BitSerializableAttribute: Attribute
    {

    }
    /// <summary>
    /// 位对象类型
    /// </summary>
    public enum BitType
    {
        /// <summary>
        /// 用户自定义类型
        /// </summary>
        Object = 0,
        /// <summary>
        /// 位
        /// </summary>
        Bit =1,
        /// <summary>
        /// 多个位
        /// </summary>
        Bits = 2,
        /// <summary>
        /// 1个字节的无符号整数
        /// </summary>
        Byte = 3,
        /// <summary>
        /// 2个字节的有符号整数
        /// </summary>
        Int16 = 4,
        /// <summary>
        /// 4个字节的有符号整数
        /// </summary>
        Int32 = 5,
        /// <summary>
        /// 8个字节的有符号整数
        /// </summary>
        Int64 = 6,
        /// <summary>
        /// 2个字节的无符号整数
        /// </summary>
        UInt16 = 7,
        /// <summary>
        /// 4个字节的无符号整数
        /// </summary>
        UInt32 = 8,
        /// <summary>
        /// 8个字节的无符号整数
        /// </summary>
        UInt64 = 9,
        /// <summary>
        /// 字符串对象
        /// </summary>
        String = 10,
        /// <summary>
        /// 字节数组
        /// </summary>
        ByteArray = 11,
        /// <summary>
        /// 无符号指数哥伦布编码
        /// </summary>
        GolombUE32 = 12,
        /// <summary>
        /// 有符号指数哥伦布编码
        /// </summary>
        GolombSE32 = 13,
    }
}
