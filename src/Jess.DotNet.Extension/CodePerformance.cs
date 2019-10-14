using System;
using System.Diagnostics;
using System.Threading.Tasks;

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

        /// <summary>
        /// 异步记录运行时间
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static async Task<int> TimeAsync(Action action)
        {
            Stopwatch stopwatch = new Stopwatch();

#if NET40
            await Task.Factory.StartNew(() =>
            {
                stopwatch.Start();
                action();
                stopwatch.Stop();
            });
#else
            await Task.Run(() =>
            {
                stopwatch.Start();
                action();
                stopwatch.Stop();
            });
#endif

            TimeSpan timespan = stopwatch.Elapsed;
            return timespan.Milliseconds;
        }

        /// <summary>
        /// 计算Task执行时间
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static int Time(Task action)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            action.Wait();

            stopwatch.Stop();
            TimeSpan timespan = stopwatch.Elapsed;
            return timespan.Milliseconds;
        }
        /// <summary>
        /// 异步计算Task运行时间
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static async Task<int> TimeAsync(Task action)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            await action;

            stopwatch.Stop();
            TimeSpan timespan = stopwatch.Elapsed;
            return timespan.Milliseconds;
        }

        /// <summary>
        /// 任务执行时间
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static int Time(Func<Task> action)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            action().Wait();

            stopwatch.Stop();
            TimeSpan timespan = stopwatch.Elapsed;
            return timespan.Milliseconds;
        }

        /// <summary>
        /// 异步记录运行时间
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static async Task<int> TimeAsync(Func<Task> action)
        {
            Stopwatch stopwatch = new Stopwatch();

#if NET40
            await Task.Factory.StartNew(() =>
            {
                stopwatch.Start();
                action().Wait();
                stopwatch.Stop();
            });
#else
            await Task.Run(() =>
            {
                stopwatch.Start();
                action().Wait();
                stopwatch.Stop();
            });
#endif

            TimeSpan timespan = stopwatch.Elapsed;
            return timespan.Milliseconds;
        }

    }
}
