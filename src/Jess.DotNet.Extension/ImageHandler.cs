using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Jess.DotNet.Extension
{
    /// <summary>
    /// 图片处理类
    /// </summary>
    public class ImageHandler
    {

        #region 图片裁剪

        /// <summary>
        /// 裁剪方形 —— 中心延伸裁剪
        /// </summary>
        /// <param name="sourcefile">源文件</param>
        /// <param name="savefilepath">裁剪后文件保存路径</param>
        /// <param name="cutwidth">裁剪宽度</param>
        /// <param name="cutheight">裁剪高度（为0则表示与宽度一样——即正方形）</param>
        /// <param name="quality">质量（0-100）</param>
        public static void CutSquare(Stream sourcefile, string savefilepath, double cutwidth, double cutheight = 0, int quality = 100)
        {
          

        }

        /// <summary>
        /// 按照比例裁剪方形
        /// </summary>
        public static void CutSquareScale(Stream sourcefile, string savefilepath, double cutwidthscale, double cutheightscale = 0, int quality = 100)
        {
            //正方形裁剪
            if (cutheightscale == 0)
            {
                cutheightscale = cutwidthscale;
            }

            //创建目录 —— 保存文件
            string dir = Path.GetDirectoryName(savefilepath);
            if (!System.IO.Directory.Exists(dir))
                System.IO.Directory.CreateDirectory(dir);

            //原始图片（获取原始图片创建对象，并使用流中嵌入的颜色管理信息）
            System.Drawing.Image initImage = System.Drawing.Image.FromStream(sourcefile, true);

            //原图宽高均小于模版，不作处理，直接保存
            if (initImage.Width <= cutwidthscale && initImage.Height <= cutheightscale)
            {
                initImage.Save(savefilepath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            else
            {
                //原始图片的宽、高
                int initWidth = initImage.Width;
                int initHeight = initImage.Height;

                //目标图的宽高比例
                double templateRate = (double)cutwidthscale / cutheightscale;
                //原图片的宽高比例
                double initRate = (double)initImage.Width / initImage.Height;

                //截图对象
                System.Drawing.Image pickedImage = null;
                System.Drawing.Graphics pickedG = null;

                //定位
                Rectangle fromR = new Rectangle(0, 0, 0, 0);//原图裁剪定位
                Rectangle toR = new Rectangle(0, 0, 0, 0);//目标定位

                //宽为标准进行裁剪
                if (templateRate > initRate)
                {
                    //对象实例化
                    pickedImage = new System.Drawing.Bitmap(initImage.Width, (int)Math.Floor(initImage.Width / templateRate));
                    pickedG = System.Drawing.Graphics.FromImage(pickedImage);

                    //定位
                    fromR = new Rectangle(0, (int)Math.Floor((initImage.Height - initImage.Width / templateRate) / 2), initImage.Width, (int)Math.Floor(initImage.Width / templateRate));
                    toR = new Rectangle(0, 0, initImage.Width, (int)Math.Floor(initImage.Width / templateRate));

                    //重置宽
                    //   initWidth = initHeight;
                }
                //高为标准进行裁剪
                else
                {
                    //对象实例化
                    pickedImage = new System.Drawing.Bitmap((int)Math.Floor(initImage.Height * templateRate), initImage.Height);
                    pickedG = System.Drawing.Graphics.FromImage(pickedImage);

                    //定位
                    fromR = new Rectangle((int)Math.Floor((initImage.Width - initImage.Height * templateRate) / 2), 0, (int)Math.Floor(initImage.Height * templateRate), initImage.Height);
                    toR = new Rectangle(0, 0, (int)Math.Floor(initImage.Height * templateRate), initImage.Height);

                    //重置高
                    // initHeight = initWidth;
                }

                //设置质量
                pickedG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                pickedG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                //画图——裁剪
                pickedG.DrawImage(initImage, toR, fromR, System.Drawing.GraphicsUnit.Pixel);

                //将截图对象赋给原图
                initImage = (System.Drawing.Image)pickedImage.Clone();
                //释放截图资源
                pickedG.Dispose();
                pickedImage.Dispose();


                ////缩略图对象
                //System.Drawing.Image resultImage = new System.Drawing.Bitmap(side, side);
                //System.Drawing.Graphics resultG = System.Drawing.Graphics.FromImage(resultImage);
                ////设置质量
                //resultG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                //resultG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                ////用指定背景色清空画布
                //resultG.Clear(Color.White);
                ////绘制缩略图
                //resultG.DrawImage(initImage, new System.Drawing.Rectangle(0, 0, side, side), new System.Drawing.Rectangle(0, 0, initWidth, initHeight), System.Drawing.GraphicsUnit.Pixel);

                //关键质量控制
                //获取系统编码类型数组,包含了jpeg,bmp,png,gif,tiff
                ImageCodecInfo[] icis = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo ici = null;
                foreach (ImageCodecInfo i in icis)
                {
                    if (i.MimeType == "image/jpeg" || i.MimeType == "image/bmp" || i.MimeType == "image/png" || i.MimeType == "image/gif")
                    {
                        ici = i;
                    }
                }
                EncoderParameters ep = new EncoderParameters(1);
                ep.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)quality);

                //保存裁剪后的图
                initImage.Save(savefilepath, ici, ep);

                //释放关键质量控制所用资源
                ep.Dispose();

                //释放缩略图资源
                //resultG.Dispose();
                //resultImage.Dispose();

                //释放原始图片资源
                initImage.Dispose();
            }
        }
        #endregion

        #region 缩放图片
        /// <summary>
        /// 按比例缩放图片
        /// </summary>
        public static void ZoomScale(Stream sourcefile, string savefilepath, double cutwidthscale, double cutheightscale = 0, int quality = 100)
        {
            //正方形
            if (cutheightscale == 0)
            {
                cutheightscale = cutwidthscale;
            }

            //创建目录
            string dir = Path.GetDirectoryName(savefilepath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            //原始图片（获取原始图片创建对象，并使用流中嵌入的颜色管理信息）
            System.Drawing.Image initImage = System.Drawing.Image.FromStream(sourcefile, true);

            //缩略图对象
            System.Drawing.Image resultImage = new System.Drawing.Bitmap((int)cutwidthscale, (int)cutheightscale);
            System.Drawing.Graphics resultG = System.Drawing.Graphics.FromImage(resultImage);
            //设置质量
            resultG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            resultG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //用指定背景色清空画布
            resultG.Clear(Color.White);
            //绘制缩略图
            resultG.DrawImage(initImage, new System.Drawing.Rectangle(0, 0, (int)cutwidthscale, (int)cutheightscale), new System.Drawing.Rectangle(0, 0, initImage.Width, initImage.Height), System.Drawing.GraphicsUnit.Pixel);

            //关键质量控制
            //获取系统编码类型数组,包含了jpeg,bmp,png,gif,tiff
            ImageCodecInfo[] icis = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo ici = null;
            foreach (ImageCodecInfo i in icis)
            {
                if (i.MimeType == "image/jpeg" || i.MimeType == "image/bmp" || i.MimeType == "image/png" || i.MimeType == "image/gif")
                {
                    ici = i;
                }
            }
            EncoderParameters ep = new EncoderParameters(1);
            ep.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)quality);

            //保存缩略图
            resultImage.Save(savefilepath, ici, ep);

            //释放关键质量控制所用资源
            ep.Dispose();

            //释放缩略图资源
            resultG.Dispose();
            resultImage.Dispose();

            //释放原始图片资源
            initImage.Dispose();
        }
        #endregion

        #region 添加水印
        /// <summary>
        /// 添加水印
        /// </summary>
        /// <param name="sourcefile"></param>
        /// <param name="savefilepath"></param>
        /// <param name="watermarkText"></param>
        /// <param name="watermarkImagepath"></param>
        public static void AddWatermark(Stream sourcefile, string savefilepath, string watermarkText = "", string watermarkImagepath = "")
        {
            if (string.IsNullOrWhiteSpace(watermarkText) && string.IsNullOrWhiteSpace(watermarkImagepath))
            {
                //未指定添加什么水印
                return;
            }

            //创建目录
            string dir = Path.GetDirectoryName(savefilepath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            //原始图片（获取原始图片创建对象，并使用流中嵌入的颜色管理信息）
            System.Drawing.Image initImage = System.Drawing.Image.FromStream(sourcefile, true);

            //文字水印
            if (watermarkText != "")
            {
                using (System.Drawing.Graphics gWater = System.Drawing.Graphics.FromImage(initImage))
                {
                    System.Drawing.Font fontWater = new Font("宋体", 10);
                    System.Drawing.Brush brushWater = new SolidBrush(Color.White);
                    gWater.DrawString(watermarkText, fontWater, brushWater, 10, 10);
                    gWater.Dispose();
                }
            }

            //透明图片水印
            if (watermarkImagepath != "")
            {
                if (File.Exists(watermarkImagepath))
                {
                    //获取水印图片
                    using (System.Drawing.Image wrImage = System.Drawing.Image.FromFile(watermarkImagepath))
                    {
                        //水印绘制条件：原始图片宽高均大于或等于水印图片
                        if (initImage.Width >= wrImage.Width && initImage.Height >= wrImage.Height)
                        {
                            Graphics gWater = Graphics.FromImage(initImage);

                            //透明属性
                            ImageAttributes imgAttributes = new ImageAttributes();
                            ColorMap colorMap = new ColorMap();
                            colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
                            colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
                            ColorMap[] remapTable = { colorMap };
                            imgAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

                            float[][] colorMatrixElements = {
                                   new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},
                                   new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},
                                   new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},
                                   new float[] {0.0f,  0.0f,  0.0f,  0.5f, 0.0f},//透明度:0.5
                                   new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}
                                };

                            ColorMatrix wmColorMatrix = new ColorMatrix(colorMatrixElements);
                            imgAttributes.SetColorMatrix(wmColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                            gWater.DrawImage(wrImage, new Rectangle(initImage.Width - wrImage.Width, initImage.Height - wrImage.Height, wrImage.Width, wrImage.Height), 0, 0, wrImage.Width, wrImage.Height, GraphicsUnit.Pixel, imgAttributes);
                            gWater.Dispose();
                        }
                        wrImage.Dispose();
                    }
                }
            }

            //保存缩略图
            initImage.Save(savefilepath, System.Drawing.Imaging.ImageFormat.Jpeg);

            //释放资源
            initImage.Dispose();
        }
        #endregion

    }
}
