using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SekretarView
{
    class MenuItemViewModel : ViewModelBase
    {
        private ICommand _command;
        private ViewModelBase _commandParameter;

        public ICommand Command {
            get
            {
                return _command;
            }
        }

        public ViewModelBase CommandParameter
        {
            get
            {
                return _commandParameter;
            }
        }

        public MenuItemViewModel(String name, ICommand command, ViewModelBase commandParameter) : base(name)
        {
            _command = command;
            _commandParameter = commandParameter;
        }
    }
}
