using System.Collections.Generic;
using System.Xml;
using VsIncludeEditor.Models;
using VsIncludeEditor.Modules.TreeView;

namespace VsIncludeEditor.Services
{
    public interface IContentParserService
    {
        IEnumerable<ContentModel> GetContentIncludes(XmlNodeList itemGroups);
        List<TreeNode> GetContentAsTree(XmlNodeList itemGroups);
    }
}
