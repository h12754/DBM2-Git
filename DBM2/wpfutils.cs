using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DBM2
{
    class wpfutils
    {
        //public static BitmapImage GetBitmapImageFromBitmap(System.Drawing.Bitmap bmp)
        //{
        //    MemoryStream str = new();
        //               bmp.Save(str, System.Drawing.Imaging.ImageFormat.Png);
        //    str.Position = 0;
        //    BitmapImage bi = new();
        //    bi.BeginInit();
        //    bi.StreamSource = str;
        //    bi.CacheOption = BitmapCacheOption.OnLoad;
        //    bi.EndInit();
        //    return bi;
        //              }

        public static BitmapImage GetBitmapImageFromUri(string imgName)
        {
            if (imgName != null & imgName != "")
            {
                Uri uri = new Uri("pack://application:,,,/Resources/" + imgName);
                BitmapImage bitmapImage = new BitmapImage(uri);
                return bitmapImage;
            }
            return null;
        }
       




     
    }
}
