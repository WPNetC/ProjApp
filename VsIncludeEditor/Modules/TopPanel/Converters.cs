using System;
using System.Globalization;
using System.Windows.Data;
using VsIncludeEditor.Modules.TreeView;

namespace VsIncludeEditor.Modules.TopPanel
{
    [ValueConversion(typeof(object), typeof(string))]
    public class ObjectIsNullToBoolConverter : BaseConverter, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
                return false;
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException("Two way conversion not supported");
        }
    }
}
