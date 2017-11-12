using System.Collections.Generic;
using VsIncludeEditor.Models;

namespace VsIncludeEditor.Modules.TreeView
{
    public class FolderNode : TreeNode
    {
        public FolderNode() : base() { }
        public FolderNode(string name) : base(name) { }
        public FolderNode(string name, ISelectable model) : base(name, model) { }
    }
}
