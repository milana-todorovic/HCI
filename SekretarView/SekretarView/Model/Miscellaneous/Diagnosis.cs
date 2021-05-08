// File:    Diagnosis.cs
// Author:  Lana
// Created: 20 April 2020 23:15:25
// Purpose: Definition of Class Diagnosis

using System;

namespace Model.Miscellaneous
{
    public class Diagnosis : Repository.Generics.Entity<String>
    {
        private String icd;
        private String name;
        private String description;
        private Boolean isInjury;

        public string GetKey()
        {
            throw new NotImplementedException();
        }

        public void SetKey(string id)
        {
            throw new NotImplementedException();
        }
    }
}