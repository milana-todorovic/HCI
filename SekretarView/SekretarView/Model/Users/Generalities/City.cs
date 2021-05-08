// File:    City.cs
// Author:  Lana
// Created: 27 May 2020 22:23:44
// Purpose: Definition of Class City

using System;

namespace Model.Users.Generalities
{
    public class City : Repository.Generics.Entity<int>
    {
        private String name;
        private String postalCode;
        private int id;

        public Country country;

        /// <summary>
        /// Property for Country
        /// </summary>
        /// <pdGenerated>Default opposite class property</pdGenerated>
        public Country Country
        {
            get
            {
                return country;
            }
            set
            {
                this.country = value;
            }
        }

        public string Name { get => name; set => name = value; }
        public string PostalCode { get => postalCode; set => postalCode = value; }
        public int Id { get => id; set => id = value; }

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
            return Name + ", " + Country.Name;
        }
    }
}