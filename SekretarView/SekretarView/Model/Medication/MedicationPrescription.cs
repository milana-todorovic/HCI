// File:    MedicationPrescription.cs
// Author:  Lana
// Created: 20 April 2020 23:19:05
// Purpose: Definition of Class MedicationPrescription

using System;

namespace Model.Medication
{
    public class MedicationPrescription : Repository.Generics.Entity<int>
    {
        private DateTime date;
        private Model.Miscellaneous.Diagnosis diagnosis;
        private int id;

        private Medication medication;
        private IntakeInstructions instructions;
        private Model.Users.Employee.Doctor prescribedBy;

        /// <summary>
        /// Property for Model.Users.Employee.Doctor
        /// </summary>
        /// <pdGenerated>Default opposite class property</pdGenerated>
        public Model.Users.Employee.Doctor PrescribedBy
        {
            get
            {
                return prescribedBy;
            }
            set
            {
                this.prescribedBy = value;
            }
        }

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