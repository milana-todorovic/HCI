// File:    PatientSurveyResponse.cs
// Author:  Lana
// Created: 21 April 2020 18:23:22
// Purpose: Definition of Class PatientSurveyResponse

using System;

namespace Model.Users.UserFeedback
{
    public class PatientSurveyResponse : Repository.Generics.Entity<int>
    {
        private int experienceRating;
        private int id;

        private Model.Users.Employee.Doctor bestDoctor;
        private Model.Users.UserAccounts.PatientAccount patientAccount;

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