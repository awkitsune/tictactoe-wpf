using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace tictactoe_wpf.Class
{
    [ValueConversion(typeof(bool?), typeof(Cursor))]
    public class BooleanToCursorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((bool?)value == true)
            {
                return Cursors.Hand;
            }
            else
            {
                return Cursors.Arrow;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
