using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Snake
{
    class LoginWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand SendNameRequest { get; set; }

        Model _model; 

        public LoginWindowViewModel()
        {
            _model = new Model();
            SendNameRequest = new SendNameRequest(_model);
        }
        
    }
}
