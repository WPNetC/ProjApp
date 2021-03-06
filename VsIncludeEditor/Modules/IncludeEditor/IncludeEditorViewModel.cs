﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using VsIncludeEditor.Interfaces;
using VsIncludeEditor.Models;
using VsIncludeEditor.Modules.TreeView;
using VsIncludeEditor.Services;

namespace VsIncludeEditor.Modules.IncludeEditor
{
    public class IncludeEditorViewModel : ViewModelBase, IProjectView
    {
        private readonly object _imgLock = new object();
        private ObservableCollection<ContentModel> _includes;
        private ObservableCollection<TreeNode> _tree;
        private ObservableCollection<TreeNode> _selectedNodes;
        private ContentModel _selectedIncludes;
        private TreeNode _selectedNode;
        private UserControl _treeView;
        private ICommand _cmdToggleGroup;
        private ICommand _cmdExcludeSelected;
        private FileInfo _currentCsprojFile;
        private ProjWriterService _writer;
        private ICommand _cmdRevertChanges;

        public IncludeEditorViewModel()
        {
            _writer = new ProjWriterService();
        }

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
                if (value != _includes)
                {
                    if (_includes != null)
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
                if (value != _tree)
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
                if (value != _selectedNode)
                {
                    _selectedNode = value;
                    OnChanged();
                }
            }
        }

        public ObservableCollection<TreeNode> SelectedNodes
        {
            get
            {
                if (_selectedNodes == null)
                    _selectedNodes = new ObservableCollection<TreeNode>();
                return _selectedNodes;
            }
            set
            {
                if (value != _selectedNodes)
                {
                    _selectedNodes = value;
                    OnChanged();
                }
            }
        }

        public UserControl TreeView
        {
            get
            {
                if (_treeView == null)
                {
                    _treeView = new TreeView.ListTreeControl();

                }
                return _treeView;
            }
            private set
            {
                if (value != _treeView)
                {
                    _treeView = value;
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

        public ICommand CmdExcludeSelected
        {
            get
            {
                if (_cmdExcludeSelected == null)
                    _cmdExcludeSelected = new ExcludeSelected(this);
                return _cmdExcludeSelected;
            }
        }

        public ICommand CmdRevertChanges
        {
            get
            {
                if (_cmdRevertChanges == null)
                    _cmdRevertChanges = new RevertChange(this);
                return _cmdRevertChanges;
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

        public void SetCurrentCsProjFile(FileInfo file)
        {
            _currentCsprojFile = file;
        }

        internal void ToggleGroupSelect()
        {
            throw new NotImplementedException();
        }

        internal void ExcludeSelected()
        {
            if (!SelectedNodes.Any() || _currentCsprojFile == null)
                return;

            var content = SelectedNodes
                .Where(n => n.NodeModel != null)
                .Select(p => p.NodeModel)
                .ToArray();

            _writer.WriteExclusions(content, _currentCsprojFile);

            foreach (var node in Tree)
            {
                foreach (var desc in node.Descendents)
                {
                    desc.IsSelected = false;
                }
            }
            SelectedNodes.Clear();
        }

        internal bool CanRevertChange()
        {
            if (_currentCsprojFile == null)
                return false;
            return ProjWriterService.CanRevert(_currentCsprojFile);
        }

        internal bool RevertChange()
        {
            if (_currentCsprojFile == null)
                return false;
            return _writer.Revert(_currentCsprojFile);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _writer = null;
                Includes = null;
                Tree = null;
                SelectedNodes = null;
                _cmdExcludeSelected = null;
                _cmdRevertChanges = null;
                _cmdToggleGroup = null;
            }

            base.Dispose(disposing);
        }

        public void SetProject(FileInfo fileInfo)
        {
            var parser = new ProjParserService();

            var tree = parser.GetContentAsTree(fileInfo.FullName);
            if (tree == null)
                return;

            SetTree(tree);
            SetCurrentCsProjFile(fileInfo);
        }
    }
}
