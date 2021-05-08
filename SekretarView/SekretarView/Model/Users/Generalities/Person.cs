// File:    Person.cs
// Author:  Lana
// Created: 27 May 2020 22:23:44
// Purpose: Definition of Class Person

using System;
using System.Collections;
using System.Collections.Generic;

namespace Model.Users.Generalities
{
    public abstract class Person
    {
        protected String name;
        protected String surname;
        protected DateTime dateOfBirth;
        protected Address address;
        protected String telephoneNumber;
        protected String jmbg;
        protected Gender gender;

        protected System.Collections.ArrayList citizenship;

        /// <summary>
        /// Property for collection of Country
        /// </summary>
        /// <pdGenerated>Default opposite class collection property</pdGenerated>
        public System.Collections.ArrayList Citizenship
        {
            get
            {
                if (citizenship == null)
                    citizenship = new System.Collections.ArrayList();
                return citizenship;
            }
            set
            {
                RemoveAllCitizenship();
                if (value != null)
                {
                    foreach (Country oCountry in value)
                        AddCitizenship(oCountry);
                }
            }
        }

        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }
        public DateTime DateOfBirth { get => dateOfBirth; set => dateOfBirth = value; }
        public Address Address { get => address; set => address = value; }
        public string TelephoneNumber { get => telephoneNumber; set => telephoneNumber = value; }
        public string JMBG { get => jmbg; set => jmbg = value; }
        public Gender Gender { get => gender; set => gender = value; }

        /// <summary>
        /// Add a new Country in the collection
        /// </summary>
        /// <pdGenerated>Default Add</pdGenerated>
        public void AddCitizenship(Country newCountry)
        {
            if (newCountry == null)
                return;
            if (this.citizenship == null)
                this.citizenship = new System.Collections.ArrayList();
            if (!this.citizenship.Contains(newCountry))
                this.citizenship.Add(newCountry);
        }

        /// <summary>
        /// Remove an existing Country from the collection
        /// </summary>
        /// <pdGenerated>Default Remove</pdGenerated>
        public void RemoveCitizenship(Country oldCountry)
        {
            if (oldCountry == null)
                return;
            if (this.citizenship != null)
                if (this.citizenship.Contains(oldCountry))
                    this.citizenship.Remove(oldCountry);
        }

        /// <summary>
        /// Remove all instances of Country from the collection
        /// </summary>
        /// <pdGenerated>Default removeAll</pdGenerated>
        public void RemoveAllCitizenship()
        {
            if (citizenship != null)
                citizenship.Clear();
        }

        public override bool Equals(object obj)
        {
            return obj is Person person &&
                   name == person.name &&
                   surname == person.surname &&
                   dateOfBirth == person.dateOfBirth &&
                   EqualityComparer<Address>.Default.Equals(address, person.address) &&
                   telephoneNumber == person.telephoneNumber &&
                   jmbg == person.jmbg &&
                   gender == person.gender &&
                   EqualityComparer<ArrayList>.Default.Equals(citizenship, person.citizenship);
        }
    }
}