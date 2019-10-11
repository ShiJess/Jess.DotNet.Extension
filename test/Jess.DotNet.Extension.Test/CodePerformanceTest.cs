using System;
using System.Diagnostics;
using System.Threading;
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
        public void TimeTest()
        {
            int time = CodePerformance.Time(() =>
            {
                Thread.Sleep(100);
                Console.WriteLine("Test!");
            });
            output.WriteLine(time.ToString());
        }
    }
}
