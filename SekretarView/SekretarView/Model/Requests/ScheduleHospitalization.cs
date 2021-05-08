// File:    ScheduleHospitalization.cs
// Author:  Lana
// Created: 27 May 2020 20:27:43
// Purpose: Definition of Class ScheduleHospitalization

using Model.Schedule.SchedulingPreferences;
using System;

namespace Model.Requests
{
    public class ScheduleHospitalization : ScheduleAdjustmentRequest
    {
        private Model.Users.Patient.Patient patient;
        private Model.Schedule.Hospitalizations.HospitalizationType type;
        private HospitalizationSchedulingPreference preference;

    }
}