// File:    Job.cs
// Author:  Gudli
// Created: 20 April 2020 17:39:56
// Purpose: Definition of Class Job

using System;

namespace Model.Users.Patient.EmploymentHistory
{
    public class Job
    {
        private String field;
        private String role;
        private String companyName;

        private System.Collections.ArrayList hazards;

        /// <summary>
        /// Property for collection of WorkplaceHazard
        /// </summary>
        /// <pdGenerated>Default opposite class collection property</pdGenerated>
        public System.Collections.ArrayList Hazards
        {
            get
            {
                if (hazards == null)
                    hazards = new System.Collections.ArrayList();
                return hazards;
            }
            set
            {
                RemoveAllHazards();
                if (value != null)
                {
                    foreach (WorkplaceHazard oWorkplaceHazard in value)
                        AddHazards(oWorkplaceHazard);
                }
            }
        }

        /// <summary>
        /// Add a new WorkplaceHazard in the collection
        /// </summary>
        /// <pdGenerated>Default Add</pdGenerated>
        public void AddHazards(WorkplaceHazard newWorkplaceHazard)
        {
            if (newWorkplaceHazard == null)
                return;
            if (this.hazards == null)
                this.hazards = new System.Collections.ArrayList();
            if (!this.hazards.Contains(newWorkplaceHazard))
                this.hazards.Add(newWorkplaceHazard);
        }

        /// <summary>
        /// Remove an existing WorkplaceHazard from the collection
        /// </summary>
        /// <pdGenerated>Default Remove</pdGenerated>
        public void RemoveHazards(WorkplaceHazard oldWorkplaceHazard)
        {
            if (oldWorkplaceHazard == null)
                return;
            if (this.hazards != null)
                if (this.hazards.Contains(oldWorkplaceHazard))
                    this.hazards.Remove(oldWorkplaceHazard);
        }

        /// <summary>
        /// Remove all instances of WorkplaceHazard from the collection
        /// </summary>
        /// <pdGenerated>Default removeAll</pdGenerated>
        public void RemoveAllHazards()
        {
            if (hazards != null)
                hazards.Clear();
        }

    }
}