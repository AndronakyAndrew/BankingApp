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
using System.Windows.Shapes;

namespace BankingApp
{
    /// <summary>
    /// Interaction logic for AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }

        private void btn_Auth_Click(object sender, RoutedEventArgs e)
        {
            string login = Loginbox.Text.Trim();
            string password = Passwordbox.Password.Trim();

            if(login.Length < 5)
            {
                Loginbox.ToolTip = "Неверный логин!";
                Loginbox.Background = Brushes.Red;
            }
            else if(password.Length < 8)
            {
                Passwordbox.ToolTip = "Неверный пароль!";
                Passwordbox.Background = Brushes.Red;
            }
            else
            {
                User user = null;
                using(ApplicationContext db = new ApplicationContext())
                {
                    user = db.Users.Where(b => b.userLogin == login && b.userPassword == password).FirstOrDefault();
                }
                if(user != null)
                {
                    MessageBox.Show("Вы успешно авторизовались!");
                }
                else
                {
                    MessageBox.Show("Пользователь не найден!");
                }
            }
        }

        private void btn_Reg_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Show();
            this.Hide();
        }
    }
}
