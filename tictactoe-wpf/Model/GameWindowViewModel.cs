using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using tictactoe_wpf.Class;

namespace tictactoe_wpf.Model
{
    class GameWindowViewModel : BaseViewModel
    {
        public ICommand CloseWindow { get; private set; }
        public ICommand MinimizeWindow { get; private set; }
        public ICommand FieldClick { get; private set; }
        public ICommand RestartClick { get; private set; }

        private Visibility stackpanelUserdataVisibility = Visibility.Collapsed;
        private bool gridGameEnabled = false;

        public GameWindowViewModel()
        {
            CloseWindow = new DelegateCommand(CloseApp);
            MinimizeWindow = new DelegateCommand(MinimizeApp);
            FieldClick = new DelegateCommand(GridCellClick);
            RestartClick = new DelegateCommand(RestartGame);
        }
        public bool GridGameEnabled
        {
            get => gridGameEnabled;
            set
            {
                gridGameEnabled = value;
                OnPropertyChanged(nameof(GridGameEnabled));
            }
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
            get => UserLifecycle.user.Username;
            set
            {
                UserLifecycle.user.Username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public string Wins
        {
            get => $"Wins: {UserLifecycle.user.WinsAmount}";
            set
            {
                UserLifecycle.user.WinsAmount = int.Parse(value);
                OnPropertyChanged(nameof(Wins));
            }
        }
        public string Loses
        {
            get => $"Loses: {UserLifecycle.user.LosesAmount}";
            set
            {
                UserLifecycle.user.LosesAmount = int.Parse(value);
                OnPropertyChanged(nameof(Loses));
            }
        }
        public string Games
        {
            get => $"Games: {UserLifecycle.user.GamesAmount}";
            set
            {
                UserLifecycle.user.GamesAmount = int.Parse(value);
                OnPropertyChanged(nameof(Games));
            }
        }

        void RestartGame(object obj)
        {

        }
        void GridCellClick(object obj)
        {

        }
        void CloseApp(object obj)
        {
            if (Application.Current.Windows[2] != null) //freezes app?
            {
                Application.Current.Windows[2].Close();
            }

            Application.Current.Windows[1].Close();
            Application.Current.Windows[0].Close();
        }
        void MinimizeApp(object obj)
        {            
            Application.Current.Windows[1].WindowState = WindowState.Minimized;
        }
    }
}
