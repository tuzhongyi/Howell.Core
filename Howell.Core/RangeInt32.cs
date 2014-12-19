using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Howell
{
    /// <summary>
    /// 限定分为的Int32值
    /// </summary>
    public class RangeInt32
    {
        private Int32 _value;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        public RangeInt32(Int32 minValue, Int32 maxValue)
        {
            this.MinValue = minValue;
            this.MaxValue = maxValue;
            _value = MinValue;
        }
        
        /// <summary>
        /// 最大值
        /// </summary>
        public Int32 MaxValue { get; private set; }
        /// <summary>
        /// 最小值
        /// </summary>
        public Int32 MinValue { get; private set; }
        /// <summary>
        /// 获取Int32值
        /// </summary>
        /// <returns></returns>
        public Int32 GetValue()
        {
            return _value;
        }
        /// <summary>
        /// 递减指定变量的值并存储结果。
        /// </summary>
        /// <returns>递减的值。</returns>
        public Int32 Decrement()
        {
            Int32 result = _value;
            if (--result < MinValue) result = MaxValue;
            _value = result;
            return result;
        }
        /// <summary>
        /// 递增指定变量的值并存储结果。
        /// </summary>
        /// <returns>递增的值。</returns>
        public Int32 Increment()
        {
            Int32 result = _value;
            if (++result > MaxValue) result = MinValue;
            _value = result;
            return result;
        }
        /// <summary>
        /// 添加两个 32 位整数并用两者的和替换第一个整数。
        /// </summary>
        /// <param name="value">要添加到整数中的 location 位置的值。</param>
        /// <returns>存储在 location 处的新值。</returns>
        public Int32 Add(int value)
        {
            Int32 result = _value;
            result += value;
            if (result > MaxValue) result = MinValue;
            if (result < MinValue) result = MaxValue;
            _value = result;
            return result;
        }
        /// <summary>
        /// 将 32 位有符号整数设置为指定的值并返回原始值。
        /// </summary>
        /// <param name="value">location 参数被设置为的值。</param>
        /// <returns>location1 的原始值。</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">value 的值不再范围内</exception>
        public Int32 Exchange(Int32 value)
        {
            if (value < MinValue || value > MaxValue) throw new ArgumentOutOfRangeException("value", "value is not in range.");
            Int32 result = _value;
            _value = value;
            return result;
        }
        /// <summary>
        /// 隐式转换为Int32类型
        /// </summary>
        /// <param name="rangeInt32"></param>
        /// <returns></returns>
        public static implicit operator Int32(RangeInt32 rangeInt32)
        {
            return rangeInt32._value;
        }
    }
}
