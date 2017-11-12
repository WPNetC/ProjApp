using System;
using System.Windows.Input;

namespace VsIncludeEditor.Modules.IncludeEditor
{
    internal sealed class ToggleGroupSelect : ICommand
    {
        public ToggleGroupSelect(IncludeEditorViewModel vm)
        {
            this._vm = vm;
        }

        private IncludeEditorViewModel _vm;

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
            _vm.ToggleGroupSelect();
        }
    }
}
