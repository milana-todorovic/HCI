// File:    MedicationInputRequest.cs
// Author:  Lana
// Created: 27 May 2020 20:29:45
// Purpose: Definition of Class MedicationInputRequest

using Model.Users.Employee;
using System;
using System.Collections.Generic;

namespace Model.Requests
{
    public class MedicationInputRequest : Request
    {
        private IEnumerable<Specialty> reviewableBy;

        public Model.Medication.Medication medication;

    }
}