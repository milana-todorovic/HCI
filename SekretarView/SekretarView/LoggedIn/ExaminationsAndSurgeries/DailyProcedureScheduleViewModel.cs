using Model.HospitalResources;
using Model.Schedule.Procedures;
using Model.Users.Employee;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SekretarView
{
    class DailyProcedureScheduleViewModel : ViewModelBase
    {
        private Procedure _filter;
        private Specialty _specialtyFilter;
        private ObservableCollection<ProcedureType> _types;
        private ObservableCollection<Room> _rooms;

        protected Dictionary<DateTime, List<Examination>> _examinations;
        protected Dictionary<DateTime, List<Surgery>> _surgeries;

        protected ObservableCollection<ProcedureViewModel> _procedures;
        protected DateTime? _date;

        private MonthlyScheduleViewModel _monthly;
        protected ICommand _changeViewCommand;
        protected ICommand _previousDate;
        protected ICommand _nextDate;
        protected ICommand _showMonthView;
        protected ICommand _clearFilters;
        protected ICommand _filterView;

        protected ObservableCollection<MenuItemViewModel> _addNew;

        public ObservableCollection<ProcedureViewModel> Procedures
        {
            get
            {
                if (_procedures == null)
                    _procedures = new ObservableCollection<ProcedureViewModel>();

                return _procedures;
            }
            private set
            {
                _procedures = value;
                OnPropertyChanged("Procedures");
            }
        }

        public ICommand ClearFilters
        {
            get
            {
                if (_clearFilters == null)
                    _clearFilters = new RelayCommand(p => loadProcedures());
                return _clearFilters;
            }
        }

        public ICommand Filter
        {
            get
            {
                if (_filterView == null)
                    _filterView = new RelayCommand(p => openFilter());
                return _filterView;
            }
        }

        public ICommand ShowMonthView
        {
            get
            {
                if (_showMonthView == null)
                {
                    _showMonthView = new RelayCommand(p => _changeViewCommand.Execute(_monthly));
                }

                return _showMonthView;
            }
        }

        public DateTime? Date
        {
            get
            {
                return _date;
            }
            set
            {
                if (value != null)
                {
                    _date = value;
                    loadProcedures();
                }

                OnPropertyChanged("Date");
            }
        }

        public ObservableCollection<MenuItemViewModel> AddNew
        {
            get
            {
                return _addNew;
            }
        }

        public ICommand NextDateCommand
        {
            get
            {
                if (_nextDate == null)
                {
                    _nextDate = new RelayCommand(p => nextDate());
                }
                return _nextDate;
            }
        }

        public ICommand PreviousDateCommand
        {
            get
            {
                if (_previousDate == null)
                {
                    _previousDate = new RelayCommand(p => previousDate());
                }
                return _previousDate;
            }
        }

        public DailyProcedureScheduleViewModel(ICommand changeViewCommand, String title,
            ObservableCollection<MenuItemViewModel> add, Dictionary<DateTime, List<Examination>> examinations,
            Dictionary<DateTime, List<Surgery>> surgeries) : base(title)
        {
            _changeViewCommand = changeViewCommand;
            _addNew = add;
            _surgeries = surgeries;
            _examinations = examinations;
            Date = DateTime.Now.Date;

            _monthly = new MonthlyScheduleViewModel(_changeViewCommand,
                this, d => Date = d, getNumberOfProcedures, title, add);

            Mediator.Register("ProcedureAdded", p => loadProcedures());
            Mediator.Register("ProcedureDeleted", p => loadProcedures());
            Mediator.Register("ProcedureUpdated", p => loadProcedures());
            Mediator.Register("ProcedureAdded", p => _monthly.refresh());
            Mediator.Register("ProcedureDeleted", p => _monthly.refresh());
            Mediator.Register("ProcedureUpdated", p => _monthly.refresh());

            if (examinations == null)
            {
                _types = new ObservableCollection<ProcedureType>(DataMockup.Instance.ProcedureTypes.Where(p => p.Kind.Equals(ProcedureKind.surgery)));
                _rooms = new ObservableCollection<Room>(DataMockup.Instance.Rooms.Where(p => p.Purpose.Equals(RoomType.operatingRoom)));
            }
            else if (surgeries == null)
            {
                _types = new ObservableCollection<ProcedureType>(DataMockup.Instance.ProcedureTypes.Where(p => p.Kind.Equals(ProcedureKind.examination)));
                _rooms = new ObservableCollection<Room>(DataMockup.Instance.Rooms.Where(p => p.Purpose.Equals(RoomType.examinationRoom)));
            }
            else
            {
                _types = new ObservableCollection<ProcedureType>(DataMockup.Instance.ProcedureTypes);
                _rooms = new ObservableCollection<Room>(DataMockup.Instance.Rooms);
            }
                
        }

        protected void loadProcedures()
        {
            List<Procedure> procedures = new List<Procedure>();
            if (_examinations != null && _examinations.ContainsKey(Date.Value))
            {
                procedures.AddRange(_examinations[Date.Value]);
            }
            if (_surgeries != null && _surgeries.ContainsKey(Date.Value))
            {
                procedures.AddRange(_surgeries[Date.Value]);
            }
            procedures.Sort((first, second) => first.TimeInterval.Start.CompareTo(second.TimeInterval.Start));

            Procedures.Clear();
            foreach (Procedure procedure in procedures)
            {
                if (procedure is Examination)
                    Procedures.Add(new ProcedureViewModel("Detalji pregleda", _changeViewCommand, procedure, this));
                else
                    Procedures.Add(new ProcedureViewModel("Detalji operacije", _changeViewCommand, procedure, this));
            }

            _filter = new Examination();
            _specialtyFilter = null;
        }

        private void openFilter()
        {
            String title = "";

            if (_examinations == null)
                title = "Filtriraj operacije";
            else if (_surgeries == null)
                title = "Filtriraj preglede";
            else
                title = "Filtriraj preglede i operacije";

            _changeViewCommand.Execute(new ProcedureFilterViewModel(
                                            title,
                                            _changeViewCommand,
                                            this,
                                            filterProcedures,
                                            new Examination
                                            {
                                                ProcedureType = _filter.ProcedureType,
                                                Patient = _filter.Patient,
                                                Doctor = _filter.Doctor,
                                                Room = _filter.Room
                                            },
                                            _specialtyFilter,
                                            _types, _rooms));
        }

        protected void filterProcedures(Procedure procedure, Specialty specialty)
        {
            loadProcedures();
            _filter = procedure;
            _specialtyFilter = specialty;

            if (Procedures.Count > 0)
                Procedures = new ObservableCollection<ProcedureViewModel>(
                    Procedures.Where(p => matches(p.Procedure, procedure, specialty)));
        }

        protected Boolean matches(Procedure first, Procedure second, Specialty specialty)
        {
            if (specialty != null && !specialty.Equals(first.Doctor.Specialty))
                return false;
            if (second.Doctor != null && !second.Doctor.Equals(first.Doctor))
                return false;
            if (second.Patient != null && !second.Patient.Equals(first.Patient))
                return false;
            if (second.Room != null && !second.Room.Equals(first.Room))
                return false;
            if (second.ProcedureType != null && !second.ProcedureType.Equals(first.ProcedureType))
                return false;

            return true;
        }

        protected int getNumberOfProcedures(DateTime date)
        {
            int retVal = 0;

            if (_examinations != null && _examinations.ContainsKey(date))
            {
                retVal += _examinations[date].Count;
            }
            if (_surgeries != null && _surgeries.ContainsKey(date))
            {
                retVal += _surgeries[date].Count;
            }

            return retVal;
        }

        protected void nextDate()
        {
            TimeSpan day = new TimeSpan(24, 0, 0);
            Date += day;
        }

        protected void previousDate()
        {
            TimeSpan day = new TimeSpan(24, 0, 0);
            Date -= day;
        }
    }
}
