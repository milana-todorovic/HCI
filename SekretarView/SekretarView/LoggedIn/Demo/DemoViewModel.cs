using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SekretarView
{
    class DemoViewModel : ViewModelBase
    {
        private Boolean _demoActive;

        private ViewModelBase _caller;

        private ICommand _back;
        private ICommand _changeViewCommand;
        private ICommand _startDemo;
        private ICommand _stopDemo;

        public ICommand StopDemo
        {
            get
            {
                if (_stopDemo == null)
                {
                    _stopDemo = new RelayCommand(p => stopDemo());
                }
                return _stopDemo;
            }
        }

        public ICommand StartDemo
        {
            get
            {
                if (_startDemo == null)
                {
                    _startDemo = new RelayCommand(p => DemoActive = true);
                }
                return _startDemo;
            }
        }

        public Boolean DemoActive
        {
            get
            {
                return _demoActive;
            }
            set
            {
                _demoActive = value;
                OnPropertyChanged("DemoActive");
            }
        }
        
        public ICommand Back
        {
            get
            {
                if (_back == null)
                    _back = new RelayCommand(p => _changeViewCommand.Execute(_caller));
                return _back;
            }
        }

        public DemoViewModel(ViewModelBase caller, ICommand changeViewCommand) : base("Demo")
        {
            _caller = caller;
            _changeViewCommand = changeViewCommand;
        }

        private void stopDemo()
        {
            DemoActive = false;
            _changeViewCommand.Execute(_caller);
        }
    }
}
