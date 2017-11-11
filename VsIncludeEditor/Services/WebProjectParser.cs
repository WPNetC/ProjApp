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
    public class WebProjectParser : IContentParserService
    {
        public List<XmlNode> GetContentNodes(XmlNodeList nodes)
        {
            var name = CSPROJ_CONTENT;
            var result = new List<XmlNode>();
            foreach (XmlNode itemGroup in nodes)
            {
                // Skip if not of correct type or an empty collection.
                if (!itemGroup.HasChildNodes || itemGroup.ChildNodes[0].Name.ToUpperInvariant() != name.ToUpperInvariant())
                    continue;

                result.Add(itemGroup);
            }

            return result;
        }

        public IEnumerable<ContentModel> GetContentIncludes(IEnumerable<XmlNode> itemGroups)
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
                            else if (property.Name == "DependentUpon")
                                refObj.DependentUpon = property.InnerText;
                            else if (property.Name != "#text")
                                throw new InvalidOperationException($"Invalid type: {property.Name}");
                        }
                    }

                    yield return refObj;
                }
            }
        }
    }
}
