using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Jess.DotNet.Extension
{
    public static class DoubleExtensions
    {
        public static bool Between(this Double current, double begin, double end, bool includeBegin = false, bool includeEnd = false)
        {
            if (includeBegin)
            {
                if (current < begin)
                    return false;
            }
            else
            {
                if (current <= begin)
                    return false;
            }

            if (includeEnd)
            {
                if (current > end)
                    return false;
            }
            else
            {
                if (current >= end)
                    return false;
            }

            return true;
        }

        public static bool BetweenInclude(this Double current, double begin, double end)
        {
            return current.Between(begin, end, true, true);
        }

        /// <summary>
        /// double大小端转换
        /// 利用long类型转换：都占位8个字节
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public static double DoubleHostToNetworkOrder(this double host)
        {
            var bd = BitConverter.GetBytes(host);
            long ld = BitConverter.ToInt64(bd, 0);
            var netld = IPAddress.HostToNetworkOrder(ld);
            var netbd = BitConverter.GetBytes(netld);
            return BitConverter.ToDouble(netbd, 0);
        }

    }
}
