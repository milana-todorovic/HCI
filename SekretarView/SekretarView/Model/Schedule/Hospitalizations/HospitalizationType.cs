// File:    HospitalizationType.cs
// Author:  Lana
// Created: 20 April 2020 23:27:02
// Purpose: Definition of Class HospitalizationType

using System;

namespace Model.Schedule.Hospitalizations
{
    public class HospitalizationType : Repository.Generics.Entity<int>
    {
        private String name;
        private int usualNumberOfDays;
        private int id;

        private System.Collections.ArrayList appropriateDepartments;

        /// <summary>
        /// Property for collection of Model.HospitalResources.Department
        /// </summary>
        /// <pdGenerated>Default opposite class collection property</pdGenerated>
        public System.Collections.ArrayList AppropriateDepartments
        {
            get
            {
                if (appropriateDepartments == null)
                    appropriateDepartments = new System.Collections.ArrayList();
                return appropriateDepartments;
            }
            set
            {
                RemoveAllAppropriateDepartments();
                if (value != null)
                {
                    foreach (Model.HospitalResources.Department oDepartment in value)
                        AddAppropriateDepartments(oDepartment);
                }
            }
        }

        /// <summary>
        /// Add a new Model.HospitalResources.Department in the collection
        /// </summary>
        /// <pdGenerated>Default Add</pdGenerated>
        public void AddAppropriateDepartments(Model.HospitalResources.Department newDepartment)
        {
            if (newDepartment == null)
                return;
            if (this.appropriateDepartments == null)
                this.appropriateDepartments = new System.Collections.ArrayList();
            if (!this.appropriateDepartments.Contains(newDepartment))
                this.appropriateDepartments.Add(newDepartment);
        }

        /// <summary>
        /// Remove an existing Model.HospitalResources.Department from the collection
        /// </summary>
        /// <pdGenerated>Default Remove</pdGenerated>
        public void RemoveAppropriateDepartments(Model.HospitalResources.Department oldDepartment)
        {
            if (oldDepartment == null)
                return;
            if (this.appropriateDepartments != null)
                if (this.appropriateDepartments.Contains(oldDepartment))
                    this.appropriateDepartments.Remove(oldDepartment);
        }

        /// <summary>
        /// Remove all instances of Model.HospitalResources.Department from the collection
        /// </summary>
        /// <pdGenerated>Default removeAll</pdGenerated>
        public void RemoveAllAppropriateDepartments()
        {
            if (appropriateDepartments != null)
                appropriateDepartments.Clear();
        }
        private System.Collections.ArrayList necessaryEquipment;

        /// <summary>
        /// Property for collection of Model.HospitalResources.EquipmentType
        /// </summary>
        /// <pdGenerated>Default opposite class collection property</pdGenerated>
        public System.Collections.ArrayList NecessaryEquipment
        {
            get
            {
                if (necessaryEquipment == null)
                    necessaryEquipment = new System.Collections.ArrayList();
                return necessaryEquipment;
            }
            set
            {
                RemoveAllNecessaryEquipment();
                if (value != null)
                {
                    foreach (Model.HospitalResources.EquipmentType oEquipmentType in value)
                        AddNecessaryEquipment(oEquipmentType);
                }
            }
        }

        public string Name { get => name; set => name = value; }
        public int UsualNumberOfDays { get => usualNumberOfDays; set => usualNumberOfDays = value; }

        /// <summary>
        /// Add a new Model.HospitalResources.EquipmentType in the collection
        /// </summary>
        /// <pdGenerated>Default Add</pdGenerated>
        public void AddNecessaryEquipment(Model.HospitalResources.EquipmentType newEquipmentType)
        {
            if (newEquipmentType == null)
                return;
            if (this.necessaryEquipment == null)
                this.necessaryEquipment = new System.Collections.ArrayList();
            if (!this.necessaryEquipment.Contains(newEquipmentType))
                this.necessaryEquipment.Add(newEquipmentType);
        }

        /// <summary>
        /// Remove an existing Model.HospitalResources.EquipmentType from the collection
        /// </summary>
        /// <pdGenerated>Default Remove</pdGenerated>
        public void RemoveNecessaryEquipment(Model.HospitalResources.EquipmentType oldEquipmentType)
        {
            if (oldEquipmentType == null)
                return;
            if (this.necessaryEquipment != null)
                if (this.necessaryEquipment.Contains(oldEquipmentType))
                    this.necessaryEquipment.Remove(oldEquipmentType);
        }

        /// <summary>
        /// Remove all instances of Model.HospitalResources.EquipmentType from the collection
        /// </summary>
        /// <pdGenerated>Default removeAll</pdGenerated>
        public void RemoveAllNecessaryEquipment()
        {
            if (necessaryEquipment != null)
                necessaryEquipment.Clear();
        }

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