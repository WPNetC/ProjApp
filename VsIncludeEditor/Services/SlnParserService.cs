using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VsIncludeEditor.Models;

namespace VsIncludeEditor.Services
{
    public class SlnParserService
    {
        public static List<ProjectModel> GetProjects(string slnPath)
        {
            var result = new List<ProjectModel>();
            var slnText = File.ReadAllLines(slnPath);

            foreach (var line in slnText)
            {
                if (line.StartsWith("Project("))
                {
                    var parts = line.Split(',');

                    var name = parts[0]
                        .Split('=')[1]
                        .Replace("\"", "")
                        .Trim();

                    var path = parts[1]
                        .Replace("\"", "")
                        .Trim();

                    var guid = Guid.Parse(parts[2]
                        .Replace("\"", "")
                        .Replace("{","")
                        .Replace("}","")
                        .Trim());

                    var absPath = Path.Combine(Path.GetDirectoryName(slnPath), path);
                    FileInfo fi = File.Exists(absPath) ? new FileInfo(absPath) : null;


                    result.Add(new ProjectModel
                    {
                        Name = name,
                        RelativePath = path,
                        Guid = guid,
                        FileInfo = fi
                    });
                }
            }

            return result;
        }
    }
}
