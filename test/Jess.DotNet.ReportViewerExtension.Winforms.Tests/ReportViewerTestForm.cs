using Jess.DotNet.ReportViewerExtension.Winforms;
using Jess.DotNet.ReportViewerExtension.Winforms.Tests;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Jess.DotNet.ReportViewerExtension.Winforms.Tests
{
    /// <summary>
    /// ReportViewer测试窗体
    /// </summary>
    public partial class ReportViewerTestForm : Form
    {
        public ReportViewerTestForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 打印【xunit启动的会有内存错误】
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Print_Click(object sender, EventArgs e)
        {
            List<TestForReport> l = new List<TestForReport>();
            TestForReport t = new TestForReport("123");
            l.Add(t);
            TestForReport t1 = new TestForReport("1231");
            l.Add(t1);

            //ReportViewerHelper rv = new ReportViewerHelper();
            ReportViewerHelper.Print(this.reportViewer1, "Report1.rdlc", "DataSet1", l);
        }

        private void re(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
        }
    }
}