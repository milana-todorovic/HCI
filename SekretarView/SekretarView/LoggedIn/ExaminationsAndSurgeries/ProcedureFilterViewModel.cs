using Model.HospitalResources;
using Model.Schedule.Procedures;
using Model.Users.Employee;
using Model.Users.Patient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SekretarView { 
    class ProcedureFilterViewModel : ViewModelBase
    {
        private Procedure _procedure;
        private Specialty _specialty;

        private ViewModelBase _caller;
        private Action<Procedure, Specialty> _callback;
        private ICommand _filter;
        private ICommand _cancel;
        private ICommand _changeViewCommand;
        private ICommand _selectPatient;
        private ICommand _clearPatientSelection;
        private ICommand _patientDetails;

        public ICommand PatientDetails
        {
            get
            {
                if (_patientDetails == null)
                    _patientDetails = new RelayCommand(p => _changeViewCommand.Execute(new PatientDetailsViewModel(_procedure.Patient, this, false, _changeViewCommand)));
                return _patientDetails;
            }
        }

        public ICommand ClearPatientSelection
        {
            get
            {
                if (_clearPatientSelection == null)
                    _clearPatientSelection = new RelayCommand(p => patientSelected(null));
                return _clearPatientSelection;
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

        public ICommand Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new RelayCommand(p => filter());
                return _filter;
            }
        }

        public Specialty Specialty
        {
            get
            {
                return _specialty;
            }
            set
            {
                _specialty = value;
                OnPropertyChanged("Specialty");
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

        public String PatientNameAndSurname
        {
            get
            {
                if (_procedure.Patient != null)
                    return _procedure.Patient.Name + " " + _procedure.Patient.Surname;
                else
                    return null;
            }
        }

        public Boolean PatientSelected
        {
            get
            {
                return _procedure.Patient != null;
            }
        }

        public Doctor Doctor
        {
            get
            {
                return _procedure.Doctor;
            }
            set
            {
                _procedure.Doctor = value;
                OnPropertyChanged("Doctor");
            }
        }

        public Room Room
        {
            get
            {
                return _procedure.Room;
            }
            set
            {
                _procedure.Room = value;
                OnPropertyChanged("Room");
            }
        }

        public ProcedureType Type
        {
            get
            {
                return _procedure.ProcedureType;
            }
            set
            {
                _procedure.ProcedureType = value;
                OnPropertyChanged("Type");
            }
        }

        public ObservableCollection<ProcedureType> Types { get; set; }
        public ObservableCollection<Doctor> Doctors { get; set; }
        public ObservableCollection<Specialty> Specialties { get; set; }
        public ObservableCollection<Room> Rooms { get; set; }

        public ProcedureFilterViewModel(String title, ICommand changeViewCommand, ViewModelBase caller, 
            Action<Procedure, Specialty> callback, Procedure procedure, Specialty specialty, 
            ObservableCollection<ProcedureType> types, ObservableCollection<Room> rooms) : base(title, true)
        {
            _changeViewCommand = changeViewCommand;
            _callback = callback;
            _caller = caller;
            _procedure = procedure;
            _specialty = specialty;

            Types = types;
            Rooms = rooms;
            Doctors = new ObservableCollection<Doctor>(DataMockup.Instance.Doctors);
            Specialties = new ObservableCollection<Specialty>(DataMockup.Instance.Specialties);
        }

        private void filter()
        {
            _callback.Invoke(_procedure, _specialty);
            _changeViewCommand.Execute(_caller);     
        }

        private void startPatientSelection()
        {
            ViewModelBase selection = new PatientSelectionViewModel(_changeViewCommand, patientSelected, this);
            _changeViewCommand.Execute(selection);
        }

        private void patientSelected(Patient patient)
        {
            _procedure.Patient = patient;
            OnPropertyChanged("PatientNameAndSurname");
            OnPropertyChanged("PatientSelected");
        }

    }
}
