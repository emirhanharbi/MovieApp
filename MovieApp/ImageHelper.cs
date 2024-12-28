using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieApp
{
    public class ImageHelper
    {
        // Resmi images klasöründen almak için fonksiyon
        public Image GetImageFromFileSystem(string imageName)
        {
            try
            {
                // EXE'nin bulunduğu dizini al
                string exeDirectory = AppDomain.CurrentDomain.BaseDirectory;  // EXE'nin bulunduğu dizin
                string imagePath = Path.Combine(exeDirectory, "images", imageName); // Resmin tam yolunu oluştur

                // Dosyanın varlığını kontrol et
                if (File.Exists(imagePath))
                {
                    // Dosya mevcutsa, resmi yükle
                    return Image.FromFile(imagePath);
                }
                else
                {
                    MessageBox.Show("Resim bulunamadı: " + imagePath);
                    return null; // Resim bulunamazsa null döner
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
                return null;
            }
        }
    }
}
