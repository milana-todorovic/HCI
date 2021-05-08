// File:    Specialty.cs
// Author:  Lana
// Created: 13 April 2020 18:40:05
// Purpose: Definition of Class Specialty

using System;

namespace Model.Users.Employee
{
    public class Specialty : Repository.Generics.Entity<int>
    {
        private String name;
        private String description;
        private int id;

        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }

        public int GetKey()
        {
            throw new NotImplementedException();
        }

        public void SetKey(int id)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}