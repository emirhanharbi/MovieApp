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
    public partial class Homepage : Form
    {
        private FlowLayoutPanel flowLayoutPanel;
        public Homepage()
        {
            
            DatabaseHelper databaseHelper = new DatabaseHelper();
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            
            flowLayoutPanel = new FlowLayoutPanel()
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                WrapContents = true,
                BackColor = Color.White
            };
            this.Controls.Add(flowLayoutPanel);

            List<Movie> movies = databaseHelper.GetAllMovies();

            foreach (Movie movie in movies)
            {
                movie.AddMoviePanel(flowLayoutPanel);
            }

            labelWelcome.Text = "Hoş Geldiniz Sayın: "+SessionManager.LoggedInUsername;







        }
        

        

  

        private void label3_Click_1(object sender, EventArgs e)
        {
            if (SessionManager.LoggedInUserId.HasValue)
            {
                int userId = SessionManager.LoggedInUserId.Value;


                MovieWantToWatch movieWantToWatch = new MovieWantToWatch(userId);
                movieWantToWatch.Show();
            }
            else
            {
                MessageBox.Show("Lütfen önce giriş yapın.");
            }
        }

        private void label4_Click_1(object sender, EventArgs e)
        {
            // Kullanıcı ID'sini alın
            if (SessionManager.LoggedInUserId.HasValue)
            {
                int userId = SessionManager.LoggedInUserId.Value;

                // FavoritedMovies formunu userId parametresiyle oluşturun ve gösterin
                FavoritedMovies favoritedMovies1 = new FavoritedMovies(userId);
                favoritedMovies1.Show();
            }
            else
            {
                MessageBox.Show("Lütfen önce giriş yapın.");
            }
        }

        private void label5_Click_1(object sender, EventArgs e)
        {
            if (SessionManager.LoggedInUserId.HasValue)
            {
                int userId = SessionManager.LoggedInUserId.Value;


                MovieWatched movieWatched = new MovieWatched(userId);
                movieWatched.Show();
            }
            else
            {
                MessageBox.Show("Lütfen önce giriş yapın.");
            }
        }

        private void addMovie_Click(object sender, EventArgs e)
        {
            AddMovie addMovie = new AddMovie();
            addMovie.Show();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
           
                string searchTerm = txtSearch.Text.Trim();

                if (string.IsNullOrEmpty(searchTerm))
                {
                MessageBox.Show("Arama Terimi Girmediğinz için tüm filmler listelenecektir.");
                DatabaseHelper databaseHelper = new DatabaseHelper();
                databaseHelper.GetAllMovies();
                    
                
                   
                }

                // Veritabanında arama yap
                DatabaseHelper dbHelper = new DatabaseHelper();
                List<Movie> searchResults = dbHelper.SearchMovies(searchTerm);

                // Arama sonuçlarını göster
                DisplayMovies(searchResults,flowLayoutPanel);
        }

        private void DisplayMovies(List<Movie> movies,FlowLayoutPanel flowLayoutPanel)
        {
            this.flowLayoutPanel=flowLayoutPanel;
          
            // Mevcut FlowLayoutPanel'i temizle
            flowLayoutPanel.Controls.Clear();

            if (movies.Count == 0)
            {
                MessageBox.Show("Arama sonuçlarına uygun film bulunamadı.");
                return;
            }

            // Her film için bir panel oluştur ve ekle
            foreach (Movie movie in movies)
            {
                movie.AddMoviePanel(flowLayoutPanel); // Movie sınıfındaki AddMoviePanel fonksiyonunu çağır
            }
        }
    }
}
