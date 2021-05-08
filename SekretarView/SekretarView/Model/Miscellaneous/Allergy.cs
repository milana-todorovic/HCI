// File:    Allergy.cs
// Author:  Lana
// Created: 20 April 2020 23:04:50
// Purpose: Definition of Class Allergy

using System;

namespace Model.Miscellaneous
{
    public class Allergy : Repository.Generics.Entity<int>
    {
        private String allergen;
        private String[] symptoms;
        private String prevention;
        private int id;

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