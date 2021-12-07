using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using tictactoe_wpf.Class;
using tictactoe_wpf.View;

namespace tictactoe_wpf.Model
{
    class GameWindowViewModel : BaseViewModel
    {
        public ICommand CloseWindow { get; private set; }
        public ICommand MinimizeWindow { get; private set; }
        public ICommand FieldClick { get; private set; }
        public ICommand RestartClick { get; private set; }
        public ICommand LogoutClick { get; private set; }

        private bool gridGameEnabled = true;

        private GameField gameField = new GameField();

        public GameWindowViewModel()
        {
            CloseWindow = new DelegateCommand(CloseApp);
            MinimizeWindow = new DelegateCommand(MinimizeApp);
            FieldClick = new DelegateCommand(GridCellClick);
            RestartClick = new DelegateCommand(RestartGame);
            LogoutClick = new DelegateCommand(LogoutUser);
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
            GridGameEnabled = true;

            if (!gameField.isWon)
            {
                MessageBoxResult result = MessageBox.Show(
                       "Вы реально хотите перезапустить игру?",
                       "Подтверждение",
                       MessageBoxButton.YesNo,
                       MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    gameField.Flush();
                    UpdateField();
                    gameField.isWon = false;
                    gameField.isPlayerWon = false;
                    UserLifecycle.user.GamesAmount++;
                    Games = UserLifecycle.user.GamesAmount.ToString();
                }
            }
            else
            {
                gameField.Flush();
                UpdateField();
                gameField.isWon = false;
                gameField.isPlayerWon = false;
            }
        }
        void WinMessage()
        {
            if (gameField.isWon &&
                gameField.WhoWin() == GameField.playerMark)
            {
                MessageBox.Show(
                    "Победил игрок!",
                    "Конец игры",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                UserLifecycle.user.WinsAmount++;
                UserLifecycle.user.GamesAmount++;
            }
            if (gameField.isWon &&
                gameField.WhoWin() == GameField.aiMark)
            {
                MessageBox.Show(
                    "Победил компьютер!",
                    "Конец игры",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                UserLifecycle.user.LosesAmount++;
                UserLifecycle.user.GamesAmount++;
            }
            Wins = UserLifecycle.user.WinsAmount.ToString();
            Loses = UserLifecycle.user.LosesAmount.ToString();
            Games = UserLifecycle.user.GamesAmount.ToString();
        }

        void GridCellClick(object parameter)
        {
            if (!gameField.isWon)
            {
                switch (parameter.ToString())
                {
                    case "b00":
                        gameField.PlayerDraw(new Pair<int, int>(0, 0));
                        break;
                    case "b10":
                        gameField.PlayerDraw(new Pair<int, int>(1, 0));
                        break;
                    case "b20":
                        gameField.PlayerDraw(new Pair<int, int>(2, 0));
                        break;

                    case "b01":
                        gameField.PlayerDraw(new Pair<int, int>(0, 1));
                        break;
                    case "b11":
                        gameField.PlayerDraw(new Pair<int, int>(1, 1));
                        break;
                    case "b21":
                        gameField.PlayerDraw(new Pair<int, int>(2, 1));
                        break;

                    case "b02":
                        gameField.PlayerDraw(new Pair<int, int>(0, 2));
                        break;
                    case "b12":
                        gameField.PlayerDraw(new Pair<int, int>(1, 2));
                        break;
                    case "b22":
                        gameField.PlayerDraw(new Pair<int, int>(2, 2));
                        break;

                    default:
                        break;
                }

                UpdateField();
                WinMessage();
            }
        }

        private void UpdateField()
        {
            GameWindow gameWindow = null;

            foreach (Window item in Application.Current.Windows)
            {
                if (item is GameWindow)
                {
                    gameWindow = item as GameWindow;
                }
            }

            foreach (var image in FindVisualChildren<Image>(gameWindow))
            {
                switch (image.Name)
                {
                    case "i00":
                        switch (gameField.field[0, 0])
                        {
                            case GameField.playerMark:
                                image.Source = new BitmapImage(new Uri("/Assets/outline_panorama_fish_eye_white_36dp.png", UriKind.Relative));
                                break;
                            case GameField.aiMark:
                                image.Source = new BitmapImage(new Uri("/Assets/outline_close_white_36dp.png", UriKind.Relative));
                                break;
                            case GameField.neutralMark:
                                image.Source = new BitmapImage(new Uri("/Assets/empty.png", UriKind.Relative));
                                break;
                            default:
                                break;
                        }
                        break;
                    case "i10":
                        switch (gameField.field[1, 0])
                        {
                            case GameField.playerMark:
                                image.Source = new BitmapImage(new Uri("/Assets/outline_panorama_fish_eye_white_36dp.png", UriKind.Relative));
                                break;
                            case GameField.aiMark:
                                image.Source = new BitmapImage(new Uri("/Assets/outline_close_white_36dp.png", UriKind.Relative));
                                break;
                            case GameField.neutralMark:
                                image.Source = new BitmapImage(new Uri("/Assets/empty.png", UriKind.Relative));
                                break;
                            default:
                                break;
                        }
                        break;
                    case "i20":
                        switch (gameField.field[2, 0])
                        {
                            case GameField.playerMark:
                                image.Source = new BitmapImage(new Uri("/Assets/outline_panorama_fish_eye_white_36dp.png", UriKind.Relative));
                                break;
                            case GameField.aiMark:
                                image.Source = new BitmapImage(new Uri("/Assets/outline_close_white_36dp.png", UriKind.Relative));
                                break;
                            case GameField.neutralMark:
                                image.Source = new BitmapImage(new Uri("/Assets/empty.png", UriKind.Relative));
                                break;
                            default:
                                break;
                        }
                        break;

                    case "i01":
                        switch (gameField.field[0, 1])
                        {
                            case GameField.playerMark:
                                image.Source = new BitmapImage(new Uri("/Assets/outline_panorama_fish_eye_white_36dp.png", UriKind.Relative));
                                break;
                            case GameField.aiMark:
                                image.Source = new BitmapImage(new Uri("/Assets/outline_close_white_36dp.png", UriKind.Relative));
                                break;
                            case GameField.neutralMark:
                                image.Source = new BitmapImage(new Uri("/Assets/empty.png", UriKind.Relative));
                                break;
                            default:
                                break;
                        }
                        break;
                    case "i11":
                        switch (gameField.field[1, 1])
                        {
                            case GameField.playerMark:
                                image.Source = new BitmapImage(new Uri("/Assets/outline_panorama_fish_eye_white_36dp.png", UriKind.Relative));
                                break;
                            case GameField.aiMark:
                                image.Source = new BitmapImage(new Uri("/Assets/outline_close_white_36dp.png", UriKind.Relative));
                                break;
                            case GameField.neutralMark:
                                image.Source = new BitmapImage(new Uri("/Assets/empty.png", UriKind.Relative));
                                break;
                            default:
                                break;
                        }
                        break;
                    case "i21":
                        switch (gameField.field[2, 1])
                        {
                            case GameField.playerMark:
                                image.Source = new BitmapImage(new Uri("/Assets/outline_panorama_fish_eye_white_36dp.png", UriKind.Relative));
                                break;
                            case GameField.aiMark:
                                image.Source = new BitmapImage(new Uri("/Assets/outline_close_white_36dp.png", UriKind.Relative));
                                break;
                            case GameField.neutralMark:
                                image.Source = new BitmapImage(new Uri("/Assets/empty.png", UriKind.Relative));
                                break;
                            default:
                                break;
                        }
                        break;

                    case "i02":
                        switch (gameField.field[0, 2])
                        {
                            case GameField.playerMark:
                                image.Source = new BitmapImage(new Uri("/Assets/outline_panorama_fish_eye_white_36dp.png", UriKind.Relative));
                                break;
                            case GameField.aiMark:
                                image.Source = new BitmapImage(new Uri("/Assets/outline_close_white_36dp.png", UriKind.Relative));
                                break;
                            case GameField.neutralMark:
                                image.Source = new BitmapImage(new Uri("/Assets/empty.png", UriKind.Relative));
                                break;
                            default:
                                break;
                        }
                        break;
                    case "i12":
                        switch (gameField.field[1, 2])
                        {
                            case GameField.playerMark:
                                image.Source = new BitmapImage(new Uri("/Assets/outline_panorama_fish_eye_white_36dp.png", UriKind.Relative));
                                break;
                            case GameField.aiMark:
                                image.Source = new BitmapImage(new Uri("/Assets/outline_close_white_36dp.png", UriKind.Relative));
                                break;
                            case GameField.neutralMark:
                                image.Source = new BitmapImage(new Uri("/Assets/empty.png", UriKind.Relative));
                                break;
                            default:
                                break;
                        }
                        break;
                    case "i22":
                        switch (gameField.field[2, 2])
                        {
                            case GameField.playerMark:
                                image.Source = new BitmapImage(new Uri("/Assets/outline_panorama_fish_eye_white_36dp.png", UriKind.Relative));
                                break;
                            case GameField.aiMark:
                                image.Source = new BitmapImage(new Uri("/Assets/outline_close_white_36dp.png", UriKind.Relative));
                                break;
                            case GameField.neutralMark:
                                image.Source = new BitmapImage(new Uri("/Assets/empty.png", UriKind.Relative));
                                break;
                            default:
                                break;
                        }
                        break;

                    default:
                        break;
                }
            }
        }
        private void LogoutUser(object obj)
        {
            MessageBoxResult result = MessageBox.Show(
                       "После этого данные будут удалены, а приложение закрыто",
                       "Подтверждение",
                       MessageBoxButton.YesNo,
                       MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Properties.Settings.Default.firstLaunch = true;
                Properties.Settings.Default.Save();
                CloseApp(obj);
            }
        }

        void CloseApp(object obj)
        {
            Properties.Settings.Default.losesAmount = UserLifecycle.user.LosesAmount;
            Properties.Settings.Default.winsAmount = UserLifecycle.user.WinsAmount;
            Properties.Settings.Default.gamesAmount = UserLifecycle.user.GamesAmount;
            Properties.Settings.Default.Save();

            foreach (Window item in Application.Current.Windows)
            {
                if (item is GameWindow)
                {
                    item.Close();
                }
                if (item is AuthWindow)
                {
                    item.Close();
                }
                if (item is SplashWindow)
                {
                    item.Close();
                }
            }
        }
        void MinimizeApp(object obj)
        {
            foreach (Window item in Application.Current.Windows)
            {
                if (item is GameWindow)
                {
                    item.WindowState = WindowState.Minimized;
                }
            }
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);

                if (child != null && child is T)
                    yield return (T)child;

                foreach (T childOfChild in FindVisualChildren<T>(child))
                    yield return childOfChild;
            }
        }
    }
}
