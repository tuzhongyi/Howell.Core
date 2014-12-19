using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Howell.Capability
{
    /// <summary>
    /// 百分比 能力
    /// </summary>
    [Serializable]
    [XmlRootAttribute(Namespace = Constants.Namespace)]
    public class PercentageCap : Int32Cap
    {        
        /// <summary>
        /// 隐式转换
        /// </summary>
        /// <param name="val">转换对象</param>
        /// <returns>返回值</returns>
        public static implicit operator Int32(PercentageCap val)
        {
            return val.Value;
        }
        /// <summary>
        /// 显式转换
        /// </summary>
        /// <param name="val">转换对象</param>
        /// <returns>返回值</returns>
        public static explicit operator PercentageCap(Int32 val)
        {
            return new PercentageCap(val);
        }
        /// <summary>
        /// 创建 PercentageCap对象
        /// </summary>
        public PercentageCap()
            : base()
        {
            maxSpecified = true;
            max = 100;
            minSpecified = true;
            min = 0;
            //rangeSpecified = true;
            //range = PSIAHelper.ToRange(0, 100);
        }
        /// <summary>
        /// 创建 PercentageCap对象
        /// </summary>
        /// <param name="val">数值</param>
        public PercentageCap(Int32 val)
            : base(val)
        {
            maxSpecified = true;
            max = 100;
            minSpecified = true;
            min = 0;      
            //rangeSpecified = true;
            //range = PSIAHelper.ToRange(0, 100);
        }
    }
}
