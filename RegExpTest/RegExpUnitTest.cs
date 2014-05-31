using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegExpLib;

namespace RegExpTest
{
    [TestClass]
    public class RegExpUnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            string mail = "11@qq.com";
            bool result = RegExp.IsEmail(mail);
            Assert.IsTrue(result);
        }
    }
}
