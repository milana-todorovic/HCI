// File:    MedicalConsumable.cs
// Author:  Lana
// Created: 18 April 2020 17:49:09
// Purpose: Definition of Class MedicalConsumable

using System;

namespace Model.HospitalResources
{
    public class MedicalConsumable : Repository.Generics.Entity<int>
    {
        private String manufacutrer;
        private int id;
        private String description;

        private MedicalConsumableType medicalConsumableType;

        /// <summary>
        /// Property for MedicalConsumableType
        /// </summary>
        /// <pdGenerated>Default opposite class property</pdGenerated>
        public MedicalConsumableType MedicalConsumableType
        {
            get
            {
                return medicalConsumableType;
            }
            set
            {
                this.medicalConsumableType = value;
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