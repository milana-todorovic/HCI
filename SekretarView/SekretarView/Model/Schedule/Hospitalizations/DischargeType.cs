// File:    DischargeType.cs
// Author:  Lana
// Created: 20 April 2020 23:27:02
// Purpose: Definition of Enum DischargeType

using System;

namespace Model.Schedule.Hospitalizations
{
    public enum DischargeType
    {
        toOtherFacility,
        toResidence,
        death,
        onPersonalRequest,
        statistical
    }
}