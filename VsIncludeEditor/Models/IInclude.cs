using System.Collections.Generic;
using System.ComponentModel;

namespace VsIncludeEditor.Models
{
    public interface IInclude : INotifyPropertyChanged
    {
        string Include { get; set; }
        Dictionary<string, string> Properties { get; set; }
        bool IsSelected { get; set; }
    }
}
