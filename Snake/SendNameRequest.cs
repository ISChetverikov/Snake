using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Snake
{
    class SendNameRequest : ICommand
    {
        private Model _model;

        public event EventHandler CanExecuteChanged;

        public SendNameRequest(Model model)
        {
            _model = model;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _model.GetNameAsync(parameter);
        }
    }
}
