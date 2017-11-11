using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using VsIncludeEditor.Models;
using VsIncludeEditor.Modules.TreeView;
using static VsIncludeEditor.Constants;

namespace VsIncludeEditor.Services
{
    public class ExeParser : IContentParserService
    {
        public IEnumerable<ContentModel> GetContentIncludes(XmlNodeList itemGroups)
        {
            // Step through each item group.
            foreach (XmlNode itemGroup in itemGroups)
            {
                // Skip if not of correct type or an empty collection.
                if (!itemGroup.HasChildNodes)
                    continue;

                foreach (XmlNode item in itemGroup.ChildNodes)
                {
                    var refObj = new ContentModel
                    {
                        Include = item.Attributes[CSPROJ_INCLUDE]?.Value
                    };

                    if (item.HasChildNodes)
                    {
                        foreach (XmlNode property in item.ChildNodes)
                        {
                            if (property.Name == "SubType")
                                refObj.SubType = property.InnerText;
                            else if (property.Name != "#text")
                                throw new InvalidOperationException($"Invalid type: {property.Name}");
                        }
                    }

                    yield return refObj;
                }
            }
        }

        public List<TreeNode> GetContentAsTree(XmlNodeList itemGroups)
        {
            var includes = GetContentIncludes(itemGroups).Select(p => p.Include).ToArray();
            return TreeParser.GetContentAsTree(includes);

        }
    }
}
