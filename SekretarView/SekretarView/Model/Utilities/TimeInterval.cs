// File:    TimeInterval.cs
// Author:  Lana
// Created: 20 April 2020 23:38:16
// Purpose: Definition of Class TimeInterval

using System;

namespace Model.Utilities
{
    public class TimeInterval
    {
        private DateTime start;
        private DateTime end;

        public DateTime Start { get => start; set => start = value; }
        public DateTime End { get => end; set => end = value; }
    }
}