using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jess.DotNet.Extension
{
    public static class StringExtensions
    {
        /// <summary>
        /// 字符串转换为指定长度字符数组：不足自动补齐；超出则自动截取
        /// </summary>
        /// <param name="sourceStr"></param>
        /// <param name="targetLength"></param>
        /// <returns></returns>
        public static char[] ToCharArray(string sourceStr, int targetLength)
        {
            char[] tmpchar = new char[targetLength];

            var copylength = sourceStr.Length < targetLength ? sourceStr.Length : targetLength;
            var tmparray = sourceStr.ToCharArray();
            Array.Copy(tmparray, tmpchar, copylength);

            return tmpchar;
        }
    }
}
