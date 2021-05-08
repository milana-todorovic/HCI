using Model.HospitalResources;
using Model.Schedule.Hospitalizations;
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
    class HospitalizationSchedulingInfoViewModel : ViewModelBase
    {

        protected int _dateMode;
        protected Boolean _exactRoom;
        protected Room _selectedRoom;

        protected ObservableCollection<Room> _qualifiedRooms;

        protected Hospitalization _hospitalization;
        protected DateTime? _selectedDate;
        protected DateTime? _earliestDate;
        protected DateTime? _latestDate;

        protected ViewModelBase _previous;
        protected ViewModelBase _caller;

        protected ICommand _changeViewCommand;
        protected ICommand _nextStep;
        protected ICommand _previousStep;
        protected ICommand _cancel;


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
                return _hospitalization.HospitalizationType.Name;
            }
        }

        public String Patient
        {
            get
            {
                return _hospitalization.Patient.Name + " " + _hospitalization.Patient.Surname;
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
                _selectedDate = value;

                if (_dateMode == 1)
                {
                    if (value == null)
                        OnErrorChanged("SelectedDate", "Datum mora biti izabran.");
                    else if (value <= DateTime.Now.Date)
                        OnErrorChanged("SelectedDate", "Izabrani datum mora biti u budućnosti.");
                    else
                        OnErrorChanged("SelectedDate", "");
                }
                else
                    OnErrorChanged("SelectedDate", "");

                OnPropertyChanged("SelectedDate");
            }
        }

        public DateTime? EarliestDate
        {
            get
            {
                return _earliestDate;
            }
            set
            {
                _earliestDate = value;

                if (_dateMode == 2)
                {
                    LatestDate = LatestDate;

                    if (value == null)
                        OnErrorChanged("EarliestDate", "Najraniji datum mora biti izabran.");
                    else if (value <= DateTime.Now.Date)
                        OnErrorChanged("EarliestDate", "Izabrani datum mora biti u budućnosti.");
                    else
                        OnErrorChanged("EarliestDate", "");
                }
                else
                    OnErrorChanged("EarliestDate", "");

                OnPropertyChanged("EarliestDate");
            }
        }

        public DateTime? LatestDate
        {
            get
            {
                return _latestDate;
            }
            set
            {
                _latestDate = value;

                if (_dateMode == 2)
                {
                    if (value == null)
                        OnErrorChanged("LatestDate", "Najkasniji datum mora biti izabran.");
                    else if (value <= DateTime.Now.Date)
                        OnErrorChanged("LatestDate", "Izabrani datum mora biti u budućnosti.");
                    else if (EarliestDate != null && value < EarliestDate)
                        OnErrorChanged("LatestDate", "Najkasniji datum mora biti posle najranijeg.");
                    else
                        OnErrorChanged("LatestDate", "");
                }
                else
                    OnErrorChanged("LatestDate", "");

                OnPropertyChanged("LatestDate");
            }
        }

        public int DateMode
        {
            get
            {
                return _dateMode;
            }
            set
            {
                _dateMode = value;

                if (value != 1)
                    SelectedDate = DateTime.Now.Date.AddDays(1);

                if (value != 2)
                {
                    EarliestDate = DateTime.Now.Date.AddDays(1);
                    LatestDate = DateTime.Now.Date.AddDays(3);
                }

                OnPropertyChanged("DateMode");
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
                    OnErrorChanged("SelectedRoom", "Mora biti izabrana jedana od ponuđenih prostorija.");
                }
                else
                    OnErrorChanged("SelectedRoom", "");

                OnPropertyChanged("SelectedRoom");
            }
        }

        private void initialise()
        {
            _qualifiedRooms = new ObservableCollection<Room>(DataMockup.Instance.Rooms.Where(r => r.Purpose.Equals(RoomType.recoveryRoom)));

            if (_qualifiedRooms.Count > 0)
                SelectedRoom = _qualifiedRooms[0];

            ExactRoom = false;
            DateMode = 0;
        }

        protected virtual Boolean canGoToNextStep()
        {
            if (HasErrors)
                return false;

            if (_exactRoom && _selectedRoom == null)
                return false;

            if (_dateMode == 1 && SelectedDate == null)
                return false;

            return true;
        }

        protected List<Hospitalization> mock()
        {
            List<Hospitalization> hospitalizations = new List<Hospitalization>();
            if (_hospitalization.Room == null && _hospitalization.TimeInterval == null)
            {
                hospitalizations.Add(new Hospitalization()
                {
                    Patient = _hospitalization.Patient,
                    HospitalizationType = _hospitalization.HospitalizationType,
                    Room = _qualifiedRooms[0],
                    TimeInterval = new Model.Utilities.TimeInterval()
                    {
                        Start = EarliestDate.Value,
                        End = EarliestDate.Value.AddDays(_hospitalization.HospitalizationType.UsualNumberOfDays - 1)
                    }
                });
                hospitalizations.Add(new Hospitalization()
                {
                    Patient = _hospitalization.Patient,
                    HospitalizationType = _hospitalization.HospitalizationType,
                    Room = _qualifiedRooms[0],
                    TimeInterval = new Model.Utilities.TimeInterval()
                    {
                        Start = LatestDate.Value,
                        End = LatestDate.Value.AddDays(_hospitalization.HospitalizationType.UsualNumberOfDays - 1)
                    }
                });
                hospitalizations.Add(new Hospitalization()
                {
                    Patient = _hospitalization.Patient,
                    HospitalizationType = _hospitalization.HospitalizationType,
                    Room = _qualifiedRooms[1],
                    TimeInterval = new Model.Utilities.TimeInterval()
                    {
                        Start = EarliestDate.Value,
                        End = EarliestDate.Value.AddDays(_hospitalization.HospitalizationType.UsualNumberOfDays - 1)
                    }
                });
            }
            else if (_hospitalization.Room != null)
            {
                hospitalizations.Add(new Hospitalization()
                {
                    Patient = _hospitalization.Patient,
                    HospitalizationType = _hospitalization.HospitalizationType,
                    Room = _hospitalization.Room,
                    TimeInterval = new Model.Utilities.TimeInterval()
                    {
                        Start = EarliestDate.Value,
                        End = EarliestDate.Value.AddDays(_hospitalization.HospitalizationType.UsualNumberOfDays - 1)
                    }
                });
                hospitalizations.Add(new Hospitalization()
                {
                    Patient = _hospitalization.Patient,
                    HospitalizationType = _hospitalization.HospitalizationType,
                    Room = _hospitalization.Room,
                    TimeInterval = new Model.Utilities.TimeInterval()
                    {
                        Start = LatestDate.Value,
                        End = LatestDate.Value.AddDays(_hospitalization.HospitalizationType.UsualNumberOfDays - 1)
                    }
                });
                hospitalizations.Add(new Hospitalization()
                {
                    Patient = _hospitalization.Patient,
                    HospitalizationType = _hospitalization.HospitalizationType,
                    Room = _hospitalization.Room,
                    TimeInterval = new Model.Utilities.TimeInterval()
                    {
                        Start = EarliestDate.Value,
                        End = EarliestDate.Value.AddDays(_hospitalization.HospitalizationType.UsualNumberOfDays - 1)
                    }
                });
            }
            else if (_hospitalization.TimeInterval != null)
            {
                hospitalizations.Add(new Hospitalization()
                {
                    Patient = _hospitalization.Patient,
                    HospitalizationType = _hospitalization.HospitalizationType,
                    Room = _qualifiedRooms[0],
                    TimeInterval = _hospitalization.TimeInterval
                });
                hospitalizations.Add(new Hospitalization()
                {
                    Patient = _hospitalization.Patient,
                    HospitalizationType = _hospitalization.HospitalizationType,
                    Room = _qualifiedRooms[1],
                    TimeInterval = _hospitalization.TimeInterval
                });
            };

            return hospitalizations;
        }

        protected virtual void nextStep()
        {
            if (_exactRoom)
                _hospitalization.Room = _selectedRoom;

            if (_dateMode == 1)
                _hospitalization.TimeInterval = new Model.Utilities.TimeInterval()
                {
                    Start = SelectedDate.Value,
                    End = SelectedDate.Value.AddDays(_hospitalization.HospitalizationType.UsualNumberOfDays - 1)
                };            

            _changeViewCommand.Execute(new ChooseHospitalizationViewModel(
                "Zakaži bolničko lečenje", _caller, this, _hospitalization, mock(), _changeViewCommand));
        }

        public HospitalizationSchedulingInfoViewModel(String title, Hospitalization hospitalization, ViewModelBase caller, ViewModelBase previous, ICommand changeViewCommand) : base(title, true)
        {
            _hospitalization = hospitalization;
            _caller = caller;
            _previous = previous;
            _changeViewCommand = changeViewCommand;

            initialise();
        }
    }
}
