// File:    Doctor.cs
// Author:  Lana
// Created: 13 April 2020 18:32:18
// Purpose: Definition of Class Doctor

using System;

namespace Model.Users.Employee
{
    public class Doctor : Employee
    {
        private Specialty specialty;


        private Model.HospitalResources.Department department;

        /// <summary>
        /// Property for Model.HospitalResources.Department
        /// </summary>
        /// <pdGenerated>Default opposite class property</pdGenerated>
        public Model.HospitalResources.Department Department
        {
            get
            {
                return department;
            }
            set
            {
                this.department = value;
            }
        }

        public Specialty Specialty { get => specialty; set => specialty = value; }

        public override string ToString()
        {
            return Name + " " + Surname + ", " + Specialty.Name;
        }
    }
}