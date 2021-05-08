// File:    HospitalizationNotification.cs
// Author:  Lana
// Created: 27 May 2020 20:47:57
// Purpose: Definition of Class HospitalizationNotification

using System;

namespace Model.Notifications
{
    public class HospitalizationNotification : Notification
    {
        public Model.Schedule.Hospitalizations.Hospitalization hospitalization;

    }
}