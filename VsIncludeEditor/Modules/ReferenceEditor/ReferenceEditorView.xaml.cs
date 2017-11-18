using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VsIncludeEditor.Modules.ReferenceEditor
{
    /// <summary>
    /// Interaction logic for ReferenceEditorView.xaml
    /// </summary>
    public partial class ReferenceEditorView : UserControl
    {
        public ReferenceEditorView()
        {
            InitializeComponent();
            this.Unloaded += (s, e) => { ((ReferenceEditorViewModel)this.DataContext).Dispose(); };
        }
    }
}
