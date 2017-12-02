using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VsIncludeEditor.Models
{
    public class ProjectModel : IEquatable<ProjectModel>
    {
        public string Name { get; set; }
        public string RelativePath { get; set; }
        public Guid Guid { get; set; }
        public FileInfo FileInfo { get; set; }

        public bool Equals(ProjectModel other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;

            return this.Guid == other.Guid;
        }
        public override bool Equals(object obj)
        {
            if (obj is ProjectModel)
                return Equals((ProjectModel)obj);
            return false;
        }
        public override int GetHashCode()
        {
            return Guid.GetHashCode();
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
