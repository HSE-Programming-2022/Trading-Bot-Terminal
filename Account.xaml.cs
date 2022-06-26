using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Net;
using System.Web;

namespace CryptoTerminal.Design
{
    public partial class Account : Window
    {
        public string[] pairs = { "BTC/USDT", "ETH/USDT", "SOL/USDT", "XRP/USDT", "LTC/USDT" };
        public Thread ThreadBTC = new Thread(ThreadView.ThreadOnAir);
        public Thread ThreadETH = new Thread(ThreadView.ThreadOnAir);
        public Thread ThreadSOL = new Thread(ThreadView.ThreadOnAir);
        public Thread ThreadXRP = new Thread(ThreadView.ThreadOnAir);
        public Thread ThreadLTC = new Thread(ThreadView.ThreadOnAir);

        
        public Account()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Settings(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings();
            settings.Show();
        }

        private void Button_Quit(object sender, RoutedEventArgs e)
        {
            if (ThreadBTC.IsAlive)
            {
                ThreadBTC.Abort();
            }

            if (ThreadETH.IsAlive)
            {
                ThreadETH.Abort();
            }

            if (ThreadSOL.IsAlive)
            {
                ThreadSOL.Abort();
            }

            if (ThreadXRP.IsAlive)
            {
                ThreadXRP.Abort();
            }

            if (ThreadLTC.IsAlive)
            {
                ThreadLTC.Abort();
            }

            MainWindow mainwindow = new MainWindow();
            mainwindow.Show();
            Close();
        }

        private void TabControl_SelectionChanged_2(object sender, SelectionChangedEventArgs e)
        {

        }

        public void Button_Click_BTC(object sender, RoutedEventArgs e)
        {
            if (!ThreadBTC.IsAlive)
            {
                ThreadBTC.Start(pairs[0]);
            }
            
                
        }

        private void Button_Click_ETH(object sender, RoutedEventArgs e)
        {
            if (!ThreadETH.IsAlive)
            {
                ThreadETH.Start(pairs[1]);
            }
            
            
        }

        private void Button_Click_SOL(object sender, RoutedEventArgs e)
        {
            if (!ThreadSOL.IsAlive)
            {
                ThreadSOL.Start(pairs[2]);
            }
            
            
        }

        private void Button_Click_XRP(object sender, RoutedEventArgs e)
        {
            if (!ThreadXRP.IsAlive)
            {
                ThreadXRP.Start(pairs[3]);
            }
            
            
        }

        private void Button_Click_LTC(object sender, RoutedEventArgs e)
        {
            if (!ThreadLTC.IsAlive)
            {
                ThreadLTC.Start(pairs[4]);
            }
            
            
        }
    }
}