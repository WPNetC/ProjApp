using System.Collections.Generic;
using System.ComponentModel;

namespace VsIncludeEditor.Models
{
    public struct ContentModel : IInclude
    {
        public Dictionary<string, string> Properties { get; set; }

        public string Include { get; set; }
        private bool _isSelected;
        public bool IsSelected {
            get { return _isSelected; }
            set
            {
                if(value != _isSelected)
                {
                    _isSelected = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsSelected"));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
