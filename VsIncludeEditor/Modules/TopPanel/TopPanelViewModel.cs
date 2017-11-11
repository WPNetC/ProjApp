using System;
using System.IO;
using System.Windows.Forms;
using System.Windows.Input;

namespace VsIncludeEditor.Modules.TopPanel
{
    public class TopPanelViewModel : ViewModelBase
    {
        private ICommand _cmdOpenFile;
        private string _projName;
        private FileInfo _projInfo;

        public event EventHandler FileInfoChanged;

        public ICommand cmdOpenFile
        {
            get
            {
                if (_cmdOpenFile == null)
                    _cmdOpenFile = new OpenProject(this);
                return _cmdOpenFile;
            }
        }

        public string ProjectName
        {
            get
            {
                return _projName ?? "No project open";
            }
            set
            {
                if (value != _projName)
                {
                    _projName = value;
                    OnChanged();
                }
            }
        }
        public FileInfo ProjectInfo
        {
            get
            {
                return _projInfo;
            }
            private set
            {
                if(value != _projInfo)
                {
                    _projInfo = value;
                }
            }
        }

        internal void OpenProject()
        {
            using (var fbd = new OpenFileDialog())
            {
                fbd.DefaultExt = ".csproj";
                fbd.Multiselect = false;

                if (fbd.ShowDialog() != DialogResult.OK)
                    return;

                ProjectName = fbd.SafeFileName;
                ProjectInfo = new FileInfo(fbd.FileName);
                FileInfoChanged?.Invoke(this, new EventArgs());
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                FileInfoChanged = null;
                ProjectInfo = null;
            }
            base.Dispose(disposing);
        }
    }
}
