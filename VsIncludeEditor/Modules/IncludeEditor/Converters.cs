using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using VsIncludeEditor.Modules.TreeView;

namespace VsIncludeEditor.Modules.IncludeEditor
{
    [ValueConversion(typeof(TreeNode), typeof(string))]
    public class TreeToNameConverter : BaseConverter, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TreeNode)
                return ((TreeNode)value).NodeModel?.Include ?? ((TreeNode)value).Name;
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException("Two way conversion not supported");
        }
    }
}
