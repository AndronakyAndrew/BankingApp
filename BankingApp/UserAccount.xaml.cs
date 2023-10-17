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
    /// Interaction logic for UserAccount.xaml
    /// </summary>
    public partial class UserAccount : Window
    {

        private List<BankAccount> accounts = new List<BankAccount>();
        ApplicationContext db;

        public UserAccount()
        {
            InitializeComponent();

            db = new ApplicationContext();
        }

        private void CreateAccountBtn_Click(object sender, RoutedEventArgs e)
        {
            // Get user input for account creation
            string accountHolder = accountHolderTextBox.Text.Trim();

            // Generate a random account number
            string accountNumber = Guid.NewGuid().ToString("N").Substring(0, 10);

            // Create a new bank account
            var newAccount = new BankAccount
            {
                AccountNumber = accountNumber,
                AccountHolder = accountHolder,
                Balance = 0
            };

            accounts.Add(newAccount);

            // Update the UI to display the new account
            AccountListBox.Items.Add(newAccount);

            //Создание новой учетной записи и добавления ее в базу данных
            BankAccount account = new BankAccount(accountNumber, accountHolder, 0);

            db.BankAccounts.Add(account);
            db.SaveChanges();

        }

        private void DepositBtn_Click(object sender, RoutedEventArgs e)
        {
            if (AccountListBox.SelectedItem != null)
            {
                BankAccount selectedAccount = (BankAccount)AccountListBox.SelectedItem;
                decimal depositAmount;

                if (decimal.TryParse(DepositTextBox.Text, out depositAmount) && depositAmount > 10)
                {
                    selectedAccount.Balance += depositAmount;
                    UpdateAccountDetails(selectedAccount);
                    AddTransactionHistory(selectedAccount, $"Deposited ${depositAmount}");
                }
            }
        }

        private void WithDrawBtn_Click(object sender, RoutedEventArgs e)
        {
            if(AccountListBox.SelectedItem != null)
            {
                BankAccount selectedAccount = (BankAccount)AccountListBox.SelectedItem;
                decimal withdrawAmount;

                if(decimal.TryParse(WithDrawBox.Text, out withdrawAmount) && withdrawAmount > 10 && withdrawAmount <= selectedAccount.Balance)
                {
                    selectedAccount.Balance -= withdrawAmount;
                    UpdateAccountDetails(selectedAccount);
                    AddTransactionHistory(selectedAccount, $"Withdrawn ${withdrawAmount}");
                }
            }   
        }

        private void UpdateAccountDetails(BankAccount account)
        {
            // Update account details in the UI
            AccountDetailsTextBox.Text = $"Account Number: {account.AccountNumber}\nAccount Holder: {account.AccountHolder}\nBalance: ${account.Balance:f2}";
        }

        private void AddTransactionHistory(BankAccount account, string transaction)
        {
            // Add transaction history to the UI
            TransactionHistoryTextBox.Document.Blocks.Add(new Paragraph(new Run(transaction)));
        }

    }
}
