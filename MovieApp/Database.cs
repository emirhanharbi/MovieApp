using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Film_Takip_Uygulaması;
using System.Security.Cryptography;
using System.Data;

namespace MovieApp
{
    public class DatabaseHelper
    {
        private string connectionString = "Server=localhost;Port=3306;Database=moviedatabase;Uid=root;Pwd=emirhan54;";
        // 1. Veritabanına bağlanma fonksiyonu
        public MySqlConnection ConnectToDatabase()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
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
            using (MySqlConnection connection = ConnectToDatabase())
            {
                if (connection == null) return;


                string hashedPassword = password;

                // SQL sorgusunu hazırla
                string query = "INSERT INTO Users (Username, Password) VALUES (@username, @password)";
                MySqlCommand cmd = new MySqlCommand(query, connection);

                // Parametreleri ekle
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", hashedPassword);

                // Sorguyu çalıştır
                cmd.ExecuteNonQuery();
                Console.WriteLine("Yeni kullanıcı başarıyla eklendi!");
            }
        }

        // 3. Login fonksiyonu
        public int? Login(string username, string password)
        {
            using (MySqlConnection connection = ConnectToDatabase())
            {
                if (connection == null) return null;

                // SQL sorgusunu hazırla
                string query = "SELECT UserID, Password FROM Users WHERE Username = @username AND Password = @password";
                MySqlCommand cmd = new MySqlCommand(query, connection);

                // Parametreleri ekle
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);  // Şifreyi düz metinle kontrol ediyoruz

                // Sorguyu çalıştır
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Giriş başarılı, kullanıcı ID'sini döndür
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

        // 4. Tüm filmleri çekme fonksiyonu
        public List<Movie> GetAllMovies()
        {
            List<Movie> movies = new List<Movie>(); // Tüm filmleri tutacak liste

            using (MySqlConnection connection = ConnectToDatabase())
            {
                if (connection == null) return movies; // Bağlantı başarısızsa boş bir liste döndür

                // SQL sorgusunu hazırla
                string query = "SELECT MovieID, MovieName, MovieCategory, MovieIMDB, MovieIMG, MovieURL FROM Movies";
                MySqlCommand cmd = new MySqlCommand(query, connection);

                // Sorguyu çalıştır ve verileri oku
                using (MySqlDataReader reader = cmd.ExecuteReader())
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


        // 5. Oturum açmış ait favori filmleri çekme fonksiyonu
        public List<Movie> GetFavoriteMovies(int userId)
        {
            List<Movie> favoriteMovies = new List<Movie>();  // Favori filmleri tutacak liste

            using (MySqlConnection connection = ConnectToDatabase())
            {
                if (connection == null) return favoriteMovies; // Bağlantı başarısızsa boş liste döndür

                // SQL sorgusunu hazırla
                string query = "SELECT m.MovieID, m.MovieName, m.MovieCategory, m.MovieIMDB, m.MovieIMG, m.MovieURL " +
                               "FROM userfavorites f " +
                               "JOIN Movies m ON f.MovieID = m.MovieID " +
                               "WHERE f.UserID = @userId";
                MySqlCommand cmd = new MySqlCommand(query, connection);

                // Parametreyi ekle
                cmd.Parameters.AddWithValue("@userId", userId);

                // Sorguyu çalıştır
                using (MySqlDataReader reader = cmd.ExecuteReader())
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
        // 5. Oturum açmış ait favori filmlerinden silme fonksiyonu
        



        // 6. Oturum açmış ait izlediği filmleri çekme fonksiyonu
        public List<Movie> GetWatchedMovies(int userId)
        {
            List<Movie> listWatchedMovies = new List<Movie>();  // Favori filmleri tutacak liste

            using (MySqlConnection connection = ConnectToDatabase())
            {
                if (connection == null) return listWatchedMovies; // Bağlantı başarısızsa boş liste döndür

                // SQL sorgusunu hazırla
                string query = "SELECT m.MovieID, m.MovieName, m.MovieCategory, m.MovieIMDB, m.MovieIMG, m.MovieURL " +
                               "FROM userwatched f " +
                               "JOIN Movies m ON f.MovieID = m.MovieID " +
                               "WHERE f.UserID = @userId";
                MySqlCommand cmd = new MySqlCommand(query, connection);

                // Parametreyi ekle
                cmd.Parameters.AddWithValue("@userId", userId);

                // Sorguyu çalıştır
                using (MySqlDataReader reader = cmd.ExecuteReader())
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

            return listWatchedMovies; // Favori filmleri döndür
        }

        // 6. Oturum açmış ait izlediği filmleri silme fonksiyonu
        


        // 7. Oturum açmış ait izlemek istediği filmleri çekme fonksiyonu
        public List<Movie> GetWantToWatchMovies(int userId)
        {
            List<Movie> ListwantToWatchMovies = new List<Movie>();  // Favori filmleri tutacak liste

            using (MySqlConnection connection = ConnectToDatabase())
            {
                if (connection == null) return ListwantToWatchMovies; // Bağlantı başarısızsa boş liste döndür

                // SQL sorgusunu hazırla
                string query = "SELECT m.MovieID, m.MovieName, m.MovieCategory, m.MovieIMDB, m.MovieIMG, m.MovieURL " +
                               "FROM userwanttowatch f " +
                               "JOIN Movies m ON f.MovieID = m.MovieID " +
                               "WHERE f.UserID = @userId";
                MySqlCommand cmd = new MySqlCommand(query, connection);

                // Parametreyi ekle
                cmd.Parameters.AddWithValue("@userId", userId);

                // Sorguyu çalıştır
                using (MySqlDataReader reader = cmd.ExecuteReader())
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
                        ListwantToWatchMovies.Add(movie);
                    }
                }
            }

            return ListwantToWatchMovies; // Favori filmleri döndür
        }
        //Oturum açmış ait izlemek istediği filmleri silme fonksiyonu
       


        // 8. Oturum açmış kullanıcının seçtiği filmi favoriye ekleme fonksiyonu
        public void AddToFavorites(int userId, int movieId)
        {
            using (MySqlConnection connection = ConnectToDatabase())
            {
                if (connection == null) return;

                // SQL sorgusunu hazırla
                string query = "INSERT INTO userfavorites (UserID, MovieID) VALUES (@userId, @movieId)";
                MySqlCommand cmd = new MySqlCommand(query, connection);

                // Parametreleri ekle
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@movieId", movieId);

                // Parametre türlerini kontrol et (isteğe bağlı)
                cmd.Parameters["@userId"].DbType = DbType.Int32;
                cmd.Parameters["@movieId"].DbType = DbType.Int32;

                // Sorguyu çalıştır
                try
                {
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Film favorilere eklendi!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Hata: " + ex.Message);
                }
            }
        }


        // 9. Oturum açmış kullanıcının seçtiği filmi izlenecek filmlere ekleme fonksiyonu

        public void AddWantToWatch(int userId, int movieId)
        {
            using (MySqlConnection connection = ConnectToDatabase())
            {
                if (connection == null) return;

                // SQL sorgusunu hazırla
                string query = "INSERT INTO userwanttowatch (UserID, MovieID) VALUES (@userId, @movieId)";
                MySqlCommand cmd = new MySqlCommand(query, connection);

                // Parametreleri ekle
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@movieId", movieId);

                // Parametre türlerini kontrol et (isteğe bağlı)
                cmd.Parameters["@userId"].DbType = DbType.Int32;
                cmd.Parameters["@movieId"].DbType = DbType.Int32;

                // Sorguyu çalıştır
                try
                {
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Film İzleneceklere eklendi!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Hata: " + ex.Message);
                }
            }
        }


        // 10. Oturum açmış kullanıcının seçtiği filmi izlenen filmlere ekleme fonksiyonu
        public void AddToWatched(int userId, int movieId)
        {
            using (MySqlConnection connection = ConnectToDatabase())
            {
                if (connection == null) return;

                // SQL sorgusunu hazırla
                string query = "INSERT INTO userwatched (UserID, MovieID) VALUES (@userId, @movieId)";
                MySqlCommand cmd = new MySqlCommand(query, connection);

                // Parametreleri ekle
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@movieId", movieId);

                // Parametre türlerini kontrol et (isteğe bağlı)
                cmd.Parameters["@userId"].DbType = DbType.Int32;
                cmd.Parameters["@movieId"].DbType = DbType.Int32;

                // Sorguyu çalıştır
                try
                {
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Film İzleneceklere eklendi!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Hata: " + ex.Message);
                }
            }
        }
        // Kullanıcı adının veritabanında var olup olmadığını kontrol eden fonksiyon
        public bool CheckIfUsernameExists(string username)
        {
            using (MySqlConnection connection = ConnectToDatabase())
            {
                if (connection == null)
                {
                    Console.WriteLine("Veritabanına bağlanılamadı!");
                    return false;
                }

                try
                {
                    // SQL sorgusunu hazırla
                    string query = "SELECT COUNT(*) FROM Users WHERE Username = @username";
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    // Parametreyi ekle
                    cmd.Parameters.AddWithValue("@username", username);

                    // Sonuç al
                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    // Kullanıcı adı varsa, count 1 veya daha büyük olur
                    return count > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Hata: " + ex.Message);
                    return false;
                }
            }
        }
        public void AddMovie(string movieName, string movieIMG, double movieIMDB, string movieCategory, string movieURL)
        {
            using (MySqlConnection connection = ConnectToDatabase())
            {
                if (connection == null) return;

                // SQL sorgusunu hazırla
                string query = "INSERT INTO Movies (MovieName, MovieIMG, MovieIMDB, MovieCategory, MovieURL) " +
                               "VALUES (@movieName, @movieIMG, @movieIMDB, @movieCategory, @movieURL)";
                MySqlCommand cmd = new MySqlCommand(query, connection);

                // Parametreleri ekle
                cmd.Parameters.AddWithValue("@movieName", movieName);
                cmd.Parameters.AddWithValue("@movieIMG", movieIMG);
                cmd.Parameters.AddWithValue("@movieIMDB", movieIMDB);
                cmd.Parameters.AddWithValue("@movieCategory", movieCategory);
                cmd.Parameters.AddWithValue("@movieURL", movieURL);

                cmd.ExecuteNonQuery();
                Console.WriteLine("Film veritabanına başarıyla eklendi!");
            }
        }


    }



}

