using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace ReagentStripTest.Converters
{
    internal class BooleanToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return value;
            if (value is bool passed && passed)
                return Utils.MahAppsPackIconHelper.CreateImageSource(MahApps.Metro.IconPacks.PackIconMaterialKind.Monitor, Brushes.Green);
            return Utils.MahAppsPackIconHelper.CreateImageSource(MahApps.Metro.IconPacks.PackIconMaterialKind.Monitor, Brushes.Red);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
