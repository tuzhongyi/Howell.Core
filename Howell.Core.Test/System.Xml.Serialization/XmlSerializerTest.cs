using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;

namespace System.Xml.Serialization
{
    public static class XmlSerializerTest
    {
        public static void Test()
        {
            Console.WriteLine("++++++++++++XmlSerializerTest++++++++++++++");

            RegularExpressionsElement element = new RegularExpressionsElement();
            element.Username = "user1";
            element.Password = "中文";
            XmlSerializer<RegularExpressionsElement> serializer = new XmlSerializer<RegularExpressionsElement>();
            serializer.OmitXmlDeclaration = true;
            String xmlString = serializer.ToXmlString(element);
            Console.WriteLine("ToXmlString: \r\n {0}", xmlString);
            RegularExpressionsElement newElement = serializer.FromXmlString(xmlString);
            Console.WriteLine("Username:{0} Password:{1}", newElement.Username, newElement.Password);

            using (FileStream file = File.Create("RegularExpressions.xml"))
            {
                serializer.ToStream(element, file);
            }
            using(FileStream file = File.Open("RegularExpressions.xml",FileMode.Open))
            {
                newElement = serializer.FromStream(file);
                Console.WriteLine("Username:{0} Password:{1}", newElement.Username, newElement.Password);
            }

        }
    }
    /// <summary>
    /// 正则表达式结构
    /// </summary>
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "RegularExpressionsElement")]
    public class RegularExpressionsElement
    {
        /// <summary>
        /// 用户名的正则表达式的CData形式
        /// </summary>
        [XmlIgnoreAttribute()]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public XmlNode UsernameCData
        {
            get
            {
                return new XmlDocument().CreateCDataSection(Username);
            }
            set
            {
                if (value == null)
                {
                    Username = null;
                    return;
                }
                Username = value.Value;
            }
        }
        /// <summary>
        /// 用户名的正则表达式
        /// </summary>
        [XmlElementAttribute("Username", Order = 1)]
        public String Username { get; set; }
        /// <summary>
        /// 用户名的正则表达式的CData形式
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [XmlIgnoreAttribute()]
        public XmlNode PasswordCData
        {
            get
            {
                return new XmlDocument().CreateCDataSection(Password);
            }
            set
            {
                if (value == null)
                {
                    Password = null;
                    return;
                }
                Password = value.Value;
            }
        }
        /// <summary>
        /// 用户名的正则表达式
        /// </summary>
        [XmlElementAttribute("Password", Order = 2)]
        public String Password { get; set; }
    }
}
