using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using VsIncludeEditor.Modules.IncludeEditor;
using VsIncludeEditor.Modules.TopPanel;
using VsIncludeEditor.Services;

namespace VsIncludeEditor
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ObservableCollection<UserControl> _userControls;
        private TopPanelView _topPanelControl;
        private UserControl _footPanelControl;
        private UserControl _centerPanelControl;
        private FileInfo _currentSelectedProject;

        public ObservableCollection<UserControl> UserControls
        {
            get
            {
                if (_userControls == null)
                    _userControls = new ObservableCollection<UserControl>
                    {

                    };
                return _userControls;
            }
            private set
            {
                _userControls = value;
                OnChanged();
            }
        }

        public UserControl TopPanelControl
        {
            get
            {
                if (_topPanelControl == null)
                {
                    _topPanelControl = new TopPanelView();
                    ((TopPanelViewModel)_topPanelControl.DataContext).FileInfoChanged += SelectedProjectChanged;
                }
                return _topPanelControl;
            }
        }

        public UserControl CenterPanelControl
        {
            get
            {
                if(_centerPanelControl == null)
                    _centerPanelControl = new IncludeEditorView();
                return _centerPanelControl;
            }
            set
            {
                if (value != _centerPanelControl)
                {
                    _centerPanelControl = value;
                    OnChanged();
                }
            }
        }

        public UserControl FootPanelControl
        {
            get
            {
                return _footPanelControl;
            }
            set
            {
                if (value != _footPanelControl)
                {
                    _footPanelControl = value;
                    OnChanged();
                }
            }
        }

        private void SelectedProjectChanged(object sender, EventArgs e)
        {
            _currentSelectedProject = ((TopPanelViewModel)_topPanelControl.DataContext).ProjectInfo;

            if (CenterPanelControl is IncludeEditorView)
            {
                var vm = CenterPanelControl.DataContext as IncludeEditorViewModel;

                var parser = new ProjParserService();

                //var refs = parser.GetContentIncludes(_currentSelectedProject.FullName).ToList();
                var tree = parser.GetContentAsTree(_currentSelectedProject.FullName);

                //vm?.SetIncludes(refs);
                vm?.SetTree(tree);
                vm?.SetCurrentCsProjFile(_currentSelectedProject);

            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ((ViewModelBase)TopPanelControl?.DataContext)?.Dispose();
                ((ViewModelBase)CenterPanelControl?.DataContext)?.Dispose();
                ((ViewModelBase)FootPanelControl?.DataContext)?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
