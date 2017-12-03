using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VsIncludeEditor.Interfaces
{
    public interface IProjectView
    {
        void SetProject(FileInfo fileInfo);
    }
}
