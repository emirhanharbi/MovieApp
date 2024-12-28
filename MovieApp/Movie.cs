using MovieApp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Film_Takip_Uygulaması
{
    public class Movie:MovieBase
    {

        ImageHelper imageHelper = new ImageHelper();
        DatabaseHelper databaseHelper = new DatabaseHelper();

        private List<Button> buttonList = new List<Button>();

        

        

        public Movie(int id, string movieName, String movieIMG, double movieIMDB, string movieCategory, string movieURL)
        {
            this.MovieID = id;
            this.MovieName = movieName;
            this.MovieIMDB = movieIMDB;
            this.MovieCategory = movieCategory;
            this.MovieURL = movieURL;


            this.MovieIMG = movieIMG;

        }


        public void AddMoviePanel(FlowLayoutPanel flowLayoutPanel)
        {
            // Panel oluştur
            Panel panel = new Panel
            {
                Size = new Size(400, 400),
                Margin = new Padding(15, 100, 5, 10),
                BorderStyle = BorderStyle.FixedSingle,
            };

            // PictureBox oluştur
            PictureBox pictureBox = new PictureBox
            {
                Image = imageHelper.GetImageFromFileSystem(MovieIMG),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Dock = DockStyle.Fill
            };




            // Bilgi simgesi ekleyelim
            PictureBox infoIcon = new PictureBox
            {

                Size = new Size(30, 30),
                Location = new Point(panel.Width - 30, 10), // Panelin sağ üst köşesine yerleştir
                Cursor = Cursors.Hand, //Cursoru Değiştir.
                SizeMode = PictureBoxSizeMode.StretchImage,


            };
            Image infoImage= imageHelper.GetImageFromFileSystem("info-icon.png");
            infoIcon.BringToFront();
            if (infoImage != null)
            {
                infoIcon.Image = infoImage;
            }
            else
            {
                MessageBox.Show("dosya yolu bulanamadı");
            }
            
            panel.Controls.Add(infoIcon);

            // Film bilgilerini gösterecek Label (Başlangıçta gizli)
            Label movieInfo = new Label
            {
                Visible = false, // Başlangıçta görünmez
                Text = $"Film Adı: {MovieName}\nIMDb: {MovieIMDB}\nKategori: {MovieCategory}",
                ForeColor = Color.White,
                BackColor = Color.Black,
                Size = new Size(200, 100),
                Location = new Point(10, 10), // Panelin içinde üst kısma yerleştir
                BorderStyle = BorderStyle.FixedSingle,
                TextAlign = ContentAlignment.MiddleLeft,
                
            };
            panel.Controls.Add(movieInfo);

            // Mouse Hover Olayı
            infoIcon.MouseEnter+=(sender,e)=>{
                movieInfo.Visible = true; // Mouse üzerine gelince bilgiyi göster
            };

            // Mouse kaybolduğunda, film bilgisi paneli kaybolur
            
                infoIcon.MouseLeave += (sender, e) =>
            {
                movieInfo.Visible = false; // Mouse ayrıldığında bilgiyi gizle
            };

            // Butonları saklayan bir liste
            var buttonList = new List<Button>();

            // Buton oluşturma
            Button buttonWatch = new Button
            {
                Text = "İzle",
                Size = new Size(panel.Width - 40, 50), // Butonun genişliğini arttırdık ve daha yüksek yaptık
                Font = new Font("Arial", 14, FontStyle.Bold), // Yazı fontunu büyüttük
                Visible = false,
            };
            buttonWatch.Click += (sender, e) =>
            {
                OpenURL(MovieURL);
            };
            Button buttonMovieWantToWatch = new Button
            {
                Text = "İzleyeceğim Filmlere Ekle",
                Size = new Size(panel.Width - 40, 50), // Butonun genişliğini arttırdık ve daha yüksek yaptık
                Font = new Font("Arial", 14, FontStyle.Bold), // Yazı fontunu büyüttük
                Visible = false,
            };
            buttonMovieWantToWatch.Click += (sender, e) =>
            {
                if (SessionManager.LoggedInUserId.HasValue)
                {
                    int userId = SessionManager.LoggedInUserId.Value;  // Giriş yapan kullanıcının ID'sini al
                    int movieId = MovieID;  // Bu, filmin ID'si, veritabanından alınmalı

                    // Favorilere eklemek için fonksiyonu çağır
                    databaseHelper.AddWantToWatch(userId, movieId);

                    MessageBox.Show("Bu filmi İzleneceklere ekledin!");
                    HideButtons(buttonList);
                }
                else
                {
                    MessageBox.Show("Önce giriş yapmalısınız.");
                }
            };

            Button buttonMovieWatched = new Button
            {
                Text = "İzlediğim Filmlere Ekle",
                Size = new Size(panel.Width - 40, 50), // Butonun genişliğini arttırdık ve daha yüksek yaptık
                Font = new Font("Arial", 14, FontStyle.Bold), // Yazı fontunu büyüttük
                Visible = false,
            };
            buttonMovieWatched.Click += (sender, e) =>
            {
                if (SessionManager.LoggedInUserId.HasValue)
                {
                    int userId = SessionManager.LoggedInUserId.Value;  // Giriş yapan kullanıcının ID'sini al
                    int movieId = MovieID;  // Bu, filmin ID'si, veritabanından alınmalı

                    // Favorilere eklemek için fonksiyonu çağır
                    databaseHelper.AddToWatched(userId, movieId);

                    MessageBox.Show("Bu filmi favorilere ekledin!");
                    HideButtons(buttonList);
                }
                else
                {
                    MessageBox.Show("Önce giriş yapmalısınız.");
                }
            };

            Button buttonMovieFavorite = new Button
            {
                Text = "Favorilere Ekle",
                Size = new Size(panel.Width - 40, 50), // Butonun genişliğini arttırdık ve daha yüksek yaptık
                Font = new Font("Arial", 14, FontStyle.Bold), // Yazı fontunu büyüttük
                Visible = false,
            };
            buttonMovieFavorite.Click += (sender, e) =>
            {
                if (SessionManager.LoggedInUserId.HasValue)
                {
                    int userId = SessionManager.LoggedInUserId.Value;  // Giriş yapan kullanıcının ID'sini al
                    int movieId = MovieID;  // Bu, filmin ID'si, veritabanından alınmalı

                    // Favorilere eklemek için fonksiyonu çağır
                    databaseHelper.AddToFavorites(userId, movieId);

                    MessageBox.Show("Bu filmi favorilere ekledin!");
                    HideButtons(buttonList);
                }
                else
                {
                    MessageBox.Show("Önce giriş yapmalısınız.");
                }
            };
            Button buttonMovieDelete = new Button
            {
                Text = "Listeden Sil",
                Size = new Size(panel.Width - 40, 50), // Butonun genişliğini arttırdık ve daha yüksek yaptık
                Font = new Font("Arial", 14, FontStyle.Bold), // Yazı fontunu büyüttük
                Visible = false,
            };
            buttonMovieDelete.Click += (sender, e) =>
            {
                if (SessionManager.LoggedInUserId.HasValue)
                {
                    int userId = SessionManager.LoggedInUserId.Value;  // Giriş yapan kullanıcının ID'sini al
                    int movieId = MovieID;  // Bu, filmin ID'si, veritabanından alınmalı


                    DialogResult result = MessageBox.Show("Bu filmi silmek istediğine emin misin?");

                    if (result == DialogResult.Yes)
                    {
                        databaseHelper.MovieDeleteOnList(userId, MovieID);
                        MessageBox.Show("Bu Filmi Sildin");
                        HideButtons(buttonList);
                    };

                    
                }
                else
                {
                    MessageBox.Show("Önce giriş yapmalısınız.");
                }
                
            };

            // Butonları listeye ekle
            buttonList.Add(buttonMovieWantToWatch);
            buttonList.Add(buttonMovieWatched);
            buttonList.Add(buttonMovieFavorite);
            buttonList.Add(buttonWatch);
            buttonList.Add(buttonMovieDelete);

            // Panelin yüksekliğini baz alarak butonların konumlarını ayarla
            int totalHeight = buttonMovieWantToWatch.Height * buttonList.Count; // Butonların toplam yüksekliği
            int startingY = (panel.Height - totalHeight) / 2; // Panelin ortasına yakın başlat

            int y = startingY; // İlk butonun Y koordinatını başlat

            foreach (var button in buttonList)
            {
                button.Location = new Point((panel.Width - button.Width) / 2, y); // Dikeyde ortalama, yatayda panelin ortası
                panel.Controls.Add(button);
                y += button.Height + 10; // Sonraki buton için Y koordinatını güncelle (butonlar arası boşluk 10 piksel)
            }

            // PictureBox olayları
            pictureBox.MouseEnter += (sender, e) =>
            {
                foreach (var button in buttonList)
                {
                    button.Visible = true;
                    button.BringToFront();
                }
            };

            // MouseLeave olayını güncelleme
            pictureBox.MouseLeave += (sender, e) =>
            {
                Point mousePosition = panel.PointToClient(Cursor.Position);

                // Eğer mouse, panelin dışında ve pictureBox dışında ise butonları gizle
                if (!panel.ClientRectangle.Contains(mousePosition) && !pictureBox.ClientRectangle.Contains(mousePosition))
                {
                    HideButtons(buttonList);
                }
            };

            // Panel'e pictureBox ekle
            panel.Controls.Add(pictureBox);

            // FlowLayoutPanel'e paneli ekle
            flowLayoutPanel.Controls.Add(panel);
        }

        private void HideButtons(List<Button> buttonList)
        {
            foreach (var button in buttonList)
            {
                button.Visible = false;
            }
        }

        private void OpenURL(string url)
        {

            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true // URL'yi varsayılan tarayıcıda açmak için
            });
        }
    }
}

