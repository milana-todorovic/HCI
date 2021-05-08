// File:    Country.cs
// Author:  Lana
// Created: 27 May 2020 22:23:44
// Purpose: Definition of Class Country

using System;

namespace Model.Users.Generalities
{
    public class Country : Repository.Generics.Entity<int>
    {
        private String name;
        private String code;
        private int id;

        public string Name { get => name; set => name = value; }
        public string Code { get => code; set => code = value; }

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