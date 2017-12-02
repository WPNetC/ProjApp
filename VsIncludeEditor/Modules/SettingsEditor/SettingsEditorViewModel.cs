using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VsIncludeEditor.Modules.SettingsEditor
{
    public class SettingsEditorViewModel : ViewModelBase
    {
        private string _vsPath;

        public string VSPath
        {
            get
            {
                return _vsPath ?? (_vsPath = @"C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\IDE\devenv.exe");
            }
            set
            {
                if (value != _vsPath)
                {
                    _vsPath = value;
                    OnChanged();
                }
            }
        }
    }
}
