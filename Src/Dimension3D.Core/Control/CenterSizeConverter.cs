using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Dimension3D.Core
{
    internal class CenterSizeConverter : IMultiValueConverter
    {
        public object? Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values != null && values.Length == 2 && values[0] is Double d)
            { 

                if (values[1] is HorizontalAlignment ha)
                {
                    switch (ha)
                    {
                        case HorizontalAlignment.Stretch:
                        case HorizontalAlignment.Center:
                            return d / 2;
                        case HorizontalAlignment.Right:
                            return d;
                        case HorizontalAlignment.Left:
                            return 0; 
                    }
                }
                else if (values[1] is VerticalAlignment va)
                {
                    switch (va)
                    {
                        case VerticalAlignment.Stretch:
                        case VerticalAlignment.Center:
                            return d / 2;
                        case VerticalAlignment.Bottom:
                            return d;
                        case VerticalAlignment.Top:
                            return 0;
                    }
                } 

            }

            return values;

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
