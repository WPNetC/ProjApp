using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VsIncludeEditor.Models;
using System.Xml;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace VsIncludeEditor.Services
{
    public class ProjWriterService
    {
        public void WriteExclusions(IEnumerable<IInclude> includes, FileInfo csprojFile)
        {
            var origText = File.ReadAllText(csprojFile.FullName);
            var xmlDoc = new XmlDocument();
            var nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
            nsmgr.AddNamespace("a", "http://schemas.microsoft.com/appx/2010/manifest");
            xmlDoc.LoadXml(origText);

            //var nodes = xmlDoc.DocumentElement.GetElementsByTagName(Constants.CSPROJ_CONTENT);
            //var cnt = nodes.Count;

            var toExclude = includes.Select(p => p.Include).ToArray();

            foreach (var item in toExclude)
            {
                var exNodes = xmlDoc.SelectNodes($"//*[local-name()='{Constants.CSPROJ_ITEMGROUP}']/*[@Include='{item}']");
                foreach (XmlNode exNode in exNodes)
                {
                    exNode.ParentNode.RemoveChild(exNode);
                }
            }

            xmlDoc.Save("C:\\Test\\test.xml");
        }
    }
}
