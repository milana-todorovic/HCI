// File:    ClearRoomsSchedule.cs
// Author:  Lana
// Created: 27 May 2020 20:22:22
// Purpose: Definition of Class ClearRoomsSchedule

using System;

namespace Model.Requests
{
    public class ClearRoomsSchedule : ScheduleAdjustmentRequest
    {
        private Model.Utilities.TimeInterval timeInterval;

        private Model.HospitalResources.Room room;

    }
}