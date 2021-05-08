using Model.Schedule.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SekretarView
{
    class ReportProcedure
    {
        private Procedure _procedure;

        public String Pocetak
        {
            get
            {
                return _procedure.TimeInterval.Start.ToShortDateString() + " " + _procedure.TimeInterval.Start.ToShortTimeString();
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


        public ReportProcedure(Procedure procedure)
        {
            _procedure = procedure;
        }
    }
}
