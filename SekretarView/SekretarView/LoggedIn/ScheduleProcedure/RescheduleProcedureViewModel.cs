using Model.Schedule.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SekretarView
{
    class RescheduleProcedureViewModel : ChooseProcedureViewModel
    {
        private Procedure _originalProcedure;

        override protected void handleProcedure(Procedure procedure)
        {
            if (procedure is Examination)
            {
                DataMockup.Instance.Examinations[_originalProcedure.TimeInterval.Start.Date].Remove((Examination)_originalProcedure);
                
                _originalProcedure.Doctor = procedure.Doctor;
                _originalProcedure.TimeInterval = procedure.TimeInterval;
                _originalProcedure.Room = procedure.Room;

                if (DataMockup.Instance.Examinations.ContainsKey(_originalProcedure.TimeInterval.Start.Date))
                    DataMockup.Instance.Examinations[_originalProcedure.TimeInterval.Start.Date].Add((Examination)_originalProcedure);
                else
                    DataMockup.Instance.Examinations[_originalProcedure.TimeInterval.Start.Date] =
                        new List<Examination> { (Examination)_originalProcedure };
            }
            else
            {
                DataMockup.Instance.Surgeries[_originalProcedure.TimeInterval.Start.Date].Remove((Surgery)_originalProcedure);

                _originalProcedure.Doctor = procedure.Doctor;
                _originalProcedure.TimeInterval = procedure.TimeInterval;
                _originalProcedure.Room = procedure.Room;

                if (DataMockup.Instance.Surgeries.ContainsKey(_originalProcedure.TimeInterval.Start.Date))
                    DataMockup.Instance.Surgeries[_originalProcedure.TimeInterval.Start.Date].Add((Surgery)_originalProcedure);
                else
                    DataMockup.Instance.Surgeries[_originalProcedure.TimeInterval.Start.Date] =
                        new List<Surgery> { (Surgery)_originalProcedure };
            }




            Mediator.NotifyColleagues("ProcedureUpdated", procedure);
            _changeViewCommand.Execute(_caller);
        }

        public RescheduleProcedureViewModel(String title, ViewModelBase caller, ViewModelBase previous, Procedure procedure,
                Procedure originalProcedure, ICommand changeViewCommand) : base(title, caller, previous, procedure, changeViewCommand)
        {
            _originalProcedure = originalProcedure;
            _handleProcedureName = "Sačuvaj";
            _stepNum = 2;
        }
    }
}
