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
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void buttonRegister_Click(object sender, EventArgs e)
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

            // Kullanıcı adı mevcut mu kontrol et
            if (dbHelper.CheckIfUsernameExists(username))
            {
                MessageBox.Show("Bu kullanıcı adı zaten mevcut!");
                return;
            }

            // Yeni kullanıcıyı veritabanına kaydet
            dbHelper.RegisterUser(username, password);
            MessageBox.Show("Kayıt başarılı! Şimdi giriş yapabilirsiniz.");


            this.Hide(); // Register formunu gizler
        }
    }
}
