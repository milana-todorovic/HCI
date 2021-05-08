// File:    ProcedureSchedulingPreference.cs
// Author:  Lana
// Created: 21 April 2020 17:32:57
// Purpose: Definition of Class ProcedureSchedulingPreference

using Model.Users.Employee;
using System;

namespace Model.Schedule.SchedulingPreferences
{
    public class ProcedureSchedulingPreference : Repository.Generics.Entity<int>
    {
        private TimingPreference preferredTime;
        private Model.Users.Employee.Doctor preferredDoctor;

        protected int id;

        public Model.HospitalResources.Room preferredRoom;

        public TimingPreference PreferredTime { get => preferredTime; set => preferredTime = value; }
        public Doctor PreferredDoctor { get => preferredDoctor; set => preferredDoctor = value; }

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