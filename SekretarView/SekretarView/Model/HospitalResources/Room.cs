// File:    Room.cs
// Author:  Lana
// Created: 18 April 2020 17:07:59
// Purpose: Definition of Class Room

using System;

namespace Model.HospitalResources
{
    public class Room : Repository.Generics.Entity<int>
    {
        private int id;
        private String name;
        private RoomType purpose;

        private EquipmentUnit[] equipment;
        private Department department;

        /// <summary>
        /// Property for Department
        /// </summary>
        /// <pdGenerated>Default opposite class property</pdGenerated>
        public Department Department
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

        public string Name { get => name; set => name = value; }
        public RoomType Purpose { get => purpose; set => purpose = value; }
        public EquipmentUnit[] Equipment { get => equipment; set => equipment = value; }

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