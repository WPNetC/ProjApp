using System;
using System.Collections.Generic;

namespace VsIncludeEditor.Modules.TreeView
{
    [Serializable]
    public abstract class TreeNode : IEquatable<TreeNode>
    {
        public TreeNode()
        {
            Children = new List<TreeNode>();
        }
        public TreeNode(string name) : this()
        {
            Name = name;
        }

        public string Name { get; set; }
        public string FullPath { get; set; }
        public TreeNode Parent { get; set; }
        public List<TreeNode> Children { get; set; }
        public bool IsSelected { get; set; }

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
