using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using VsIncludeEditor.Models;
using VsIncludeEditor.Modules.TreeView;

namespace VsIncludeEditor.Modules.IncludeEditor
{
    public class IncludeEditorViewModel : ViewModelBase
    {
        private readonly object _imgLock = new object();
        private ObservableCollection<ContentModel> _includes;
        private ObservableCollection<TreeNode> _tree;
        private ContentModel _selectedIncludes;
        private TreeNode _selectedNode;
        private ICommand _cmdToggleGroup;

        public ObservableCollection<ContentModel> Includes
        {
            get
            {
                if (_includes == null)
                {
                    _includes = new ObservableCollection<ContentModel>();
                    BindingOperations.EnableCollectionSynchronization(_includes, _imgLock);
                }
                return _includes;
            }
            private set
            {
                if(value != _includes)
                {
                    if(_includes != null)
                        BindingOperations.DisableCollectionSynchronization(_includes);

                    _includes = value;
                    BindingOperations.EnableCollectionSynchronization(_includes, _imgLock);
                    OnChanged();
                }
            }
        }

        public ObservableCollection<TreeNode> Tree
        {
            get
            {
                if (_tree == null)
                    _tree = new ObservableCollection<TreeNode>();
                return _tree;
            }
            private set
            {
                if(value != _tree)
                {
                    _tree = value;
                    OnChanged();
                }
            }
        }

        public ContentModel SelectedIncludes
        {
            get
            {
                return _selectedIncludes;
            }
            set
            {
                _selectedIncludes = value;
                OnChanged();
            }
        }

        public TreeNode SelectedNode
        {
            get
            {
                return _selectedNode;
            }
            set
            {
                if(value != _selectedNode)
                {
                    _selectedNode = value;
                    OnChanged();
                }
            }
        }

        public ICommand CmdToggleGroup
        {
            get
            {
                if (_cmdToggleGroup == null)
                    _cmdToggleGroup = new ToggleGroupSelect(this);
                return _cmdToggleGroup;
            }
        }

        public void SetIncludes(IEnumerable<ContentModel> references)
        {
            Includes.Clear();
            foreach (var item in references)
            {
                Includes.Add(item);
            }
        }

        public void SetTree(IEnumerable<TreeNode> tree)
        {
            Tree.Clear();
            foreach (var item in tree)
            {
                Tree.Add(item);
            }
        }

        internal void ToggleGroupSelect()
        {
            throw new NotImplementedException();
        }

    }
}
