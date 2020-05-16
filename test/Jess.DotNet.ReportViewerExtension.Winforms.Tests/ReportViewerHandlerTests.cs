using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.IO;
using Xunit;
using Jess.DotNet.ReportViewerExtension.Winforms;

namespace Jess.DotNet.ReportViewerExtension.Winforms.Tests
{
    public class ReportViewerHandlerTests
    {

        [Fact]
        public void ExportPDFTest()
        {
            List<TestForReport> l = new List<TestForReport>();
            TestForReport t = new TestForReport("123");
            l.Add(t);
            TestForReport t1 = new TestForReport("1231");
            l.Add(t1);
            ReportViewerHelper.ExportPDF("Report1.rdlc", "2.pdf", "DataSet1", l);
        }

        [Fact]
        public void LocalReportPrintDocumnetTest()
        {
            List<TestForReport> l = new List<TestForReport>();
            TestForReport t = new TestForReport("123");
            l.Add(t);
            TestForReport t1 = new TestForReport("1231");
            l.Add(t1);

            ReportViewerHelper.Print("Report1.rdlc", "DataSet1", l);
        }

    }
}
