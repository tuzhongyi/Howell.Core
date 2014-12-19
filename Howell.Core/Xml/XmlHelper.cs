using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using Howell.IO;

namespace Howell.Xml
{
    /// <summary>
    /// XML帮助工具
    /// </summary>
    public static class XmlHelper
    {
        /// <summary>
        /// 将枚举类型转换为Optional XML选项。
        /// </summary>
        /// <param name="type">枚举类型。</param>
        /// <returns>返回Optional选项。</returns>
        public static String ToOptional(Type type)
        {
            String result = "";
            Int32 index = 0;
            if (type.IsEnum == false) throw new ArgumentException("Parameter type is not Enum Type");
            foreach (String item in Enum.GetNames(type))
            {
                if (index == 0)
                    result += item;
                else
                    result += "," + item;
                ++index;
            }
            return result;
        }
        /// <summary>
        /// 转换为XML取值范围
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns></returns>
        public static String ToRange(Int32 min, Int32 max)
        {
            return String.Format("{0}~{1}", min, max);
        }
        /// <summary>
        /// 将字节数据转换为HexBinary字符串
        /// </summary>
        /// <param name="hexBinary">字节数组</param>
        /// <returns>返回HexBinary字符串</returns>
        public static String ToHexBinary(Byte[] hexBinary)
        {
            String result = "";
            for (Int32 i = 0; i < hexBinary.Length; ++i)
            {
                result += String.Format("{0:X2}", hexBinary[i]);
            }
            return result;
        }
        /// <summary>
        /// 将HexBinary字符串转换为字节数组
        /// </summary>
        /// <param name="hexBinary">HexBinary字符串</param>
        /// <returns>返回字节数组</returns>
        public static Byte[] FromHexBinary(String hexBinary)
        {
            Byte[] result = new Byte[hexBinary.Length / 2];
            for (Int32 i = 0; i < hexBinary.Length / 2; ++i)
            {
                result[i] = Convert.ToByte(hexBinary.Substring(i * 2, 2), 16);
            }
            return result;
        }
        /// <summary>
        /// 将对象序列化为数据流
        /// </summary>
        /// <typeparam name="T">序列化对象类型</typeparam>
        /// <param name="o">对象</param>
        /// <param name="stream">数据流</param>
        /// <param name="ns">名空间</param>
        public static void Serialize<T>(T o, Stream stream, String ns)
        {
            Serialize<T>(o, stream, ns, new UTF8Encoding(false));
        }
        /// <summary>
        /// 将对象序列化为数据流
        /// </summary>
        /// <typeparam name="T">序列化对象类型</typeparam>
        /// <param name="o">对象</param>
        /// <param name="stream">数据流</param>
        /// <param name="ns">名空间</param>
        /// <param name="encoding">编码格式</param>
        public static void Serialize<T>(T o, Stream stream, String ns, Encoding encoding)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            XmlWriterSettings xws = new XmlWriterSettings();
            xws.OmitXmlDeclaration = false;
            xws.Indent = true;
            xws.Encoding = encoding;
            //XmlSerializerNamespaces xns = new XmlSerializerNamespaces();
            //xns.Add("xml-ns", ns);
            using (CloseableStreamWriter writer = new CloseableStreamWriter(stream, encoding))
            {
                writer.Closeable = false;
                xs.Serialize(writer, o);
            }
        }
        /// <summary>
        /// 将对象序列化为字符串
        /// </summary>
        /// <typeparam name="T">序列化对象类型</typeparam>
        /// <param name="o">对象</param>
        /// <param name="ns">名空间</param>
        /// <returns>字符串</returns>
        public static String Serialize<T>(T o, String ns)
        {
            using (MemoryStream s = new MemoryStream())
            {
                Serialize<T>(o, s, ns);
                return System.Text.Encoding.UTF8.GetString(s.ToArray());
            }
        }
        /// <summary>
        /// 将对象序列化为字符串
        /// </summary>
        /// <typeparam name="T">序列化对象类型</typeparam>
        /// <param name="o">对象</param>
        /// <returns>字符串</returns>
        public static String Serialize<T>(T o)
        {
            return Serialize<T>(o,"");
        }
        /// <summary>
        /// 返回序列化字符串到指定对象
        /// </summary>
        /// <typeparam name="T">序列化对象类型</typeparam>
        /// <param name="s">字符串</param>
        /// <returns>对象</returns>
        public static T Deserialize<T>(String s)
        {
            using (MemoryStream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(s)))
            {
                return Deserialize<T>(stream);
            }
        }
        /// <summary>
        /// 返回序列化XML到指定对象
        /// </summary>
        /// <typeparam name="T">序列化对象类型</typeparam>
        /// <param name="stream">数据流</param>
        /// <returns>返回反序列化后的对象实例</returns>
        public static T Deserialize<T>(Stream stream)
        {
            return Deserialize<T>(stream, new UTF8Encoding(false));
        }
        /// <summary>
        /// 返回序列化XML到指定对象
        /// </summary>
        /// <typeparam name="T">序列化对象类型</typeparam>
        /// <param name="stream">数据流</param>
        /// <param name="encoding">编码格式</param>
        /// <returns>返回反序列化后的对象实例</returns>
        public static T Deserialize<T>(Stream stream,Encoding encoding)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            using (CloseableStreamReader reader = new CloseableStreamReader(stream, encoding))
            {
                T result = (T)xs.Deserialize(reader);
                return result;
            }
        }
        /// <summary>
        /// 将对象序列化为数据流
        /// </summary>
        /// <typeparam name="T">序列化对象类型</typeparam>
        /// <param name="o">对象</param>
        /// <param name="builder">数据流</param>
        /// <param name="ns">名空间</param>
        public static void Serialize<T>(T o, ref StringBuilder builder, String ns)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            XmlWriterSettings xws = new XmlWriterSettings();
            xws.OmitXmlDeclaration = true;
            xws.Indent = true;
            xws.Encoding = new UTF8Encoding(false);

            //XmlSerializerNamespaces xns = new XmlSerializerNamespaces();
            //xns.Add("xml-ns", ns);
            using (StringWriter writer = new StringWriter(builder))
            {
                xs.Serialize(writer, o);
            }
        }
        /// <summary>
        /// 返回序列化XML到指定对象
        /// </summary>
        /// <typeparam name="T">序列化对象类型</typeparam>
        /// <param name="builder">数据流</param>
        /// <returns>返回反序列化后的对象实例</returns>
        public static T Deserialize<T>(StringBuilder builder)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            using (MemoryStream s = new MemoryStream(new UTF8Encoding(false).GetBytes(builder.ToString())))
            {
                using (CloseableStreamReader reader = new CloseableStreamReader(s, new UTF8Encoding(false)))
                {
                    T result = (T)xs.Deserialize(reader);
                    return result;
                }
            }
        }
        /// <summary>
        /// 读取Any lax
        /// </summary>
        /// <param name="reader">xml reader</param>
        /// <returns>返回XmlElement 数组</returns>
        public static XmlElement[] ReadAny(XmlReader reader)
        {
            int depth = reader.Depth;
            XmlDocument doc = new XmlDocument();
            List<XmlElement> list = new List<XmlElement>();
            //do
            while (depth == reader.Depth)
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    doc.LoadXml(reader.ReadOuterXml());
                    list.Add(doc.DocumentElement);
                }
                else
                {
                    reader.Read();
                    continue;
                }
            }
            if (list.Count == 0) return null;
            return list.ToArray();
        }
        /// <summary>
        /// 写入 Any lax
        /// </summary>
        /// <param name="writer">xml writer</param>
        /// <param name="eles">XmlElement 数组</param>
        public static void WriteAny(XmlWriter writer, XmlElement[] eles)
        {
            if (eles == null) return;
            foreach (XmlElement ele in eles)
            {
                writer.WriteRaw(ele.OuterXml.ToString());
            }
        }
        /// <summary>
        /// 读取XML节点数组元素
        /// </summary>
        /// <typeparam name="T">节点元素类型</typeparam>
        /// <param name="reader">xml reader</param>
        /// <param name="localName">元素节点的名称</param>
        /// <returns>返回泛型元素数组</returns>
        public static T[] ReadArray<T>(XmlReader reader, String localName) where T : IXmlSerializable, new()
        {
            List<T> result = new List<T>();
            do
            {
                T item = new T();
                item.ReadXml(reader);
                result.Add(item);
            } while (reader.IsStartElement(localName) == true);
            if (result.Count <= 0) return null;
            return result.ToArray();
        }
        /// <summary>
        /// 写入XML节点数组元素
        /// </summary>
        /// <typeparam name="T">节点元素类型</typeparam>        
        /// <param name="writer">xml writer</param>
        /// <param name="resources">节点数组</param>
        public static void WriteArray<T>(XmlWriter writer, T[] resources) where T : IXmlSerializable
        {
            if (resources == null) return;
            foreach (T rc in resources)
            {
                if (rc != null)
                    rc.WriteXml(writer);
            }
        }
        /// <summary>
        /// 克隆XML序列化对象
        /// </summary>
        /// <typeparam name="T">可序列化对象</typeparam>
        /// <param name="value">数值</param>
        /// <param name="ns">Namespace of XML</param>
        /// <returns>返回克隆后的新对象</returns>
        public static T Clone<T>(T value,String ns) where T: new()
        {
            using(Stream s = new MemoryStream())
            {
                Serialize<T>(value, s, ns);
                s.Seek(0, SeekOrigin.Begin);
                return Deserialize<T>(s);
            }
        }
    }
}
