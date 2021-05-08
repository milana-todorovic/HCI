using Model.Users.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SekretarView
{
    class PatientDetailsViewModel : ViewModelBase
    {
        private Patient _patient;
        private ViewModelBase _caller;
        private Boolean _canSchedule;

        private ICommand _changeViewCommand;
        private ICommand _scheduleSurgery;
        private ICommand _scheduleExamination;
        private ICommand _scheduleHospitalization;
        private ICommand _register;
        private ICommand _back;
        

        public ICommand Register
        {
            get
            {
                if (_register == null)
                    _register = new RelayCommand(p => _changeViewCommand.Execute(new RegisterPatientViewModel(_patient, _changeViewCommand, (ViewModelBase)p)));
                return _register;
            }
        }

        public Boolean CanSchedule
        {
            get
            {
                return _canSchedule;
            }
        }

        public ICommand ScheduleExamination
        {
            get
            {
                if (_scheduleExamination == null)
                    _scheduleExamination = new RelayCommand(p => _changeViewCommand.Execute(new ChoosePatientAndTypeViewModel("Zakaži pregled", this, _changeViewCommand, true, _patient)));
                return _scheduleExamination;
            }
        }

        public ICommand ScheduleHospitalization
        {
            get
            {
                if (_scheduleHospitalization == null)
                    _scheduleHospitalization = new RelayCommand(p => _changeViewCommand.Execute(new HospitalizationTypeAndPatientViewModel(this, _changeViewCommand, _patient)));
                return _scheduleHospitalization;
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

        public ICommand ScheduleSurgery
        {
            get
            {
                if (_scheduleSurgery == null)
                    _scheduleSurgery = new RelayCommand(p => _changeViewCommand.Execute(new ChoosePatientAndTypeViewModel("Zakaži operaciju", this, _changeViewCommand, false, _patient)));
                return _scheduleSurgery;
            }
        }

        public Patient Patient
        {
            get
            {
                return _patient;
            }
        }

        public String FullName
        {
            get
            {
                return _patient.Name + " " + _patient.Surname;
            }
        }

        public DateTime DateOfBirth
        {
            get
            {
                return _patient.DateOfBirth;
            }
        }

        public String PhoneNumber
        {
            get
            {
                return _patient.TelephoneNumber;
            }
        }

        public String JMBG
        {
            get
            {
                return _patient.JMBG;
            }
        }

        public Boolean CanBeRegistered
        {
            get
            {
                return !_patient.Registered && _canSchedule;
            }
        }

        public String RegistrationStatus
        {
            get
            {
                if (_patient.Registered)
                    return "Registrovan";
                else
                    return "Nije registrovan";
            }
        }

        public PatientDetailsViewModel(Patient patient, ViewModelBase caller, Boolean canSchedule, ICommand changeViewCommand) : base("Informacije o pacijentu", true)
        {
            this._patient = patient;
            _caller = caller;
            _canSchedule = canSchedule;
            _changeViewCommand = changeViewCommand;
        }

        public Boolean matches(Patient other)
        {
            if (other.JMBG != null && !other.JMBG.Equals("") && !other.JMBG.Equals(_patient.JMBG))
                return false;

            if (other.Name != null && !other.Name.Equals("") && !_patient.Name.ToLower().Contains(other.Name.ToLower()))
                return false;

            if (other.Surname != null && !other.Surname.Equals("") && !_patient.Surname.ToLower().Contains(other.Surname.ToLower()))
                return false;

            return true;
        }
    }
}
