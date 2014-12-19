using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Howell.Cryptography
{
    /// <summary>
    /// DES 对称加密算法(BASE64字符串)
    /// </summary>
    public class DESBase64
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="rgbKey">DES加密算法密钥</param>
        /// <param name="rgbIV">DES加密算法的初始化向量</param>
        public DESBase64(Byte[] rgbKey, Byte[] rgbIV)
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
        /// 加密成BASE64字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Encrypt(string value)
        {
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            cryptoProvider.Padding = PaddingMode.Zeros;
            cryptoProvider.Mode = CipherMode.CBC;
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateEncryptor(Key, IV), CryptoStreamMode.Write);
            StreamWriter sw = new StreamWriter(cs,Encoding.UTF8);
            sw.Write(value.ToCharArray(), 0, value.Length);
            sw.Flush();
            cs.FlushFinalBlock();
            ms.Flush();
            return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
        }
        /// <summary>
        /// 解密BASE64字符串 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Decrypt(string value)
        {
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            cryptoProvider.Padding = PaddingMode.Zeros;
            cryptoProvider.Mode = CipherMode.CBC;            
            byte[] buffer = Convert.FromBase64String(value);
            MemoryStream ms = new MemoryStream(buffer);
            CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateDecryptor(Key, IV), CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cs, Encoding.UTF8);
            return sr.ReadToEnd();
        }
    }
}
