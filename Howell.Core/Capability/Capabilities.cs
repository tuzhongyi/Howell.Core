using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;

namespace Howell.Capability
{
    /// <summary>
    /// 类型能力
    /// </summary>
    [Serializable]
    [XmlRootAttribute(Namespace = Constants.Namespace)]
    public class Capabilities
    {
        private static char[] options_splits = { ',' };
        /// <summary>
        /// 创建Capabilities对象
        /// </summary>
        public Capabilities()
        {
            AnyAttr = null;
            def = String.Empty;
            dynamic = false;
            dynamicSpecified = false;
            max = 0;
            min = 0;
            maxSpecified = false;
            minSpecified = false;
            opt = "";
            range = "";
            reqReboot = false;
            reqRebootSpecified = false;
            size = 0;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="val">拷贝构造对象</param>
        public Capabilities(Capabilities val)
        {
            this.AnyAttr = val.AnyAttr;
            this.def = val.def;
            this.defSpecified = val.defSpecified;
            this.dynamic = val.dynamic;
            this.dynamicSpecified = val.dynamicSpecified;
            this.maxSpecified = val.maxSpecified;
            this.max = val.max;
            this.min = val.min;
            this.minSpecified = val.minSpecified;
            this.opt = val.opt;
            this.optSpecified = val.optSpecified;
            this.range = val.range;
            this.rangeSpecified = val.rangeSpecified;
            this.reqReboot = val.reqReboot;
            this.reqRebootSpecified = val.reqRebootSpecified;
            this.size = val.size;
            this.sizeSpecified = val.sizeSpecified;
        }
        /// <summary>
        /// Any属性
        /// </summary>
        [XmlAnyAttributeAttribute()]
        public XmlAttribute[] AnyAttr { set; get; }
        /// <summary>
        /// 默认值
        /// </summary>
        [XmlAttributeAttribute()]
        public String def { set; get; }
        /// <summary>
        /// 是否包含def XML属性
        /// </summary>
        [XmlIgnoreAttribute()]//not exist in PSIA demo
        public Boolean defSpecified { set; get; }
        /// <summary>
        /// 动态
        /// </summary>
        [XmlIgnore()]
        public Boolean dynamic { set; get; }
        /// <summary>
        /// 是否包含dynamic XML属性
        /// </summary>
        [XmlIgnoreAttribute()]
        public Boolean dynamicSpecified { set; get; }
        /// <summary>
        /// 最大值
        /// </summary>
        [XmlAttribute()]
        public Double max { set; get; }
        /// <summary>
        /// 是否包含max XML属性
        /// </summary>
        [XmlIgnoreAttribute()]
        public Boolean maxSpecified { set; get; }
        /// <summary>
        /// 最小值
        /// </summary>
        [XmlAttribute()]
        public Double min { set; get; }
        /// <summary>
        /// 是否包含min XML属性
        /// </summary>
        [XmlIgnoreAttribute()]
        public Boolean minSpecified { set; get; }
        /// <summary>
        /// 可能的数值选项
        /// </summary>
        [XmlAttribute()] //RegexExpression "([^,])+(,(([^,])+))*"
        public String opt { set; get; }
        /// <summary>
        /// 是否包含opt XML属性
        /// </summary>
        [XmlIgnoreAttribute()]//not exist in PSIA demo
        public Boolean optSpecified { set; get; }
        /// <summary>
        /// 数值范围
        /// </summary>
        [XmlAttribute()]//RegexExpression "(([0-9])+(~([0-9])+)?)(,(([0-9])+(~([0-9])+)?))*"
        public String range { set; get; }
        /// <summary>
        /// 是否包含range XML属性
        /// </summary>
        [XmlIgnoreAttribute()]//not exist in PSIA demo
        public Boolean rangeSpecified { set; get; }
        /// <summary>
        /// 设置属性是否需要重启
        /// </summary>
        [XmlAttribute()]
        public Boolean reqReboot { set; get; }
        /// <summary>
        /// 是否包含reqReboot XML属性
        /// </summary>
        [XmlIgnoreAttribute()]
        public Boolean reqRebootSpecified { set; get; }
        /// <summary>
        /// size
        /// </summary>
        [XmlAttribute()]//not exist in PSIA demo
        public Int32 size { set; get; }
        /// <summary>
        /// 是否包含size XML属性
        /// </summary>
        [XmlIgnoreAttribute()]
        public Boolean sizeSpecified { set; get; }
        /// <summary>
        /// 数值 
        /// </summary>
        /// <remarks>如果没有给出选项则返回null，否则返回字符串数组。</remarks>
        [XmlIgnoreAttribute()]
        public virtual String[] options
        {
            get
            {
                if (optSpecified == true)
                {
                    return opt.Split(options_splits);
                }
                return null;
            }
            set
            {
                if (value == null || value.Length == 0)
                {
                    optSpecified = false;
                    opt = null;
                }
                else
                {
                    Int32 i = 0;
                    foreach (String item in value)
                    {
                        if (i == 0)
                            opt = item;
                        else
                            opt += "," + item;
                        ++i;
                    }
                    optSpecified = true;
                }
            }
        }
    }
}
