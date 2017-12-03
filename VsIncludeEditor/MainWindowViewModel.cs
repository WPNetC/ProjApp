using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using VsIncludeEditor.Interfaces;
using VsIncludeEditor.Models;
using VsIncludeEditor.Modules.IncludeEditor;
using VsIncludeEditor.Modules.ReferenceEditor;
using VsIncludeEditor.Modules.TopPanel;
using VsIncludeEditor.Services;

namespace VsIncludeEditor
{
    public class MainWindowViewModel : ViewModelBase
    {
        private UserControl _centerPanelControl;
        private ProjectModel _selectedProject;
        private SolutionModel _selectedSolution;

        public UserControl CenterPanelControl
        {
            get
            {
                if (_centerPanelControl == null)
                    _centerPanelControl = new IncludeEditorView();
                return _centerPanelControl;
            }
            set
            {
                if (value != _centerPanelControl)
                {
                    _centerPanelControl = value;
                    OnChanged();

                    if (value != null)
                        SelectedProjectChanged();
                }
            }
        }

        public ProjectModel SelectedProject
        {
            get { return _selectedProject; }
            set
            {
                _selectedProject = value;
                OnChanged();
                SelectedProjectChanged();
            }
        }
        public SolutionModel SelectedSolution
        {
            get { return _selectedSolution; }
            set
            {
                _selectedSolution = value;
                OnChanged();
            }
        }

        private void SelectedProjectChanged()
        {
            if (SelectedProject is null)
                return;

            if (CenterPanelControl?.DataContext is IProjectView)
            {
                var vm = CenterPanelControl.DataContext as IProjectView;
                vm.SetProject(SelectedProject.FileInfo);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ((ViewModelBase)CenterPanelControl?.DataContext)?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
