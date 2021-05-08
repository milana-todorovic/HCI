using Model.HospitalResources;
using Model.Schedule.Procedures;
using Model.Users.Employee;
using Model.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SekretarView
{
    class ChooseProcedureViewModel : ViewModelBase
    {
        protected ObservableCollection<ProcedureViewModel> _procedures;
        protected Procedure _procedure;

        protected int _stepNum;

        protected ViewModelBase _previous;
        protected ViewModelBase _caller;

        protected ICommand _changeViewCommand;
        protected ICommand _previousStep;
        protected ICommand _cancel;
        protected ICommand _handleProcedure;

        protected String _handleProcedureName;

        public int StepNum
        {
            get
            {
                return _stepNum;
            }
        }

        public String HandleProcedureName
        {
            get
            {
                return _handleProcedureName;
            }
        }

        public ICommand HandleProcedure
        {
            get
            {
                if (_handleProcedure == null)
                    _handleProcedure = new RelayCommand(p => handleProcedure(((ProcedureViewModel)p).Procedure));
                return _handleProcedure;
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

        public ObservableCollection<ProcedureViewModel> Procedures
        {
            get
            {
                return _procedures;
            }
        }

        protected virtual void handleProcedure(Procedure procedure)
        {
            if (procedure is Examination)
            {
                if (DataMockup.Instance.Examinations.ContainsKey(procedure.TimeInterval.Start.Date))
                    DataMockup.Instance.Examinations[procedure.TimeInterval.Start.Date].Add((Examination)procedure);
                else
                    DataMockup.Instance.Examinations[procedure.TimeInterval.Start.Date] =
                        new List<Examination> { (Examination)procedure };
            }
            else
            {
                if (DataMockup.Instance.Surgeries.ContainsKey(procedure.TimeInterval.Start.Date))
                    DataMockup.Instance.Surgeries[procedure.TimeInterval.Start.Date].Add((Surgery)procedure);
                else
                    DataMockup.Instance.Surgeries[procedure.TimeInterval.Start.Date] =
                        new List<Surgery> { (Surgery)procedure };
            }

            Mediator.NotifyColleagues("ProcedureAdded", procedure);
            _changeViewCommand.Execute(_caller);
        }

        public ChooseProcedureViewModel(String title, ViewModelBase caller, ViewModelBase previous, Procedure procedure,
            ICommand changeViewCommand) :
            base(title, true)
        {
            _procedure = procedure;
            _caller = caller;
            _previous = previous;
            _changeViewCommand = changeViewCommand;
            _handleProcedureName = "Zakaži";
            _stepNum = 3;

            DateTime now = DateTime.Now;
            DateTime generate = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute - now.Minute % 10, 0);

            _procedures = new ObservableCollection<ProcedureViewModel>();

            if (_procedure.Doctor != null && _procedure.Room != null)
            {
                if (_procedure is Examination)
                {
                    _procedures.Add(new ProcedureViewModel("", _changeViewCommand,
                    new Examination()
                    {
                        Patient = _procedure.Patient,
                        Doctor = _procedure.Doctor,
                        Room = _procedure.Room,
                        ProcedureType = _procedure.ProcedureType,
                        TimeInterval = new TimeInterval()
                        {
                            Start = DateTime.Now.AddSeconds(10),
                            End = DateTime.Now.AddSeconds(10) + _procedure.ProcedureType.Duration
                        }
                    }
                    , this)) ;
                    _procedures.Add(new ProcedureViewModel("", _changeViewCommand,
                    new Examination()
                    {
                        Patient = _procedure.Patient,
                        Doctor = _procedure.Doctor,
                        Room = _procedure.Room,
                        ProcedureType = _procedure.ProcedureType,
                        TimeInterval = new TimeInterval()
                        {
                            Start = generate.AddMinutes(30),
                            End = generate.AddMinutes(30) + _procedure.ProcedureType.Duration
                        }
                    }
                    , this));
                    _procedures.Add(new ProcedureViewModel("", _changeViewCommand,
                    new Examination()
                    {
                        Patient = _procedure.Patient,
                        Doctor = _procedure.Doctor,
                        Room = _procedure.Room,
                        ProcedureType = _procedure.ProcedureType,
                        TimeInterval = new TimeInterval()
                        {
                            Start = generate.AddHours(2),
                            End = generate.AddHours(2) + _procedure.ProcedureType.Duration
                        }
                    }
                    , this));
                    _procedures.Add(new ProcedureViewModel("", _changeViewCommand,
                    new Examination()
                    {
                        Patient = _procedure.Patient,
                        Doctor = _procedure.Doctor,
                        Room = _procedure.Room,
                        ProcedureType = _procedure.ProcedureType,
                        TimeInterval = new TimeInterval()
                        {
                            Start = generate.AddHours(5),
                            End = generate.AddHours(5) + _procedure.ProcedureType.Duration
                        }
                    }
                    , this));
                }
                else
                {
                    _procedures.Add(new ProcedureViewModel("", _changeViewCommand,
                    new Surgery()
                    {
                        Patient = _procedure.Patient,
                        Doctor = _procedure.Doctor,
                        Room = _procedure.Room,
                        ProcedureType = _procedure.ProcedureType,
                        TimeInterval = new TimeInterval()
                        {
                            Start = DateTime.Now.AddMinutes(1),
                            End = DateTime.Now.AddMinutes(1) + _procedure.ProcedureType.Duration
                        }
                    }
                    , this));
                    _procedures.Add(new ProcedureViewModel("", _changeViewCommand,
                    new Surgery()
                    {
                        Patient = _procedure.Patient,
                        Doctor = _procedure.Doctor,
                        Room = _procedure.Room,
                        ProcedureType = _procedure.ProcedureType,
                        TimeInterval = new TimeInterval()
                        {
                            Start = generate.AddMinutes(30),
                            End = generate.AddMinutes(30) + _procedure.ProcedureType.Duration
                        }
                    }
                    , this));
                    _procedures.Add(new ProcedureViewModel("", _changeViewCommand,
                    new Surgery()
                    {
                        Patient = _procedure.Patient,
                        Doctor = _procedure.Doctor,
                        Room = _procedure.Room,
                        ProcedureType = _procedure.ProcedureType,
                        TimeInterval = new TimeInterval()
                        {
                            Start = generate.AddHours(2),
                            End = generate.AddHours(2) + _procedure.ProcedureType.Duration
                        }
                    }
                    , this));
                    _procedures.Add(new ProcedureViewModel("", _changeViewCommand,
                    new Surgery()
                    {
                        Patient = _procedure.Patient,
                        Doctor = _procedure.Doctor,
                        Room = _procedure.Room,
                        ProcedureType = _procedure.ProcedureType,
                        TimeInterval = new TimeInterval()
                        {
                            Start = generate.AddHours(5),
                            End = generate.AddHours(5) + _procedure.ProcedureType.Duration
                        }
                    }
                    , this));
                }
            }
        }

    }
}
