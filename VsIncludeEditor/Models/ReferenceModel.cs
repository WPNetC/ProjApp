using System.ComponentModel;
using System.Text.RegularExpressions;

namespace VsIncludeEditor.Models
{
    public struct ReferenceModel: IInclude
    {
        public string Include { get; set; }
        public string SpecificVersion { get; set; }
        public string HintPath { get; set; }
        public string RequiredTargetFramework { get; set; }
        public bool? Private { get; set; }

        public string ShortName => Include?.Split(',')[0];
        public string Version => Include != null ? Regex.Match(Include, "Version=([\\d\\.]+)")?.Value : null;
        public string Culture => Include != null ? Regex.Match(Include, "Culture=([a-zA-Z]+)")?.Value : null;
        public string PublicKey => Include != null ? Regex.Match(Include, "PublicKeyToken=([a-zA-Z0-9]+)")?.Value : null;
        public string Architecture => Include != null ? Regex.Match(Include, "processorArchitecture=([a-zA-Z0-9]+)")?.Value : null;

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value != _isSelected)
                {
                    _isSelected = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsSelected"));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
