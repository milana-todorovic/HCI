// File:    ProcedureType.cs
// Author:  Lana
// Created: 20 April 2020 23:40:27
// Purpose: Definition of Class ProcedureType

using System;

namespace Model.Schedule.Procedures
{
    public class ProcedureType : Repository.Generics.Entity<int>
    {
        private String name;
        private TimeSpan duration;
        private ProcedureKind kind;
        protected int id;

        protected System.Collections.ArrayList qualifiedSpecialties;

        /// <summary>
        /// Property for collection of Model.Users.Employee.Specialty
        /// </summary>
        /// <pdGenerated>Default opposite class collection property</pdGenerated>
        public System.Collections.ArrayList QualifiedSpecialties
        {
            get
            {
                if (qualifiedSpecialties == null)
                    qualifiedSpecialties = new System.Collections.ArrayList();
                return qualifiedSpecialties;
            }
            set
            {
                RemoveAllQualifiedSpecialties();
                if (value != null)
                {
                    foreach (Model.Users.Employee.Specialty oSpecialty in value)
                        AddQualifiedSpecialties(oSpecialty);
                }
            }
        }

        /// <summary>
        /// Add a new Model.Users.Employee.Specialty in the collection
        /// </summary>
        /// <pdGenerated>Default Add</pdGenerated>
        public void AddQualifiedSpecialties(Model.Users.Employee.Specialty newSpecialty)
        {
            if (newSpecialty == null)
                return;
            if (this.qualifiedSpecialties == null)
                this.qualifiedSpecialties = new System.Collections.ArrayList();
            if (!this.qualifiedSpecialties.Contains(newSpecialty))
                this.qualifiedSpecialties.Add(newSpecialty);
        }

        /// <summary>
        /// Remove an existing Model.Users.Employee.Specialty from the collection
        /// </summary>
        /// <pdGenerated>Default Remove</pdGenerated>
        public void RemoveQualifiedSpecialties(Model.Users.Employee.Specialty oldSpecialty)
        {
            if (oldSpecialty == null)
                return;
            if (this.qualifiedSpecialties != null)
                if (this.qualifiedSpecialties.Contains(oldSpecialty))
                    this.qualifiedSpecialties.Remove(oldSpecialty);
        }

        /// <summary>
        /// Remove all instances of Model.Users.Employee.Specialty from the collection
        /// </summary>
        /// <pdGenerated>Default removeAll</pdGenerated>
        public void RemoveAllQualifiedSpecialties()
        {
            if (qualifiedSpecialties != null)
                qualifiedSpecialties.Clear();
        }
        protected System.Collections.ArrayList necessaryEquipment;

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
        public TimeSpan Duration { get => duration; set => duration = value; }
        public ProcedureKind Kind { get => kind; set => kind = value; }

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

        public ProcedurePriority procedurePriority;

        public override string ToString()
        {
            return Name;
        }
    }
}