using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace System.Xml.Serialization
{
    /// <summary>
    /// 将对象序列化到 XML 文档中和从 XML 文档中反序列化对象。System.Xml.Serialization.XmlSerializer 使您得以控制如何将对象编码到XML 中。
    /// </summary>
    /// <typeparam name="T">序列化或反序列化对象类型</typeparam>
    public class XmlSerializer<T>
        where T : new()
    {
        /// <summary>
        /// 创建System.Xml.Serialization.XmlSerializer对象
        /// </summary>
        public XmlSerializer()
            : this(new UTF8Encoding(false))
        {            
            //注意此处必须忽略BOM，否则生成的MemoryStream上前几个字节为BOM(字节顺序标记)
        }
        /// <summary>
        /// 创建System.Xml.Serialization.XmlSerializer对象
        /// </summary>
        /// <param name="encoding">XML 序列化时的编码方式，默认是utf-8。</param>
        public XmlSerializer(Encoding encoding)
        {
            this.Encoding = encoding;
            this.OmitXmlSerializerNamespaces = true;
            this.OmitXmlDeclaration = false;
        }

        /// <summary>
        /// XML 序列化时的编码方式，默认是utf-8。
        /// </summary>
        public Encoding Encoding { get; set; }
        /// <summary>
        /// 忽略XML序列化名空间，默认是true。
        /// </summary>
        public Boolean OmitXmlSerializerNamespaces  { get; set; }
        /// <summary>
        /// 忽略XML声明，默认是false。
        /// </summary>
        public Boolean OmitXmlDeclaration { get; set; }
        /// <summary>
        /// 将指定的类型对象转换为XML字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public String ToXmlString(T value)
        {
            using (StreamReader reader = new StreamReader(ToStream(value)))
            {
                return reader.ReadToEnd();
            }
            //return Encoding.GetString((ToStream(value) as MemoryStream).ToArray());
        }
        /// <summary>
        /// 将指定的类型对象转换为XML文件
        /// </summary>
        /// <param name="value"></param>
        /// <param name="path"></param>
        public void ToFile( T value,String path)
        {
            using (FileStream fileStream = File.Create(path))
            {
                ToStream(value, fileStream);
            }
        }
        /// <summary>
        /// 将指定的类型对象转换为XML流
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public System.IO.Stream ToStream(T value)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            MemoryStream result = new MemoryStream();
            using (MemoryStream stream = new MemoryStream())
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Encoding = this.Encoding;
                settings.OmitXmlDeclaration = this.OmitXmlDeclaration;                
                using (XmlWriter writer = XmlWriter.Create(stream,settings))
                {
                    if (this.OmitXmlSerializerNamespaces == true)
                    {
                        //Create our own namespaces for the output
                        XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                        //Add an empty namespace and empty value
                        ns.Add(String.Empty, String.Empty);
                        serializer.Serialize(writer, value, ns);
                    }
                    else
                    {
                        serializer.Serialize(writer, value);
                    }
                    stream.Flush();
                    stream.Seek(0,SeekOrigin.Begin);
                    stream.CopyTo(result);                    
                }
            }
            result.Seek(0, SeekOrigin.Begin);
            return result;
        }
        /// <summary>
        /// 将指定的类型对象转换为XML流
        /// </summary>
        /// <param name="value"></param>
        /// <param name="stream"></param>
        public void ToStream(T value,Stream stream)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = this.Encoding;
            settings.OmitXmlDeclaration = this.OmitXmlDeclaration;
            using (XmlWriter writer = XmlWriter.Create(stream, settings))
            {
                if (this.OmitXmlSerializerNamespaces == true)
                {
                    //Create our own namespaces for the output
                    XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                    //Add an empty namespace and empty value
                    ns.Add(String.Empty, String.Empty);
                    serializer.Serialize(writer, value, ns);
                }
                else
                {                    
                    serializer.Serialize(writer, value);
                }
                stream.Flush();
            }
        }
        /// <summary>
        /// 将XML字符串转换为指定的类型
        /// </summary>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        public T FromXmlString(String xmlString)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(xmlString);
                    writer.Flush();
                    stream.Seek(0, SeekOrigin.Begin);
                    return FromStream(stream);
                }
            }
        }
        /// <summary>
        /// 将XML流转换为指定的类型
        /// </summary>
        /// <param name="xmlStream"></param>
        /// <returns></returns>
        public T FromStream(System.IO.Stream xmlStream)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));            
            using (StreamReader streamReader = new StreamReader(xmlStream, Encoding, true))
            {
                return (T)serializer.Deserialize(streamReader);
            }
        }
        /// <summary>
        /// 将XML文件转换为指定的类型
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public T FromFile(System.String path)
        {
            using (FileStream fileStream = File.OpenRead(path))
            {
                return FromStream(fileStream);
            }
        }
    }
}
