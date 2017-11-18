using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace VsIncludeEditor.Modules.TopPanel
{
    /// <summary>
    /// Interaction logic for TopPanelView.xaml
    /// </summary>
    public partial class TopPanelView : UserControl
    {
        public TopPanelView()
        {
            InitializeComponent();
            grdWrapper.DataContext = this;
        }



        public FileInfo CurrentProjectFile
        {
            get { return (FileInfo)GetValue(CurrentProjectFileProperty); }
            set { SetValue(CurrentProjectFileProperty, value); }
        }

        // Using a DependencyProperty as the backing store for fileInfo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentProjectFileProperty =
            DependencyProperty.Register("CurrentProjectFile", typeof(FileInfo), typeof(TopPanelView), new PropertyMetadata(null));



        public UserControl CurrentControl
        {
            get { return (UserControl)GetValue(CurrentControlProperty); }
            set { SetValue(CurrentControlProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentControl.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentControlProperty =
            DependencyProperty.Register("CurrentControl", typeof(UserControl), typeof(TopPanelView), new PropertyMetadata(null));



        private void FileOpen_Click(object sender, RoutedEventArgs e)
        {
            using (var fbd = new System.Windows.Forms.OpenFileDialog())
            {
                fbd.Filter = "csproj files|*.csproj";
                fbd.Multiselect = false;

                if (fbd.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return;

                CurrentProjectFile = new FileInfo(fbd.FileName);
            }
        }

        private void ViewSelect_Clicked(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn == null)
                return;

            switch (btn.Name)
            {
                case "btnContentIncludes":
                    CurrentControl = new IncludeEditor.IncludeEditorView();
                    break;
                case "btnReferenceIncludes":
                    CurrentControl = new ReferenceEditor.ReferenceEditorView();
                    break;
                default:
                    break;
            }
        }
    }
}
