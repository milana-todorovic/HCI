// File:    ProcedurePriority.cs
// Author:  Lana
// Created: 27 May 2020 21:59:05
// Purpose: Definition of Class ProcedurePriority

using System;

namespace Model.Schedule.Procedures
{
    public class ProcedurePriority : Repository.Generics.Entity<int>
    {
        private int id;
        private String name;
        private String description;
        private TimeSpan maximumWaitTime;
        private ProcedureKind applicableFor;
        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public TimeSpan MaximumWaitTime { get => maximumWaitTime; set => maximumWaitTime = value; }
        public ProcedureKind ApplicableFor { get => applicableFor; set => applicableFor = value; }

        public int GetKey()
        {
            throw new NotImplementedException();
        }

        public void SetKey(int id)
        {
            throw new NotImplementedException();
        }
    }
}