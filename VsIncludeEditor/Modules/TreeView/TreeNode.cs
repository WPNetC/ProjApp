using System;
using System.Collections.Generic;
using System.ComponentModel;
using VsIncludeEditor.Models;

namespace VsIncludeEditor.Modules.TreeView
{
    [Serializable]
    public abstract class TreeNode : IEquatable<TreeNode>, INotifyPropertyChanged
    {
        private string _fullPath;
        private string _name;
        private List<TreeNode> _children;
        private bool _isSelected;
        
        public TreeNode()
        {
            Children = new List<TreeNode>();
        }
        public TreeNode(string name) : this()
        {
            Name = name;
        }
        public TreeNode(string name, IInclude model) : this(name)
        {
            NodeModel = model;
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
                }
            }
        }
        public string FullPath
        {
            get { return _fullPath; }
            set
            {
                if (value != _fullPath)
                {
                    _fullPath = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FullPath"));
                }
            }
        }
        public List<TreeNode> Children
        {
            get { return _children; }
            set
            {
                if (value != _children)
                {
                    _children = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Children"));
                }
            }
        }    
        public List<TreeNode> Descendents
        {
            get
            {
                var list = new List<TreeNode> { this };
                foreach (var item in Children)
                {
                    list.AddRange(item.Descendents);
                }
                return list;
            }
        }
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value != _isSelected)
                {
                    _isSelected = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsSelected"));
                }
            }
        }
        public IInclude NodeModel { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        public override bool Equals(object obj)
        {
            if (obj is TreeNode)
                return Equals((TreeNode)obj);
            return false;
        }

        public bool Equals(TreeNode other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;

            return this.GetHashCode() == other.GetHashCode();
        }

        public override int GetHashCode()
        {
            return FullPath == null ? Name.GetHashCode() : FullPath.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
