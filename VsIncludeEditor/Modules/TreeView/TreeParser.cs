using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VsIncludeEditor.Models;

namespace VsIncludeEditor.Modules.TreeView
{
    public class TreeParser
    {
        public static List<TreeNode> GetContentAsTree(IEnumerable<ContentModel> content)
        {
            var result = new List<TreeNode>();
            foreach (var item in content)
            {
                var pathParts = item.Include.Split('\\');
                int cnt = pathParts.Length;
                var root = pathParts[0];
                var fileName = pathParts[cnt - 1];

                if (root == fileName)
                {
                    var node = new FileNode(fileName, item) { FullPath = item.Include };
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
                        var isEnd = idx == cnt - 1;
                        var isFile = isEnd && childName.Contains('.');
                        var childPath = isEnd ? string.Join("\\", pathParts) : string.Join("\\", pathParts.Take(idx + 1));

                        var childNode = parentNode.Children.FirstOrDefault(p => p.Name == childName);
                        if (childNode == null)
                        {
                            if (isFile)
                            {
                                childNode = new FileNode(childName, item) { FullPath = childPath };
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
