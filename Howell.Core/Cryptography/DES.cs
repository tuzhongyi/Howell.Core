using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Howell.Cryptography
{
    /// <summary>
    /// DES 对称加密算法
    /// </summary>
    public class DES
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="rgbKey">DES加密算法密钥</param>
        /// <param name="rgbIV">DES加密算法的初始化向量</param>
        public DES(Byte[] rgbKey, Byte[] rgbIV)
        {
            this.Key = rgbKey;
            this.IV = rgbIV;
        }
        /// <summary>
        /// DES加密算法密钥
        /// </summary>
        Byte[] Key { get; set; }
        /// <summary>
        /// DES加密算法的初始化向量 (8字节)
        /// </summary>
        Byte[] IV { get; set; }
        /// <summary>
        /// 加密成字节字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Encrypt(string value)
        {
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            cryptoProvider.Padding = PaddingMode.Zeros;
            cryptoProvider.Mode = CipherMode.CBC;
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateEncryptor(Key,IV), CryptoStreamMode.Write);
            StreamWriter sw = new StreamWriter(cs);
            sw.Write(value.ToCharArray(), 0, value.Length);
            sw.Flush();
            cs.FlushFinalBlock();
            ms.Flush();
            Byte[] buffer = ms.ToArray();
            String result = String.Empty;
            for (Int32 i = 0; i < buffer.Length; ++i)
            {
                result += buffer[i].ToString("X2");
            }
            return result;
        }
        /// <summary>
        /// 解密字节字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Decrypt(string value)
        {
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            cryptoProvider.Padding = PaddingMode.Zeros;
            cryptoProvider.Mode = CipherMode.CBC;
            Byte [] buffer = new Byte[value.Length / 2];
            for (Int32 i = 0; i < value.Length / 2; i++)
            {
                buffer[i] = Convert.ToByte(value.Substring(i * 2, 2), 16);
            }
            MemoryStream ms = new MemoryStream(buffer);
            CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateDecryptor(Key, IV), CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }
    }
}
