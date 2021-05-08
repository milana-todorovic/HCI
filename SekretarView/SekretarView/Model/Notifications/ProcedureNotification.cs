// File:    ProcedureNotification.cs
// Author:  Lana
// Created: 27 May 2020 20:47:30
// Purpose: Definition of Class ProcedureNotification

using System;

namespace Model.Notifications
{
    public class ProcedureNotification : Notification
    {
        public Model.Schedule.Procedures.Procedure procedure;

    }
}