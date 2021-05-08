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
    class ChoosePatientAndTypeViewModel : ViewModelBase
    {
        private Procedure _procedure;

        private ViewModelBase _caller;

        private ICommand _changeViewCommand;
        private ICommand _addPatient;
        private ICommand _selectPatient;
        private ICommand _cancel;
        private ICommand _nextStep;
        private ICommand _patientDetails;
        private ICommand _launchDemo;
        private ICommand _nextStepDummy;

        public ICommand NextStepDummy
        {
            get
            {
                if (_nextStepDummy == null)
                    _nextStepDummy = new RelayCommand(p => { }, p=> canGoToNextStep());
                return _nextStepDummy;
            }
        }

        public Procedure Procedure
        {
            get
            {
                return _procedure;
            }
        }
        
        public ICommand LaunchDemo
        {
            get
            {
                if (_launchDemo == null)
                    _launchDemo = new RelayCommand(p => _changeViewCommand.Execute(new DemoViewModel(this, _changeViewCommand)));
                return _launchDemo;
            }
        }

        private ObservableCollection<ProcedureType> _types;
        private String _typeTitle;

        public Boolean ShowDuration
        {
            get
            {
                return true;
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
                    _patientDetails = new RelayCommand(p => _changeViewCommand.Execute(new PatientDetailsViewModel(_procedure.Patient, this, false, _changeViewCommand)));
                return _patientDetails;
            }
        }

        public ICommand NextStep
        {
            get
            {
                if (_nextStep == null)
                    _nextStep = new RelayCommand(p => _changeViewCommand.Execute(
                        new ChooseSchedulingInfoViewModel(Title, _procedure, _caller, this, _changeViewCommand)),
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

        public ObservableCollection<ProcedureType> Types
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
                return _procedure.ProcedureType != null;
            }
        }

        public ProcedureType SelectedType
        {
            get
            {
                return _procedure.ProcedureType;
            }
            set
            {
                _procedure.ProcedureType = value;

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
                return _procedure.Patient != null;
            }
        }

        public String PatientNameAndSurname
        {
            get
            {
                if (PatientSelected)
                    return _procedure.Patient.Name + " " + _procedure.Patient.Surname;
                else
                    return "";
                   
            }
        }

        public void patientSelected(Patient patient)
        {
            _procedure.Patient = patient;
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
            if (_procedure.Patient == null)
                return false;
            if (_procedure.ProcedureType == null)
                return false;

            return true;
        }

        public ChoosePatientAndTypeViewModel(String title, ViewModelBase caller,
            ICommand changeViewCommand, Boolean examination) : base(title, true)
        {
            if (examination)
            {
                _procedure = new Examination();
                _types = new ObservableCollection<ProcedureType>(
                    DataMockup.Instance.ProcedureTypes.Where(p => p.Kind.Equals(ProcedureKind.examination)));
                _typeTitle = "Tip pregleda";
            }
            else
            {
                _procedure = new Surgery();
                _types = new ObservableCollection<ProcedureType>(
                    DataMockup.Instance.ProcedureTypes.Where(p => p.Kind.Equals(ProcedureKind.surgery)));
                _typeTitle = "Tip operacije";
            }
            if (Types.Count != 0)
                SelectedType = Types[0];
            else
                SelectedType = null;
            _caller = caller;
            _changeViewCommand = changeViewCommand;
        }

        public ChoosePatientAndTypeViewModel(String title, ViewModelBase caller,
            ICommand changeViewCommand, Boolean examination, Patient patient) : base(title, true)
        {
            if (examination)
            {
                _procedure = new Examination();
                _types = new ObservableCollection<ProcedureType>(
                    DataMockup.Instance.ProcedureTypes.Where(p => p.Kind.Equals(ProcedureKind.examination)));
                _typeTitle = "Tip pregleda";
            }
            else
            {
                _procedure = new Surgery();
                _types = new ObservableCollection<ProcedureType>(
                    DataMockup.Instance.ProcedureTypes.Where(p => p.Kind.Equals(ProcedureKind.surgery)));
                _typeTitle = "Tip operacije";
            }
            if (Types.Count != 0)
                SelectedType = Types[0];
            else
                SelectedType = null;
            _caller = caller;
            _changeViewCommand = changeViewCommand;
            _procedure.Patient = patient;
        }
    }
}
