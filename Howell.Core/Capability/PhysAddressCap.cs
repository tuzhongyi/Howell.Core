using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Howell.Capability
{

    /// <summary>
    /// PhysAddress 能力
    /// </summary>
    [Serializable]
    [XmlRootAttribute(Namespace = Constants.Namespace)]
    public class PhysAddressCap : StringCap
    {
        /// <summary>
        /// 空MAC地址
        /// </summary>
        public static readonly PhysAddressCap Default = new PhysAddressCap("00-00-00-00-00-00");

        /// <summary>
        /// 隐式转换
        /// </summary>
        /// <param name="val">转换对象</param>
        /// <returns>返回值</returns>
        public static implicit operator String(PhysAddressCap val)            
        {            
            return val.Value;
        }
        /// <summary>
        /// 显式转换
        /// </summary>
        /// <param name="val">转换对象</param>
        /// <returns>返回值</returns>
        public static explicit operator PhysAddressCap(String val)
        {
            return new PhysAddressCap(val);
        }
        /// <summary>
        /// 创建 MACCap对象
        /// </summary>
        public PhysAddressCap() 
            : base()         
        {
            TextSpecified = false;
        }
        /// <summary>
        /// 创建 MACCap对象
        /// </summary>
        /// <param name="val">数值</param>
        public PhysAddressCap(String val)
            : base(val)
        {
        }
        /// <summary>
        /// 拷贝构造
        /// </summary>
        /// <param name="val"></param>
        public PhysAddressCap(PhysAddressCap val)
            : base((StringCap)val)
        {
        }
    }
}
