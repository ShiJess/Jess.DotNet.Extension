using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Jess.ImageProcessing;

namespace ImageProcessingTest
{
    [TestClass]
    public class UnitTest1
    {
       

        [TestMethod]
        public void TestCutSquare()
        {
            string exportPath0 = "..\\..\\..\\TestDataFile\\Input\\testimg.JPG";
            string exportPath1 = Path.GetFullPath(exportPath0);
            if (!File.Exists(exportPath1))
            {
                Console.WriteLine("文件不存在！");
                return;
            }
            using (FileStream fs1 = new FileStream(exportPath1, FileMode.Open))
            {
                string savepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "OutPut");
                if (!Directory.Exists(savepath))
                {
                    Directory.CreateDirectory(savepath);
                }
                string savefilename = Path.Combine(savepath, DateTime.Now.ToString("yyyy-MM-dd-HHMMss") + "cut.jpg");
                ImageHandler.CutSquareScale(fs1, savefilename, 100);
                savefilename = Path.Combine(savepath, DateTime.Now.ToString("yyyy-MM-dd-HHMMss") + "zoom.jpg");
                ImageHandler.ZoomScale(fs1, savefilename, 100);
            }
        }

        [TestMethod]
        public void TestAddWatermark()
        {
            string exportPath0 = "..\\..\\..\\TestDataFile\\Input\\testimg.JPG";
            string watermarkimg = "..\\..\\..\\TestDataFile\\Input\\logo.png";
            string exportPath1 = Path.GetFullPath(exportPath0);
            string watermarkimgpth = Path.GetFullPath(watermarkimg);
            if (!File.Exists(exportPath1))
            {
                Console.WriteLine("文件不存在！");
                return;
            }
            using (FileStream fs1 = new FileStream(exportPath1, FileMode.Open))
            {
                string savepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "OutPut");
                if (!Directory.Exists(savepath))
                {
                    Directory.CreateDirectory(savepath);
                }
                string savefilename = Path.Combine(savepath, DateTime.Now.ToString("yyyy-MM-dd-HHMMss") + "watermark.jpg");
                ImageHandler.AddWatermark(fs1, savefilename, "水印测试", watermarkimgpth);

            }
        }
    }
}
