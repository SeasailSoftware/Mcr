using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ReagentStripTest.Converters
{
    internal class GreaterThanValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
           if (value == null) return System.Windows.Media.Brushes.Black;
            var para1 = int.Parse(value.ToString());
            var para2 = int.Parse(parameter.ToString());
            if (Math.Abs(para1) > para2) return System.Windows.Media.Brushes.Red;
            return System.Windows.Media.Brushes.Green;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
