// File:    Patient.cs
// Author:  Lana
// Created: 13 April 2020 18:23:49
// Purpose: Definition of Class Patient

using Model.Users.Generalities;
using System;
using System.Collections.Generic;

namespace Model.Users.Patient
{
    public class Patient : Model.Users.Generalities.Person, Repository.Generics.Entity<int>
    {
        private String insuranceNumber;
        private String middleName;
        private MaritalStatus martialStatus;
        private int medicalRecordID;
        private PatientStatus status;
        private Boolean registered;

        private Model.Users.Generalities.City cityOfBirth;

        public string InsuranceNumber { get => insuranceNumber; set => insuranceNumber = value; }
        public string MiddleName { get => middleName; set => middleName = value; }
        public MaritalStatus MartialStatus { get => martialStatus; set => martialStatus = value; }
        public int MedicalRecordID { get => medicalRecordID; set => medicalRecordID = value; }
        public PatientStatus Status { get => status; set => status = value; }
        public City CityOfBirth { get => cityOfBirth; set => cityOfBirth = value; }
        public Boolean Registered { get => registered; set => registered = value; }

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