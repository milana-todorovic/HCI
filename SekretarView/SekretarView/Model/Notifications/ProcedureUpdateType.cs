// File:    ProcedureUpdateType.cs
// Author:  Lana
// Created: 27 May 2020 20:49:55
// Purpose: Definition of Enum ProcedureUpdateType

using System;

namespace Model.Notifications
{
    public enum ProcedureUpdateType
    {
        scheduled,
        rescheduled,
        cancelled
    }
}