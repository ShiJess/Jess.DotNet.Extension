using System;
using System.IO;

namespace Jess.DotNet.ReportViewerExtension.Winforms.Tests
{
    public class TestForReport
    {
        public string Field { get; set; }

        public string L { get; set; }
        public string C { get; set; }

        public byte[] image { get; set; }

        public TestForReport(string field)
        {
            Field = field;
            L = "1";
            FileStream fs = new FileStream("TestReportViewer.png", FileMode.Open);
            byte[] b = new Byte[fs.Length];
            image = new Byte[fs.Length];
            StreamReader sr = new StreamReader(fs);
            fs.Read(image, 0, (int)fs.Length);
            //b.CopyTo(image, 0);
            fs.Close();
        }

    }
}