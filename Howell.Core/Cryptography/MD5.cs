using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Howell.Cryptography
{
    /// <summary>
    /// MD5加密及解码对象
    /// </summary>
    public class MD5 : IDisposable
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public MD5()
        {
        }
        /// <summary>
        /// 加密MD5
        /// </summary>
        /// <param name="input">加密字符串</param>
        /// <returns>返回MD5加密后的数据.</returns>
        public Byte[] Encrypt(String input)
        {
            return Encrypt(Encoding.ASCII.GetBytes(input));
        }
        /// <summary>
        /// 加密MD5
        /// </summary>
        /// <param name="buffer">加密数据.</param>
        /// <returns>返回MD5加密后的数据.</returns>
        public Byte[] Encrypt(Byte[] buffer)
        {
            return Encrypt(buffer, 0, buffer.Length);
        }
        /// <summary>
        /// 加密MD5
        /// </summary>
        /// <param name="buffer">加密数据</param>
        /// <param name="offset">加密数据偏移量</param>
        /// <param name="count">加密数据长度</param>
        /// <returns>返回MD5加密后的数据.</returns>
        public Byte[] Encrypt(Byte[] buffer, Int32 offset, Int32 count)
        {
            using (System.Security.Cryptography.MD5 md5Provider = System.Security.Cryptography.MD5.Create())
            {                
                return md5Provider.ComputeHash(buffer, offset, count);
            }
        }
        /// <summary>
        /// 加密MD5
        /// </summary>
        /// <param name="input">加密字符串</param>
        /// <returns>返回MD5加密后的字符串.</returns>
        public String EncryptToString(String input)
        {
            return new HexadecimalString(Encrypt(input)).ToString();
        }
        /// <summary>
        /// 加密MD5
        /// </summary>
        /// <param name="buffer">加密数据.</param>
        /// <returns>返回MD5加密后的字符串.</returns>
        public String EncryptToString(Byte[] buffer)
        {
            return new HexadecimalString(Encrypt(buffer)).ToString();
        }
        /// <summary>
        /// 加密MD5
        /// </summary>
        /// <param name="buffer">加密数据</param>
        /// <param name="offset">加密数据偏移量</param>
        /// <param name="count">加密数据长度</param>
        /// <returns>返回MD5加密后的字符串.</returns>
        public String EncryptToString(Byte[] buffer, Int32 offset, Int32 count)
        {
            return new HexadecimalString(Encrypt(buffer, offset, count)).ToString();
        }
        /// <summary>
        /// 销毁对象
        /// </summary>
        public void Dispose()
        {
        }

    }
}
