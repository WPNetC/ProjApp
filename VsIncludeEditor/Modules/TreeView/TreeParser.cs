using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VsIncludeEditor.Modules.TreeView
{
    public class TreeParser
    {
        public static List<TreeNode> GetContentAsTree(params string[] args)
        {
            var result = new List<TreeNode>();
            foreach (var item in args)
            {
                var pathParts = item.Split('\\');
                int cnt = pathParts.Length;
                var root = pathParts[0];
                var fileName = pathParts[cnt - 1];

                if (root == fileName)
                {
                    var node = new FileNode(fileName) { FullPath = item };
                    if (!result.Contains(node))
                        result.Add(node);
                }
                else
                {
                    var parentNode = result.FirstOrDefault(p => p.Name == root) ?? new FolderNode(root) { FullPath = root };
                    if (!result.Contains(parentNode))
                        result.Add(parentNode);
                    for (int idx = 1; idx < cnt; idx++)
                    {
                        var childName = pathParts[idx];
                        var childPath = idx == cnt - 1 ? string.Join("\\", pathParts) : string.Join("\\", pathParts.Take(idx + 1));

                        var childNode = parentNode.Children.FirstOrDefault(p => p.Name == childName);
                        if (childNode == null)
                        {
                            if (childPath.Split('.').Length == 2)
                            {
                                childNode = new FileNode(childName) { FullPath = childPath };
                            }
                            else
                            {
                                childNode = new FolderNode(childName) { FullPath = childPath };
                            }
                        }
                        if (!parentNode.Children.Contains(childNode))
                            parentNode.Children.Add(childNode);
                        parentNode = childNode;

                    }
                }
            }

            return result;
        }
    }
}
