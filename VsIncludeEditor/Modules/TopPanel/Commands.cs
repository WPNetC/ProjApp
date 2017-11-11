using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace VsIncludeEditor.Modules.TopPanel
{
    internal sealed class OpenProject : ICommand
    {
        public OpenProject(TopPanelViewModel vm)
        {
            this._vm = vm;
        }

        private TopPanelViewModel _vm;

        bool ICommand.CanExecute(object parameter)
        {
            return true;
        }

        event EventHandler ICommand.CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        void ICommand.Execute(object parameter)
        {
            _vm.OpenProject();
        }
    }

}
