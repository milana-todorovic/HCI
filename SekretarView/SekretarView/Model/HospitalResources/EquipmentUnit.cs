// File:    EquipmentUnit.cs
// Author:  Lana
// Created: 18 April 2020 17:00:15
// Purpose: Definition of Class EquipmentUnit

using System;

namespace Model.HospitalResources
{
    public class EquipmentUnit : Repository.Generics.Entity<int>
    {
        private int id;
        private DateTime acquisitionDate;
        private String manufacturer;

        private Room currentLocation;
        private EquipmentType equipmentType;

        /// <summary>
        /// Property for EquipmentType
        /// </summary>
        /// <pdGenerated>Default opposite class property</pdGenerated>
        public EquipmentType EquipmentType
        {
            get
            {
                return equipmentType;
            }
            set
            {
                this.equipmentType = value;
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