// File:    ClearDoctorsSchedule.cs
// Author:  Lana
// Created: 27 May 2020 20:22:22
// Purpose: Definition of Class ClearDoctorsSchedule

using System;

namespace Model.Requests
{
    public class ClearDoctorsSchedule : ScheduleAdjustmentRequest
    {
        private Model.Utilities.TimeInterval timeInterval;

        private Model.Users.Employee.Doctor doctor;

    }
}