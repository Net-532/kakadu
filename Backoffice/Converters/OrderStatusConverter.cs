using System;
using System.Globalization;
using System.Windows.Data;

namespace Backoffice.Converters
{        public class OrderStatusConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                string status = value as string;
                if (status != null)
                {
                    switch (status)
                    {
                        case "Done":
                            return "Готове";
                        case "Processing":
                            return "Виконується";
                        default:
                            return status;
                    }
                }
                return null;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return null;
            }
        }
}
