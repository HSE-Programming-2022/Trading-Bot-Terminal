using Azure;
using CryptoTerminal.Core;
using CryptoTerminal.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
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
using System.Net;


namespace CryptoTerminal.Design
{
    public partial class MainWindow : Window
    {
        Context context = new Context();
        public MainWindow()
        {
            
            InitializeComponent();
            //InitialUser();
            
        }
        /*
        private void InitialUser()
        {
            using (Context context = new Context()) //Создание подключения (локальной копии БД)
            {
                //Объявление объектов
                User user1 = new User
                {
                    Id = 1,
                    Username = "Alpha",
                    Password = "123456aboba"
                };
                User user2 = new User
                {
                    Id = 2,
                    Username = "Test",
                    Password = "123"
                };

                //Добавление объектов в контекст
                context.Users.Add(user1);
                context.Users.Add(user2);

                context.SaveChanges(); //Чтобы добавленные объекты отправились в базу данных, нужно вызвать метод, сохраняющий изменения

                MessageBox.Show("Data saved.");
            }
        } */


        private void Button_Auth_Click(object sender, RoutedEventArgs e)
        {

            string username = textBoxUsername.Text.Trim();
            string password = textBoxPassword.Password.Trim();

            //User id = new User();
            User user = new User();
            if ((context.Users.Any(a => a.Username == username)))
            {
                foreach (var id in context.Users)
                {
                    if (id.Username == username)
                    {
                        if (id.Password == password)
                        {
                            SuccessWindow successWindow = new SuccessWindow();
                            successWindow.Show();
                            Close();
                            break;
                        }
                        else
                        {
                            MessageBox.Show("Неверный логин или пароль", "");
                            MainWindow mainwindow = new MainWindow();
                            mainwindow.Show();
                            Close();
                            break;
                        }
                    }
                }
            }

            else
            {
                MessageBox.Show("Неверный логин или пароль", "");
                MainWindow mainwindow = new MainWindow();
                mainwindow.Show();
                Close();
            }
        }

        

        private void Button_Register_Click(object sender, RoutedEventArgs e)
        {
           System.Diagnostics.Process.Start("https://t.me/shev_alex");
        }
        

        private void Button_Quit(object sender, RoutedEventArgs e)
        {
            Close();
            
        }

        private void textBoxUsername_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
