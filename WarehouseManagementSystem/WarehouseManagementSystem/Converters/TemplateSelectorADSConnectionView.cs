using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WarehouseManagementSystem.Converters
{
    public class TemplateSelectorADSConnectionView : DataTemplateSelector
    {
        public DataTemplate? BoolTemplate { get; set; }
        public DataTemplate? TextTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if(item is string boolValue &&
                bool.TryParse(boolValue, out _))
            {
                return BoolTemplate;
            }

            return TextTemplate;
        }
    }
}
