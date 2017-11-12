using System.ComponentModel;

namespace VsIncludeEditor.Models
{
    public struct ContentModel : IInclude
    {

        public string AutoGen { get; set; }
        public string DesignTime { get; set; }
        public string DesignTimeSharedInput { get; set; }
        public string Generator { get; set; }
        public string Include { get; set; }
        public string LastGenOutput { get; set; }
        public string Link { get; set; }
        public string SubType { get; set; }
        public string DependentUpon { get; set; }

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
