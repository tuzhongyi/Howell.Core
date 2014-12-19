using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace Howell.IO.Serialization
{
    /// <summary>
    /// Bit序列化器
    /// </summary>
    public class BitSerializer
    {
        /// <summary>
        /// 可序列化类型
        /// </summary>
        protected Type m_Type = null;
        /// <summary>
        /// 初始化 BitSerializer 类的新实例。
        /// </summary>
        protected BitSerializer()
        {

        }
        /// <summary>
        /// 初始化 BitSerializer 类的新实例。
        /// </summary>
        /// <param name="type">此 BitSerializer 可序列化的对象的类型。</param>
        public BitSerializer(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            this.m_Type = type;
        }

        /// <summary>
        /// 是否是字符串
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static Boolean TypeIsString(Type type)
        {
            return type.Name.ToLower().Equals("string");
        }
        /// <summary>
        /// 是否是结构
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static Boolean TypeIsStruct(Type type)
        {
            return type.IsValueType && !type.IsEnum && !type.IsPrimitive;
        }
        /// <summary>
        /// 获取对象下所有属性
        /// </summary>
        /// <param name="o"></param>
        /// <param name="types"></param>
        /// <param name="values"></param>
        /// <param name="attributes"></param>
        private void GetProperties(Object o, out Type[] types, out Object[] values, out BitFormatElementAttribute[] attributes)
        {
            var ps = m_Type.GetProperties();
            ps = ps.Where(x => GetBitFormatElementAttribute(x) != null).OrderBy(x => GetBitFormatElementAttribute(x).Order).ToArray();

            types = new Type[ps.Length];
            values = new Object[ps.Length];
            attributes = new BitFormatElementAttribute[ps.Length];

            for (int i = 0; i < ps.Length; i++)
            {
                attributes[i] = GetBitFormatElementAttribute(ps[i]);
                values[i] = ps[i].GetValue(o, null);

                if (!attributes[i].IsNullable && values[i] == null)
                    throw new ArgumentNullException(ps[i].Name);

                types[i] = ps[i].PropertyType;
            }
        }
        /// <summary>
        /// 获取序列化类型下所有属性
        /// </summary>
        /// <returns></returns>
        private Type[] GetProperties()
        {
            var ps = m_Type.GetProperties();
            var types = new Type[ps.Length];
            for (int i = 0; i < ps.Length; i++)
            {
                types[i] = ps[i].PropertyType;
            }
            return types;
        }
        /// <summary>
        /// 获取枚举值
        /// </summary>
        /// <param name="type">枚举类型</param>
        /// <param name="value">数值</param>
        /// <returns>枚举值</returns>
        private static Object GetEnumValue(Type type, Object value)
        {
            return Enum.Parse(type, Enum.GetName(type, value));
        }
        #region IsBitSerializable
        /// <summary>
        /// 是否继承自IBitSerializable
        /// </summary>
        /// <param name="writer">写入器</param>
        /// <param name="o">序列化对象</param>
        /// <returns>是/否</returns>
        private Boolean IsBitSerializable(BitWriter writer, Object o)
        {
            if (o is IBitSerializable)
            {
                (o as IBitSerializable).WriteBitFormat(writer);
                return true;
            }
            return false;
        }
        /// <summary>
        /// 是否继承自IBitSerializable
        /// </summary>
        /// <param name="reader">读取器</param>
        /// <param name="o">触发对象</param>
        /// <returns>是/否</returns>
        private Boolean IsBitSerializable(BitReader reader, Object o)
        {
            if (o is IBitSerializable)
            {
                (o as IBitSerializable).ReadBitFormat(reader);
                return true;
            }
            return false;
        }
        #endregion
        /// <summary>
        /// 是否是IsBitSerializableAttribute属性
        /// </summary>
        private Boolean IsBitSerializableAttribute
        {
            get
            {
                var attributes = m_Type.GetCustomAttributes(typeof(BitSerializableAttribute), true);
                return attributes.Count() > 0;
            }
        }
        /// <summary>
        /// 获取属性特性
        /// </summary>
        /// <param name="property">属性</param>
        /// <returns>特性</returns>
        private BitFormatElementAttribute GetBitFormatElementAttribute(PropertyInfo property)
        {
            var attributes = property.GetCustomAttributes(false);
            var result = attributes.Where(x => x is BitFormatElementAttribute).SingleOrDefault();
            if (result == null)
                return null;
            return result as BitFormatElementAttribute;
        }

        /// <summary>
        /// 返回从类型数组创建的 BitSerializer 对象的数组。
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        public static BitSerializer[] FromTypes(Type[] types)
        {
            if (types == null)
                throw new ArgumentNullException("types");
            BitSerializer[] result = new BitSerializer[types.Length];
            for (int i = 0; i < types.Length; i++)
            {
                result[i] = new BitSerializer(types[i]);
            }
            return result;
        }
        #region Serialize
        /// <summary>
        /// 序列化Class对象
        /// </summary>
        /// <param name="type"></param>
        /// <param name="o"></param>
        /// <param name="writer"></param>
        /// <param name="encoding"></param>
        private void SerializeClass(Type type, Object o, BitWriter writer, Encoding encoding)
        {
            if (type.Equals(m_Type))
                return;
            Boolean isNull = o == null || type.Equals(m_Type);
            writer.WriteBit(isNull ? 1 : 0);
            if (!isNull)
            {
                BitSerializer serializer = new BitSerializer(type);
                serializer.Serialize(writer, o, encoding);
            }
        }
        /// <summary>
        /// 序列化String对象
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="attribute"></param>
        /// <param name="encoding"></param>
        private static void SerializeString(BitWriter writer, String value, BitFormatElementAttribute attribute, Encoding encoding)
        {
            if(value == null)
                return;
            if (attribute.Length <= 0)
                throw new ArgumentOutOfRangeException("length");            
            
            writer.Write(value, attribute.Length);
        }
        /// <summary>
        /// 序列化数组
        /// </summary>
        /// <param name="value"></param>
        /// <param name="writer"></param>
        /// <param name="attribute"></param>
        /// <param name="encoding"></param>
        private void SerializeArray(Object value, BitWriter writer, BitFormatElementAttribute attribute, Encoding encoding)
        {
            Array vs = (Array)value;
            if (attribute.Length <= 0)
                throw new ArgumentOutOfRangeException("length");
            for (int j = 0; j < attribute.Length; j++)
            {
                var v = vs.GetValue(j);
                SerializeAnalyse(v.GetType(), v, writer, attribute, encoding);
            }
        }
        /// <summary>
        /// 序列化值类型
        /// </summary>
        /// <param name="type">序列化类型</param>
        /// <param name="value">值</param>
        /// <param name="writer">写入器</param>
        /// <param name="encoding">序列化编码样式。</param>
        private static void SerializeValue(Type type, Object value, BitWriter writer, Encoding encoding)
        {
            String typeName = type.Name;
            if (type.IsEnum)
                typeName = Enum.GetUnderlyingType(type).Name;
            switch (typeName)
            {
                case "Boolean":
                    writer.WriteBit(((Boolean)value) ? 1 : 0);
                    break;
                case "Byte":
                    writer.Write((Byte)value);
                    break;
                case "Char":
                    writer.Write(BitConverter.GetBytes((Char)value));
                    //writer.Write(encoding.GetBytes(new Char[] { (Char)value }, ));
                    break;
                case "Int16":
                    writer.Write((Int16)value);
                    break;
                case "Int32":
                    writer.Write((Int32)value);
                    break;
                case "Int64":
                    writer.Write((Int64)value);
                    break;
                case "UInt16":
                    writer.Write((UInt16)value);
                    break;
                case "UInt32":
                    writer.Write((UInt32)value);
                    break;
                case "UInt64":
                    writer.Write((UInt64)value);
                    break;
                default:
                    throw new InvalidOperationException(typeName);
            }
        }
        private Boolean SerializeByAttribute(BitFormatElementAttribute attribute, BitWriter writer, Object value)
        {
            switch (attribute.Type)
            {
                case BitType.Bits:
                    if (attribute.Length <= 0)
                        throw new ArgumentOutOfRangeException("length");
                    writer.WriteBits((Int32)value, attribute.Length);
                    return true;
                case BitType.GolombUE32:
                    throw new InvalidOperationException("GolombUE32");
                    //return true;
                case BitType.GolombSE32:
                    throw new InvalidOperationException("GolombSE32");
                    //return true;
                default:
                    return false;
            }
        }
        /// <summary>
        /// 分析需要哪种序列化
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <param name="writer"></param>
        /// <param name="attribute"></param>
        /// <param name="encoding"></param>
        private void SerializeAnalyse(Type type, Object value, BitWriter writer, BitFormatElementAttribute attribute, Encoding encoding)
        {
            if (type.IsInterface)
                return;
            if (SerializeByAttribute(attribute, writer, value))
                return;

            if (TypeIsString(type))
                SerializeString(writer, (String)value, attribute, encoding);
            else if (type.IsArray)
                SerializeArray(value, writer, attribute, encoding);
            else if (type.IsClass)
                SerializeClass(type, value, writer, encoding);
            else if (TypeIsStruct(type))
                SerializeClass(type, value, writer, encoding);
            else
                SerializeValue(type, value, writer, encoding);
        }
        /// <summary>
        /// 使用指定的 BitWriter 序列化指定的 System.Object。
        /// </summary>
        /// <param name="writer">写入器</param>
        /// <param name="o">将要序列化的 System.Object。</param>
        /// <param name="encoding">编码类型(默认UTF8)</param>
        public void Serialize(BitWriter writer, Object o, Encoding encoding)
        {
            if (IsBitSerializable(writer, o))
                return;
            if (!IsBitSerializableAttribute)
                throw new NotSupportedException("Object does not have attribute BitSerializableAttribute, and does not inherit from IBitSerializable");
            Type[] types;
            Object[] values;
            BitFormatElementAttribute[] attributes;
            GetProperties(o, out types, out values, out attributes);
            for (int i = 0; i < types.Length; i++)
            {
                SerializeAnalyse(types[i], values[i], writer, attributes[i], encoding);
            }
        }
        /// <summary>
        /// 使用指定的 System.IO.Stream 序列化指定的 System.Object。
        /// </summary>
        /// <param name="stream">数据流</param>
        /// <param name="o">将要序列化的 System.Object。</param>
        public void Serialize(BitStream stream, Object o)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
            if (o == null)
                throw new ArgumentNullException("o");
            Serialize(stream, o, Encoding.UTF8);
        }
        /// <summary>
        /// 使用指定的 System.IO.Stream 序列化指定的 System.Object。
        /// </summary>
        /// <param name="stream">数据流</param>
        /// <param name="o">将要序列化的 System.Object。</param>
        /// <param name="encoding">编码类型(默认UTF8)</param>
        public void Serialize(BitStream stream, Object o, Encoding encoding)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            using (BitWriter writer = new BitWriter(stream, encoding))
            {
                Serialize(writer, o, encoding);
            }
        }
        /// <summary>
        /// 使用指定的 BitWriter 序列化指定的 System.Object。
        /// </summary>
        /// <param name="writer">写入器</param>
        /// <param name="o">将要序列化的 System.Object。</param>
        public void Serialize(BitWriter writer, Object o)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");
            if (o == null)
                throw new ArgumentNullException("o");
            Serialize(writer, o, Encoding.UTF8);
        }
        #endregion


        #region Deserialize
        /// <summary>
        /// 反序列化类
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="reader">读取器</param>
        /// <returns>反序列化对象</returns>
        private static Object DeserializeClass(Type type, BitReader reader)
        {
            Boolean isNull = reader.ReadBit() == 1;
            if (!isNull)
            {
                BitSerializer serializer = new BitSerializer(type);
                return serializer.Deserialize(reader);
            }
            return null;
        }
        /// <summary>
        /// 反序列化字符串
        /// </summary>
        /// <param name="reader">读取器</param>
        /// <param name="attribute"></param>
        /// <returns>字符串</returns>
        private static String DeserializeString(BitReader reader, BitFormatElementAttribute attribute)
        {
            if (attribute.Length <= 0)
                throw new ArgumentOutOfRangeException("length");
            return reader.ReadString(attribute.Length);
        }
        /// <summary>
        /// 反序列化数组
        /// </summary>
        /// <param name="type">序列化类型</param>
        /// <param name="reader">读取器</param>
        /// <param name="attribute"></param>
        /// <param name="encoding">编码类型(默认UTF8)</param>
        /// <returns>数组</returns>
        private static Array DeserializeArray(Type type, BitReader reader, BitFormatElementAttribute attribute, Encoding encoding)
        {
            if(attribute.Length <=0)
                throw new ArgumentOutOfRangeException("length");

            Array vs = Array.CreateInstance(type.GetElementType(), attribute.Length);
            for (int i = 0; i < vs.Length; i++)
            {
                var v = DeserializeAnalyse(type.GetElementType(), reader, attribute, encoding);
                if (type.GetElementType().IsEnum)
                    v = GetEnumValue(type.GetElementType(), v);
                vs.SetValue(v, i);
            }
            return vs;
        }
        /// <summary>
        /// 反序列化值类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="reader">读取器</param>
        /// <param name="encoding">编码类型(默认UTF8)</param>
        /// <returns>获得值对象</returns>
        private static Object DeserializeValue(Type type, BitReader reader, Encoding encoding)
        {
            String typeName = type.Name;
            if (type.IsEnum)
                typeName = Enum.GetUnderlyingType(type).Name;
            switch (typeName)
            {
                case "Boolean":
                    return reader.ReadBit() == 1;
                case "Byte":
                    return reader.ReadByte();
                case "Char":
                    return BitConverter.ToChar(reader.ReadBytes(2), 0);
                    //return encoding.GetChars(reader.ReadBytes(2)).SingleOrDefault();
                case "Int16":
                    return reader.ReadInt16();
                case "Int32":
                    return reader.ReadInt32();
                case "Int64":
                    return reader.ReadInt64();
                case "UInt16":
                    return reader.ReadUInt16();
                case "UInt32":
                    return reader.ReadUInt32();
                case "UInt64":
                    return reader.ReadUInt64();
                default:
                    throw new InvalidOperationException(typeName);
            }
        }
        private static Boolean DeserializeByAttribute(BitReader reader, BitFormatElementAttribute attribute, out Object value)
        {
            value = null;
            switch (attribute.Type)
            {
                case BitType.Bits:
                    if (attribute.Length <= 0)
                        throw new ArgumentOutOfRangeException("length");
                    value = reader.ReadBits(attribute.Length);
                    return true;
                case BitType.GolombUE32:
                    throw new InvalidOperationException("GolombUE32");
                    //return true;
                case BitType.GolombSE32:
                    throw new InvalidOperationException("GolombSE32");
                    //return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// 反序列化分析
        /// </summary>
        /// <param name="type">分析类型</param>
        /// <param name="reader">读取器</param>
        /// <param name="attribute"></param>
        /// <param name="encoding">编码类型(默认UTF8)</param>
        /// <returns>反序列化得到的对象</returns>
        private static Object DeserializeAnalyse(Type type, BitReader reader, BitFormatElementAttribute attribute, Encoding encoding)
        {
            if (type.IsInterface)
                return null;
            Object value;
            if (DeserializeByAttribute(reader, attribute, out value))
                return value;

            if (TypeIsString(type))
                return DeserializeString(reader, attribute);
            else if (type.IsArray)
                return DeserializeArray(type, reader, attribute, encoding);
            else if (type.IsClass)
                return DeserializeClass(type, reader);
            else if (TypeIsStruct(type))
                return DeserializeClass(type, reader);
            else
                return DeserializeValue(type, reader, encoding);
        }
        /// <summary>
        /// 反序列化指定 System.IO.Stream 包含的 Bit 数据。
        /// </summary>
        /// <param name="reader">包含要反序列化的 XML 文档的 BitReader。</param>
        /// <param name="encoding">序列化的编码样式。</param>
        /// <returns>正被反序列化的 System.Object。</returns>
        public Object Deserialize(BitReader reader, Encoding encoding)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");
            Object result = System.Activator.CreateInstance(m_Type);
            //如果继承IBitSerializable接口
            if (IsBitSerializable(reader, result))
                return result;
            if (!IsBitSerializableAttribute)
                throw new NotSupportedException("Object does not have attribute BitSerializableAttribute, and does not inherit from IBitSerializable");

            var ps = m_Type.GetProperties();
            ps = ps.Where(x => GetBitFormatElementAttribute(x) != null).OrderBy(x => GetBitFormatElementAttribute(x).Order).ToArray();
            for (int i = 0; i < ps.Length; i++)
            {
                var pt = ps[i].PropertyType;
                var attribute = GetBitFormatElementAttribute(ps[i]);
                var value = DeserializeAnalyse(pt, reader, attribute, encoding);
                ps[i].SetValue(result, value, null);
            }
            return result;
        }
        /// <summary>
        /// 反序列化指定 System.IO.Stream 包含的 Bit 数据。
        /// </summary>
        /// <param name="reader">包含要反序列化的 XML 文档的 BitReader。</param>
        /// <returns>正被反序列化的 System.Object。</returns>
        public Object Deserialize(BitReader reader)
        {
            return Deserialize(reader, Encoding.UTF8);
        }
        /// <summary>
        /// 反序列化指定 System.IO.Stream 包含的 Bit 数据。
        /// </summary>
        /// <param name="stream">包含要反序列化的 Bit 数据的 System.IO.Stream。</param>
        /// <param name="encoding">序列化的编码样式。</param>
        /// <returns>正被反序列化的 System.Object。</returns>
        public Object Deserialize(Stream stream, Encoding encoding)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            using (BitReader reader = new BitReader(stream, encoding))
            {
                return Deserialize(reader, encoding);
            }
        }
        /// <summary>
        /// 反序列化指定 System.IO.Stream 包含的 Bit 数据。
        /// </summary>
        /// <param name="stream">包含要反序列化的 Bit 数据的 System.IO.Stream。</param>
        /// <returns>正被反序列化的 System.Object。</returns>
        public Object Deserialize(Stream stream)
        {
            return Deserialize(stream, Encoding.UTF8);
        }
        #endregion
    }
}
