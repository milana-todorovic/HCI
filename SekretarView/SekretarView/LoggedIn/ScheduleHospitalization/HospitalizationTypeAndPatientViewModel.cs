using Model.Schedule.Hospitalizations;
using Model.Schedule.Procedures;
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
    class HospitalizationTypeAndPatientViewModel : ViewModelBase
    {
        private Hospitalization _hospitalization;

        private ViewModelBase _caller;

        private ICommand _changeViewCommand;
        private ICommand _addPatient;
        private ICommand _selectPatient;
        private ICommand _cancel;
        private ICommand _nextStep;
        private ICommand _patientDetails;
        

        

        private ObservableCollection<HospitalizationType> _types;
        private String _typeTitle;

        public Boolean ShowDuration
        {
            get
            {
                return false;
            }
        }
        
        public String TypeTitle
        {
            get
            {
                return _typeTitle;
            }
        }

        public ICommand PatientDetails
        {
            get
            {
                if (_patientDetails == null)
                    _patientDetails = new RelayCommand(p => _changeViewCommand.Execute(new PatientDetailsViewModel(_hospitalization.Patient, this, false, _changeViewCommand)));
                return _patientDetails;
            }
        }

        public ICommand NextStep
        {
            get
            {
                if (_nextStep == null)
                    _nextStep = new RelayCommand(p => _changeViewCommand.Execute(
                        new HospitalizationSchedulingInfoViewModel(Title, _hospitalization, _caller, this, _changeViewCommand)),
                        p => canGoToNextStep());
                return _nextStep;
            }
        }

        public ICommand Cancel
        {
            get
            {
                if (_cancel == null)
                    _cancel = new RelayCommand(p => _changeViewCommand.Execute(_caller));
                return _cancel;
            }
        }

        public ICommand AddPatient
        {
            get
            {
                if (_addPatient == null)
                    _addPatient = new RelayCommand(p => startAddingPatient());
                return _addPatient;
            }
        }

        public ICommand SelectPatient
        {
            get
            {
                if (_selectPatient == null)
                    _selectPatient = new RelayCommand(p => startPatientSelection());
                return _selectPatient;
            }
        }

        public ObservableCollection<HospitalizationType> Types
        {
            get
            {
                return _types;
            }
        }

        public Boolean TypeSelected
        {
            get
            {
                return _hospitalization.HospitalizationType != null;
            }
        }

        public HospitalizationType SelectedType
        {
            get
            {
                return _hospitalization.HospitalizationType;
            }
            set
            {
                _hospitalization.HospitalizationType = value;

                if (value == null)
                    OnErrorChanged("SelectedType", "Mora biti izabran jedan od postojećih tipova.");
                else
                    OnErrorChanged("SelectedType", "");

                OnPropertyChanged("SelectedType");
                OnPropertyChanged("TypeSelected");
            }
        }

        public Boolean PatientSelected
        {
            get
            {
                return _hospitalization.Patient != null;
            }
        }

        public String PatientNameAndSurname
        {
            get
            {
                if (PatientSelected)
                    return _hospitalization.Patient.Name + " " + _hospitalization.Patient.Surname;
                else
                    return "";

            }
        }

        private void patientSelected(Patient patient)
        {
            _hospitalization.Patient = patient;
            OnPropertyChanged("PatientSelected");
            OnPropertyChanged("PatientNameAndSurname");
        }

        private void startPatientSelection()
        {
            ViewModelBase selection = new PatientSelectionViewModel(_changeViewCommand, patientSelected, this);
            _changeViewCommand.Execute(selection);
        }

        private void startAddingPatient()
        {
            ViewModelBase addPatient = new AddGuestAccountViewModel(_changeViewCommand, patientSelected, this);
            _changeViewCommand.Execute(addPatient);
        }

        private Boolean canGoToNextStep()
        {
            if (HasErrors)
                return false;
            if (_hospitalization.Patient == null)
                return false;
            if (_hospitalization.HospitalizationType == null)
                return false;

            return true;
        }

        public HospitalizationTypeAndPatientViewModel(ViewModelBase caller,
            ICommand changeViewCommand) : base("Zakaži bolničko lečenje", true)
        {
            _hospitalization = new Hospitalization();
            _types = new ObservableCollection<HospitalizationType>(DataMockup.Instance.HospitalizationTypes);
            _typeTitle = "Tip bolničkog lečenja";

            if (Types.Count != 0)
                SelectedType = Types[0];
            else
                SelectedType = null;
            _caller = caller;
            _changeViewCommand = changeViewCommand;
        }

        public HospitalizationTypeAndPatientViewModel(ViewModelBase caller,
            ICommand changeViewCommand, Patient patient) : base("Zakaži operaciju", true)
        {
            _hospitalization = new Hospitalization();
            _hospitalization.Patient = patient;
            _types = new ObservableCollection<HospitalizationType>(DataMockup.Instance.HospitalizationTypes);
            _typeTitle = "Tip operacije";

            if (Types.Count != 0)
                SelectedType = Types[0];
            else
                SelectedType = null;
            _caller = caller;
            _changeViewCommand = changeViewCommand;
            _hospitalization.Patient = patient;
        }
    }
}
