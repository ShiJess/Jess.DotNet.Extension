using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Jess.DotNet.Extension
{
    /// <summary>
    /// 简易证书生成类
    /// </summary>
    public sealed class SimpleLicenseFile
    {
        /// <summary>
        /// 获取解密后的文件内容——字符串
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>文件内容</returns>
        public string ReadLicFile(string path, string key, string iv = null)
        {
            if (!File.Exists(path))
            {
                throw new Exception("文件不存在!");
            }
            string str = "";

            IFormatter binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                str = (string)binaryFormatter.Deserialize(fileStream);
                fileStream.Close();
            }
            if (string.IsNullOrEmpty(iv))
            {
                return Cryptor.DESDecrypt(str, key);
            }
            return Cryptor.DESDecrypt(str, key, iv);
        }


        /// <summary>
        /// 生成证书文件
        /// </summary>
        /// <param name="data">注册信息</param>
        /// <param name="fileName">证书文件路径</param>
        /// <param name="key"></param>
        public void BuildLicFile(string data, string fileName, string key, string iv = null)
        {
            string str = string.Empty;
            if (string.IsNullOrEmpty(iv))
            {
                str = Cryptor.DESEncrypt(str, key);
            }
            else
            {
                str = Cryptor.DESEncrypt(data, key, iv);
            }

            IFormatter binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            if (str != null)
            {
                using (FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    binaryFormatter.Serialize(fileStream, str);
                    fileStream.Close();
                }
            }
        }

    }
}
