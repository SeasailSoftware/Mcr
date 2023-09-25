using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ReagentStripTest.Converters
{
    internal class FloatArrayToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var arr = value as float[];
            if (arr != null)
            {
                var list = new List<string>(Array.ConvertAll(arr, x => x.ToString("F2")));

                string str = "";
                for (int i = 0; i < arr.Length; i += 16)
                {
                    int n = Math.Min(arr.Length - i, 16);
                    str += string.Join("\t", list.GetRange(i, n));
                    if (arr.Length - i > 16)
                        str += "\r\n";
                }

                return str;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string buffer)
            {
                try
                {
                    var array = buffer.Replace("\r\n", "").Split(' ');
                    if (array == null || array.Length == 1) return null;
                    return Array.ConvertAll(array, x => float.Parse(x));
                }
                catch
                {
                    return value;
                }
            }
            return value;
        }
    }
}
