using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using WarehouseManagementSystem.Models.Orders;

namespace WarehouseManagementSystem.Converters
{
    public class StringToOrderTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string orderType)
            {
                if (Enum.TryParse<OrderType>(orderType, out OrderType result))
                {
                    return result;
                }
                else
                {
                    return OrderType.None;
                }
            }

            return OrderType.None;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
