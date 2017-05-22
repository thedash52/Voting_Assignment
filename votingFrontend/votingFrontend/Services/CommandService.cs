using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace votingFrontend.Services
{
    public class CommandService : ICommand
    {
        private Predicate<object> canExecute;
        private Action<object> execute;

        public CommandService(Action<object> execute, Predicate<object> canExecute)
        {
            this.canExecute = canExecute;
            this.execute = execute;
        }

        public CommandService(Action<object> execute) : this(execute, null)
        {

        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return this.canExecute == null ? true : this.canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }

            this.execute(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
