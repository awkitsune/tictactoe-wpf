using System;
using System.Threading;
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

namespace tictactoe_wpf.View
{
    /// <summary>
    /// Логика взаимодействия для SplashWindow.xaml
    /// </summary>
    public partial class SplashWindow : Window
    {
        public SplashWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var thread = new Thread(WaitingThread);
            thread.Start();
        }
        private void WaitingThread()
        {
            Thread.Sleep(3000);
            Application.Current.Dispatcher.Invoke(() =>
            {
                var main = new GameWindow();
                main.Show();

                this.Hide();
            });
        }
    }
}
