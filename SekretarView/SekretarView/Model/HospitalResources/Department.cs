// File:    Department.cs
// Author:  Lana
// Created: 18 April 2020 17:20:00
// Purpose: Definition of Class Department

using System;

namespace Model.HospitalResources
{
    public class Department : Repository.Generics.Entity<int>
    {
        private String name;
        private int id;
        private String description;

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
    }
}