using System;
using System.Windows.Input;

namespace tictactoe_wpf.Model
{
    public class DelegateCommand : ICommand
    {
        private readonly Action<object> execute;
        private readonly Func<object, bool> canExecute;

        public DelegateCommand(Action<object> executeAction, Func<object, bool> canExecuteFunc)
        {
            execute = executeAction;
            canExecute = canExecuteFunc;
        }

        public DelegateCommand(Action<object> executeAction) : this(executeAction, null) { }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter)
        {
            if (canExecute != null)
            {
                return canExecute(parameter);
            }

            return true;
        }

        public void Execute(object parameter)
        {
            execute?.Invoke(parameter);
        }
    }
}
