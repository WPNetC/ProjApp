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
    public class ProjParserService
    {
        private IContentParserService _parser;

        private XmlNodeList GetNodes(string csprojPath)
        {
            var text = File.ReadAllText(csprojPath);
            var xml = new XmlDocument();
            xml.LoadXml(text);
            var itemGroups = xml.DocumentElement.GetElementsByTagName(CSPROJ_ITEMGROUP);

            var projType = xml.DocumentElement.GetElementsByTagName(CSPROJ_PROJECTTYPE)[0]?.InnerText;
            if (projType.ToLower().Contains(CSPROJ_WEB_GUID))
            {
                _parser = new WebProjectParser();
            }
            else
            {
                _parser = new ExeParser();
            }

            return itemGroups;
        }

        private List<XmlNode> GetNodesOfName(string csprojPath)
        {
            if (!csprojPath.ToLower().EndsWith("csproj") || !File.Exists(csprojPath))
                return null;

            var nodes = GetNodes(csprojPath);
            if (_parser == null)
                return null;

            return _parser.GetContentNodes(nodes);
        }


        public IEnumerable<ReferenceModel> GetReferences(string csprojPath)
        {
            if (!csprojPath.ToLower().EndsWith("csproj") || !File.Exists(csprojPath))
                yield break;

            // Step through each item group.
            foreach (XmlNode itemGroup in GetNodes(csprojPath))
            {
                // Skip if not of correct type or an empty collection.
                if (!itemGroup.HasChildNodes || itemGroup.ChildNodes[0].Name != CSPROJ_REFERENCE)
                    continue;


                foreach (XmlNode item in itemGroup.ChildNodes)
                {
                    var refObj = new ReferenceModel
                    {
                        Include = item.Attributes[CSPROJ_INCLUDE]?.Value
                    };

                    if (item.HasChildNodes)
                    {
                        foreach (XmlNode property in item.ChildNodes)
                        {
                            if (property.Name == "SpecificVersion")
                                refObj.SpecificVersion = property.InnerText;
                            else if (property.Name == "HintPath")
                                refObj.HintPath = property.InnerText;
                            else if (property.Name == "Private")
                                refObj.Private = Convert.ToBoolean(property.InnerText);
                        }
                    }

                    yield return refObj;
                }
            }
        }


        public IEnumerable<ContentModel> GetContentIncludes(string csprojPath)
        {
            var nodes = GetNodesOfName(csprojPath);
            if (_parser == null || nodes == null)
                return null;
            return _parser.GetContentIncludes(nodes);
        }

        public List<TreeNode> GetContentAsTree(string csprojPath)
        {
            var includes = GetContentIncludes(csprojPath);
            return TreeParser.GetContentAsTree(includes);

        }
    }
}
