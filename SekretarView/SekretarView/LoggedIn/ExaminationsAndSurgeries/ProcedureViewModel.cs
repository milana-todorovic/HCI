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
    class ProcedureViewModel : ViewModelBase
    {
        protected Procedure _procedure;
        protected String _status;
        protected Boolean _updatable;

        private DispatcherTimer _timer;

        private ViewModelBase _caller;

        protected ICommand _changeViewCommand;
        protected ICommand _rescheduleCommand;
        protected ICommand _cancelCommand;
        protected ICommand _showDetailsCommad;
        protected ICommand _back;

        public Procedure Procedure
        {
            get
            {
                return _procedure;
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
                return _procedure.Patient.Name + " " + _procedure.Patient.Surname;
            }
        }

        public String Doctor
        {
            get
            {
                return _procedure.Doctor.Name + " " + _procedure.Doctor.Surname;
            }
        }

        public String Room
        {
            get
            {
                return _procedure.Room.Name;
            }
        }

        public String Type
        {
            get
            {
                return _procedure.ProcedureType.Name;
            }
        }

        public DateTime StartsAt
        {
            get
            {
                return _procedure.TimeInterval.Start;
            }
        }

        public DateTime EndsAt
        {
            get
            {
                return _procedure.TimeInterval.End;
            }
        }

        public TimeSpan Duration
        {
            get
            {
                return _procedure.TimeInterval.End - _procedure.TimeInterval.Start;
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

        public ProcedureViewModel(String title, ICommand changeViewCommand, Procedure procedure, ViewModelBase caller) : base(title)
        {
            _procedure = procedure;
            _changeViewCommand = changeViewCommand;
            _caller = caller;
            updateTime();
            setUpTimer();
        }

        private void setUpTimer()
        {
            _timer = new DispatcherTimer();
            _timer.Tick += (object sender, EventArgs e) => updateTime();
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Start();
        }

        private void updateTime()
        {
            if (DateTime.Now < _procedure.TimeInterval.Start)
            {
                Status = "Predstojeće";
                Updatable = true;
            }
            else if (DateTime.Now <= _procedure.TimeInterval.End)
            {
                Status = "U toku";
                Updatable = false;
            }
            else
            {
                Status = "Završeno";
                Updatable = false;
            }
        }

        protected void reschedule(ViewModelBase caller)
        {
            updateTime();
            if (!Updatable)
                return;

            String title = "";
            if (_procedure is Examination)
                title = "Izmeni pregled";
            else
                title = "Izmeni operaciju";

            _changeViewCommand.Execute(new ChooseReschedulingInfoViewModel(title, _procedure, caller, _changeViewCommand));
        }

        protected void cancel()
        {
            updateTime();
            if (!Updatable)
                return;

            if (_procedure is Examination)
                DataMockup.Instance.Examinations[_procedure.TimeInterval.Start.Date].Remove((Examination)_procedure);
            else
                DataMockup.Instance.Surgeries[_procedure.TimeInterval.Start.Date].Remove((Surgery)_procedure);

            Mediator.NotifyColleagues("ProcedureDeleted", _procedure);

            _timer.Stop();

            _changeViewCommand.Execute(_caller);
        }
        protected void showDetails()
        {
            _changeViewCommand.Execute(this);
        }
    }
}
