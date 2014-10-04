using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace GraphView.Framework.Converters
{
    public class OffsetConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double val, offset;
            if (value != null && parameter != null
                && double.TryParse(value.ToString(), out val)
                && double.TryParse(parameter.ToString(), out offset))
            {
                return val + offset;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
