using System.Collections.Generic;
using VsIncludeEditor.Models;

namespace VsIncludeEditor.Modules.TreeView
{
    public class FileNode : TreeNode
    {
        public FileNode() : base() { }
        public FileNode(string name) : base(name) { }
        public FileNode(string name, IInclude model) : base(name, model) { }
    }
}
