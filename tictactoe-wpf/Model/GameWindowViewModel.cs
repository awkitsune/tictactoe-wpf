using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace tictactoe_wpf.Model
{
    class GameWindowViewModel : BaseViewModel
    {
        public ICommand CloseWindow { get; private set; }
        public ICommand MinimizeWindow { get; private set; }

        private string username = "username";
        private int wins = 0;
        private int loses = 0;
        private int games = 0;
        private Visibility stackpanelUserdataVisibility = Visibility.Collapsed;

        public GameWindowViewModel()
        {
            CloseWindow = new DelegateCommand(CloseApp);
            MinimizeWindow = new DelegateCommand(MinimizeApp);
        }
        public Visibility SPUserdataVisibility
        {
            get => stackpanelUserdataVisibility;
            set
            {
                stackpanelUserdataVisibility = value;
                OnPropertyChanged(nameof(SPUserdataVisibility));
            }
        }
        public string Username
        {
            get => username;
            set
            {
                username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public string Wins
        {
            get => $"Wins: {wins}";
            set
            {
                wins = int.Parse(value);
                OnPropertyChanged(nameof(Wins));
            }
        }
        public string Loses
        {
            get => $"Loses: {loses}";
            set
            {
                loses = int.Parse(value);
                OnPropertyChanged(nameof(Loses));
            }
        }
        public string Games
        {
            get => $"Games: {games}";
            set
            {
                games = int.Parse(value);
                OnPropertyChanged(nameof(Games));
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
