using Model.Schedule.Hospitalizations;
using Model.Schedule.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace SekretarView
{
    class HospitalizationViewModel : ViewModelBase
    {
        protected Hospitalization _hospitalization;
        protected String _status;
        protected Boolean _deletable;
        protected Boolean _updatable;

        private DispatcherTimer _timer;

        private ViewModelBase _caller;

        protected ICommand _changeViewCommand;
        protected ICommand _rescheduleCommand;
        protected ICommand _cancelCommand;
        protected ICommand _showDetailsCommad;
        protected ICommand _back;

        public Hospitalization Hospitalization
        {
            get
            {
                return _hospitalization;
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

        public String Patient
        {
            get
            {
                return _hospitalization.Patient.Name + " " + _hospitalization.Patient.Surname;
            }
        }

        public int NumberOfDays
        {
            get
            {
                return (_hospitalization.TimeInterval.End - _hospitalization.TimeInterval.Start).Days + 1;
            }
        }

        public String Room
        {
            get
            {
                return _hospitalization.Room.Name;
            }
        }

        public String Type
        {
            get
            {
                return _hospitalization.HospitalizationType.Name;
            }
        }

        public DateTime StartsAt
        {
            get
            {
                return _hospitalization.TimeInterval.Start;
            }
        }

        public DateTime EndsAt
        {
            get
            {
                return _hospitalization.TimeInterval.End;
            }
        }
        
        public String Status
        {
            get
            {
                return _status;
            }
            set
            {
                if (_status == null || !_status.Equals(value))
                {
                    _status = value;
                    OnPropertyChanged("Status");
                }
            }
        }

        public Boolean Deletable
        {
            get
            {
                return _deletable;
            }
            set
            {
                if (_deletable != value)
                {
                    _deletable = value;
                    OnPropertyChanged("Deletable");
                }
            }
        }

        public Boolean Updatable
        {
            get
            {
                return _updatable;
            }
            set
            {
                if (_updatable != value)
                {
                    _updatable = value;
                    OnPropertyChanged("Updatable");
                }
            }
        }

        public ICommand RescheduleCommand
        {
            get
            {
                if (_rescheduleCommand == null)
                {
                    _rescheduleCommand = new RelayCommand(p => reschedule((ViewModelBase)p));
                }

                return _rescheduleCommand;
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                {
                    _cancelCommand = new RelayCommand(p => cancel());
                }

                return _cancelCommand;
            }
        }

        public ICommand ShowDetailsCommand
        {
            get
            {
                if (_showDetailsCommad == null)
                {
                    _showDetailsCommad = new RelayCommand(p => showDetails());
                }

                return _showDetailsCommad;
            }
        }

        public HospitalizationViewModel(ICommand changeViewCommand, Hospitalization hospitalization, ViewModelBase caller) : base("Detalji bolničkog lečenja")
        {
            _hospitalization = hospitalization;
            _changeViewCommand = changeViewCommand;
            _caller = caller;
            updateTime();
            setUpTimer();
        }

        private void setUpTimer()
        {
            _timer = new DispatcherTimer();
            _timer.Tick += (object sender, EventArgs e) => updateTime();
            _timer.Interval = new TimeSpan(0, 0, 5);
            _timer.Start();
        }

        private void updateTime()
        {
            if (DateTime.Now.Date < _hospitalization.TimeInterval.Start)
            {
                Status = "Predstojeće";
                Deletable = true;
                Updatable = true;
            }
            else if (DateTime.Now.Date <= _hospitalization.TimeInterval.End)
            {
                Status = "U toku";
                Deletable = false;
                Updatable = true;
            }
            else
            {
                Status = "Završeno";
                Deletable = false;
                Updatable = false;
            }
        }

        protected void reschedule(ViewModelBase caller)
        {
            updateTime();
            if (!Updatable)
                return;

            _changeViewCommand.Execute(new HospitalizationReschedulingInfoViewModel(_hospitalization, caller, _changeViewCommand));
        }

        protected void cancel()
        {
            updateTime();
            if (!Deletable)
                return;

            DataMockup.Instance.Hospitalizations.Remove(_hospitalization);

            Mediator.NotifyColleagues("HospitalizationDeleted", _hospitalization);

            _timer.Stop();

            _changeViewCommand.Execute(_caller);
        }

        protected void showDetails()
        {
            _changeViewCommand.Execute(this);
        }
    }
}
