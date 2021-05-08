using Model.HospitalResources;
using Model.Schedule.Procedures;
using Model.Users.Employee;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SekretarView
{
    class ChooseSchedulingInfoViewModel : ViewModelBase
    {

        protected Boolean _exactDate;
        protected Boolean _exactTime;
        protected Boolean _exactDoctor;
        protected Boolean _exactRoom;
        protected Doctor _selectedDoctor;
        protected Room _selectedRoom;

        protected ObservableCollection<Doctor> _qualifiedDoctors;
        protected ObservableCollection<Room> _qualifiedRooms;

        protected Procedure _procedure;
        protected DateTime? _selectedDate;
        protected TimeSpan? _earliestTime;
        protected TimeSpan? _latestTime;

        protected ViewModelBase _previous;
        protected ViewModelBase _caller;

        protected ICommand _changeViewCommand;
        protected ICommand _nextStep;
        protected ICommand _previousStep;
        protected ICommand _cancel;
        private ICommand _nextStepDummy;

        public ICommand NextStepDummy
        {
            get
            {
                if (_nextStepDummy == null)
                    _nextStepDummy = new RelayCommand(p => { }, p => canGoToNextStep());
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

        public ObservableCollection<Doctor> Doctors
        {
            get
            {
                return _qualifiedDoctors;
            }
        }

        public ObservableCollection<Room> Rooms
        {
            get
            {
                return _qualifiedRooms;
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

        public ICommand NextStep
        {
            get
            {
                if (_nextStep == null)
                    _nextStep = new RelayCommand(
                        p => nextStep(),
                        p => canGoToNextStep());
                return _nextStep;
            }
        }

        public ICommand PreviousStep
        {
            get
            {
                if (_previousStep == null)
                    _previousStep = new RelayCommand(p => _changeViewCommand.Execute(_previous));
                return _previousStep;
            }
        }

        public String Type
        {
            get
            {
                return _procedure.ProcedureType.Name;
            }
        }

        public String Patient
        {
            get
            {
                return _procedure.Patient.Name + " " + _procedure.Patient.Surname;
            }
        }

        public DateTime? SelectedDate
        {
            get
            {
                return _selectedDate;
            }
            set
            {
                if (value.HasValue)
                    _selectedDate = value.Value.Date;
                else
                    _selectedDate = null;

                if (_exactDate)
                {
                    EarliestTime = EarliestTime;

                    if (value == null)
                        OnErrorChanged("SelectedDate", "Datum početka mora biti izabran.");
                    else if (value < DateTime.Now.Date)
                        OnErrorChanged("SelectedDate", "Izabrani datum mora biti u budućnosti.");
                    else
                        OnErrorChanged("SelectedDate", "");
                }
                else
                    OnErrorChanged("SelectedDate", "");

                OnPropertyChanged("SelectedDate");
            }
        }

        public TimeSpan? EarliestTime
        {
            get
            {
                return _earliestTime;
            }
            set
            {
                _earliestTime = value;

                if (_exactTime)
                {
                    LatestTime = LatestTime;

                    if (value == null)
                        OnErrorChanged("EarliestTime", "Najranije vreme početka mora biti izabrano.");
                    else if (_exactDate && SelectedDate != null && (SelectedDate + value) <= DateTime.Now)
                        OnErrorChanged("EarliestTime", "Izabrani najraniji termin mora biti u budućnosti.");
                    else
                        OnErrorChanged("EarliestTime", "");
                }
                else
                    OnErrorChanged("EarliestTime", "");


                OnPropertyChanged("EarliestTime");
            }
        }

        public TimeSpan? LatestTime
        {
            get
            {
                return _latestTime;
            }
            set
            {
                _latestTime = value;

                if (value == null)
                    OnErrorChanged("LatestTime", "Najkasnije vreme početka mora biti izabrano.");
                else if (_exactTime && EarliestTime != null && value < _earliestTime)
                    OnErrorChanged("LatestTime", "Najkasnije vreme mora biti posle najranijeg.");
                else
                    OnErrorChanged("LatestTime", "");

                OnPropertyChanged("LatestTime");
            }
        }

        public Boolean ExactDate
        {
            get
            {
                return _exactDate;
            }
            set
            {
                _exactDate = value;

                if (value == false)
                    SelectedDate = DateTime.Now.Date.AddDays(1);

                OnPropertyChanged("ExactDate");
            }
        }

        public Boolean ExactTime
        {
            get
            {
                return _exactTime;
            }
            set
            {
                _exactTime = value;

                if (value == false)
                {
                    EarliestTime = new TimeSpan(0, 0, 0);
                    LatestTime = new TimeSpan(23, 59, 59);
                }

                OnPropertyChanged("ExactTime");
            }
        }

        public Boolean ExactDoctor
        {
            get
            {
                return _exactDoctor;
            }
            set
            {
                _exactDoctor = value;

                OnPropertyChanged("ExactDoctor");
            }
        }

        public Boolean ExactRoom
        {
            get
            {
                return _exactRoom;
            }
            set
            {
                _exactRoom = value;

                OnPropertyChanged("ExactRoom");
            }
        }

        public Doctor SelectedDoctor
        {
            get
            {
                return _selectedDoctor;
            }
            set
            {
                _selectedDoctor = value;

                if (_exactDoctor && value == null)
                {
                    OnErrorChanged("SelectedDoctor", "Mora biti izabran jedan od ponuđenih doktora.");
                }
                else
                    OnErrorChanged("SelectedDoctor", "");

                OnPropertyChanged("SelectedDoctor");
            }
        }

        public Room SelectedRoom
        {
            get
            {
                return _selectedRoom;
            }
            set
            {
                _selectedRoom = value;

                if (_exactRoom && value == null)
                {
                    OnErrorChanged("SelectedRoom", "Mora biti izabran jedana od ponuđenih prostorija.");
                }
                else
                    OnErrorChanged("SelectedRoom", "");

                OnPropertyChanged("SelectedRoom");
            }
        }

        private void initialise()
        {
            _qualifiedDoctors = new ObservableCollection<Doctor>();
            foreach (Specialty specialty in _procedure.ProcedureType.QualifiedSpecialties)
                foreach (Doctor doctor in DataMockup.Instance.Doctors.Where(d => d.Specialty.Name.Equals(specialty.Name)))
                    _qualifiedDoctors.Add(doctor);

            if (_qualifiedDoctors.Count > 0)
                SelectedDoctor = _qualifiedDoctors[0];

            if (_procedure is Examination)
                _qualifiedRooms = new ObservableCollection<Room>(DataMockup.Instance.Rooms.Where(r => r.Purpose.Equals(RoomType.examinationRoom)));
            else
                _qualifiedRooms = new ObservableCollection<Room>(DataMockup.Instance.Rooms.Where(r => r.Purpose.Equals(RoomType.operatingRoom)));

            if (_qualifiedRooms.Count > 0)
                SelectedRoom = _qualifiedRooms[0];

            ExactDoctor = false;
            ExactRoom = false;
            ExactTime = false;
            ExactDate = false;
        }

        protected virtual Boolean canGoToNextStep()
        {
            if (HasErrors)
                return false;

            if (_exactDoctor && _selectedDoctor == null)
                return false;

            if (_exactRoom && _selectedRoom == null)
                return false;

            if (_exactDate && SelectedDate == null)
                return false;

            if (_exactTime && (EarliestTime == null || LatestTime == null))
                return false;

            return true;
        }

        protected virtual void nextStepDummy()
        {
            if (_exactDoctor)
                _procedure.Doctor = _selectedDoctor;
            else if (_qualifiedDoctors.Count > 0)
                _procedure.Doctor = _qualifiedDoctors[0];

            if (_exactRoom)
                _procedure.Room = _selectedRoom;
            else if (_qualifiedRooms.Count > 0)
                _procedure.Room = _qualifiedRooms[0];
        }

        protected virtual void nextStep()
        {
            if (_exactDoctor)
                _procedure.Doctor = _selectedDoctor;
            else if (_qualifiedDoctors.Count > 0)
                _procedure.Doctor = _qualifiedDoctors[0];

            if (_exactRoom)
                _procedure.Room = _selectedRoom;
            else if (_qualifiedRooms.Count > 0)
                _procedure.Room = _qualifiedRooms[0];

            _changeViewCommand.Execute(new ChooseProcedureViewModel(Title, _caller, this, _procedure, _changeViewCommand));
        }

        public ChooseSchedulingInfoViewModel(String title, Procedure procedure, ViewModelBase caller, ViewModelBase previous, ICommand changeViewCommand) : base(title, true)
        {
            _procedure = procedure;
            _caller = caller;
            _previous = previous;
            _changeViewCommand = changeViewCommand;

            initialise();
        }

    }
}
