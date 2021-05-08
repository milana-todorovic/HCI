// File:    Medication.cs
// Author:  Lana
// Created: 14 April 2020 20:48:37
// Purpose: Definition of Class Medication

using System;

namespace Model.Medication
{
    public class Medication : Repository.Generics.Entity<int>
    {
        private String name;
        private String manufacturer;
        private Medication[] alternatives;
        private String description;
        private MedicineType type;
        private int id;

        private System.Collections.ArrayList MedicationsCanHaveSideEffects;
        private Amount[] MedicationsHaveIngredients;

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