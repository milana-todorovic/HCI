using Model.Schedule.Hospitalizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SekretarView
{
    class RescheduleHospitalizationViewModel : ChooseHospitalizationViewModel
    {
        private Hospitalization _originalHospitalization;

        protected override void handleHospitalization(Hospitalization hospitalization)
        {
            _originalHospitalization.Room = hospitalization.Room;
            _originalHospitalization.TimeInterval = hospitalization.TimeInterval;

            Mediator.NotifyColleagues("HospitalizationUpdated", _originalHospitalization);
            _changeViewCommand.Execute(_caller);
        }

        public RescheduleHospitalizationViewModel(String title, ViewModelBase caller, ViewModelBase previous,
            Hospitalization hospitalization, List<Hospitalization> hospitalizations, ICommand changeViewCommand) :
            base(title, caller, previous, hospitalization, hospitalizations, changeViewCommand)
        {
            _originalHospitalization = hospitalization;
            _stepNum = 2;
            _handleHospitalizationName = "Sačuvaj";
        }
    }
}
