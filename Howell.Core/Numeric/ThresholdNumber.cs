using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Howell.Numeric
{
    /// <summary>
    /// 阀值数
    /// </summary>
    /// <typeparam name="T">阀值的类型</typeparam>
    public class ThresholdNumber<T>  where T : IComparable<T>
    {
        private T m_InitialValue;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="maxValue">作为阀值的数值, 用于指定数值的最大值</param>
        public ThresholdNumber(T maxValue)
            : this(maxValue, default(T), default(T))
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="maxValue">作为阀值的数值, 用于指定数值的最大值</param>
        /// <param name="minValue">作为阀值的数值, 用于指定数值的最小值</param>
        /// <param name="initialValue">初始值</param>
        public ThresholdNumber(T maxValue, T minValue, T initialValue)
        {
            this.MaxValue = maxValue;
            this.MinValue = minValue;
            m_InitialValue = initialValue;
            Exchange(initialValue);
        }
        /// <summary>
        /// 作为阀值的数值, 用于指定数值的最大值
        /// </summary>
        public T MaxValue { get; private set; }
        /// <summary>
        /// 作为阀值的数值, 用于指定数值的最小值
        /// </summary>
        public T MinValue { get; private set; }
        /// <summary>
        /// 当前数值
        /// </summary>
        public T Value { get; private set; }
        /// <summary>
        /// 重置初始值
        /// </summary>
        public void Reset()
        {
            Exchange(m_InitialValue);
        }
        /// <summary>
        /// 将当前数值设置为指定的值并返回原始值。
        /// </summary>
        /// <param name="value">指定的新数值。</param>
        /// <returns>返回原始值。</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">ThresholdNumber is out of range.</exception>
        public T Exchange(T value)
        {
            T oldValue;
            if (!TryExchange(value,out oldValue))
            {
                throw new ArgumentOutOfRangeException("value", String.Format("ThresholdNumber {0} is not is range[{1}{2}].", value, MinValue, MaxValue));
            }
            return oldValue;
        }
        /// <summary>
        /// 尝试将当前数值设置为指定的值并输出原始值。
        /// </summary>
        /// <param name="value">新的数值</param>
        /// <param name="oldValue">原始值</param>
        /// <returns>如果尝试成功则返回true，否则返回false。</returns>
        public Boolean TryExchange(T value, out T oldValue)
        {
            oldValue = this.Value;
            if (value.CompareTo(MaxValue) > 0 || value.CompareTo(MinValue) < 0)
            {
                return false;
            }
            else
            {
                this.Value = value;
                return true;
            }
        }
    }
    /// <summary>
    /// ThresholdNumber泛型扩展方法
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class ThresholdNumberExtensions
    {
        #region Increment
        /// <summary>
        ///  增量数值，如果成功返回增量后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <returns>返回增量后的数值</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">ThresholdNumber is out of range.</exception>
        public static Int64 Increment(this ThresholdNumber<Int64> number)
        {
            return number.Exchange(number.Value + 1) + 1;
        }
        /// <summary>
        ///  增量数值，如果成功返回增量后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <returns>返回增量后的数值</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">ThresholdNumber is out of range.</exception>
        public static UInt64 Increment(this ThresholdNumber<UInt64> number)
        {
            return number.Exchange(number.Value + 1) + 1;
        }
        /// <summary>
        ///  增量数值，如果成功返回增量后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <returns>返回增量后的数值</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">ThresholdNumber is out of range.</exception>
        public static Int32 Increment(this ThresholdNumber<Int32> number)
        {
            return number.Exchange(number.Value + 1)+ 1;
        }
        /// <summary>
        ///  增量数值，如果成功返回增量后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <returns>返回增量后的数值</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">ThresholdNumber is out of range.</exception>
        public static UInt32 Increment(this ThresholdNumber<UInt32> number)
        {
            return number.Exchange(number.Value + 1)+ 1;
        }
        /// <summary>
        ///  增量数值，如果成功返回增量后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <returns>返回增量后的数值</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">ThresholdNumber is out of range.</exception>
        public static Int16 Increment(this ThresholdNumber<Int16> number)
        {
            return Convert.ToInt16(number.Exchange(Convert.ToInt16(number.Value + 1)) + 1);
        }
        /// <summary>
        ///  增量数值，如果成功返回增量后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <returns>返回增量后的数值</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">ThresholdNumber is out of range.</exception>
        public static UInt16 Increment(this ThresholdNumber<UInt16> number)
        {
            return Convert.ToUInt16(number.Exchange(Convert.ToUInt16(number.Value + 1)) + 1);
        }
        /// <summary>
        ///  增量数值，如果成功返回增量后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <returns>返回增量后的数值</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">ThresholdNumber is out of range.</exception>
        public static Byte Increment(this ThresholdNumber<Byte> number)
        {
            return Convert.ToByte(number.Exchange(Convert.ToByte(number.Value + 1)) + 1);
        }
        /// <summary>
        /// 增量数值，如果成功返回增量后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <returns>返回增量后的数值</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">ThresholdNumber is out of range.</exception>
        public static Double Increment(this ThresholdNumber<Double> number)
        {
            return number.Exchange(number.Value + 1) + 1;
        }      
        /// <summary>
        /// 增量数值，如果成功返回增量后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <returns>返回增量后的数值</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">ThresholdNumber is out of range.</exception>
        public static Single Increment(this ThresholdNumber<Single> number)
        {
            return number.Exchange(number.Value + 1) + 1;
        }
        #endregion
        #region TryIncrement
        /// <summary>
        ///  尝试增量数值，如果成功输出增量后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">输出增量后的数值</param>
        /// <returns>返回成功则返回true，否则返回false。</returns>
        public static Boolean TryIncrement(this ThresholdNumber<Int64> number,out Int64 value)
        {
            Int64 oldValue;
            if(number.TryExchange(number.Value + 1, out oldValue) == false)
            {
                value = oldValue;
                return false;
            }
            else
            {
                value = oldValue + 1;
                return true;
            }
        }
        /// <summary>
        ///  尝试增量数值，如果成功输出增量后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">输出增量后的数值</param>
        /// <returns>返回成功则返回true，否则返回false。</returns>
        public static Boolean TryIncrement(this ThresholdNumber<UInt64> number, out UInt64 value)
        {
            UInt64 oldValue;
            if (number.TryExchange(number.Value + 1, out oldValue) == false)
            {
                value = oldValue;
                return false;
            }
            else
            {
                value = oldValue + 1;
                return true;
            }
        }
        /// <summary>
        ///  尝试增量数值，如果成功输出增量后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">输出增量后的数值</param>
        /// <returns>返回成功则返回true，否则返回false。</returns>
        public static Boolean TryIncrement(this ThresholdNumber<Int32> number, out Int32 value)
        {
            Int32 oldValue;
            if (number.TryExchange(number.Value + 1, out oldValue) == false)
            {
                value = oldValue;
                return false;
            }
            else
            {
                value = oldValue + 1;
                return true;
            }
        }
        /// <summary>
        ///  尝试增量数值，如果成功输出增量后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">输出增量后的数值</param>
        /// <returns>返回成功则返回true，否则返回false。</returns>
        public static Boolean TryIncrement(this ThresholdNumber<UInt32> number, out UInt32 value)
        {
            UInt32 oldValue;
            if (number.TryExchange(number.Value + 1, out oldValue) == false)
            {
                value = oldValue;
                return false;
            }
            else
            {
                value = oldValue + 1;
                return true;
            }
        }
        /// <summary>
        ///  尝试增量数值，如果成功输出增量后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">输出增量后的数值</param>
        /// <returns>返回成功则返回true，否则返回false。</returns>
        public static Boolean TryIncrement(this ThresholdNumber<Int16> number, out Int16 value)
        {
            Int16 oldValue;
            if (number.TryExchange(Convert.ToInt16(number.Value + 1), out oldValue) == false)
            {
                value = oldValue;
                return false;
            }
            else
            {
                value = Convert.ToInt16(oldValue + 1);
                return true;
            }
        }
        /// <summary>
        ///  尝试增量数值，如果成功输出增量后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">输出增量后的数值</param>
        /// <returns>返回成功则返回true，否则返回false。</returns>
        public static Boolean TryIncrement(this ThresholdNumber<UInt16> number, out UInt16 value)
        {
            UInt16 oldValue;
            if (number.TryExchange(Convert.ToUInt16(number.Value + 1), out oldValue) == false)
            {
                value = oldValue;
                return false;
            }
            else
            {
                value = Convert.ToUInt16(oldValue + 1);
                return true;
            }
        }
        /// <summary>
        ///  尝试增量数值，如果成功输出增量后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">输出增量后的数值</param>
        /// <returns>返回成功则返回true，否则返回false。</returns>
        public static Boolean TryIncrement(this ThresholdNumber<Byte> number, out Byte value)
        {
            Byte oldValue;
            if (number.TryExchange(Convert.ToByte(number.Value + 1), out oldValue) == false)
            {
                value = oldValue;
                return false;
            }
            else
            {
                value = Convert.ToByte(oldValue + 1);
                return true;
            }
        }
        /// <summary>
        ///  尝试增量数值，如果成功输出增量后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">输出增量后的数值</param>
        /// <returns>返回成功则返回true，否则返回false。</returns>
        public static Boolean TryIncrement(this ThresholdNumber<Double> number, out Double value)
        {
            Double oldValue;
            if (number.TryExchange(number.Value + 1, out oldValue) == false)
            {
                value = oldValue;
                return false;
            }
            else
            {
                value = oldValue + 1;
                return true;
            }
        }
        /// <summary>
        ///  尝试增量数值，如果成功输出增量后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">输出增量后的数值</param>
        /// <returns>返回成功则返回true，否则返回false。</returns>
        public static Boolean TryIncrement(this ThresholdNumber<Single> number, out Single value)
        {
            Single oldValue;
            if (number.TryExchange(number.Value + 1, out oldValue) == false)
            {
                value = oldValue;
                return false;
            }
            else
            {
                value = oldValue + 1;
                return true;
            }
        }
        #endregion
        #region Decrement
        /// <summary>
        ///  减量数值，如果成功返回减量后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <returns>返回减量后的数值</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">ThresholdNumber is out of range.</exception>
        public static Int64 Decrement(this ThresholdNumber<Int64> number)
        {
            return number.Exchange(number.Value - 1) - 1;
        }
        /// <summary>
        ///  减量数值，如果成功返回减量后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <returns>返回减量后的数值</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">ThresholdNumber is out of range.</exception>
        public static UInt64 Decrement(this ThresholdNumber<UInt64> number)
        {
            return number.Exchange(number.Value - 1) - 1;
        }
        /// <summary>
        ///  减量数值，如果成功返回减量后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <returns>返回减量后的数值</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">ThresholdNumber is out of range.</exception>
        public static Int32 Decrement(this ThresholdNumber<Int32> number)
        {
            return number.Exchange(number.Value - 1) - 1;
        }
        /// <summary>
        ///  减量数值，如果成功返回减量后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <returns>返回减量后的数值</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">ThresholdNumber is out of range.</exception>
        public static UInt32 Decrement(this ThresholdNumber<UInt32> number)
        {
            return number.Exchange(number.Value - 1) - 1;
        }
        /// <summary>
        ///  减量数值，如果成功返回减量后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <returns>返回减量后的数值</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">ThresholdNumber is out of range.</exception>
        public static Int16 Decrement(this ThresholdNumber<Int16> number)
        {
            return Convert.ToInt16(number.Exchange(Convert.ToInt16(number.Value - 1)) - 1);
        }
        /// <summary>
        ///  减量数值，如果成功返回减量后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <returns>返回减量后的数值</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">ThresholdNumber is out of range.</exception>
        public static UInt16 Decrement(this ThresholdNumber<UInt16> number)
        {
            return Convert.ToUInt16(number.Exchange(Convert.ToUInt16(number.Value - 1)) - 1);
        }
        /// <summary>
        ///  减量数值，如果成功返回减量后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <returns>返回减量后的数值</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">ThresholdNumber is out of range.</exception>
        public static Byte Decrement(this ThresholdNumber<Byte> number)
        {
            return Convert.ToByte(number.Exchange(Convert.ToByte(number.Value - 1)) - 1);
        }
        /// <summary>
        /// 减量数值，如果成功返回减量后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <returns>返回减量后的数值</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">ThresholdNumber is out of range.</exception>
        public static Double Decrement(this ThresholdNumber<Double> number)
        {
            return number.Exchange(number.Value - 1) - 1;
        }
        /// <summary>
        /// 减量数值，如果成功返回减量后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <returns>返回减量后的数值</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">ThresholdNumber is out of range.</exception>
        public static Single Decrement(this ThresholdNumber<Single> number)
        {
            return number.Exchange(number.Value - 1) - 1;
        }        
        #endregion
        #region TryDecrement
        /// <summary>
        ///  尝试减量数值，如果成功输出减量后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">输出减量后的数值</param>
        /// <returns>返回成功则返回true，否则返回false。</returns>
        public static Boolean TryDecrement(this ThresholdNumber<Int64> number, out Int64 value)
        {
            Int64 oldValue;
            if (number.TryExchange(number.Value - 1, out oldValue) == false)
            {
                value = oldValue;
                return false;
            }
            else
            {
                value = oldValue - 1;
                return true;
            }
        }
        /// <summary>
        ///  尝试减量数值，如果成功输出减量后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">输出减量后的数值</param>
        /// <returns>返回成功则返回true，否则返回false。</returns>
        public static Boolean TryDecrement(this ThresholdNumber<UInt64> number, out UInt64 value)
        {
            UInt64 oldValue;
            if (number.TryExchange(number.Value - 1, out oldValue) == false)
            {
                value = oldValue;
                return false;
            }
            else
            {
                value = oldValue - 1;
                return true;
            }
        }
        /// <summary>
        ///  尝试减量数值，如果成功输出减量后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">输出减量后的数值</param>
        /// <returns>返回成功则返回true，否则返回false。</returns>
        public static Boolean TryDecrement(this ThresholdNumber<Int32> number, out Int32 value)
        {
            Int32 oldValue;
            if (number.TryExchange(number.Value - 1, out oldValue) == false)
            {
                value = oldValue;
                return false;
            }
            else
            {
                value = oldValue - 1;
                return true;
            }
        }
        /// <summary>
        ///  尝试减量数值，如果成功输出减量后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">输出减量后的数值</param>
        /// <returns>返回成功则返回true，否则返回false。</returns>
        public static Boolean TryDecrement(this ThresholdNumber<UInt32> number, out UInt32 value)
        {
            UInt32 oldValue;
            if (number.TryExchange(number.Value - 1, out oldValue) == false)
            {
                value = oldValue;
                return false;
            }
            else
            {
                value = oldValue - 1;
                return true;
            }
        }
        /// <summary>
        ///  尝试减量数值，如果成功输出减量后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">输出减量后的数值</param>
        /// <returns>返回成功则返回true，否则返回false。</returns>
        public static Boolean TryDecrement(this ThresholdNumber<Int16> number, out Int16 value)
        {
            Int16 oldValue;
            if (number.TryExchange(Convert.ToInt16(number.Value - 1), out oldValue) == false)
            {
                value = oldValue;
                return false;
            }
            else
            {
                value = Convert.ToInt16(oldValue - 1);
                return true;
            }
        }
        /// <summary>
        ///  尝试减量数值，如果成功输出减量后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">输出减量后的数值</param>
        /// <returns>返回成功则返回true，否则返回false。</returns>
        public static Boolean TryDecrement(this ThresholdNumber<UInt16> number, out UInt16 value)
        {
            UInt16 oldValue;
            if (number.TryExchange(Convert.ToUInt16(number.Value - 1), out oldValue) == false)
            {
                value = oldValue;
                return false;
            }
            else
            {
                value = Convert.ToUInt16(oldValue - 1);
                return true;
            }
        }
        /// <summary>
        ///  尝试减量数值，如果成功输出减量后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">输出减量后的数值</param>
        /// <returns>返回成功则返回true，否则返回false。</returns>
        public static Boolean TryDecrement(this ThresholdNumber<Byte> number, out Byte value)
        {
            Byte oldValue;
            if (number.TryExchange(Convert.ToByte(number.Value - 1), out oldValue) == false)
            {
                value = oldValue;
                return false;
            }
            else
            {
                value = Convert.ToByte(oldValue - 1);
                return true;
            }
        }
        /// <summary>
        ///  尝试减量数值，如果成功输出减量后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">输出减量后的数值</param>
        /// <returns>返回成功则返回true，否则返回false。</returns>
        public static Boolean TryDecrement(this ThresholdNumber<Double> number, out Double value)
        {
            Double oldValue;
            if (number.TryExchange(number.Value - 1, out oldValue) == false)
            {
                value = oldValue;
                return false;
            }
            else
            {
                value = oldValue - 1;
                return true;
            }
        }
        /// <summary>
        ///  尝试减量数值，如果成功输出减量后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">输出减量后的数值</param>
        /// <returns>返回成功则返回true，否则返回false。</returns>
        public static Boolean TryDecrement(this ThresholdNumber<Single> number, out Single value)
        {
            Single oldValue;
            if (number.TryExchange(number.Value - 1, out oldValue) == false)
            {
                value = oldValue;
                return false;
            }
            else
            {
                value = oldValue - 1;
                return true;
            }
        }
        #endregion
        #region Add
        /// <summary>
        /// 加值，如果成功返回计算后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">要增加的差量数值。</param>
        /// <returns>返回计算后的新数值。</returns>
        public static Int64 Add(this ThresholdNumber<Int64> number, Int64 value)
        {
            return number.Exchange(number.Value + value) + value;
        } 
        /// <summary>
        /// 加值，如果成功返回计算后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">要增加的差量数值。</param>
        /// <returns>返回计算后的新数值。</returns>
        public static UInt64 Add(this ThresholdNumber<UInt64> number, UInt64 value)
        {
            return number.Exchange(number.Value + value) + value;
        }
        /// <summary>
        /// 加值，如果成功返回计算后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">要增加的差量数值。</param>
        /// <returns>返回计算后的新数值。</returns>
        public static Int32 Add(this ThresholdNumber<Int32> number, Int32 value)
        {
            return number.Exchange(number.Value + value) + value;
        }
        /// <summary>
        /// 加值，如果成功返回计算后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">要增加的差量数值。</param>
        /// <returns>返回计算后的新数值。</returns>
        public static UInt32 Add(this ThresholdNumber<UInt32> number, UInt32 value)
        {
            return number.Exchange(number.Value + value) + value;
        }
        /// <summary>
        /// 加值，如果成功返回计算后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">要增加的差量数值。</param>
        /// <returns>返回计算后的新数值。</returns>
        public static Int16 Add(this ThresholdNumber<Int16> number, Int16 value)
        {
            return Convert.ToInt16(number.Exchange(Convert.ToInt16(number.Value + value)) + value);
        }
        /// <summary>
        /// 加值，如果成功返回计算后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">要增加的差量数值。</param>
        /// <returns>返回计算后的新数值。</returns>
        public static UInt16 Add(this ThresholdNumber<UInt16> number, UInt16 value)
        {
            return Convert.ToUInt16(number.Exchange(Convert.ToUInt16(number.Value + value)) + value);
        }
        /// <summary>
        /// 加值，如果成功返回计算后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">要增加的差量数值。</param>
        /// <returns>返回计算后的新数值。</returns>
        public static Byte Add(this ThresholdNumber<Byte> number, Byte value)
        {
            return Convert.ToByte(number.Exchange(Convert.ToByte(number.Value + value)) + value);
        }
        /// <summary>
        /// 加值，如果成功返回计算后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">要增加的差量数值。</param>
        /// <returns>返回计算后的新数值。</returns>
        public static Double Add(this ThresholdNumber<Double> number, Double value)
        {
            return number.Exchange(number.Value + value) + value;
        }
        /// <summary>
        /// 加值，如果成功返回计算后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">要增加的差量数值。</param>
        /// <returns>返回计算后的新数值。</returns>
        public static Single Add(this ThresholdNumber<Single> number, Single value)
        {
            return number.Exchange(number.Value + value) + value;
        }

        #endregion
        #region TryAdd
        /// <summary>
        /// 加值，如果成功输出计算后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">要增加的差量数值。</param>
        /// <param name="newValue">输出增加后的数值。</param>
        /// <returns>如果成功返回true，否则返回false。</returns>
        public static Boolean TryAdd(this ThresholdNumber<Int64> number, Int64 value,out Int64 newValue)
        {
            Int64 oldValue;
            if (number.TryExchange(number.Value + value, out oldValue) == false)
            {
                newValue = oldValue;
                return false;
            }
            else
            {
                newValue = oldValue + value;
                return true;
            }
        }
        /// <summary>
        /// 加值，如果成功输出计算后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">要增加的差量数值。</param>
        /// <param name="newValue">输出增加后的数值。</param>
        /// <returns>如果成功返回true，否则返回false。</returns>
        public static Boolean TryAdd(this ThresholdNumber<UInt64> number, UInt64 value, out UInt64 newValue)
        {
            UInt64 oldValue;
            if (number.TryExchange(number.Value + value, out oldValue) == false)
            {
                newValue = oldValue;
                return false;
            }
            else
            {
                newValue = oldValue + value;
                return true;
            }
        }
        /// <summary>
        /// 加值，如果成功输出计算后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">要增加的差量数值。</param>
        /// <param name="newValue">输出增加后的数值。</param>
        /// <returns>如果成功返回true，否则返回false。</returns>
        public static Boolean TryAdd(this ThresholdNumber<Int32> number, Int32 value, out Int32 newValue)
        {
            Int32 oldValue;
            if (number.TryExchange(number.Value + value, out oldValue) == false)
            {
                newValue = oldValue;
                return false;
            }
            else
            {
                newValue = oldValue + value;
                return true;
            }
        }
        /// <summary>
        /// 加值，如果成功输出计算后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">要增加的差量数值。</param>
        /// <param name="newValue">输出增加后的数值。</param>
        /// <returns>如果成功返回true，否则返回false。</returns>
        public static Boolean TryAdd(this ThresholdNumber<UInt32> number, UInt32 value, out UInt32 newValue)
        {
            UInt32 oldValue;
            if (number.TryExchange(number.Value + value, out oldValue) == false)
            {
                newValue = oldValue;
                return false;
            }
            else
            {
                newValue = oldValue + value;
                return true;
            }
        }
        /// <summary>
        /// 加值，如果成功输出计算后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">要增加的差量数值。</param>
        /// <param name="newValue">输出增加后的数值。</param>
        /// <returns>如果成功返回true，否则返回false。</returns>
        public static Boolean TryAdd(this ThresholdNumber<Int16> number, Int16 value, out Int16 newValue)
        {
            Int16 oldValue;
            if (number.TryExchange(Convert.ToInt16(number.Value + value), out oldValue) == false)
            {
                newValue = oldValue;
                return false;
            }
            else
            {
                newValue = Convert.ToInt16(oldValue + value);
                return true;
            }
        }
        /// <summary>
        /// 加值，如果成功输出计算后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">要增加的差量数值。</param>
        /// <param name="newValue">输出增加后的数值。</param>
        /// <returns>如果成功返回true，否则返回false。</returns>
        public static Boolean TryAdd(this ThresholdNumber<UInt16> number, UInt16 value, out UInt16 newValue)
        {
            UInt16 oldValue;
            if (number.TryExchange(Convert.ToUInt16(number.Value + value), out oldValue) == false)
            {
                newValue = oldValue;
                return false;
            }
            else
            {
                newValue = Convert.ToUInt16(oldValue + value);
                return true;
            }
        }
        /// <summary>
        /// 加值，如果成功输出计算后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">要增加的差量数值。</param>
        /// <param name="newValue">输出增加后的数值。</param>
        /// <returns>如果成功返回true，否则返回false。</returns>
        public static Boolean TryAdd(this ThresholdNumber<Byte> number, Byte value, out Byte newValue)
        {
            Byte oldValue;
            if (number.TryExchange(Convert.ToByte(number.Value + value), out oldValue) == false)
            {
                newValue = oldValue;
                return false;
            }
            else
            {
                newValue = Convert.ToByte(oldValue + value);
                return true;
            }
        }
        /// <summary>
        /// 加值，如果成功输出计算后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">要增加的差量数值。</param>
        /// <param name="newValue">输出增加后的数值。</param>
        /// <returns>如果成功返回true，否则返回false。</returns>
        public static Boolean TryAdd(this ThresholdNumber<Double> number, Double value, out Double newValue)
        {
            Double oldValue;
            if (number.TryExchange(number.Value + value, out oldValue) == false)
            {
                newValue = oldValue;
                return false;
            }
            else
            {
                newValue = oldValue + value;
                return true;
            }
        }
        /// <summary>
        /// 加值，如果成功输出计算后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">要增加的差量数值。</param>
        /// <param name="newValue">输出增加后的数值。</param>
        /// <returns>如果成功返回true，否则返回false。</returns>
        public static Boolean TryAdd(this ThresholdNumber<Single> number, Single value, out Single newValue)
        {
            Single oldValue;
            if (number.TryExchange(number.Value + value, out oldValue) == false)
            {
                newValue = oldValue;
                return false;
            }
            else
            {
                newValue = oldValue + value;
                return true;
            }
        }
        #endregion
        #region Subtract
        /// <summary>
        /// 减值，如果成功返回计算后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">要减的差量数值。</param>
        /// <returns>返回计算后的新数值。</returns>
        public static Int64 Subtract(this ThresholdNumber<Int64> number, Int64 value)
        {
            return number.Exchange(number.Value - value) - value;
        }
        /// <summary>
        /// 减值，如果成功返回计算后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">要减的差量数值。</param>
        /// <returns>返回计算后的新数值。</returns>
        public static UInt64 Subtract(this ThresholdNumber<UInt64> number, UInt64 value)
        {
            return number.Exchange(number.Value - value) - value;
        }
        /// <summary>
        /// 减值，如果成功返回计算后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">要减的差量数值。</param>
        /// <returns>返回计算后的新数值。</returns>
        public static Int32 Subtract(this ThresholdNumber<Int32> number, Int32 value)
        {
            return number.Exchange(number.Value - value) - value;
        }
        /// <summary>
        /// 减值，如果成功返回计算后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">要减的差量数值。</param>
        /// <returns>返回计算后的新数值。</returns>
        public static UInt32 Subtract(this ThresholdNumber<UInt32> number, UInt32 value)
        {
            return number.Exchange(number.Value - value) - value;
        }
        /// <summary>
        /// 减值，如果成功返回计算后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">要减的差量数值。</param>
        /// <returns>返回计算后的新数值。</returns>
        public static Int16 Subtract(this ThresholdNumber<Int16> number, Int16 value)
        {
            return Convert.ToInt16(number.Exchange(Convert.ToInt16(number.Value - value)) - value);
        }
        /// <summary>
        /// 减值，如果成功返回计算后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">要减的差量数值。</param>
        /// <returns>返回计算后的新数值。</returns>
        public static UInt16 Subtract(this ThresholdNumber<UInt16> number, UInt16 value)
        {
            return Convert.ToUInt16(number.Exchange(Convert.ToUInt16(number.Value - value)) - value);
        }
        /// <summary>
        /// 减值，如果成功返回计算后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">要减的差量数值。</param>
        /// <returns>返回计算后的新数值。</returns>
        public static Byte Subtract(this ThresholdNumber<Byte> number, Byte value)
        {
            return Convert.ToByte(number.Exchange(Convert.ToByte(number.Value - value)) - value);
        }
        /// <summary>
        /// 减值，如果成功返回计算后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">要减的差量数值。</param>
        /// <returns>返回计算后的新数值。</returns>
        public static Double Subtract(this ThresholdNumber<Double> number, Double value)
        {
            return number.Exchange(number.Value - value) - value;
        }
        /// <summary>
        /// 减值，如果成功返回计算后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">要减的差量数值。</param>
        /// <returns>返回计算后的新数值。</returns>
        public static Single Subtract(this ThresholdNumber<Single> number, Single value)
        {
            return number.Exchange(number.Value - value) - value;
        }
        #endregion
        #region TrySubtract
        /// <summary>
        /// 减值，如果成功输出计算后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">要减的差量数值。</param>
        /// <param name="newValue">输出减后的数值。</param>
        /// <returns>如果成功返回true，否则返回false。</returns>
        public static Boolean TrySubtract(this ThresholdNumber<Int64> number, Int64 value, out Int64 newValue)
        {
            Int64 oldValue;
            if (number.TryExchange(number.Value - value, out oldValue) == false)
            {
                newValue = oldValue;
                return false;
            }
            else
            {
                newValue = oldValue - value;
                return true;
            }
        }
        /// <summary>
        /// 减值，如果成功输出计算后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">要减的差量数值。</param>
        /// <param name="newValue">输出减后的数值。</param>
        /// <returns>如果成功返回true，否则返回false。</returns>
        public static Boolean TrySubtract(this ThresholdNumber<UInt64> number, UInt64 value, out UInt64 newValue)
        {
            UInt64 oldValue;
            if (number.TryExchange(number.Value - value, out oldValue) == false)
            {
                newValue = oldValue;
                return false;
            }
            else
            {
                newValue = oldValue - value;
                return true;
            }
        }
        /// <summary>
        /// 减值，如果成功输出计算后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">要减的差量数值。</param>
        /// <param name="newValue">输出减后的数值。</param>
        /// <returns>如果成功返回true，否则返回false。</returns>
        public static Boolean TrySubtract(this ThresholdNumber<Int32> number, Int32 value, out Int32 newValue)
        {
            Int32 oldValue;
            if (number.TryExchange(number.Value - value, out oldValue) == false)
            {
                newValue = oldValue;
                return false;
            }
            else
            {
                newValue = oldValue - value;
                return true;
            }
        }
        /// <summary>
        /// 减值，如果成功输出计算后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">要减的差量数值。</param>
        /// <param name="newValue">输出减后的数值。</param>
        /// <returns>如果成功返回true，否则返回false。</returns>
        public static Boolean TrySubtract(this ThresholdNumber<UInt32> number, UInt32 value, out UInt32 newValue)
        {
            UInt32 oldValue;
            if (number.TryExchange(number.Value - value, out oldValue) == false)
            {
                newValue = oldValue;
                return false;
            }
            else
            {
                newValue = oldValue - value;
                return true;
            }
        }
        /// <summary>
        /// 减值，如果成功输出计算后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">要减的差量数值。</param>
        /// <param name="newValue">输出减后的数值。</param>
        /// <returns>如果成功返回true，否则返回false。</returns>
        public static Boolean TrySubtract(this ThresholdNumber<Int16> number, Int16 value, out Int16 newValue)
        {
            Int16 oldValue;
            if (number.TryExchange(Convert.ToInt16(number.Value - value), out oldValue) == false)
            {
                newValue = oldValue;
                return false;
            }
            else
            {
                newValue = Convert.ToInt16(oldValue - value);
                return true;
            }
        }
        /// <summary>
        /// 减值，如果成功输出计算后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">要减的差量数值。</param>
        /// <param name="newValue">输出减后的数值。</param>
        /// <returns>如果成功返回true，否则返回false。</returns>
        public static Boolean TrySubtract(this ThresholdNumber<UInt16> number, UInt16 value, out UInt16 newValue)
        {
            UInt16 oldValue;
            if (number.TryExchange(Convert.ToUInt16(number.Value - value), out oldValue) == false)
            {
                newValue = oldValue;
                return false;
            }
            else
            {
                newValue = Convert.ToUInt16(oldValue - value);
                return true;
            }
        }
        /// <summary>
        /// 减值，如果成功输出计算后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">要减的差量数值。</param>
        /// <param name="newValue">输出减后的数值。</param>
        /// <returns>如果成功返回true，否则返回false。</returns>
        public static Boolean TrySubtract(this ThresholdNumber<Byte> number, Byte value, out Byte newValue)
        {
            Byte oldValue;
            if (number.TryExchange(Convert.ToByte(number.Value - value), out oldValue) == false)
            {
                newValue = oldValue;
                return false;
            }
            else
            {
                newValue = Convert.ToByte(oldValue - value);
                return true;
            }
        }
        /// <summary>
        /// 减值，如果成功输出计算后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">要减的差量数值。</param>
        /// <param name="newValue">输出减后的数值。</param>
        /// <returns>如果成功返回true，否则返回false。</returns>
        public static Boolean TrySubtract(this ThresholdNumber<Double> number, Double value, out Double newValue)
        {
            Double oldValue;
            if (number.TryExchange(number.Value - value, out oldValue) == false)
            {
                newValue = oldValue;
                return false;
            }
            else
            {
                newValue = oldValue - value;
                return true;
            }
        }
        /// <summary>
        /// 减值，如果成功输出计算后的数值
        /// </summary>
        /// <param name="number">ThresholdNumber对象</param>
        /// <param name="value">要减的差量数值。</param>
        /// <param name="newValue">输出减后的数值。</param>
        /// <returns>如果成功返回true，否则返回false。</returns>
        public static Boolean TrySubtract(this ThresholdNumber<Single> number, Single value, out Single newValue)
        {
            Single oldValue;
            if (number.TryExchange(number.Value - value, out oldValue) == false)
            {
                newValue = oldValue;
                return false;
            }
            else
            {
                newValue = oldValue - value;
                return true;
            }
        }
        #endregion
    }
}
