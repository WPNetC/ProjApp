using System.ComponentModel;

namespace VsIncludeEditor.Models
{
    public interface ISelectable : INotifyPropertyChanged
    {
        bool IsSelected { get; set; }
    }
}
