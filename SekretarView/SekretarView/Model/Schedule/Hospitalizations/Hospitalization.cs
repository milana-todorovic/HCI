// File:    Hospitalization.cs
// Author:  Lana
// Created: 20 April 2020 23:27:02
// Purpose: Definition of Class Hospitalization

using Model.HospitalResources;
using Model.Miscellaneous;
using Model.Utilities;
using System;

namespace Model.Schedule.Hospitalizations
{
    public class Hospitalization : Repository.Generics.Entity<int>
    {
        private Model.Miscellaneous.Diagnosis diagnosis;
        private String causeOfAdmission;
        private DischargeType dischargeType;
        private int id;

        private Model.HospitalResources.Room room;
        private HospitalizationType hospitalizationType;


        public int GetKey()
        {
            throw new NotImplementedException();
        }

        public void SetKey(int id)
        {
            throw new NotImplementedException();
        }

        private Model.Utilities.TimeInterval timeInterval;

        public Model.Users.Patient.Patient patient;

        /// <summary>
        /// Property for Model.Users.Patient.Patient
        /// </summary>
        /// <pdGenerated>Default opposite class property</pdGenerated>
        public Model.Users.Patient.Patient Patient
        {
            get
            {
                return patient;
            }
            set
            {
                this.patient = value;
            }
        }

        
        public Diagnosis Diagnosis { get => diagnosis; set => diagnosis = value; }
        public string CauseOfAdmission { get => causeOfAdmission; set => causeOfAdmission = value; }
        public DischargeType DischargeType { get => dischargeType; set => dischargeType = value; }
        public int Id { get => id; set => id = value; }
        public Room Room { get => room; set => room = value; }
        public HospitalizationType HospitalizationType { get => hospitalizationType; set => hospitalizationType = value; }
        public TimeInterval TimeInterval { get => timeInterval; set => timeInterval = value; }
    }
}