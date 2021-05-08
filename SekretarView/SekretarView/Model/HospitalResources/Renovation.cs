// File:    Renovation.cs
// Author:  Lana
// Created: 21 April 2020 00:07:14
// Purpose: Definition of Class Renovation

using System;

namespace Model.HospitalResources
{
    public class Renovation : Repository.Generics.Entity<int>
    {
        private String description;
        private int id;

        private Model.Utilities.TimeInterval timeInterval;
        private Room room;

        /// <summary>
        /// Property for Room
        /// </summary>
        /// <pdGenerated>Default opposite class property</pdGenerated>
        public Room Room
        {
            get
            {
                return room;
            }
            set
            {
                this.room = value;
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