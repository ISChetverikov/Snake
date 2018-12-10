using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Snake
{
    public sealed class SendDirectionRequest : ICommand
    {
        Model _model;

        public SendDirectionRequest(Model model)
        {
            _model = model;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            await _model.SendDirectionRequestAsync((string)parameter);
        }
    }
}
