// File:    ScheduleProcedure.cs
// Author:  Lana
// Created: 27 May 2020 20:27:43
// Purpose: Definition of Class ScheduleProcedure

using Model.Schedule.SchedulingPreferences;
using System;

namespace Model.Requests
{
    public class ScheduleProcedure : ScheduleAdjustmentRequest
    {
        private Model.Users.Patient.Patient patient;
        private Model.Schedule.Procedures.ProcedureType type;
        private ProcedureSchedulingPreference preference;

    }
}