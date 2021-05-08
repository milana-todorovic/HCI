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
    class RegisterPatientViewModel : AddGuestAccountViewModel
    {
        private Patient _realPatient;

        private String _username;
        private String _password;

        public String Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;

                if (value == null || value.Equals(""))
                    OnErrorChanged("Username", "Polje je obavezno.");
                else
                    OnErrorChanged("Username", null);

                OnPropertyChanged("Username");
            }
        }

        public String Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;

                if (value == null || value.Equals(""))
                    OnErrorChanged("Password", "Polje je obavezno.");
                else
                    OnErrorChanged("Password", null);

                OnPropertyChanged("Password");
            }
        }

        public ObservableCollection<City> Cities
        {
            get; private set;
        }

        public City City
        {
            get
            {
                return _patient.Address.City;
            }
            set
            {
                _patient.Address.City = value;

                if (value == null)
                    OnErrorChanged("City", "Mora biti izabran jedan od ponuđenh gradova.");
                else
                    OnErrorChanged("City", null);

                OnPropertyChanged("City");
            }
        }

        public String Street
        {
            get
            {
                return _patient.Address.Street;
            }
            set
            {
                _patient.Address.Street = value;

                if (value == null || value.Equals(""))
                    OnErrorChanged("Street", "Polje je obavezno.");
                else
                    OnErrorChanged("Street", null);

                OnPropertyChanged("Street");
            }
        }

        public RegisterPatientViewModel(ICommand changeViewCommand, ViewModelBase caller) : base(changeViewCommand, caller)
        {
            _realPatient = null;
            _patient = new Patient() { Address = new Address() };

            Cities = new ObservableCollection<City>(DataMockup.Instance.Cities);
        }

        public RegisterPatientViewModel(Patient patient, ICommand changeViewCommand, ViewModelBase caller) : base(changeViewCommand, caller)
        {
            _realPatient = patient;
            _patient = new Patient()
            {
                Name = patient.Name,
                Surname = patient.Surname,
                JMBG = patient.JMBG,
                DateOfBirth = patient.DateOfBirth,
                TelephoneNumber = patient.TelephoneNumber,
                Gender = patient.Gender,
                Address = new Address()
            };

            Cities = new ObservableCollection<City>(DataMockup.Instance.Cities);
        }

        protected override Boolean canSavePatient()
        {
            if (_patient.Address.City == null)
                return false;
            if (_patient.Address.Street == null || _patient.Address.Street.Equals(""))
                return false;
            if (_username == null || _username.Equals(""))
                return false;
            if (_username == null || _username.Equals(""))
                return false;

            return base.canSavePatient();
        }

        protected override void savePatient()
        {
            if (_realPatient == null)
            {
                _patient.DateOfBirth = DateOfBirth.Value;
                _patient.Registered = true;
                DataMockup.Instance.Patients.Add(_patient);

                if (_saved != null)
                    _saved.Invoke(_patient);

                Mediator.NotifyColleagues("PatientAdded", _patient);
                _changeViewCommand.Execute(_caller);
            }
            else
            {
                _realPatient.Registered = true;
                _realPatient.Name = _patient.Name;
                _realPatient.Surname = _patient.Surname;
                _realPatient.DateOfBirth = DateOfBirth.Value;
                _realPatient.Gender = _patient.Gender;
                _realPatient.TelephoneNumber = _patient.TelephoneNumber;
                _realPatient.Address = _patient.Address;

                Mediator.NotifyColleagues("PatientUpdated", _realPatient);
            }
            
        }
    }
}
