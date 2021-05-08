using Model.Schedule.Hospitalizations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SekretarView
{
    class HospitalizationReschedulingInfoViewModel : HospitalizationSchedulingInfoViewModel
    {
        private Boolean _automatic;
        private ObservableCollection<String> _automaticOptions;
        private String _selectedOption;
        private Hospitalization _originalHospitalization;
        private String _dateName;

        public String DateName
        {
            get
            {
                return _dateName;
            }
        }

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

        private static Hospitalization copyHospitalization(Hospitalization hospitalization)
        {
            return new Hospitalization()
            {
                Patient = hospitalization.Patient,
                HospitalizationType = hospitalization.HospitalizationType,
                Room = hospitalization.Room,
                TimeInterval = new Model.Utilities.TimeInterval()
                {
                    Start = hospitalization.TimeInterval.Start,
                    End = hospitalization.TimeInterval.End
                }
            };
        }

        override protected void nextStep()
        {
            if (_exactRoom)
                _hospitalization.Room = _selectedRoom;

            if (_hospitalization.TimeInterval.Start > DateTime.Now.Date && _dateMode == 1)
            {
                _hospitalization.TimeInterval.Start = SelectedDate.Value;
            }
            else if (_hospitalization.TimeInterval.Start <= DateTime.Now.Date && _dateMode == 1)
            {
                _hospitalization.TimeInterval.End = SelectedDate.Value;
            }

            _changeViewCommand.Execute(new RescheduleHospitalizationViewModel(
                Title, _caller, this, _originalHospitalization, mock(), _changeViewCommand));
        }

        public HospitalizationReschedulingInfoViewModel(Hospitalization hospitalization, ViewModelBase caller, ICommand changeViewCommand) :
            base("Izmeni bolničko lečenje", copyHospitalization(hospitalization), caller, null, changeViewCommand)
        {
            _originalHospitalization = hospitalization;

            _automaticOptions = new ObservableCollection<string>();

            if (hospitalization.TimeInterval.Start > DateTime.Now.Date)
            {
                _dateName = "Datum prijema";
                _automaticOptions.Add("Kasniji datum prijema");
                _automaticOptions.Add("Raniji datum prijema");
                _automaticOptions.Add("Oslobodi prostoriju");
            }
            else
            {
                _dateName = "Datum otpusta";
                _automaticOptions.Add("Kasniji datum otpusta");
                if (hospitalization.TimeInterval.End > DateTime.Now.Date)
                    _automaticOptions.Add("Raniji datum otpusta");
                _automaticOptions.Add("Oslobodi prostoriju");
            }

            SelectedOption = AutomaticOptions[0];
        }
    }
}
