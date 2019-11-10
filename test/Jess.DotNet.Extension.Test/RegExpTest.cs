using Jess.DotNet.Extension;
using System;
using Xunit;

namespace Jess.DotNet.Extension.Test
{
    
    public class RegExpTest
    {
        [Fact]
        public void TestMethod1()
        {
            string mail = "11@qq.com";
            bool result = RegExp.IsEmail(mail);
            Assert.True(result);
        }
    }
}
