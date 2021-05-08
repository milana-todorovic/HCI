using Model.Schedule.Procedures;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SekretarView
{
    class MonthlyScheduleViewModel : ViewModelBase
    {
        private ViewModelBase _daily;
        private Action<DateTime> _setDate;
        private Func<DateTime, int> _getNumberOfEvents;

        private ICommand _changeViewCommand;
        private ICommand _showDateViewWithSetDate;
        private ICommand _showDateView;
        private ICommand _nextMonth;
        private ICommand _previousMonth;

        private ObservableCollection<MenuItemViewModel> _addNew;

        private DateTime _month;
        private ObservableCollection<MonthlyScheduleItem> _days;
        private ObservableCollection<String> _daysOfTheWeek;

        public ObservableCollection<MenuItemViewModel> AddNew
        {
            get
            {
                return _addNew;
            }
        }

        public ICommand ShowDateView
        {
            get
            {
                if (_showDateView == null)
                    _showDateView = new RelayCommand((p) => _changeViewCommand.Execute(_daily));
                return _showDateView;
            }
        }

        public ICommand ShowDateViewWithSetDate
        {
            get
            {
                if (_showDateViewWithSetDate == null)
                    _showDateViewWithSetDate = new RelayCommand((p) => showDayView((DateTime)p));
                return _showDateViewWithSetDate;
            }
        }

        public ICommand NextMonth
        {
            get
            {
                if (_nextMonth == null)
                    _nextMonth = new RelayCommand((p) => Month = Month.AddMonths(1));
                return _nextMonth;
            }
        }
        public ICommand PreviousMonth
        {
            get
            {
                if (_previousMonth == null)
                    _previousMonth = new RelayCommand((p) => Month = Month.AddMonths(-1));
                return _previousMonth;
            }
        }

        public ObservableCollection<String> DaysOfTheWeek
        {
            get
            {
                if (_daysOfTheWeek == null)
                {
                    _daysOfTheWeek = new ObservableCollection<string> { "Pon", "Uto", "Sre", "Čet", "Pet", "Sub", "Ned" };
                }

                return _daysOfTheWeek;
            }
        }

        public DateTime Month
        {
            get
            {
                return _month;
            }
            set
            {
                DateTime newMonth = new DateTime(value.Year, value.Month, 1);

                if (_month == null || !_month.Equals(newMonth))
                {
                    _month = newMonth;
                    refresh(_month);
                    OnPropertyChanged("Month");
                    OnPropertyChanged("MonthString");
                }
            }
        }

        public String MonthString
        {
            get
            {
                return Month.ToString("MM/yyyy");
            }
        }

        public ObservableCollection<MonthlyScheduleItem> Days
        {
            get
            {
                if (_days == null)
                    _days = new ObservableCollection<MonthlyScheduleItem>();

                return _days;
            }
        }

        public void refresh()
        {
            refresh(Month);
        }

        private void refresh(DateTime month)
        {
            Days.Clear();

            DayOfWeek dayOf = month.DayOfWeek;
            int offset = Convert.ToInt32(dayOf.ToString("D"));

            if (offset != 1) month = month.AddDays(-offset);

            for (int box = 0; box < 42; box++)
            {
                Days.Add(new MonthlyScheduleItem()
                {
                    Date = month,
                    NumberOfEvents = _getNumberOfEvents.Invoke(month),
                    IsCorrectMonth = month.Month == Month.Month,
                    IsToday = month.Date.Equals(DateTime.Now.Date)
                });
                month = month.AddDays(1);
            }
        }

        private void showDayView(DateTime date)
        {
            _setDate.Invoke(date);
            _changeViewCommand.Execute(_daily);
        }

        public MonthlyScheduleViewModel(ICommand changeViewCommand, ViewModelBase daily, 
            Action<DateTime> setDate, Func<DateTime, int> getNumberOfEvents, String title,
            ObservableCollection<MenuItemViewModel> addNew) : base(daily.Title)
        {
            _changeViewCommand = changeViewCommand;
            _daily = daily;
            _setDate = setDate;
            _getNumberOfEvents = getNumberOfEvents;
            _addNew = addNew;
            Month = DateTime.Now;
        }
    }
}
