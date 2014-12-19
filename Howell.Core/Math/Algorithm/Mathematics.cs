using System;
using System.Collections.Generic;
using System.Text;

namespace Howell.Math.Algorithm
{
    /// <summary>
    /// 数学算法
    /// </summary>
    public static class Mathematics
    {
        #region Min
        /// <summary>
        /// 求最小值
        /// </summary>
        /// <param name="values">数值</param>
        /// <returns>返回最小值</returns>
        public static SByte Min(params SByte[] values)
        {
            SByte result = SByte.MaxValue;
            foreach (SByte value in values)
            {
                result = System.Math.Min(value, result);
            }
            return result;
        }
        /// <summary>
        /// 求最小值
        /// </summary>
        /// <param name="values">数值</param>
        /// <returns>返回最小值</returns>
        public static Int16 Min(params Int16[] values)
        {
            Int16 result = Int16.MaxValue;
            foreach (Int16 value in values)
            {
                result = System.Math.Min(value, result);
            }
            return result;
        }
        /// <summary>
        /// 求最小值
        /// </summary>
        /// <param name="values">数值</param>
        /// <returns>返回最小值</returns>
        public static Int32 Min(params Int32[] values)
        {
            Int32 result = Int32.MaxValue;
            foreach (Int32 value in values)
            {
                result = System.Math.Min(value, result);
            }
            return result;
        }
        /// <summary>
        /// 求最小值
        /// </summary>
        /// <param name="values">数值</param>
        /// <returns>返回最小值</returns>
        public static Int64 Min(params Int64[] values)
        {
            Int64 result = Int64.MaxValue;
            foreach (Int64 value in values)
            {
                result = System.Math.Min(value, result);
            }
            return result;
        }

        /// <summary>
        /// 求最小值
        /// </summary>
        /// <param name="values">数值</param>
        /// <returns>返回最小值</returns>
        public static Byte Min(params Byte[] values)
        {
            Byte result = Byte.MaxValue;
            foreach (Byte value in values)
            {
                result = System.Math.Min(value, result);
            }
            return result;
        }
        /// <summary>
        /// 求最小值
        /// </summary>
        /// <param name="values">数值</param>
        /// <returns>返回最小值</returns>
        public static UInt16 Min(params UInt16[] values)
        {
            UInt16 result = UInt16.MaxValue;
            foreach (UInt16 value in values)
            {
                result = System.Math.Min(value, result);
            }
            return result;
        }
        /// <summary>
        /// 求最小值
        /// </summary>
        /// <param name="values">数值</param>
        /// <returns>返回最小值</returns>
        public static UInt32 Min(params UInt32[] values)
        {
            UInt32 result = UInt32.MaxValue;
            foreach (UInt32 value in values)
            {
                result = System.Math.Min(value, result);
            }
            return result;
        }
        /// <summary>
        /// 求最小值
        /// </summary>
        /// <param name="values">数值</param>
        /// <returns>返回最小值</returns>
        public static UInt64 Min(params UInt64[] values)
        {
            UInt64 result = UInt64.MaxValue;
            foreach (UInt64 value in values)
            {
                result = System.Math.Min(value, result);
            }
            return result;
        }
        /// <summary>
        /// 求最小值
        /// </summary>
        /// <param name="values">数值</param>
        /// <returns>返回最小值</returns>
        public static Single Min(params Single[] values)
        {
            Single result = Single.MaxValue;
            foreach (Single value in values)
            {
                result = System.Math.Min(value, result);
            }
            return result;
        }
        /// <summary>
        /// 求最小值
        /// </summary>
        /// <param name="values">数值</param>
        /// <returns>返回最小值</returns>
        public static Decimal Min(params Decimal[] values)
        {
            Decimal result = Decimal.MaxValue;
            foreach (Decimal value in values)
            {
                result = System.Math.Min(value, result);
            }
            return result;
        }
        /// <summary>
        /// 求最小值
        /// </summary>
        /// <param name="values">数值</param>
        /// <returns>返回最小值</returns>
        public static Double Min(params Double[] values)
        {
            Double result = Double.MaxValue;
            foreach (Double value in values)
            {
                result = System.Math.Min(value, result);
            }
            return result;
        }
        #endregion
        #region Max
        /// <summary>
        /// 求最大值
        /// </summary>
        /// <param name="values">数值</param>
        /// <returns>返回最大值</returns>
        public static SByte Max(params SByte[] values)
        {
            SByte result = SByte.MinValue;
            foreach (SByte value in values)
            {
                result = System.Math.Max(value, result);
            }
            return result;
        }
        /// <summary>
        /// 求最大值
        /// </summary>
        /// <param name="values">数值</param>
        /// <returns>返回最大值</returns>
        public static Int16 Max(params Int16[] values)
        {
            Int16 result = Int16.MinValue;
            foreach (Int16 value in values)
            {
                result = System.Math.Max(value, result);
            }
            return result;
        }
        /// <summary>
        /// 求最大值
        /// </summary>
        /// <param name="values">数值</param>
        /// <returns>返回最大值</returns>
        public static Int32 Max(params Int32[] values)
        {
            Int32 result = Int32.MinValue;
            foreach (Int32 value in values)
            {
                result = System.Math.Max(value, result);
            }
            return result;
        }
        /// <summary>
        /// 求最大值
        /// </summary>
        /// <param name="values">数值</param>
        /// <returns>返回最大值</returns>
        public static Int64 Max(params Int64[] values)
        {
            Int64 result = Int64.MinValue;
            foreach (Int64 value in values)
            {
                result = System.Math.Max(value, result);
            }
            return result;
        }

        /// <summary>
        /// 求最大值
        /// </summary>
        /// <param name="values">数值</param>
        /// <returns>返回最大值</returns>
        public static Byte Max(params Byte[] values)
        {
            Byte result = Byte.MinValue;
            foreach (Byte value in values)
            {
                result = System.Math.Max(value, result);
            }
            return result;
        }
        /// <summary>
        /// 求最大值
        /// </summary>
        /// <param name="values">数值</param>
        /// <returns>返回最大值</returns>
        public static UInt16 Max(params UInt16[] values)
        {
            UInt16 result = UInt16.MinValue;
            foreach (UInt16 value in values)
            {
                result = System.Math.Max(value, result);
            }
            return result;
        }
        /// <summary>
        /// 求最大值
        /// </summary>
        /// <param name="values">数值</param>
        /// <returns>返回最大值</returns>
        public static UInt32 Max(params UInt32[] values)
        {
            UInt32 result = UInt32.MinValue;
            foreach (UInt32 value in values)
            {
                result = System.Math.Max(value, result);
            }
            return result;
        }
        /// <summary>
        /// 求最大值
        /// </summary>
        /// <param name="values">数值</param>
        /// <returns>返回最大值</returns>
        public static UInt64 Max(params UInt64[] values)
        {
            UInt64 result = UInt64.MinValue;
            foreach (UInt64 value in values)
            {
                result = System.Math.Max(value, result);
            }
            return result;
        }
        /// <summary>
        /// 求最大值
        /// </summary>
        /// <param name="values">数值</param>
        /// <returns>返回最大值</returns>
        public static Single Max(params Single[] values)
        {
            Single result = Single.MinValue;
            foreach (Single value in values)
            {
                result = System.Math.Max(value, result);
            }
            return result;
        }
        /// <summary>
        /// 求最大值
        /// </summary>
        /// <param name="values">数值</param>
        /// <returns>返回最大值</returns>
        public static Decimal Max(params Decimal[] values)
        {
            Decimal result = Decimal.MinValue;
            foreach (Decimal value in values)
            {
                result = System.Math.Max(value, result);
            }
            return result;
        }
        /// <summary>
        /// 求最大值
        /// </summary>
        /// <param name="values">数值</param>
        /// <returns>返回最大值</returns>
        public static Double Max(params Double[] values)
        {
            Double result = Double.MinValue;
            foreach (Double value in values)
            {
                result = System.Math.Max(value, result);
            }
            return result;
        }
        #endregion
        #region GCD Greatest Common Divisor
        /// <summary>
        /// Greatest Common Divisor 最大公约数
        /// </summary>
        /// <param name="a">数值a</param>
        /// <param name="b">数值b</param>
        /// <returns>返回最大公约数。</returns>
        public static Int32 GCD(Int32 a, Int32 b)
        {            
            if(b == 0) return a;
            else return GCD(b,a%b);
        }
        /// <summary>
        /// Greatest Common Divisor 最大公约数
        /// </summary>
        /// <param name="a">数值a</param>
        /// <param name="b">数值b</param>
        /// <returns>返回最大公约数。</returns>
        public static UInt32 GCD(UInt32 a, UInt32 b)
        {
            if (b == 0) return a;
            else return GCD(b, a % b);
        }
        /// <summary>
        /// Greatest Common Divisor 最大公约数
        /// </summary>
        /// <param name="a">数值a</param>
        /// <param name="b">数值b</param>
        /// <returns>返回最大公约数。</returns>
        public static Int64 GCD(Int64 a, Int64 b)
        {
            if (b == 0) return a;
            else return GCD(b, a % b);
        }
        /// <summary>
        /// Greatest Common Divisor 最大公约数
        /// </summary>
        /// <param name="a">数值a</param>
        /// <param name="b">数值b</param>
        /// <returns>返回最大公约数。</returns>
        public static UInt64 GCD(UInt64 a, UInt64 b)
        {
            if (b == 0) return a;
            else return GCD(b, a % b);
        }
        
        #endregion
        #region LCM lease common multiple
        /*
         LCM(a,b) = (a*b)/GCD(a,b);
        */
        /// <summary>
        /// Lease Common Multiple
        /// </summary>
        /// <param name="a">数值a</param>
        /// <param name="b">数值b</param>
        /// <returns>返回最小公倍数。</returns>
        public static Int32 LCM(Int32 a, Int32 b)
        {
            Int32 gcd = GCD(a, b);
            if (gcd == 0) return 0;
            return (a * b) / gcd;            
        }
        /// <summary>
        /// Lease Common Multiple
        /// </summary>
        /// <param name="a">数值a</param>
        /// <param name="b">数值b</param>
        /// <returns>返回最小公倍数。</returns>
        public static UInt32 LCM(UInt32 a, UInt32 b)
        {
            UInt32 gcd = GCD(a, b);
            if (gcd == 0) return 0;
            return (a * b) / gcd;
        }
        /// <summary>
        /// Lease Common Multiple
        /// </summary>
        /// <param name="a">数值a</param>
        /// <param name="b">数值b</param>
        /// <returns>返回最小公倍数。</returns>
        public static Int64 LCM(Int64 a, Int64 b)
        {
            Int64 gcd = GCD(a, b);
            if (gcd == 0) return 0;
            return (a * b) / gcd;
        }
        /// <summary>
        /// Lease Common Multiple
        /// </summary>
        /// <param name="a">数值a</param>
        /// <param name="b">数值b</param>
        /// <returns>返回最小公倍数。</returns>
        public static UInt64 LCM(UInt64 a, UInt64 b)
        {
            UInt64 gcd = GCD(a, b);
            if (gcd == 0) return 0;
            return (a * b) / gcd;
        }
        #endregion
        #region Factorial
        /// <summary>
        /// 阶乘
        /// </summary>
        /// <param name="value">数值</param>
        /// <returns>返回阶乘结果。</returns>
        public static Double Factorial(Int32 value)
        {
            Double result = 1;
            for (Int32 i = 0; i < value; ++i)
            {
                result *= (value - i);
            }
            return result;
        }
        /// <summary>
        /// 阶乘
        /// </summary>
        /// <param name="value">数值</param>
        /// <returns>返回阶乘结果。</returns>
        public static Double Factorial(UInt32 value)
        {
            Double result = 1;
            for (UInt32 i = 0; i < value; ++i)
            {
                result *= (value - i);
            }
            return result;
        }
        /// <summary>
        /// 阶乘
        /// </summary>
        /// <param name="value">数值</param>
        /// <returns>返回阶乘结果。</returns>
        public static Double Factorial(Int64 value)
        {
            Double result = 1;
            for (Int64 i = 0; i < value; ++i)
            {
                result *= (value - i);
            }
            return result;
        }
        /// <summary>
        /// 阶乘
        /// </summary>
        /// <param name="value">数值</param>
        /// <returns>返回阶乘结果。</returns>
        public static Double Factorial(UInt64 value)
        {
            Double result = 1;
            for (UInt64 i = 0; i < value; ++i)
            {
                result *= (value - i);
            }
            return result;
        }
        #endregion
    }

}
