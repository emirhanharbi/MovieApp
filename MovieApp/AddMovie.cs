using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieApp
{
    public partial class AddMovie : Form
    {
        public AddMovie()
        {
            InitializeComponent();
        }







        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            // OpenFileDialog ile resim dosyasını seçme
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;";
            openFileDialog.Title = "Resim Seç";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Resmin yolu
                string selectedImagePath = openFileDialog.FileName;
                string destinationPath = Path.Combine(Application.StartupPath, "images", Path.GetFileName(selectedImagePath));

                // Seçilen resmin 'images' klasörüne kopyalanması
                try
                {
                    
                    
                    

                    // Resmi kopyala
                    File.Copy(selectedImagePath, destinationPath, true);  // 'true' parametresi, mevcut dosyayı üzerine yazmaya zorlar
                    // Kaydedilen resmi pictureBox'a yükleme
                    pictureBoxSelectedImage.ImageLocation = destinationPath;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Resim kaydedilemedi: " + ex.Message);
                }
            }
        }


        private void btnAddMovie_Click(object sender, EventArgs e)
        {
            string movieName = txtMovieName.Text;
            string movieCategory = txtMovieCategory.Text;
            double movieIMDB;
            string movieURL = txtMovieURL.Text;

            // Fotoğraf yolunu al
            string movieIMG = pictureBoxSelectedImage.ImageLocation;  // Burada resmin yolunu alıyoruz

            // IMDb Puanı kontrolü
            if (!double.TryParse(txtMovieIMDB.Text, out movieIMDB)) // txtMovieIMDB.Text, kullanıcı tarafından girilen değer
            {
                MessageBox.Show("Geçerli bir IMDb puanı girin.");
                return;
            }

            // Boş giriş kontrolü
            if (string.IsNullOrEmpty(movieName) || 
                string.IsNullOrEmpty(movieCategory) || string.IsNullOrEmpty(movieURL))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.");
                return;
            }


            // Film veritabanına ekle
            DatabaseHelper dbHelper = new DatabaseHelper();
            dbHelper.AddMovie(movieName, movieIMG, movieIMDB, movieCategory, movieURL);
            MessageBox.Show("Film başarıyla eklendi!");
            Homepage homepage1 = new Homepage();
            
            homepage1.Show();
            this.Close(); // Formu kapat
        }


    }
}
