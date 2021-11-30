using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tictactoe_wpf.Class
{
    public class User
    {
        public string Username;
        public int GamesAmount = 0;
        public int WinsAmount = 0;
        public int LosesAmount = 0;

        public User()
        {
            Username = "";
        }
        public User(string username)
        {
            Username = username;
        }
        public User(string username, int gamesAmount, int winsAmount, int losesAmount)
        {
            Username = username;

            GamesAmount = gamesAmount;
            WinsAmount = winsAmount;
            LosesAmount = losesAmount;
        }
    }
}
