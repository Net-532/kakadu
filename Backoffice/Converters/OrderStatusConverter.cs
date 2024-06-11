using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Backoffice.Converters
{
    [ValueConversion(typeof(string), typeof(string))]
    public class OrderStatusConverter : IValueConverter
    {
        private static readonly IDictionary<string, string> _valueToRepresentation = new Dictionary<string, string>()
        {
            { "Done", "Готове" },
            { "Processing", "Виконується" }
        };

        private static readonly IDictionary<string, string> _representationToValue = new Dictionary<string, string>()
        {
            { "Готове", "Done" },
            { "Виконується", "Processing" }
        };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string status)
            {
                if (_valueToRepresentation.TryGetValue(status, out string representation))
                {
                    return representation;
                }
                return DependencyProperty.UnsetValue;
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string representation)
            {
                if (_representationToValue.TryGetValue(representation, out string status))
                {
                    return status;
                }
                return DependencyProperty.UnsetValue;
            }
            return DependencyProperty.UnsetValue;
        }
    }
}
