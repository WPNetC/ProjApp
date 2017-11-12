using System.ComponentModel;

namespace VsIncludeEditor.Models
{
    public interface IInclude : INotifyPropertyChanged
    {
        string Include { get; set; }
        bool IsSelected { get; set; }
    }
}
