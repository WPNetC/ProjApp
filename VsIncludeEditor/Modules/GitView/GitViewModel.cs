using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VsIncludeEditor.Interfaces;
using VsIncludeEditor.Services;

namespace VsIncludeEditor.Modules.GitView
{
    public class GitViewModel : ViewModelBase, IProjectView
    {
        private ObservableCollection<string> _branches;
        private string _currentBranch;

        public ObservableCollection<string> Branches
        {
            get
            {
                return _branches ?? (_branches = new ObservableCollection<string>());
            }
            set
            {
                if (value != _branches)
                {
                    _branches = value;
                    OnChanged();
                }
            }
        }

        public string CurrentBranch
        {
            get
            {
                return _currentBranch;
            }
            set
            {
                if (value != _currentBranch)
                {
                    _currentBranch = value;
                    OnChanged();
                }
            }
        }



        public void SetProject(FileInfo fileInfo)
        {
            if (fileInfo == null)
                return;

            Branches = new ObservableCollection<string>(GitService.GetBranchNames(fileInfo));
            CurrentBranch = GitService.GetCurrentBranchName(fileInfo.DirectoryName);
        }
    }
}
