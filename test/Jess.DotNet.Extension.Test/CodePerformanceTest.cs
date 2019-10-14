using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Jess.DotNet.Extension.Test
{
    /// <summary>
    /// ´úÂëĞÔÄÜ²âÊÔ
    /// </summary>
    public class CodePerformanceTest
    {
        private readonly ITestOutputHelper output;

        public CodePerformanceTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TimeActionTest()
        {
            int time = CodePerformance.Time(() =>
            {
                Thread.Sleep(100);
                Console.WriteLine("Test!");
            });
            output.WriteLine(time.ToString());
            Assert.True(time >= 100);
        }

        [Fact]
        public async void TimeActionAsyncTest()
        {
            int time = await CodePerformance.TimeAsync(() =>
             {
                 Thread.Sleep(100);
                 Console.WriteLine("Test!");
             });
            output.WriteLine(time.ToString());
            Assert.True(time >= 100);
        }

        [Fact]
        public void TimeTaskTest()
        {
            int time = CodePerformance.Time(
                Task.Run(() =>
                {
                    Thread.Sleep(100);
                    Console.WriteLine("Test!");
                })
                );
            output.WriteLine(time.ToString());
            Assert.True(time >= 100);
        }

        [Fact]
        public async void TimeTaskAsyncTest()
        {
            int time = await CodePerformance.TimeAsync(
                Task.Run(() =>
                {
                    Thread.Sleep(100);
                    Console.WriteLine("Test!");
                })
                );
            output.WriteLine(time.ToString());
            Assert.True(time >= 100);
        }

        [Fact]
        public void TimeTaskFuncTest()
        {
            int time = CodePerformance.Time(async () =>
            {
                await Task.Run(() =>
                {
                    Thread.Sleep(100);
                    Console.WriteLine("Test!");
                });
            });
            output.WriteLine(time.ToString());
            Assert.True(time >= 100);
        }

        [Fact]
        public async void TimeTaskFuncAsyncTest()
        {
            int time = await CodePerformance.TimeAsync(async () =>
             {
                 await Task.Run(() =>
                 {
                     Thread.Sleep(100);
                     Console.WriteLine("Test!");
                 });
             });
            output.WriteLine(time.ToString());
            Assert.True(time >= 100);
        }

    }
}
