﻿using System;
using System.Linq;
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

    internal sealed class ExcludeSelected : ICommand
    {
        public ExcludeSelected(IncludeEditorViewModel vm)
        {
            this._vm = vm;
        }

        private IncludeEditorViewModel _vm;

        bool ICommand.CanExecute(object parameter)
        {
            return _vm.SelectedNodes.Any();
        }

        event EventHandler ICommand.CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        void ICommand.Execute(object parameter)
        {
            _vm.ExcludeSelected();
        }
    }

    internal sealed class RevertChange : ICommand
    {
        public RevertChange(IncludeEditorViewModel vm)
        {
            this._vm = vm;
        }

        private IncludeEditorViewModel _vm;

        bool ICommand.CanExecute(object parameter)
        {
            return _vm.CanRevertChange();
        }

        event EventHandler ICommand.CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        void ICommand.Execute(object parameter)
        {
            _vm.RevertChange();
        }
    }
}
