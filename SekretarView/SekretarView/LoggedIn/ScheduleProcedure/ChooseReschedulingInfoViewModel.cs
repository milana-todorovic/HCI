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
    class ChooseReschedulingInfoViewModel : ChooseSchedulingInfoViewModel
    {
        private Boolean _automatic;
        private ObservableCollection<String> _automaticOptions;
        private String _selectedOption;
        private Procedure _originalProcedure;


        public String SelectedOption
        {
            get
            {
                return _selectedOption;
            }
            set
            {
                _selectedOption = value;
                OnPropertyChanged("SelectedOption");
            }
        }

        public ObservableCollection<String> AutomaticOptions
        {
            get
            {
                return _automaticOptions;
            }
        }

        public Boolean Automatic
        {
            get
            {
                return _automatic;
            }
            set
            {
                _automatic = value;
                OnPropertyChanged("Automatic");
            }
        }

        override protected Boolean canGoToNextStep()
        {
            if (_automatic)
                return true;
            else
                return base.canGoToNextStep();
        }

        override protected void nextStep()
        {
            if (_exactDoctor)
                _procedure.Doctor = _selectedDoctor;
            else if (_qualifiedDoctors.Count > 0)
                _procedure.Doctor = _qualifiedDoctors[0];

            if (_exactRoom)
                _procedure.Room = _selectedRoom;
            else if (_qualifiedRooms.Count > 0)
                _procedure.Room = _qualifiedRooms[0];

            _changeViewCommand.Execute(new RescheduleProcedureViewModel(Title, _caller, this, _procedure, _originalProcedure, _changeViewCommand));
        }

        private static Procedure copyProcedure(Procedure procedure)
        {
            if (procedure is Examination)
                return new Examination()
                {
                    ProcedureType = procedure.ProcedureType,
                    Patient = procedure.Patient
                };
            else
                return new Surgery()
                {
                    ProcedureType = procedure.ProcedureType,
                    Patient = procedure.Patient
                };
        }

        public ChooseReschedulingInfoViewModel(String title, Procedure procedure, ViewModelBase caller, ICommand changeViewCommand) : 
            base(title, copyProcedure(procedure), caller, null, changeViewCommand)
        {
            _originalProcedure = procedure;
            _automaticOptions = new ObservableCollection<string>
                { "Kasniji datum", "Isti datum, kasnije vreme", "Isti datum, ranije vreme",
                    "Oslobodi doktora", "Oslobodi prostoriju"};
            _selectedOption = _automaticOptions[0];
            if (procedure.TimeInterval.Start.Date < DateTime.Now.Date)
                _automaticOptions.Insert(0, "Raniji datum");
        }
    }
}
