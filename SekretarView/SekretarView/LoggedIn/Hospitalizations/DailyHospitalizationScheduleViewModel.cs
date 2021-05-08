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
    class DailyHospitalizationScheduleViewModel : ViewModelBase
    {
        private Hospitalization _filter;
        private ObservableCollection<Room> _rooms;

        protected Dictionary<DateTime, List<Examination>> _examinations;
        protected Dictionary<DateTime, List<Surgery>> _surgeries;

        protected ObservableCollection<HospitalizationViewModel> _hospitalizations;
        protected DateTime? _date;

        private MonthlyScheduleViewModel _monthly;
        protected ICommand _changeViewCommand;
        protected ICommand _previousDate;
        protected ICommand _nextDate;
        protected ICommand _showMonthView;
        protected ICommand _clearFilters;
        protected ICommand _filterView;

        protected ObservableCollection<MenuItemViewModel> _addNew;

        public ObservableCollection<HospitalizationViewModel> Hospitalizations
        {
            get
            {
                if (_hospitalizations == null)
                    _hospitalizations = new ObservableCollection<HospitalizationViewModel>();

                return _hospitalizations;
            }
            private set
            {
                _hospitalizations = value;
                OnPropertyChanged("Procedures");
            }
        }

        public ICommand ClearFilters
        {
            get
            {
                if (_clearFilters == null)
                    _clearFilters = new RelayCommand(p => loadHospitalizations());
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
                    loadHospitalizations();
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

        public DailyHospitalizationScheduleViewModel(ICommand changeViewCommand) : base("Bolnička lečenja")
        {
            _changeViewCommand = changeViewCommand;
            _addNew = new ObservableCollection<MenuItemViewModel>
                    { new MenuItemViewModel("Zakaži bolničko lečenje", _changeViewCommand, 
                    new HospitalizationTypeAndPatientViewModel(this, _changeViewCommand)) };
            Date = DateTime.Now.Date;

            _monthly = new MonthlyScheduleViewModel(_changeViewCommand,
                this, d => Date = d, getNumberOfProcedures, "Operacije", _addNew);

            _filter = new Hospitalization();

            Mediator.Register("HospitalizationAdded", p => loadHospitalizations());
            Mediator.Register("HospitalizationDeleted", p => loadHospitalizations());
            Mediator.Register("HospitalizationUpdated", p => loadHospitalizations());
            Mediator.Register("HospitalizationAdded", p => _monthly.refresh());
            Mediator.Register("HospitalizationDeleted", p => _monthly.refresh());
            Mediator.Register("HospitalizationUpdated", p => _monthly.refresh());
        }

        protected void loadHospitalizations()
        {
            Hospitalizations.Clear();
            foreach (Hospitalization hospitalization in DataMockup.Instance.Hospitalizations)
                if (hospitalization.TimeInterval.Start <= Date && hospitalization.TimeInterval.End >= Date)
                    Hospitalizations.Add(new HospitalizationViewModel(_changeViewCommand, hospitalization, this));
            
            _filter = new Hospitalization();
        }

        private void openFilter()
        {
            _changeViewCommand.Execute(new HospitalizationFilterViewModel(_changeViewCommand, this, filterProcedures, new Hospitalization() { 
            HospitalizationType = _filter.HospitalizationType, Patient = _filter.Patient, Room = _filter.Room}));
        }

        protected void filterProcedures(Hospitalization hospitalization)
        {
            loadHospitalizations();
            _filter = hospitalization;

            if (Hospitalizations.Count > 0)
                Hospitalizations = new ObservableCollection<HospitalizationViewModel>(
                    Hospitalizations.Where(p => matches(p.Hospitalization, hospitalization)));
        }

        protected Boolean matches(Hospitalization first, Hospitalization second)
        {
            if (second.Patient != null && !second.Patient.Equals(first.Patient))
                return false;
            if (second.Room != null && !second.Room.Equals(first.Room))
                return false;
            if (second.HospitalizationType != null && !second.HospitalizationType.Equals(first.HospitalizationType))
                return false;

            return true;
        }

        protected int getNumberOfProcedures(DateTime date)
        {
            return DataMockup.Instance.Hospitalizations.Where(
                h => h.TimeInterval.Start <= date.Date && h.TimeInterval.End >= date.Date).Count();
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
