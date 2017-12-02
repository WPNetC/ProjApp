using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using VsIncludeEditor.Models;
using VsIncludeEditor.Services;

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



        public SolutionModel CurrentSolution
        {
            get { return (SolutionModel)GetValue(CurrentSolutionProperty); }
            set { SetValue(CurrentSolutionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for fileInfo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentSolutionProperty =
            DependencyProperty.Register("CurrentSolution", typeof(SolutionModel), typeof(TopPanelView), new PropertyMetadata(null));
        

        public List<ProjectModel> ProjectList
        {
            get { return (List<ProjectModel>)GetValue(ProjectListProperty); }
            set { SetValue(ProjectListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for list.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProjectListProperty =
            DependencyProperty.Register("ProjectList", typeof(List<ProjectModel>), typeof(TopPanelView), new PropertyMetadata(null));
        

        public ProjectModel SelectedProjectModel
        {
            get { return (ProjectModel)GetValue(SelectedProjectModelProperty); }
            set { SetValue(SelectedProjectModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for projectModel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedProjectModelProperty =
            DependencyProperty.Register("SelectedProjectModel", typeof(ProjectModel), typeof(TopPanelView), new PropertyMetadata(null));
        

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
                fbd.Filter = "Visual Studio Solution|*.sln";
                fbd.Multiselect = false;

                if (fbd.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return;

                if (!File.Exists(fbd.FileName))
                {
                    CurrentSolution = null;
                    return;
                }
                var fi = new FileInfo(fbd.FileName);
                CurrentSolution = new SolutionModel
                {
                    FileInfo = fi,
                    Name = fbd.FileName.Split('.')[0]
                };
                ProjectList = SlnParserService.GetProjects(CurrentSolution.FileInfo.FullName);
                SelectedProjectModel = ProjectList.FirstOrDefault();
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

        private void LaunchVs_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentSolution is null)
                return;

            var info = new ProcessStartInfo
            {
                Arguments = CurrentSolution.FileInfo.FullName,
                FileName = @"C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\IDE\devenv.exe"
            };

            int exitCode;
            using (var proc = Process.Start(info))
            {
                proc.WaitForExit();
                exitCode = proc.ExitCode;
            }
        }
    }
}
