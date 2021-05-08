using Model.Users.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SekretarView
{
    class PatientFilterViewModel : ViewModelBase
    {
        private String _name;
        private String _surname;
        private String _jmbg;

        private ViewModelBase _caller;
        private Action<String, String, String> _callback;
        private ICommand _filter;
        private ICommand _cancel;
        private ICommand _changeViewCommand;

        public ICommand Cancel
        {
            get
            {
                if (_cancel == null)
                    _cancel = new RelayCommand(p => _changeViewCommand.Execute(_caller));
                return _cancel;
            }
        }

        public ICommand Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new RelayCommand(p => filter(), p => !HasErrors);
                return _filter;
            }
        }

        public String Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public String Surame
        {
            get
            {
                return _surname;
            }
            set
            {
                _surname = value;
                OnPropertyChanged("Surname");
            }
        }

        public String JMBG
        {
            get
            {
                return _jmbg;
            }
            set
            {
                _jmbg = value;

                if (value != null && !value.Equals("") && !value.All(c => c >= '0' && c <= '9'))
                    OnErrorChanged("JMBG", "Matični broj sme sadržati cifre od 0 do 9.");
                else if (value != null && !value.Equals("") && value.Length != 13)
                    OnErrorChanged("JMBG", "Matični broj mora sadržati tačno 13 cifara.");
                else
                    OnErrorChanged("JMBG", "");

                OnPropertyChanged("JMBG");
            }
        }

        private void filter()
        {
            _callback.Invoke(_name, _surname, _jmbg);
            _changeViewCommand.Execute(_caller);
        }

        public PatientFilterViewModel(ViewModelBase caller, Action<String, String, String> callback, ICommand changeViewCommand, Patient patient) : base("Filtriraj pacijente", true)
        {
            _name = patient.Name;
            _surname = patient.Surname;
            _jmbg = patient.JMBG;
            _caller = caller;
            _callback = callback;
            _changeViewCommand = changeViewCommand;
        }
    }
}
