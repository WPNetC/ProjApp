namespace VsIncludeEditor.Models
{
    public struct ContentModel : ISelectable
    {
        public string Include { get; set; }
        public string SubType { get; set; }
        public string DependentUpon { get; set; }
        public bool IsSelected { get; set; }
    }
}
