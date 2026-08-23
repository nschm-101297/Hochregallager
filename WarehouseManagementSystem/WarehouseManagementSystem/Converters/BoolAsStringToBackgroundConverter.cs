using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace WarehouseManagementSystem.Converters
{
    public class BoolAsStringToBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string boolValue)
            {
                if (bool.TryParse(boolValue, out bool result))
                {
                    if (result)
                    {
                        return new SolidColorBrush(Colors.LightGreen);
                    }
                    else
                    {
                        return new SolidColorBrush(Colors.Red);
                    }
                }
                else
                {
                    return new SolidColorBrush(Colors.Transparent);
                }
            }

            return new SolidColorBrush(Colors.Transparent);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
