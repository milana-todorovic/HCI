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
    class ChooseHospitalizationViewModel : ViewModelBase
    {
        protected ObservableCollection<HospitalizationViewModel> _hospitalizations;
        protected Hospitalization _hospitalization;

        protected int _stepNum;

        protected ViewModelBase _previous;
        protected ViewModelBase _caller;

        protected ICommand _changeViewCommand;
        protected ICommand _previousStep;
        protected ICommand _cancel;
        protected ICommand _handleHospitalization;

        protected String _handleHospitalizationName;

        public int StepNum
        {
            get
            {
                return _stepNum;
            }
        }

        public String HandleHospitalizationName
        {
            get
            {
                return _handleHospitalizationName;
            }
        }

        public ICommand HandleHospitalization
        {
            get
            {
                if (_handleHospitalization == null)
                    _handleHospitalization = new RelayCommand(p => handleHospitalization(((HospitalizationViewModel)p).Hospitalization));
                return _handleHospitalization;
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

        public ObservableCollection<HospitalizationViewModel> Hospitalizations
        {
            get
            {
                return _hospitalizations;
            }
        }

        protected virtual void handleHospitalization(Hospitalization hospitalization)
        {
            DataMockup.Instance.Hospitalizations.Add(hospitalization);

            Mediator.NotifyColleagues("HospitalizationAdded", hospitalization);
            _changeViewCommand.Execute(_caller);
        }

        public ChooseHospitalizationViewModel(String title, ViewModelBase caller, ViewModelBase previous, 
            Hospitalization hospitalization, List<Hospitalization> hospitalizations ,ICommand changeViewCommand) :
            base(title, true)
        {
            _caller = caller;
            _previous = previous;
            _hospitalization = hospitalization;
            _hospitalizations = new ObservableCollection<HospitalizationViewModel>();
            foreach (Hospitalization hosp in hospitalizations)
                _hospitalizations.Add(new HospitalizationViewModel(_changeViewCommand, hosp, this));
            _handleHospitalizationName = "Zakaži";
            _stepNum = 3;
            _changeViewCommand = changeViewCommand;
        }
    }
}
