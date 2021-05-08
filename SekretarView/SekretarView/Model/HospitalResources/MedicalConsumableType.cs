// File:    MedicalConsumableType.cs
// Author:  Lana
// Created: 18 April 2020 17:38:58
// Purpose: Definition of Class MedicalConsumableType

using System;

namespace Model.HospitalResources
{
    public class MedicalConsumableType : Repository.Generics.Entity<int>
    {
        private String name;
        private int id;
        private String purpose;

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