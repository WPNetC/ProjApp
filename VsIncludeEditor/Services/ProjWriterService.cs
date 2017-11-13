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
            
            var toExclude = includes.Select(p => p.Include).ToArray();
            
            var exNodes = xmlDoc.SelectNodes($"//*[local-name()='{Constants.CSPROJ_ITEMGROUP}']/*[@Include]");
            var cnt = exNodes.Count;

            for (int ii = 0; ii < cnt; ii++)
            {
                var node = exNodes[ii];
                if (toExclude.Contains(node.Attributes[Constants.CSPROJ_INCLUDE]?.Value))
                    node.ParentNode.RemoveChild(node);
            }

            var bakPath = $"{csprojFile.FullName}.bak";
            if (File.Exists(bakPath))
                File.Delete(bakPath);
            File.Copy(csprojFile.FullName, bakPath);
            xmlDoc.Save(csprojFile.FullName);
        }

        public bool Revert(FileInfo csprojFile)
        {
            var bak = $"{csprojFile.FullName}.bak";
            if (!File.Exists(bak) || !File.Exists(csprojFile.FullName))
                return false;

            File.Delete(csprojFile.FullName);
            File.Move(bak, csprojFile.FullName);
            return true;
        }

        public static bool CanRevert(FileInfo csprojFile)
        {
            return File.Exists($"{csprojFile.FullName}.bak");
        }
    }
}
