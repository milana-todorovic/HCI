// File:    HospitalizationSchedulingPreference.cs
// Author:  Lana
// Created: 21 April 2020 17:32:57
// Purpose: Definition of Class HospitalizationSchedulingPreference

using Model.HospitalResources;
using System;

namespace Model.Schedule.SchedulingPreferences
{
    public class HospitalizationSchedulingPreference : Repository.Generics.Entity<int>
    {
        private Model.HospitalResources.Room room;
        private TimingPreference preferredAdmissionDate;
        private TimingPreference preferredDischargeDate;

        protected int id;

        public Room Room { get => room; set => room = value; }
        public TimingPreference PreferredAdmissionDate { get => preferredAdmissionDate; set => preferredAdmissionDate = value; }
        public TimingPreference PreferredDischargeDate { get => preferredDischargeDate; set => preferredDischargeDate = value; }

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