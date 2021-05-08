// File:    EquipmentType.cs
// Author:  Lana
// Created: 18 April 2020 16:54:14
// Purpose: Definition of Class EquipmentType

using System;

namespace Model.HospitalResources
{
    public class EquipmentType : Repository.Generics.Entity<int>
    {
        private String name;
        private int code;
        private String purpose;
        private Boolean requiresRenovationToMove;

        public string Name { get => name; set => name = value; }
        public int Code { get => code; set => code = value; }
        public string Purpose { get => purpose; set => purpose = value; }
        public bool RequiresRenovationToMove { get => requiresRenovationToMove; set => requiresRenovationToMove = value; }

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