using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using tictactoe_wpf.Class;
using tictactoe_wpf.View;

namespace tictactoe_wpf.Model
{
    class AuthWindowViewModel : BaseViewModel
    {
        public ICommand CloseWindow { get; private set; }
        public ICommand MinimizeWindow { get; private set; }
        public ICommand LogUserIn { get; private set; }

        public AuthWindowViewModel()
        {
            CloseWindow = new DelegateCommand(CloseApp);
            MinimizeWindow = new DelegateCommand(MinimizeApp);
            LogUserIn = new DelegateCommand(UserLogin);
        }

        public string Username
        {
            get => UserLifecycle.user.Username;
            set
            {
                UserLifecycle.user.Username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private void UserLogin(object obj)
        {
            if (Username.Length > 0)
            {
                UserLifecycle.user.Username = Username;

                Properties.Settings.Default.username = Username;
                Properties.Settings.Default.firstLaunch = false;
                Properties.Settings.Default.Save();

                var gameWindow = new GameWindow();
                gameWindow.Show();

                Application.Current.Windows[1].Hide();
            }
        }
        void CloseApp(object obj)
        {
            Application.Current.Windows[1].Close();
            Application.Current.Windows[0].Close();
        }
        void MinimizeApp(object obj)
        {
            Application.Current.Windows[1].WindowState = WindowState.Minimized;
        }
    }
}
