using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VsIncludeEditor.Interfaces;
using VsIncludeEditor.Models;
using VsIncludeEditor.Services;

namespace VsIncludeEditor.Modules.ReferenceEditor
{
    public class ReferenceEditorViewModel : ViewModelBase, IProjectView
    {
        private ObservableCollection<ReferenceModel> _references;
        private ReferenceModel _selectedReference;

        public ObservableCollection<ReferenceModel> References
        {
            get {
                if (_references == null)
                    _references = new ObservableCollection<ReferenceModel>();
                return _references;
            }
            private set
            {
                if (value != _references)
                {
                    _references = value;
                    OnChanged();
                }
            }
        }
        public ReferenceModel SelectedReference
        {
            get
            {
                return _selectedReference;
            }
            set
            {
                if(value != _selectedReference)
                {
                    _selectedReference = value;
                    OnChanged();
                }
            }
        }

        public void SetProject(FileInfo fileInfo)
        {
            var parser = new ProjParserService();

            var refs = parser.GetReferences(fileInfo.FullName);
            if (refs == null)
                return;

            SetReferences(refs);
        }

        public void SetReferences(IEnumerable<ReferenceModel> models)
        {
            References.Clear();
            foreach (var item in models)
            {
                References.Add(item);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                References = null;
                SelectedReference = default(ReferenceModel);
            }
            base.Dispose(disposing);
        }
    }
}
