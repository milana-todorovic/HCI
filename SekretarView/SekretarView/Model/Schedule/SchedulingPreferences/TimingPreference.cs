// File:    TimingPreference.cs
// Author:  Lana
// Created: 21 April 2020 17:32:57
// Purpose: Definition of Class TimingPreference

using Model.Utilities;
using System;

namespace Model.Schedule.SchedulingPreferences
{
    public class TimingPreference
    {
        private TimingPrefrenceType type;
        private Model.Utilities.TimeIntervalCollection range;

        public TimingPrefrenceType Type { get => type; set => type = value; }
        public TimeIntervalCollection Range { get => range; set => range = value; }
    }
}