using Model.HospitalResources;
using Model.Schedule.Hospitalizations;
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

namespace SekretarView
{
    class HospitalizationFilterViewModel : ViewModelBase
    {
        private Hospitalization _hospitalization;

        private ViewModelBase _caller;
        private Action<Hospitalization> _callback;
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
                    _patientDetails = new RelayCommand(p => _changeViewCommand.Execute(new PatientDetailsViewModel(_hospitalization.Patient, this, false, _changeViewCommand)));
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
                if (_hospitalization.Patient != null)
                    return _hospitalization.Patient.Name + " " + _hospitalization.Patient.Surname;
                else
                    return null;
            }
        }

        public Boolean PatientSelected
        {
            get
            {
                return _hospitalization.Patient != null;
            }
        }


        public Room Room
        {
            get
            {
                return _hospitalization.Room;
            }
            set
            {
                _hospitalization.Room = value;
                OnPropertyChanged("Room");
            }
        }

        public HospitalizationType Type
        {
            get
            {
                return _hospitalization.HospitalizationType;
            }
            set
            {
                _hospitalization.HospitalizationType = value;
                OnPropertyChanged("Type");
            }
        }

        public ObservableCollection<HospitalizationType> Types { get; set; }
        public ObservableCollection<Room> Rooms { get; set; }

        public HospitalizationFilterViewModel(ICommand changeViewCommand, ViewModelBase caller,
            Action<Hospitalization> callback, Hospitalization hospitalization) : base("Filtriraj bolnička lečenja", true)
        {
            _changeViewCommand = changeViewCommand;
            _callback = callback;
            _caller = caller;
            _hospitalization = hospitalization;

            Types = new ObservableCollection<HospitalizationType>(DataMockup.Instance.HospitalizationTypes);
            Rooms = new ObservableCollection<Room>(DataMockup.Instance.Rooms.Where(p => p.Purpose == RoomType.recoveryRoom));
        }

        private void filter()
        {
            _callback.Invoke(_hospitalization);
            _changeViewCommand.Execute(_caller);
        }

        private void startPatientSelection()
        {
            ViewModelBase selection = new PatientSelectionViewModel(_changeViewCommand, patientSelected, this);
            _changeViewCommand.Execute(selection);
        }

        private void patientSelected(Patient patient)
        {
            _hospitalization.Patient = patient;
            OnPropertyChanged("PatientNameAndSurname");
            OnPropertyChanged("PatientSelected");
        }
    }
}
