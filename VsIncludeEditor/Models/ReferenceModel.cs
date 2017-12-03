using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace VsIncludeEditor.Models
{
    public struct ReferenceModel: IInclude, IEquatable<ReferenceModel>
    {
        public string Include { get; set; }
        public Dictionary<string, string> Properties { get; set; }

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
        public static bool operator == (ReferenceModel a, ReferenceModel b)
        {
            if (ReferenceEquals(null, a))
                return ReferenceEquals(null, b);
            if (ReferenceEquals(null, b))
                return ReferenceEquals(null, a);

            return a.Equals(b);
        }
        public static bool operator !=(ReferenceModel a, ReferenceModel b)
        {
            if (ReferenceEquals(null, a))
                return !ReferenceEquals(null, b);
            if (ReferenceEquals(null, b))
                return !ReferenceEquals(null, a);

            return !a.Equals(b);
        }
        public bool Equals(ReferenceModel other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return this.GetHashCode() == other.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if(obj is ReferenceModel)
                return Equals((ReferenceModel)obj);
            return false;
        }
        public override int GetHashCode()
        {
            return Include?.GetHashCode() ?? base.GetHashCode();
        }
        public override string ToString()
        {
            return ShortName;
        }
    }
}
