using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace VSW.Lib.Global
{
    public class Image
    {
        public static void ResizeImageFile(int Width, int Height, int Type, string Location, string Target, ImageFormat Format)
        {
            Bitmap loBMP = new Bitmap(Location);

            int lnOldWidth = loBMP.Width;
            int lnOldHeight = loBMP.Height;

            int lnNewWidth = 0;
            int lnNewHeight = 0;

            if (Type == 1)
            {
                lnNewWidth = (Width == 0) ? lnOldWidth : Width;
                lnNewHeight = (Height == 0) ? lnOldHeight : Height;
            }
            else if (Type == 2)
            {
                lnNewWidth = (Width == 0) ? (int)((double)lnOldWidth * ((double)Height / (double)lnOldHeight)) : Width;
                lnNewHeight = (Height == 0) ? (int)((double)lnOldHeight * ((double)Width / (double)lnOldWidth)) : Height;
            }
            else if (Type == 3)
            {
                lnNewWidth = (Width == 0) ? lnOldWidth : (int)((double)lnOldWidth * ((double)Width / 100));
                lnNewHeight = (Height == 0) ? lnOldHeight : (int)((double)lnOldHeight * ((double)Height / 100));
            }
            else if (Type == 4)
            {
                decimal lnRatio;

                if (lnOldWidth > lnOldHeight)
                {
                    lnRatio = (decimal)Width / lnOldWidth;
                    lnNewWidth = Width;
                    decimal lnTemp = lnOldHeight * lnRatio;
                    lnNewHeight = (int)lnTemp;
                }
                else
                {
                    lnRatio = (decimal)Height / lnOldHeight;
                    lnNewHeight = Height;
                    decimal lnTemp = lnOldWidth * lnRatio;
                    lnNewWidth = (int)lnTemp;
                }
            }

            lnNewWidth = (lnNewWidth == 0) ? lnOldWidth : lnNewWidth;
            lnNewHeight = (lnNewHeight == 0) ? lnOldHeight : lnNewHeight;


            Bitmap bmpOut = new Bitmap(lnNewWidth, lnNewHeight);
            Graphics g = Graphics.FromImage(bmpOut);

            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;

            g.FillRectangle(Brushes.White, 0, 0, lnNewWidth, lnNewHeight);
            g.DrawImage(loBMP, 0, 0, lnNewWidth, lnNewHeight);

            //if (IsStamp && lnNewWidth >= 200 & lnNewHeight >= 200)
            //{
            //    if (!StampFile)
            //    {
            //        g.DrawString(StampText, new Font("Arial", 12, FontStyle.Bold), new SolidBrush(Color.Red), lnNewWidth - 25 - StampText.Length * 9, lnNewHeight - 35);
            //    }
            //    else
            //    {
            //        System.Drawing.Image _Logo = System.Drawing.Image.FromFile(System.Web.HttpContext.Current.Server.MapPath(StampText));
            //        if (_Logo != null)
            //            g.DrawImage(_Logo, lnNewWidth - _Logo.Width, lnNewHeight - _Logo.Height, _Logo.Width, _Logo.Height);
            //    }
            //}

            g.Dispose();

            bmpOut.Save(Target, Format);

            loBMP.Dispose();
            bmpOut.Dispose();
        }
    }
}
