using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Howell.IO.Serialization
{
    /// <summary>
    /// 二进制序列化器
    /// </summary>
    public class BinarySerializer
    {
        /// <summary>
        /// 可序列化的对象的类型.
        /// </summary>
        private Type m_Type;
        /// <summary>
        /// 初始化 BinarySerializer 类的新实例。
        /// </summary>
        protected BinarySerializer()
        {

        }
        /// <summary>
        /// 初始化 BinarySerializer 类的新实例。
        /// </summary>
        /// <param name="type">此 BinarySerializer 可序列化的对象的类型。</param>
        public BinarySerializer(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            this.m_Type = type;
        }
        #region Private
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
        private void GetProperties(Object o, out Type[] types, out Object[] values)
        {
            var ps = m_Type.GetProperties();
            types = new Type[ps.Length];
            values = new Object[ps.Length];
            for (int i = 0; i < ps.Length; i++)
            {
                types[i] = ps[i].PropertyType;
                values[i] = ps[i].GetValue(o, null);
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
        #region IsBinarySerializable
        /// <summary>
        /// 是否继承自IBinarySerializable
        /// </summary>
        /// <param name="writer">写入器</param>
        /// <param name="o">序列化对象</param>
        /// <returns>是/否</returns>
        private Boolean IsBinarySerializable(BinaryWriter writer, Object o)
        {
            if (o is IBinarySerializable)
            {
                (o as IBinarySerializable).WriteBinary(writer);
                return true;
            }
            return false;
        }
        /// <summary>
        /// 是否继承自IBinarySerializable
        /// </summary>
        /// <param name="reader">读取器</param>
        /// <param name="o">触发对象</param>
        /// <returns>是/否</returns>
        private Boolean IsBinarySerializable(BinaryReader reader, Object o)
        {
            if (o is IBinarySerializable)
            {
                (o as IBinarySerializable).ReadBinary(reader);
                return true;
            }
            return false;
        }
        #endregion
        #region Serialize
        /// <summary>
        /// 序列化Class对象
        /// </summary>
        /// <param name="type"></param>
        /// <param name="o"></param>
        /// <param name="writer"></param>
        private void SerializeClass(Type type, Object o, BinaryWriter writer)
        {
            if (type.Equals(m_Type))
                return;
            Boolean isNull = o == null || type.Equals(m_Type);
            writer.Write(isNull);
            if (!isNull)
            {
                BinarySerializer serializer = new BinarySerializer(type);
                serializer.Serialize(writer, o);
            }
        }
        /// <summary>
        /// 序列化String对象
        /// </summary>
        /// <param name="value"></param>
        /// <param name="writer"></param>
        private static void SerializeString(Object value, BinaryWriter writer)
        {
            if (value == null)
            {
                writer.Write(String.Empty);
                return;
            }
            writer.Write((String)value);
        }
        /// <summary>
        /// 序列化数组
        /// </summary>
        /// <param name="value"></param>
        /// <param name="writer"></param>
        private void SerializeArray(Object value, BinaryWriter writer)
        {
            Array vs = (Array)value;
            Int32 length = 0;
            if (vs != null)
                length = vs.Length;
            writer.Write(length);
            for (int j = 0; j < length; j++)
            {
                var v = vs.GetValue(j);
                SerializeAnalyse(v.GetType(), v, writer);
            }
        }
        /// <summary>
        /// 序列化值类型
        /// </summary>
        /// <param name="type">序列化类型</param>
        /// <param name="value">值</param>
        /// <param name="writer">写入器</param>
        private static void SerializeValue(Type type, Object value, BinaryWriter writer)
        {
            String typeName = type.Name;
            if (type.IsEnum)
                typeName = Enum.GetUnderlyingType(type).Name;
            switch (typeName)
            {
                case "Boolean":
                    writer.Write((Boolean)value);
                    break;
                case "Byte":
                    writer.Write((Byte)value);
                    break;
                case "SByte":
                    writer.Write((SByte)value);
                    break;
                case "Char":
                    writer.Write((Char)value);
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
                case "Decimal":
                    writer.Write((Decimal)value);
                    break;
                case "Float":
                    writer.Write((float)value);
                    break;
                case "Double":
                    writer.Write((Double)value);
                    break;
                default:
                    throw new InvalidOperationException(typeName);
            }
        }
        /// <summary>
        /// 分析需要哪种序列化
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <param name="writer"></param>
        private void SerializeAnalyse(Type type, Object value, BinaryWriter writer)
        {
            if (type.IsInterface)
                return;
            if (TypeIsString(type))
                SerializeString(value, writer);
            else if (type.IsArray)
                SerializeArray(value, writer);
            else if (type.IsClass)
                SerializeClass(type, value, writer);
            else if (TypeIsStruct(type))
                SerializeClass(type, value, writer);
            else
                SerializeValue(type, value, writer);
        }
        #endregion

        #region Deserialize
        /// <summary>
        /// 反序列化类
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="reader">读取器</param>
        /// <returns>反序列化对象</returns>
        private static Object DeserializeClass(Type type, BinaryReader reader)
        {
            Boolean isNull = reader.ReadBoolean();
            if (!isNull)
            {
                BinarySerializer serializer = new BinarySerializer(type);
                return serializer.Deserialize(reader);
            }
            return null;
        }
        /// <summary>
        /// 反序列化字符串
        /// </summary>
        /// <param name="reader">读取器</param>
        /// <returns>字符串</returns>
        private static String DeserializeString(BinaryReader reader)
        {
            return reader.ReadString();
        }
        /// <summary>
        /// 反序列化数组
        /// </summary>
        /// <param name="type">序列化类型</param>
        /// <param name="reader">读取器</param>
        /// <returns>数组</returns>
        private static Array DeserializeArray(Type type, BinaryReader reader)
        {
            var length = reader.ReadInt32();
            Array vs = Array.CreateInstance(type.GetElementType(), length);
            for (int i = 0; i < length; i++)
            {
                var v = DeserializeAnalyse(type.GetElementType(), reader);
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
        /// <returns>获得值对象</returns>
        private static Object DeserializeValue(Type type, BinaryReader reader)
        {
            String typeName = type.Name;
            if (type.IsEnum)
                typeName = Enum.GetUnderlyingType(type).Name;
            switch (typeName)
            {
                case "Boolean":
                    return reader.ReadBoolean();
                case "Byte":
                    return reader.ReadByte();
                case "SByte":
                    return reader.ReadSByte();
                case "Char":
                    return reader.ReadChar();
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
                case "Decimal":
                    return reader.ReadDecimal();
                case "Float":
                    return reader.ReadSingle();
                case "Double":
                    return reader.ReadDouble();
                default:
                    throw new InvalidOperationException(typeName);
            }
        }
        /// <summary>
        /// 反序列化分析
        /// </summary>
        /// <param name="type"></param>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static Object DeserializeAnalyse(Type type, BinaryReader reader)
        {
            if (type.IsInterface)
                return null;
            if (TypeIsString(type))
                return DeserializeString(reader);
            else if (type.IsArray)
                return DeserializeArray(type, reader);
            else if (type.IsClass)
                return DeserializeClass(type, reader);
            else if (TypeIsStruct(type))
                return DeserializeClass(type, reader);
            else
                return DeserializeValue(type, reader);
        }
        #endregion

        #endregion
        /// <summary>
        /// 返回从类型数组创建的 BinarySerializer 对象的数组。
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        public static BinarySerializer[] FromTypes(Type[] types)
        {
            if (types == null)
                throw new ArgumentNullException("types");
            BinarySerializer[] result = new BinarySerializer[types.Length];
            for (int i = 0; i < types.Length; i++)
            {
                result[i] = new BinarySerializer(types[i]);
            }
            return result;
        }
        #region Serialize

        /// <summary>
        /// 使用指定的 BinaryWriter 序列化指定的 System.Object。
        /// </summary>
        /// <param name="writer">写入器</param>
        /// <param name="o">将要序列化的 System.Object。</param>
        public void Serialize(BinaryWriter writer, Object o)
        {
            if (IsBinarySerializable(writer, o))
                return;
            Type[] types;
            Object[] values;
            GetProperties(o, out types, out values);
            for (int i = 0; i < types.Length; i++)
            {
                SerializeAnalyse(types[i], values[i], writer);
            }
        }
        /// <summary>
        /// 使用指定的 System.IO.Stream 序列化指定的 System.Object。
        /// </summary>
        /// <param name="stream">数据流</param>
        /// <param name="o">将要序列化的 System.Object。</param>
        public void Serialize(Stream stream, Object o)
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
        public void Serialize(Stream stream, Object o, Encoding encoding)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
            if (o == null)
                throw new ArgumentNullException("o");
            if (encoding == null)
                throw new ArgumentNullException("encoding");

            using (BinaryWriter writer = new BinaryWriter(stream, encoding))
            {
                Serialize(writer, o);
            }
        }
        #endregion


        #region Deserialize
        
        /// <summary>
        /// 反序列化指定 System.IO.Stream 包含的 Binary 数据。
        /// </summary>
        /// <param name="reader">包含要反序列化的 XML 文档的 BinaryReader。</param>
        /// <returns>正被反序列化的 System.Object。</returns>
        public Object Deserialize(BinaryReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");
            Object result = System.Activator.CreateInstance(m_Type);
            //如果继承IBinarySerializable接口
            if (IsBinarySerializable(reader, result))
                return result;

            var ps = m_Type.GetProperties();
            for (int i = 0; i < ps.Length; i++)
            {
                var pt = ps[i].PropertyType;
                var value = DeserializeAnalyse(pt, reader);
                ps[i].SetValue(result, value, null);
            }
            return result;
        }
        /// <summary>
        /// 反序列化指定 System.IO.Stream 包含的 Binary 数据。
        /// </summary>
        /// <param name="stream">包含要反序列化的 Binary 数据的 System.IO.Stream。</param>
        /// <param name="encoding">序列化的编码样式。</param>
        /// <returns>正被反序列化的 System.Object。</returns>
        public Object Deserialize(Stream stream, Encoding encoding)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            using (BinaryReader reader = new BinaryReader(stream, encoding))
            {
                return Deserialize(reader);
            }
        }
        /// <summary>
        /// 反序列化指定 System.IO.Stream 包含的 Binary 数据。
        /// </summary>
        /// <param name="stream">包含要反序列化的 Binary 数据的 System.IO.Stream。</param>
        /// <returns>正被反序列化的 System.Object。</returns>
        public Object Deserialize(Stream stream)
        {
            return Deserialize(stream, Encoding.UTF8);
        }
        #endregion
    }
}
