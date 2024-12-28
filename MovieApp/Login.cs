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
    public partial class Login : Form
    {
        string username;
        int password;
        DatabaseHelper dbHelper = new DatabaseHelper();
        public Login()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            // Kullanıcı adı ve şifreyi TextBox'lardan al
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            // Boş giriş kontrolü
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Kullanıcı adı ve şifre boş olamaz!");
                return;
            }

            // DatabaseHelper sınıfını kullanarak işlemleri gerçekleştir
            DatabaseHelper dbHelper = new DatabaseHelper();

            int? userId = dbHelper.Login(username, password);  // Şifreyi düz metinle kontrol et

            if (userId.HasValue)
            {
                // Kullanıcı doğrulandıysa, session'a userId'yi kaydediyoruz
                SessionManager.LoggedInUserId = userId.Value;
                SessionManager.LoggedInUsername = username;

                // Giriş başarılı mesajı
                MessageBox.Show("Giriş başarılı! Hoş geldiniz, " + username);

                // Ana ekrana geçiş yapılabilir
                this.Hide(); // Login formu gizlenir
                Homepage homepage = new Homepage();
                homepage.Show();
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre yanlış!");
            }
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
           

            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();

        }
    }
    
}
