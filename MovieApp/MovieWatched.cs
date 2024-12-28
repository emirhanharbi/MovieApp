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
    public partial class MovieWatched : Form
    {
        private int userId;

        // Yeni constructor: userId parametresi alır
        public MovieWatched(int userId)
        {
            InitializeComponent();
            this.userId = userId;  // userId'yi formda tutuyoruz
            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel()
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                WrapContents = true,
                BackColor = Color.White
            };
            this.Controls.Add(flowLayoutPanel);

            // Favori filmleri yükle
            LoadWatchedMovies(userId, flowLayoutPanel);
        }
        private void LoadWatchedMovies(int userId, FlowLayoutPanel flowLayoutPanel)
        {
            // Veritabanından favori filmleri al
            DatabaseHelper dbHelper = new DatabaseHelper();
            List<Movie> ListedWatchedMovies = dbHelper.GetWatchedMovies(userId);

            // Filmleri FlowLayoutPanel içinde göster
            foreach (var movie in ListedWatchedMovies)
            {
                movie.AddMoviePanel(flowLayoutPanel);  // Her filmi panelde göster
            }
        }
    }
}
