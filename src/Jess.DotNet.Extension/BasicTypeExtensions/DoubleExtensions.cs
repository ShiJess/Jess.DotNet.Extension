using System;
using System.Collections.Generic;
using System.Linq;
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

    }
}
