using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Jess.DotNet.Extension
{
    /// <summary>
    /// 加解密处理
    /// </summary>
    public sealed class Cryptor
    {
        #region DES加解密

        /// <summary>
        /// 
        /// 使用常用【微软官方示例值】iv值
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string DESEncrypt(string data, string key)
        {
            byte[] myKey = Encoding.UTF8.GetBytes(key);
            byte[] myIV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            return DESEncrypt(data, myKey, myIV);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key">8位密钥</param>
        /// <param name="iv">8位初始向量</param>
        /// <returns></returns>
        public static string DESEncrypt(string data, string key, string iv)
        {
            byte[] myKey = Encoding.UTF8.GetBytes(key);
            byte[] myIV = Encoding.UTF8.GetBytes(iv);
            return DESEncrypt(data, myKey, myIV);
        }
        

        public static string DESEncrypt(string data, byte[] key, byte[] iv)
        {
            string str = string.Empty;
            if (string.IsNullOrEmpty(data))
            {
                return str;
            }

            using (MemoryStream ms = new MemoryStream())
            {
                DES myProvider = new DESCryptoServiceProvider();
                using (CryptoStream cs = new CryptoStream(ms, myProvider.CreateEncryptor(key, iv), CryptoStreamMode.Write))
                {

                    byte[] bs = Encoding.UTF8.GetBytes(data);
                    cs.Write(bs, 0, bs.Length);
                    cs.FlushFinalBlock();
                    str = Convert.ToBase64String(ms.ToArray());

                }
            }
            return str;
        }


        /// <summary>
        /// 
        /// 使用常用【微软官方示例值】iv值
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string DESDecrypt(string data, string key)
        {
            byte[] myKey = Encoding.UTF8.GetBytes(key);
            byte[] myIV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            return DESDecrypt(data, myKey, myIV);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key">8位密钥</param>
        /// <param name="iv">8位初始向量</param>
        /// <returns></returns>
        public static string DESDecrypt(string data, string key, string iv)
        {
            byte[] myKey = Encoding.UTF8.GetBytes(key);
            byte[] myIV = Encoding.UTF8.GetBytes(iv);
            return DESDecrypt(data, myKey, myIV);
        }
        public static string DESDecrypt(string data, byte[] key, byte[] iv)
        {
            string str = string.Empty;

            if (string.IsNullOrEmpty(data))
            {
                throw new Exception("待加密字符串为空！");
            }

            using (MemoryStream ms = new MemoryStream())
            {

                DES myProvider = new DESCryptoServiceProvider();
                using (CryptoStream cs = new CryptoStream(ms, myProvider.CreateDecryptor(key, iv), CryptoStreamMode.Write))
                {
                    byte[] bs = Convert.FromBase64String(data);
                    cs.Write(bs, 0, bs.Length);
                    cs.FlushFinalBlock();
                    str = Encoding.UTF8.GetString(ms.ToArray());
                }
            }

            return str;
        }

        #endregion

    }

}
