using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VsIncludeEditor.Interfaces;
using VsIncludeEditor.Services;

namespace VsIncludeEditor.Modules.GitView
{
    public class GitViewModel : ViewModelBase, IProjectView
    {
        private ObservableCollection<string> _branches;
        private ObservableCollection<Commit> _commits;

        private string _currentBranch;
        private string _gitPath;
        private string _selectedBranch;
        private Commit _selectedCommit;



        public string CurrentGitPath
        {
            get
            {
                return _gitPath;
            }
            set
            {
                if (value != _gitPath)
                {
                    _gitPath = value;
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
        
        public string SelectedBranch
        {
            get
            {
                return _selectedBranch;
            }
            set
            {
                if (value != _selectedBranch)
                {
                    _selectedBranch = value;
                    OnChanged();
                }
            }
        }

        public ObservableCollection<Commit> Commits
        {
            get
            {
                return _commits;
            }
            set
            {
                if (value != _commits)
                {
                    _commits = value;
                    OnChanged();
                }
            }
        }

        public Commit SelectedCommit
        {
            get
            {
                return _selectedCommit;
            }
            set
            {
                if (value != _selectedCommit)
                {
                    _selectedCommit = value;
                    OnChanged();
                }
            }
        }



        public void SetProject(FileInfo fileInfo)
        {
            if (fileInfo == null)
                return;

            CurrentGitPath = GitService.GetGitDirectory(fileInfo.FullName)?.FullName;
            if (string.IsNullOrEmpty(CurrentGitPath?.Trim()))
                return;

            Branches = new ObservableCollection<string>(GitService.GetBranchNames(CurrentGitPath));
            Commits = new ObservableCollection<Commit>(GitService.GetCommits(CurrentGitPath));
            CurrentBranch = GitService.GetCurrentBranchName(CurrentGitPath);
        }
    }
}
