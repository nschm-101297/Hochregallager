using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using WarehouseManagementSystem.Models.Orders;

namespace WarehouseManagementSystem.Converters
{
    internal class StringToOrderStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is OrderStatus status)
            {
                return Enum.GetName(typeof(OrderStatus), status) ?? "";
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string orderType)
            {
                if (Enum.TryParse<OrderStatus>(orderType, out OrderStatus result))
                {
                    return result;
                }
                else
                {
                    return OrderStatus.Unknown;
                }
            }

            return Binding.DoNothing;
        }
    }
}
