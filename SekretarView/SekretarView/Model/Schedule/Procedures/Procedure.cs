// File:    Procedure.cs
// Author:  Lana
// Created: 20 April 2020 23:40:27
// Purpose: Definition of Class Procedure

using Model.HospitalResources;
using Model.Users.Employee;
using Model.Users.Patient;
using Model.Utilities;
using System;

namespace Model.Schedule.Procedures
{
    public abstract class Procedure : Repository.Generics.Entity<int>
    {
        protected int id;

        protected Model.Utilities.TimeInterval timeInterval;
        protected Model.Users.Employee.Doctor doctor;
        protected Model.HospitalResources.Room room;
        protected ProcedureType procedureType;
        protected Model.Users.Patient.Patient patient;
        protected Examination referredFrom;

        public TimeInterval TimeInterval { get => timeInterval; set => timeInterval = value; }
        public Doctor Doctor { get => doctor; set => doctor = value; }
        public Room Room { get => room; set => room = value; }
        public ProcedureType ProcedureType { get => procedureType; set => procedureType = value; }
        public Patient Patient { get => patient; set => patient = value; }
        public Examination ReferredFrom { get => referredFrom; set => referredFrom = value; }

        public int GetKey()
        {
            throw new NotImplementedException();
        }

        public void SetKey(int id)
        {
            throw new NotImplementedException();
        }
    }
}