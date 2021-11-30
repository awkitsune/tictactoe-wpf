using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tictactoe_wpf.Class
{
    public static class Utilities
    {
        public static float Normalize(float value, float min, float max) =>
            (value - min) / (max - min);
    }
}
