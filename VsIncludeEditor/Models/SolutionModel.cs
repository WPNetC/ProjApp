using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VsIncludeEditor.Models
{
    public class SolutionModel : IEquatable<SolutionModel>
    {
        public string Name { get; set; }
        public FileInfo FileInfo { get; set; }

        public bool Equals(SolutionModel other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;

            return this.Name == other.Name;
        }
        public override bool Equals(object obj)
        {
            if (obj is ProjectModel)
                return Equals((ProjectModel)obj);
            return false;
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
