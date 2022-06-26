using CryptoTerminal.Design;
using CryptoTerminal.Core;
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

namespace CryptoTerminal
{
    //Done
   
    public partial class SuccessWindow : Window
    {
        public SuccessWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void textBoxEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void ButtonClickAccount(object sender, RoutedEventArgs e)
        {
            string mail = textBoxEmail.Text.Trim();
            if (mail != "")
            {
                if (mail.Contains("@") && mail.Contains(".") && (mail.Length >= 5))
                {
                    MessageBox.Show("Авторизация прошла успешно!");
                    Account account = new Account();
                    account.Show();
                    Close();
                    Email.Mail = mail;
                }
                else
                {
                    MessageBox.Show("Почта введена некорректно. Повторите попытку или оставьте поле пустым.");
                    SuccessWindow successWindow = new SuccessWindow();
                    successWindow.Show();
                    Close();
                }
            }
            else
            {
                Account account = new Account();
                account.Show();
                Close();
            }
            
        }
    }
}
