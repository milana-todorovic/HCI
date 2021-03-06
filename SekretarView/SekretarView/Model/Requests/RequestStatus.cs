// File:    RequestStatus.cs
// Author:  Lana
// Created: 27 May 2020 20:17:42
// Purpose: Definition of Enum RequestStatus

using System;

namespace Model.Requests
{
    public enum RequestStatus
    {
        pending,
        approved,
        rejected,
        expired
    }
}