using Film_Takip_Uygulaması;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieApp
{
    public partial class FavoritedMovies : Form
    {
        private int userId;

        // Yeni constructor: userId parametresi alır
        public FavoritedMovies(int userId)
        {
            InitializeComponent();
            this.userId = userId;  // userId'yi formda tutuyoruz

            // FlowLayoutPanel'i formda tanımla
            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel()
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                WrapContents = true,
                BackColor = Color.White
            };
            this.Controls.Add(flowLayoutPanel);

            // Favori filmleri yükle
            LoadFavoriteMovies(userId, flowLayoutPanel);
        }

        private void LoadFavoriteMovies(int userId, FlowLayoutPanel flowLayoutPanel)
        {
            // Veritabanından favori filmleri al
            DatabaseHelper dbHelper = new DatabaseHelper();
            List<Movie> favoriteMovies = dbHelper.GetFavoriteMovies(userId);

            // Filmleri FlowLayoutPanel içinde göster
            foreach (var movie in favoriteMovies)
            {
                movie.AddMoviePanel(flowLayoutPanel);  // Her filmi panelde göster
            }
        }
    }


}
