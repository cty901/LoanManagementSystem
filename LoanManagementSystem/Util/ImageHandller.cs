using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LoanManagementSystem.Util
{
    class ImageHandller
    {
        public static void setProfImage(byte[] _image, ImageBrush ib)
        {
            if (_image != null)
            {
                ImageBrush brush;
                BitmapImage bi;
                using (var ms = new MemoryStream(_image))
                {
                    brush = new ImageBrush();

                    bi = new BitmapImage();
                    bi.BeginInit();
                    bi.CreateOptions = BitmapCreateOptions.None;
                    bi.CacheOption = BitmapCacheOption.OnLoad;
                    bi.StreamSource = ms;
                    bi.EndInit();
                }
                ib.ImageSource = bi;
            }
        }
    }
}
