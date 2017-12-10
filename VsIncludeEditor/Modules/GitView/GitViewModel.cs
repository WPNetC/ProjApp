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
        private ObservableCollection<Branch> _branches;
        private ObservableCollection<Commit> _commits;

        private string _currentBranch;
        private string _gitPath;
        private Branch _selectedBranch;
        private Commit _selectedCommit;
        private ICommand _cmdCkecoutBranch;
        private ICommand _cmdCkecoutCommit;

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

        public ObservableCollection<Branch> Branches
        {
            get
            {
                return _branches ?? (_branches = new ObservableCollection<Branch>());
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
        
        public Branch SelectedBranch
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

        public ICommand CmdCheckoutBranch => _cmdCkecoutBranch ?? (_cmdCkecoutBranch = new CheckoutBranch(this));

        public ICommand CmdCheckoutCommit => _cmdCkecoutCommit ?? (_cmdCkecoutCommit = new CheckoutCommit(this));

        internal bool CanCheckoutBranch => SelectedBranch != null;

        internal bool CanCheckoutCommit => SelectedCommit != null;
        
        public void SetProject(FileInfo fileInfo)
        {
            if (fileInfo == null)
                return;

            CurrentGitPath = GitService.GetGitDirectory(fileInfo.FullName)?.FullName;
            if (string.IsNullOrEmpty(CurrentGitPath?.Trim()))
                return;

            Branches = new ObservableCollection<Branch>(GitService.GetBranches(CurrentGitPath));
            CurrentBranch = GitService.GetCurrentBranchName(CurrentGitPath);
            Commits = new ObservableCollection<Commit>(GitService.GetCommits(CurrentGitPath));
        }
        
        internal void CheckoutBranch()
        {
            GitService.CheckoutBranch(CurrentGitPath, SelectedBranch);
            CurrentBranch = GitService.GetCurrentBranchName(CurrentGitPath);
            Commits = new ObservableCollection<Commit>(GitService.GetCommits(CurrentGitPath));
        }

        internal void CheckoutCommit()
        {
            GitService.CheckoutCommit(CurrentGitPath, SelectedCommit);
        }

    }
}
