// File:    TimeIntervalCollection.cs
// Author:  Lana
// Created: 02 June 2020 03:26:54
// Purpose: Definition of Class TimeIntervalCollection

using System;
using System.Collections.Generic;

namespace Model.Utilities
{
    public class TimeIntervalCollection
    {
        private IEnumerable<TimeInterval> intervals;

        public TimeIntervalCollection Overlap(TimeIntervalCollection other)
        {
            throw new NotImplementedException();
        }

        public TimeIntervalCollection AddInterval(TimeInterval interval)
        {
            throw new NotImplementedException();
        }

        public TimeIntervalCollection SubtractInterval(TimeInterval interval)
        {
            throw new NotImplementedException();
        }

        public TimeIntervalCollection Filter(TimeSpan minimumLength)
        {
            throw new NotImplementedException();
        }

    }
}