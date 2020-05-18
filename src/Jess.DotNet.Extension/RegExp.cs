using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Jess.DotNet.Extension
{
    /// <summary>
    /// 正则表达式验证
    /// regular expression
    /// </summary>
    public class RegExp
    {
        /// <summary>
        /// 是否是Email地址
        /// </summary>
        /// <param name="verify">待验证的字符串</param>
        /// <returns>验证通过与否</returns>
        public static bool IsEmail(string verify)
        {
            return Regex.IsMatch(verify, @"[\w!#$%&'*+/=?^_`{|}~-]+(?:\.[\w!#$%&'*+/=?^_`{|}~-]+)*@(?:[\w](?:[\w-]*[\w])?\.)+[\w](?:[\w-]*[\w])?");
        }

    }
}
