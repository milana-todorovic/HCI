using Model.Users.Generalities;
using Model.Users.Patient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SekretarView
{
    class AddGuestAccountViewModel : ViewModelBase
    {
        protected Patient _patient;
        protected DateTime? _dateOfBirth;

        protected ICommand _changeViewCommand;
        protected ICommand _savePatientCommand;
        protected ICommand _cancelCommand;
        protected ViewModelBase _caller;
        protected Action<Patient> _saved;

        public ICommand SavePatientCommand
        {
            get
            {
                if (_savePatientCommand == null)
                    _savePatientCommand = new RelayCommand(p => savePatient(), p => canSavePatient());

                return _savePatientCommand;
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                    _cancelCommand = new RelayCommand(p => _changeViewCommand.Execute(_caller));

                return _cancelCommand;
            }
        }

        public String Name
        {
            get
            {
                return _patient.Name;
            }
            set
            {
                if (_patient.Name == null || !_patient.Name.Equals(value))
                {
                    _patient.Name = value;

                    if (value == null || value.Equals(""))
                        OnErrorChanged("Name", "Polje je obavezno.");
                    else
                        OnErrorChanged("Name", "");

                    OnPropertyChanged("Name");
                }
            }
        }

        public String Surname
        {
            get
            {
                return _patient.Surname;
            }
            set
            {
                if (_patient.Surname == null || !_patient.Surname.Equals(value))
                {
                    _patient.Surname = value;

                    if (value == null || value.Equals(""))
                        OnErrorChanged("Surname", "Polje je obavezno.");
                    else
                        OnErrorChanged("Surname", "");

                    OnPropertyChanged("Surname");
                }
            }
        }

        public DateTime? DateOfBirth
        {
            get
            {
                return _dateOfBirth;
            }
            set
            {
                _dateOfBirth = value;

                if (value == null)
                    OnErrorChanged("DateOfBirth", "Polje je obavezno.");
                else if (value > DateTime.Now)
                    OnErrorChanged("DateOfBirth", "Datum rođenja ne može biti u budućnosti.");
                else
                    OnErrorChanged("DateOfBirth", "");

                OnPropertyChanged("DateOfBirth");
            }
        }

        public String JMBG
        {
            get
            {
                return _patient.JMBG;
            }
            set
            {
                if (_patient.JMBG == null || !_patient.JMBG.Equals(value))
                {
                    _patient.JMBG = value;

                    if (value == null || value.Equals(""))
                        OnErrorChanged("JMBG", "Polje je obavezno.");
                    else if (!value.All(c => c >= '0' && c <= '9'))
                        OnErrorChanged("JMBG", "Matični broj sme sadržati cifre od 0 do 9.");
                    else if (value.Length != 13)
                        OnErrorChanged("JMBG", "Matični broj mora sadržati tačno 13 cifara.");
                    else if (DataMockup.Instance.Patients.Exists(p => p.JMBG.Equals(value)))
                        OnErrorChanged("JMBG", "Matični broj mora biti jedinstven.");
                    else
                        OnErrorChanged("JMBG", "");

                    OnPropertyChanged("JMBG");
                }
            }
        }

        public Gender Gender
        {
            get
            {
                return _patient.Gender;
            }
            set
            {
                if (!_patient.Gender.Equals(value))
                {
                    _patient.Gender = value;
                    OnPropertyChanged("Gender");
                }
            }
        }

        public String PhoneNumber
        {
            get
            {
                return _patient.TelephoneNumber;
            }
            set
            {
                if (_patient.TelephoneNumber == null || !_patient.TelephoneNumber.Equals(value))
                {
                    _patient.TelephoneNumber = value;

                    if (value == null || value.Equals(""))
                        OnErrorChanged("PhoneNumber", "Polje je obavezno.");
                    else if (!value.All(c => (c >= '0' && c <= '9') || c == '-' || c == '/'))
                        OnErrorChanged("PhoneNumber", "Broj telefona sme sadržati cifre od 0 do 9, i karaktere / i -.");
                    else
                        OnErrorChanged("PhoneNumber", "");

                    OnPropertyChanged("PhoneNumber");
                }
            }
        }

        public AddGuestAccountViewModel(ICommand changeViewCommand, ViewModelBase caller) : base("Dodaj pacijenta", true)
        {
            _patient = new Patient();
            DateOfBirth = DateTime.Now;
            _patient.Registered = false;
            _changeViewCommand = changeViewCommand;
            _caller = caller;
            _saved = null;
        }

        public AddGuestAccountViewModel(ICommand changeViewCommand, Action<Patient> saved, ViewModelBase caller) : base("Dodaj pacijenta", true)
        {
            _patient = new Patient();
            DateOfBirth = DateTime.Now.Date;
            _patient.Registered = false;
            _changeViewCommand = changeViewCommand;
            _caller = caller;
            _saved = saved;
        }

        protected virtual Boolean canSavePatient()
        {
            if (HasErrors)
                return false;

            if (DateOfBirth == null)
                return false;

            if (Name == null || Name.Equals(""))
                return false;

            if (Surname == null || Surname.Equals(""))
                return false;

            if (JMBG == null || JMBG.Equals(""))
                return false;

            if (PhoneNumber == null || PhoneNumber.Equals(""))
                return false;

            return true;
        }

        protected virtual void savePatient()
        {
            _patient.DateOfBirth = DateOfBirth.Value;
            DataMockup.Instance.Patients.Add(_patient);

            if (_saved != null)
                _saved.Invoke(_patient);

            Mediator.NotifyColleagues("PatientAdded", _patient);
            _changeViewCommand.Execute(_caller);
        }
    }
}
