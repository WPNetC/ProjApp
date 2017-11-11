using System.Collections.Generic;
using System.Xml;
using VsIncludeEditor.Models;
using VsIncludeEditor.Modules.TreeView;

namespace VsIncludeEditor.Services
{
    public interface IContentParserService
    {
        List<XmlNode> GetContentNodes(XmlNodeList nodes);
        IEnumerable<ContentModel> GetContentIncludes(IEnumerable<XmlNode> itemGroups);
    }
}
