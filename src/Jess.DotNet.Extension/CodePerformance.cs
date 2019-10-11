using System;
using System.Diagnostics;

namespace Jess.DotNet.Extension
{
    /// <summary>
    /// 代码性能辅助方法
    /// </summary>
    public sealed class CodePerformance
    {

        /// <summary>
        /// 运行时间记录
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static int Time(Action action)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            action();

            stopwatch.Stop();
            TimeSpan timespan = stopwatch.Elapsed;
            return timespan.Milliseconds;
        }
    }
}
