using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace System.Runtime.Serialization.Json
{
    /// <summary>
    /// Json序列化器
    /// </summary>
    /// <typeparam name="T">Json序列化对象类型</typeparam>
    public class JsonSerializer<T>
        where T : new()
    {
        private DataContractJsonSerializer m_Serializer;
        /// <summary>
        /// 创建 System.Runtime.Serialization.Json.JsonSerializer 对象
        /// </summary>
        public JsonSerializer()
            : this(new UTF8Encoding(false))
        {
            m_Serializer = new DataContractJsonSerializer(typeof(T));            
        }
        /// <summary>
        /// 创建 System.Runtime.Serialization.Json.JsonSerializer 对象
        /// </summary>
        /// <param name="encoding">XML 序列化时的编码方式，默认是utf-8。</param>
        public JsonSerializer(Encoding encoding)
        {
            this.Encoding = encoding;
        }
        /// <summary>
        /// Json序列化时的编码方式，默认是utf-8。
        /// </summary>
        public Encoding Encoding { get; set; }
        /// <summary>
        /// 将指定的类型对象转换为Json字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public String ToJsonString(T value)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                serializer.WriteObject(stream, value);
                stream.Seek(0, SeekOrigin.Begin);
                return Encoding.GetString(stream.ToArray());
            }

        }
        /// <summary>
        /// 将指定的类型对象转换为Json文件
        /// </summary>
        /// <param name="value"></param>
        /// <param name="path"></param>
        public void ToFile(T value, String path)
        {
            using (FileStream fileStream = File.Create(path))
            {
                ToStream(value, fileStream);
            }
        }
        /// <summary>
        /// 将指定的类型对象转换为Json流
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public System.IO.Stream ToStream(T value)
        {
            return new MemoryStream(Encoding.GetBytes(ToJsonString(value)));
        }
        /// <summary>
        /// 将指定的类型对象转换为Json字节数组
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public System.Byte[] ToArray(T value)
        {
            using (MemoryStream ms = new MemoryStream(Encoding.GetBytes(ToJsonString(value))))
            {
                return ms.ToArray();
            }
        }
        /// <summary>
        /// 将指定的类型对象转换为Json流
        /// </summary>
        /// <param name="value"></param>
        /// <param name="stream"></param>
        public void ToStream(T value, Stream stream)
        {
            Byte[] bytes = Encoding.GetBytes(ToJsonString(value));
            stream.Write(bytes, 0, bytes.Length);
            stream.Flush();
        }
        /// <summary>
        /// 将XML字符串转换为指定的类型
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public T FromJsonString(String jsonString)
        {            
            using (MemoryStream stream = new MemoryStream(Encoding.GetBytes(jsonString)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                return (T)serializer.ReadObject(stream);
            }            
        }
        /// <summary>
        /// 将JSON流转换为指定的类型
        /// </summary>
        /// <param name="jsonStream"></param>
        /// <returns></returns>
        public T FromStream(System.IO.Stream jsonStream)
        {
            using (MemoryStream s = new MemoryStream())
            {
                jsonStream.CopyTo(s);
                return FromJsonString(Encoding.GetString(s.ToArray()));
            }
        }
        /// <summary>
        /// 将JSON文件转换为指定的类型
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
        /// <summary>
        /// 将JSON数组转换为指定的类型
        /// </summary>
        /// <param name="jsonBytes"></param>
        /// <returns></returns>
        public T FromArray(System.Byte[] jsonBytes)
        {
            using (MemoryStream stream = new MemoryStream(jsonBytes))
            {
                return FromStream(stream);
            }
        }
    }
}
