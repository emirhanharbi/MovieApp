using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using Film_Takip_Uygulaması;
using System.Windows.Forms;

namespace MovieApp
{
    public class DatabaseHelper
    {
        private string connectionString = @"Data Source=moviedatabase.db;Version=3;";  // SQLite bağlantısı
        // 1. Veritabanına bağlanma fonksiyonu
        public SQLiteConnection ConnectToDatabase()
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            try
            {
                connection.Open();
                Console.WriteLine("Veritabanına başarıyla bağlanıldı!");
                return connection;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Veritabanına bağlanırken hata oluştu: " + ex.Message);
                return null;
            }
        }

        // 2. Üye olma fonksiyonu
        public void RegisterUser(string username, string password)
        {
            using (SQLiteConnection connection = ConnectToDatabase())
            {
                if (connection == null) return;

                // SQL sorgusunu hazırla
                string query = "INSERT INTO Users (Username, Password) VALUES (@username, @password)";
                SQLiteCommand cmd = new SQLiteCommand(query, connection);

                // Parametreleri ekle
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                // Sorguyu çalıştır
                cmd.ExecuteNonQuery();
                
            }
        }

        // 3. Login fonksiyonu
        public int? Login(string username, string password)
        {
            using (SQLiteConnection connection = ConnectToDatabase())
            {
                if (connection == null) return null;

                // SQL sorgusunu hazırla
                string query = "SELECT UserID FROM Users WHERE Username = @username AND Password = @password";
                SQLiteCommand cmd = new SQLiteCommand(query, connection);

                // Parametreleri ekle
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                // Sorguyu çalıştır
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Giriş başarılı ise kullanıcı ID'sini döndürüyoruz
                        int userId = Convert.ToInt32(reader["UserID"]);
                        Console.WriteLine("Giriş başarılı!");
                        return userId;  // Kullanıcının ID'sini döndürüyoruz
                    }
                    else
                    {
                        // Giriş başarısız 
                        Console.WriteLine("Kullanıcı adı veya şifre yanlış!");
                        return null;  // Hatalı giriş durumunda null döndürüyoruz
                    }
                }
            }
        }
        // Oturum açmış ait izlemek istediği filmleri çekme fonksiyonu
        public List<Movie> GetWantToWatchMovies(int userId)
        {
            List<Movie> listWantToWatchMovies = new List<Movie>();  // İzlemek istediği filmleri tutacak liste

            using (SQLiteConnection connection = ConnectToDatabase())
            {
                if (connection == null) return listWantToWatchMovies; // Bağlantı başarısızsa boş liste döndür

                // SQL sorgusunu hazırla
                string query = "SELECT m.MovieID, m.MovieName, m.MovieCategory, m.MovieIMDB, m.MovieIMG, m.MovieURL " +
                               "FROM userwanttowatch f " +
                               "JOIN Movies m ON f.MovieID = m.MovieID " +
                               "WHERE f.UserID = @userId";
                SQLiteCommand cmd = new SQLiteCommand(query, connection);

                // Parametreyi ekle
                cmd.Parameters.AddWithValue("@userId", userId);

                // Sorguyu çalıştır
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Veritabanından gelen bilgileri Movie nesnesine dönüştür
                        Movie movie = new Movie(
                            id: Convert.ToInt32(reader["MovieID"]),
                            movieName: reader["MovieName"].ToString(),
                            movieIMG: reader["MovieIMG"].ToString(),
                            movieIMDB: Convert.ToDouble(reader["MovieIMDB"]),
                            movieCategory: reader["MovieCategory"].ToString(),
                            movieURL: reader["MovieURL"].ToString()
                        );

                        // Listeye ekle
                        listWantToWatchMovies.Add(movie);
                    }
                }
            }

            return listWantToWatchMovies; // İzlemek istediği filmleri döndür
        }


        // 4. Tüm filmleri çekme fonksiyonu
        public List<Movie> GetAllMovies()
        {
            List<Movie> movies = new List<Movie>(); // Tüm filmleri tutacak liste

            using (SQLiteConnection connection = ConnectToDatabase())
            {
                if (connection == null) return movies; // Bağlantı başarısızsa boş bir liste döndür

                // SQL sorgusunu hazırla
                string query = "SELECT MovieID, MovieName, MovieCategory, MovieIMDB, MovieIMG, MovieURL FROM Movies";
                SQLiteCommand cmd = new SQLiteCommand(query, connection);

                // Sorguyu çalıştır ve verileri oku
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Satırdaki verileri al ve bir Movie nesnesine dönüştür
                        Movie movie = new Movie(
                            id: Convert.ToInt32(reader["MovieID"]),
                            movieName: reader["MovieName"].ToString(),
                            movieIMG: reader["MovieIMG"].ToString(),
                            movieIMDB: Convert.ToDouble(reader["MovieIMDB"]),
                            movieCategory: reader["MovieCategory"].ToString(),
                            movieURL: reader["MovieURL"].ToString()
                        );

                        // Listeye ekle
                        movies.Add(movie);
                    }
                }
            }

            return movies; // Filmlerin listesini döndür
        }

        // 5. Favori filmleri çekme fonksiyonu
        public List<Movie> GetFavoriteMovies(int userId)
        {
            List<Movie> favoriteMovies = new List<Movie>();  // Favori filmleri tutacak liste

            using (SQLiteConnection connection = ConnectToDatabase())
            {
                if (connection == null) return favoriteMovies; // Bağlantı başarısızsa boş liste döndür

                // SQL sorgusunu hazırla
                string query = "SELECT m.MovieID, m.MovieName, m.MovieCategory, m.MovieIMDB, m.MovieIMG, m.MovieURL " +
                               "FROM userfavorites f " +
                               "JOIN Movies m ON f.MovieID = m.MovieID " +
                               "WHERE f.UserID = @userId";
                SQLiteCommand cmd = new SQLiteCommand(query, connection);

                // Parametreyi ekle
                cmd.Parameters.AddWithValue("@userId", userId);

                // Sorguyu çalıştır
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Veritabanından gelen bilgileri Movie nesnesine dönüştür
                        Movie movie = new Movie(
                            id: Convert.ToInt32(reader["MovieID"]),
                            movieName: reader["MovieName"].ToString(),
                            movieIMG: reader["MovieIMG"].ToString(),
                            movieIMDB: Convert.ToDouble(reader["MovieIMDB"]),
                            movieCategory: reader["MovieCategory"].ToString(),
                            movieURL: reader["MovieURL"].ToString()
                        );

                        // Listeye ekle
                        favoriteMovies.Add(movie);
                    }
                }
            }

            return favoriteMovies; // Favori filmleri döndür
        }

        // 6. Oturum açmış ait izlediği filmleri çekme fonksiyonu
        public List<Movie> GetWatchedMovies(int userId)
        {
            List<Movie> listWatchedMovies = new List<Movie>();  // İzlediği filmleri tutacak liste

            using (SQLiteConnection connection = ConnectToDatabase())
            {
                if (connection == null) return listWatchedMovies; // Bağlantı başarısızsa boş liste döndür

                // SQL sorgusunu hazırla
                string query = "SELECT m.MovieID, m.MovieName, m.MovieCategory, m.MovieIMDB, m.MovieIMG, m.MovieURL " +
                               "FROM userwatched f " +
                               "JOIN Movies m ON f.MovieID = m.MovieID " +
                               "WHERE f.UserID = @userId";
                SQLiteCommand cmd = new SQLiteCommand(query, connection);

                // Parametreyi ekle
                cmd.Parameters.AddWithValue("@userId", userId);

                // Sorguyu çalıştır
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Veritabanından gelen bilgileri Movie nesnesine dönüştür
                        Movie movie = new Movie(
                            id: Convert.ToInt32(reader["MovieID"]),
                            movieName: reader["MovieName"].ToString(),
                            movieIMG: reader["MovieIMG"].ToString(),
                            movieIMDB: Convert.ToDouble(reader["MovieIMDB"]),
                            movieCategory: reader["MovieCategory"].ToString(),
                            movieURL: reader["MovieURL"].ToString()
                        );

                        // Listeye ekle
                        listWatchedMovies.Add(movie);
                    }
                }
            }

            return listWatchedMovies; // İzlenen filmleri döndür
        }

        // 7. Kullanıcının seçtiği filmi favorilere ekleme fonksiyonu
        public void AddToFavorites(int userId, int movieId)
        {
            using (SQLiteConnection connection = ConnectToDatabase())
            {
                if (connection == null) return;

                // SQL sorgusunu hazırla
                string query = "INSERT INTO userfavorites (UserID, MovieID) VALUES (@userId, @movieId)";
                SQLiteCommand cmd = new SQLiteCommand(query, connection);

                // Parametreleri ekle
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@movieId", movieId);

                // Sorguyu çalıştır
                cmd.ExecuteNonQuery();
                Console.WriteLine("Film favorilere eklendi!");
            }
        }

        // 8. Kullanıcının seçtiği filmi izlenecek filmlere ekleme fonksiyonu
        public void AddWantToWatch(int userId, int movieId)
        {
            using (SQLiteConnection connection = ConnectToDatabase())
            {
                if (connection == null) return;

                // SQL sorgusunu hazırla
                string query = "INSERT INTO userwanttowatch (UserID, MovieID) VALUES (@userId, @movieId)";
                SQLiteCommand cmd = new SQLiteCommand(query, connection);

                // Parametreleri ekle
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@movieId", movieId);

                // Sorguyu çalıştır
                cmd.ExecuteNonQuery();
                Console.WriteLine("Film izlenecekler listesine eklendi!");
            }
        }

        // 9. Kullanıcının seçtiği filmi izlenen filmlere ekleme fonksiyonu
        public void AddToWatched(int userId, int movieId)
        {
            using (SQLiteConnection connection = ConnectToDatabase())
            {
                if (connection == null) return;

                // SQL sorgusunu hazırla
                string query = "INSERT INTO userwatched (UserID, MovieID) VALUES (@userId, @movieId)";
                SQLiteCommand cmd = new SQLiteCommand(query, connection);

                // Parametreleri ekle
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@movieId", movieId);

                // Sorguyu çalıştır
                cmd.ExecuteNonQuery();
                Console.WriteLine("Film izlenenler listesine eklendi!");
            }
        }
        //Mevcut Filmi Silme Fonksiyonu
        public void MovieDeleteOnList(int userId,int movieId) {
        using (SQLiteConnection connection = ConnectToDatabase())
            {
                if(connection == null) return;

                //SQL Sorgusu Hazırla
                string query = "Delete From Movies Where MovieID=@movieId";
                SQLiteCommand cmd = new SQLiteCommand(query, connection);



                //Parametreleri ekle
                cmd.Parameters.AddWithValue("userId", userId);
                cmd.Parameters.AddWithValue("@movieID", movieId);
                

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    
                    MessageBox.Show(ex.Message+"Silme işleminde bir hata oldu");
                }
            }

        }
        // 10. Kullanıcı adının veritabanında var olup olmadığını kontrol etme fonksiyonu
        public bool CheckIfUsernameExists(string username)
        {
            using (SQLiteConnection connection = ConnectToDatabase())
            {
                if (connection == null) return false;

                // SQL sorgusunu hazırla
                string query = "SELECT COUNT(*) FROM Users WHERE Username = @username";
                SQLiteCommand cmd = new SQLiteCommand(query, connection);

                // Parametreyi ekle
                cmd.Parameters.AddWithValue("@username", username);

                // Sonuç al
                int count = Convert.ToInt32(cmd.ExecuteScalar());

                return count > 0; // Eğer kullanıcı adı varsa, count 1 veya daha büyük olur
            }
        }

        // 11. Movie verilerini eklemek
        public void AddMovie(string movieName, string movieIMG, double movieIMDB, string movieCategory, string movieURL)
        {
            using (SQLiteConnection connection = ConnectToDatabase())
            {
                if (connection == null) return;

                // SQL sorgusunu hazırla
                string query = "INSERT INTO Movies (MovieName, MovieIMG, MovieIMDB, MovieCategory, MovieURL) VALUES (@movieName, @movieIMG, @movieIMDB, @movieCategory, @movieURL)";
                SQLiteCommand cmd = new SQLiteCommand(query, connection);

                // Parametreleri ekle
                cmd.Parameters.AddWithValue("@movieName", movieName);
                cmd.Parameters.AddWithValue("@movieIMG", movieIMG);
                cmd.Parameters.AddWithValue("@movieIMDB", movieIMDB);
                cmd.Parameters.AddWithValue("@movieCategory", movieCategory);
                cmd.Parameters.AddWithValue("@movieURL", movieURL);

                // Sorguyu çalıştır
                cmd.ExecuteNonQuery();
                Console.WriteLine("Film veritabanına başarıyla eklendi!");
            }
        }

        //Film Arama Fonksiyonu
        public List<Movie> SearchMovies(string searchTerm)
        {
            List<Movie> movies = new List<Movie>();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // MovieName sütununda arama yapan SQL sorgusu
                string query = "SELECT MovieID, MovieName, MovieIMG, MovieIMDB, MovieCategory, MovieURL " +
                               "FROM Movies WHERE MovieName LIKE @searchTerm";
                SQLiteCommand cmd = new SQLiteCommand(query, connection);

                // Parametre ekle
                cmd.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");

                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Movie movie = new Movie(
                            id: Convert.ToInt32(reader["MovieID"]),
                            movieName: reader["MovieName"].ToString(),
                            movieIMG: reader["MovieIMG"].ToString(),
                            movieIMDB: Convert.ToDouble(reader["MovieIMDB"]),
                            movieCategory: reader["MovieCategory"].ToString(),
                            movieURL: reader["MovieURL"].ToString()
                        );

                        movies.Add(movie);
                    }
                }
            }

            return movies;
        }

    }
}
