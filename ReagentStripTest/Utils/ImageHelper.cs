using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ReagentStripTest.Utils
{
    public static class ImageHelper
    {

        public static System.Drawing.Bitmap ImageSourceToBitmap(this BitmapSource source,int width,int height)
        {
            if (source == null) return null;
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(width,height);

            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, width, height);
            System.Drawing.Imaging.BitmapData bmpData = bitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, bitmap.PixelFormat);
            IntPtr ptr = bmpData.Scan0;

            byte[] rgbValues = BitmapImageToByteArray(source);

            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, rgbValues.Length);
            bitmap.UnlockBits(bmpData);

            return bitmap;
        }
        public static System.Drawing.Bitmap ImageSourceToBitmap(this BitmapSource source)
        {
            if (source == null) return null;
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap((int)source.Width, (int)source.Height);
            int width = bitmap.Width;
            int height = bitmap.Height;

            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, width, height);
            System.Drawing.Imaging.BitmapData bmpData = bitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, bitmap.PixelFormat);
            IntPtr ptr = bmpData.Scan0;

            byte[] rgbValues = BitmapImageToByteArray(source);

            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, rgbValues.Length);
            bitmap.UnlockBits(bmpData);

            return bitmap;
        }

        /// <summary>
        /// BitmapImage转数组
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        public static byte[] BitmapImageToByteArray(BitmapSource bmp)
        {
            Int32 PixelHeight = bmp.PixelHeight; // 图像高度
            Int32 PixelWidth = bmp.PixelWidth;   // 图像宽度
            Int32 Stride = PixelWidth << 2;         // 扫描行跨距
            Byte[] Pixels = new Byte[PixelHeight * Stride];
            if (bmp.Format == PixelFormats.Bgr32 || bmp.Format == PixelFormats.Bgra32)
            {   // 拷贝像素数据
                bmp.CopyPixels(Pixels, Stride, 0);
            }
            else
            {   // 先进行像素格式转换，再拷贝像素数据
                new FormatConvertedBitmap(bmp, PixelFormats.Bgr32, null, 0).CopyPixels(Pixels, Stride, 0);
            }
            return Pixels;

        }
    }
}
