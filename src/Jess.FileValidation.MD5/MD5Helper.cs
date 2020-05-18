using System;
using System.Collections.Generic;
using System.IO;

namespace Jess.FileValidation.MD5
{
    public sealed class MD5Helper
    {
        /// <summary>
        /// 生成MD5文件信息
        /// 将指定目录下所有文件的md5信息生成为一个文件
        /// </summary>
        /// <param name="rootdir"></param>
        public static void GenerateMD5Info(string rootdir)
        {

        }

        //public string void GenerateMD5Info

        /// <summary>
        /// 获取文件中的md5键值对信息
        /// </summary>
        /// <param name="md5infopath"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetMD5InfoList(string md5infopath)
        {
            return new Dictionary<string, string>();
        }

        /// <summary>
        /// 验证md5
        /// </summary>
        /// <param name="fileext">需要验证的文件后缀</param>
        /// <returns></returns>
        public static bool Validate(string fileext)
        {
            return true;
        }
    }
}
