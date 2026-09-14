using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WarehouseManagementSystem.Models.Orders;

namespace WarehouseManagementSystem.Converters
{
    public class StringToOrderPriorityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string orderType)
            {
                if (Enum.TryParse<OrderPriority>(orderType, out OrderPriority result))
                {
                    return result;
                }
                else
                {
                    return OrderPriority.None;
                }
            }

            return OrderPriority.None;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is OrderPriority priority)
            {
                return Enum.GetName(typeof(OrderPriority), priority) ?? "";
            }
            return "";
        }
    }
}
