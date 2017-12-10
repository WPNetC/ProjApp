using System;
using System.Linq;
using System.Windows.Input;

namespace VsIncludeEditor.Modules.GitView
{
    internal sealed class CheckoutBranch : ICommand
    {
        public CheckoutBranch(GitViewModel vm)
        {
            this._vm = vm;
        }

        private GitViewModel _vm;

        bool ICommand.CanExecute(object parameter)
        {
            return _vm.CanCheckoutBranch;
        }

        event EventHandler ICommand.CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        void ICommand.Execute(object parameter)
        {
            _vm.CheckoutBranch();
        }
    }

    internal sealed class CheckoutCommit : ICommand
    {
        public CheckoutCommit(GitViewModel vm)
        {
            this._vm = vm;
        }

        private GitViewModel _vm;

        bool ICommand.CanExecute(object parameter)
        {
            return _vm.CanCheckoutCommit;
        }

        event EventHandler ICommand.CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        void ICommand.Execute(object parameter)
        {
            _vm.CheckoutCommit();
        }
    }
}
