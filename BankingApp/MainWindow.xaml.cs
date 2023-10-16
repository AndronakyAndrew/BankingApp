using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BankingApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {

        ApplicationContext db;

        public RegistrationWindow()
        {
            InitializeComponent();

            db = new ApplicationContext();
        }

        private void btn_Auth_window_click(object sender, RoutedEventArgs e)
        {
            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
            this.Hide();
        }

        private void btn_Reg_Click(object sender, RoutedEventArgs e)
        {
            string login = Loginbox.Text.Trim();
            string password = Passwordbox.Password.Trim();
            string password2 = Passwordbox_2.Password.Trim();
            string email = Emailbox.Text.Trim().ToLower();

            if (login.Length < 5)
            {
                Loginbox.ToolTip = "Логин должен содержать больше 5 символов!";
                Loginbox.Background = Brushes.Red;
            }
            else if (password.Length < 8)
            {
                Passwordbox.ToolTip = "Пароль должен содержать больше 8 символов!";
                Passwordbox.Background = Brushes.Red;
            }
            else if (password != password2)
            {
                Passwordbox_2.ToolTip = "Пароли не совпадают!";
                Passwordbox_2.Background = Brushes.Red;
            }
            else if (!email.Contains("@") || !email.Contains("."))
            {
                Emailbox.ToolTip = "Почта введена неверно!";
                Emailbox.Background = Brushes.Red;
            }
            else
            {
                Loginbox.ToolTip = "";
                Loginbox.Background = Brushes.Transparent;
                Passwordbox.ToolTip = "";
                Passwordbox.Background = Brushes.Transparent;
                Passwordbox_2.ToolTip = "";
                Passwordbox_2.Background = Brushes.Transparent;
                Emailbox.ToolTip = "";
                Emailbox.Background = Brushes.Transparent;

                MessageBox.Show("Регистрация прошла успешно!");

                User user = new User(login, password, email);

                db.Users.Add(user);
                db.SaveChanges();

                AuthWindow authWindow = new AuthWindow();
                authWindow.Show();
                this.Hide();

            }
        }
    }
}
