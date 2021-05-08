// File:    Shift.cs
// Author:  Lana
// Created: 21 April 2020 00:09:43
// Purpose: Definition of Class Shift

using System;

namespace Model.Users.Employee
{
    public class Shift : Repository.Generics.Entity<int>
    {
        private Model.Utilities.TimeInterval timeInterval;
        private int id;

        private Model.HospitalResources.Room assignedExamRoom;
        private Doctor doctor;

        /// <summary>
        /// Property for Doctor
        /// </summary>
        /// <pdGenerated>Default opposite class property</pdGenerated>
        public Doctor Doctor
        {
            get
            {
                return doctor;
            }
            set
            {
                this.doctor = value;
            }
        }

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