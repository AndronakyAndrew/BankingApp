using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp
{
    public class User
    {
        public int userId { get; set; }
        public string userLogin { get; set; }
        public string userPassword { get; set; }
        public string userEmail { get; set; }

        public User() { }

        public User(string userLogin, string userPassword, string userEmail)
        {
            this.userLogin = userLogin;
            this.userPassword = userPassword;
            this.userEmail = userEmail;
        }

    }
}
