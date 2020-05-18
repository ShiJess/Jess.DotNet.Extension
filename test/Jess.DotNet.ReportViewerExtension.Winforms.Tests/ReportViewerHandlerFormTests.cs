using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.IO;
using Xunit;
using Jess.DotNet.ReportViewerExtension.Winforms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jess.DotNet.ReportViewerExtension.Winforms.Tests
{
    /// <summary>
    /// 下列Winform打印报表，使用xunit出错，使用微软原始的UnitTest可以
    /// </summary>
    [TestClass]
    public class ReportViewerHandlerFormTests
    {                
        [TestMethod]
        public void ReportViewerPrintTest()
        {
            ReportViewerTestForm reportviewerform = new ReportViewerTestForm();
            reportviewerform.ShowDialog();
        }
                       
    }
}
