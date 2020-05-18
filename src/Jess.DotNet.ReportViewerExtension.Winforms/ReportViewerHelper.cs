using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Jess.DotNet.ReportViewerExtension.Winforms
{
    /// <summary>
    /// ReportViewer辅助扩展操作
    /// </summary>
    public class ReportViewerHelper
    {
        /// <summary>
        /// 导出pdf
        /// </summary>
        /// <param name="templatepath">rdlc模板路径</param>
        /// <param name="fullfilename">导出文件路径</param>
        /// <param name="datasetname">模板中数据集名称</param>
        /// <param name="datasource">待绑定的数据集</param>
        /// <param name="rparams">模板中使用的参数</param>
        public static void ExportPDF(string templatepath, string fullfilename, string datasetname, object datasource, List<ReportParameter> rparams = null)
        {
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;

            LocalReport lr = new LocalReport();
            lr.ReportPath = templatepath;
            lr.DataSources.Add(new ReportDataSource(datasetname, datasource));
            if (rparams != null)
            {
                lr.SetParameters(rparams);
            }

            byte[] bytes = lr.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

            using (FileStream fs = File.OpenWrite(fullfilename))
            {
                fs.Write(bytes, 0, bytes.Length);
                Console.WriteLine("导出成功！");
            }
        }

        #region LocalReport利用PrintDocument方式打印
        /// <summary>
        /// 打印——LocalReport利用PrintDocument方式打印
        /// </summary>
        /// <param name="templatepath"></param>
        /// <param name="datasetname"></param>
        /// <param name="datasource"></param>
        /// <param name="rparams"></param>
        public static bool Print(string templatepath, string datasetname, object datasource, List<ReportParameter> rparams = null)
        {

            List<Stream> m_streams = new List<Stream>();
            int m_currentPageIndex = 0;

            PrintDialog pdg = new PrintDialog();
            pdg.Document = new System.Drawing.Printing.PrintDocument();
            DialogResult result = pdg.ShowDialog();
            if (result != DialogResult.OK && result != DialogResult.Yes)
                return false;
            pdg.Document.PrinterSettings = pdg.PrinterSettings;
            PrintDocument pd = pdg.Document;

            LocalReport lr = new LocalReport();
            lr.ReportPath = templatepath;
            lr.DataSources.Add(new ReportDataSource(datasetname, datasource));
            if (rparams != null)
            {
                lr.SetParameters(rparams);
            }
            lr.Refresh();

            string deviceInfo =
                       "<DeviceInfo>" +
                         "  <OutputFormat>EMF</OutputFormat>" +
                       //"  <PageWidth>8.5in</PageWidth>" +
                       //"  <PageHeight>11in</PageHeight>" +
                       //"  <MarginTop>0.15in</MarginTop>" +
                       //"  <MarginLeft>0.5cm</MarginLeft>" +
                       //"  <MarginRight>0.1in</MarginRight>" +
                       //"  <MarginBottom>0.1in</MarginBottom>" +
                       "</DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();
            m_currentPageIndex = 0;
            CreateStreamCallback createStream = (name, fileNameExtension, encoding, mimeType, willSeek) =>
            {
                //Stream stream = new FileStream(name + "." + fileNameExtension, FileMode.Create);
                Stream stream = new MemoryStream();
                m_streams.Add(stream);
                return stream;
            };
            lr.Render("Image", deviceInfo, createStream, out warnings);

            foreach (Stream stream in m_streams)
                stream.Position = 0;

            pd.PrintPage += (sender, e) =>
            {
                System.Drawing.Imaging.Metafile pageImage = new System.Drawing.Imaging.Metafile(m_streams[m_currentPageIndex]);
                //e.Graphics.DrawImage(pageImage, 0, 0,e.PageSettings.PaperSize.Width,e.PageSettings.PaperSize.Height);
                e.Graphics.DrawImage(pageImage, e.PageBounds);
                m_currentPageIndex++;
                e.HasMorePages = (m_currentPageIndex < m_streams.Count);
            };
            pd.Print();
            return true;
        }

        /// <summary>
        /// 多任务【多数据】打印——LocalReport利用PrintDocument方式打印
        /// </summary>
        /// <param name="templatepath"></param>
        /// <param name="datasetname"></param>
        /// <param name="datasource"></param>
        /// <param name="rparams"></param>
        public static bool PrintList(string templatepath, string datasetname, List<object> datasource, List<ReportParameter> rparams = null)
        {
            List<Stream> m_streams = new List<Stream>();
            int m_currentPageIndex = 0;

            PrintDialog pdg = new PrintDialog();
            pdg.Document = new System.Drawing.Printing.PrintDocument();
            DialogResult result = pdg.ShowDialog();
            if (result != DialogResult.OK && result != DialogResult.Yes)
                return false;
            pdg.Document.PrinterSettings = pdg.PrinterSettings;
            PrintDocument pd = pdg.Document;
            pd.PrintPage += (sender, e) =>
            {
                System.Drawing.Imaging.Metafile pageImage = new System.Drawing.Imaging.Metafile(m_streams[m_currentPageIndex]);
                //e.Graphics.DrawImage(pageImage, 0, 0,e.PageSettings.PaperSize.Width,e.PageSettings.PaperSize.Height);
                e.Graphics.DrawImage(pageImage, e.PageBounds);
                m_currentPageIndex++;
                e.HasMorePages = (m_currentPageIndex < m_streams.Count);
            };
            foreach (var item in datasource)
            {
                LocalReport lr = new LocalReport();
                lr.ReportPath = templatepath;
                lr.DataSources.Clear();
                lr.DataSources.Add(new ReportDataSource(datasetname, item));

                if (rparams != null)
                {
                    lr.SetParameters(rparams);
                }
                lr.Refresh();

                string deviceInfo =
                           "<DeviceInfo>" +
                             "  <OutputFormat>EMF</OutputFormat>" +
                           //"  <PageWidth>8.5in</PageWidth>" +
                           //"  <PageHeight>11in</PageHeight>" +
                           //"  <MarginTop>0.15in</MarginTop>" +
                           //"  <MarginLeft>0.5cm</MarginLeft>" +
                           //"  <MarginRight>0.1in</MarginRight>" +
                           //"  <MarginBottom>0.1in</MarginBottom>" +
                           "</DeviceInfo>";
                Warning[] warnings;
                m_streams = new List<Stream>();
                m_currentPageIndex = 0;
                CreateStreamCallback createStream = (name, fileNameExtension, encoding, mimeType, willSeek) =>
                {
                    //Stream stream = new FileStream(name + "." + fileNameExtension, FileMode.Create);
                    Stream stream = new MemoryStream();
                    m_streams.Add(stream);
                    return stream;
                };
                lr.Render("Image", deviceInfo, createStream, out warnings);

                foreach (Stream stream in m_streams)
                    stream.Position = 0;

                pd.Print();
            }

            return true;
        }

        #endregion

        #region ReportViewer方式打印
        /// <summary>
        /// 打印——ReportViewer方式
        /// </summary>
        /// <param name="rv"></param>
        /// <param name="templatepath"></param>
        /// <param name="datasetname"></param>
        /// <param name="datasource"></param>
        /// <param name="rparams"></param>
        public static void Print(ReportViewer rv, string templatepath, string datasetname, object datasource, List<ReportParameter> rparams = null)
        {
            LocalReport lr = rv.LocalReport;
            lr.ReportPath = templatepath;
            lr.DataSources.Add(new ReportDataSource(datasetname, datasource));
            if (rparams != null)
            {
                lr.SetParameters(rparams);
            }

            Action unRegisterCompleteEvent = null;
            RenderingCompleteEventHandler renderingCompleteEvent = (sender, e) =>
            {
                unRegisterCompleteEvent?.Invoke();
                rv.PrintDialog();
            };
            unRegisterCompleteEvent = () =>
            {
                rv.RenderingComplete -= renderingCompleteEvent;
            };
            rv.RenderingComplete += renderingCompleteEvent;

            rv.RefreshReport();
        }
        #endregion

    }
}