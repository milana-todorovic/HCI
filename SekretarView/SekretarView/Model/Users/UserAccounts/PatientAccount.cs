// File:    PatientAccount.cs
// Author:  Lana
// Created: 21 April 2020 11:34:46
// Purpose: Definition of Class PatientAccount

using System;

namespace Model.Users.UserAccounts
{
    public class PatientAccount : UserAccount
    {
        private Boolean respondedToSurvey;

        private Model.Users.Patient.Patient patient;
        private System.Collections.ArrayList favouriteDoctors;

        /// <summary>
        /// Property for collection of Model.Users.Employee.Doctor
        /// </summary>
        /// <pdGenerated>Default opposite class collection property</pdGenerated>
        public System.Collections.ArrayList FavouriteDoctors
        {
            get
            {
                if (favouriteDoctors == null)
                    favouriteDoctors = new System.Collections.ArrayList();
                return favouriteDoctors;
            }
            set
            {
                RemoveAllFavouriteDoctors();
                if (value != null)
                {
                    foreach (Model.Users.Employee.Doctor oDoctor in value)
                        AddFavouriteDoctors(oDoctor);
                }
            }
        }

        /// <summary>
        /// Add a new Model.Users.Employee.Doctor in the collection
        /// </summary>
        /// <pdGenerated>Default Add</pdGenerated>
        public void AddFavouriteDoctors(Model.Users.Employee.Doctor newDoctor)
        {
            if (newDoctor == null)
                return;
            if (this.favouriteDoctors == null)
                this.favouriteDoctors = new System.Collections.ArrayList();
            if (!this.favouriteDoctors.Contains(newDoctor))
                this.favouriteDoctors.Add(newDoctor);
        }

        /// <summary>
        /// Remove an existing Model.Users.Employee.Doctor from the collection
        /// </summary>
        /// <pdGenerated>Default Remove</pdGenerated>
        public void RemoveFavouriteDoctors(Model.Users.Employee.Doctor oldDoctor)
        {
            if (oldDoctor == null)
                return;
            if (this.favouriteDoctors != null)
                if (this.favouriteDoctors.Contains(oldDoctor))
                    this.favouriteDoctors.Remove(oldDoctor);
        }

        /// <summary>
        /// Remove all instances of Model.Users.Employee.Doctor from the collection
        /// </summary>
        /// <pdGenerated>Default removeAll</pdGenerated>
        public void RemoveAllFavouriteDoctors()
        {
            if (favouriteDoctors != null)
                favouriteDoctors.Clear();
        }

    }
}