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
            var result = new List<XmlNode>();
            foreach (XmlNode itemGroup in nodes)
            {
                if (CSPROJ_GEN_INCLUDE_TAGS.Contains(itemGroup.Name) || CSPROJ_EXE_INCLUDE_TAGS.Contains(itemGroup.Name))
                    result.Add(itemGroup);
            }

            return result;
        }

        public IEnumerable<ContentModel> GetContentIncludes(IEnumerable<XmlNode> itemGroups)
        {
            // Step through each item group.
            foreach (XmlNode item in itemGroups)
            {
                var refObj = new ContentModel
                {
                    Include = item.Attributes[CSPROJ_INCLUDE]?.Value,
                    Properties = new Dictionary<string, string>()
                };

                if (item.HasChildNodes)
                {
                    foreach (XmlNode property in item.ChildNodes)
                    {
                        if(!refObj.Properties.ContainsKey(property.Name))
                            refObj.Properties.Add(property.Name, property.InnerText);
                    }
                }

                yield return refObj;

            }
        }
    }
}
