using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using VsIncludeEditor.Modules.IncludeEditor;
using VsIncludeEditor.Modules.ReferenceEditor;
using VsIncludeEditor.Modules.TopPanel;
using VsIncludeEditor.Services;

namespace VsIncludeEditor
{
    public class MainWindowViewModel : ViewModelBase
    {
        private UserControl _centerPanelControl;
        private FileInfo _selectedProject;

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

        public FileInfo SelectedProject
        {
            get { return _selectedProject; }
            set
            {
                _selectedProject = value;
                OnChanged();

                if (value != null)
                    SelectedProjectChanged();
            }
        }

        private void SelectedProjectChanged()
        {
            if (SelectedProject == null)
                return;

            if (CenterPanelControl is IncludeEditorView)
            {
                var vm = CenterPanelControl.DataContext as IncludeEditorViewModel;

                if (vm == null)
                    return;

                var parser = new ProjParserService();

                var tree = parser.GetContentAsTree(SelectedProject.FullName);
                if (tree == null)
                    return;

                vm.SetTree(tree);
                vm.SetCurrentCsProjFile(SelectedProject);

            }
            else if (CenterPanelControl is ReferenceEditorView)
            {
                var vm = CenterPanelControl.DataContext as ReferenceEditorViewModel;
                if (vm == null)
                    return;

                var parser = new ProjParserService();

                var refs = parser.GetReferences(SelectedProject.FullName);
                if (refs == null)
                    return;

                vm.SetReferences(refs);
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
