// File:    Request.cs
// Author:  Lana
// Created: 27 May 2020 20:17:42
// Purpose: Definition of Class Request

using System;

namespace Model.Requests
{
    public abstract class Request : Repository.Generics.Entity<int>
    {
        protected RequestStatus status;
        protected DateTime creationDate;
        protected DateTime reviewDate;
        protected String reviewerComment;
        protected int id;

        protected Model.Users.UserAccounts.EmployeeAccount sender;

        /// <summary>
        /// Property for Model.Users.UserAccounts.EmployeeAccount
        /// </summary>
        /// <pdGenerated>Default opposite class property</pdGenerated>
        public Model.Users.UserAccounts.EmployeeAccount Sender
        {
            get
            {
                return sender;
            }
            set
            {
                if (this.sender == null || !this.sender.Equals(value))
                {
                    if (this.sender != null)
                    {
                        Model.Users.UserAccounts.EmployeeAccount oldEmployeeAccount = this.sender;
                        this.sender = null;
                    }
                    if (value != null)
                    {
                        this.sender = value;
                    }
                }
            }
        }
        protected Model.Users.UserAccounts.EmployeeAccount reviewer;

        /// <summary>
        /// Property for Model.Users.UserAccounts.EmployeeAccount
        /// </summary>
        /// <pdGenerated>Default opposite class property</pdGenerated>
        public Model.Users.UserAccounts.EmployeeAccount Reviewer
        {
            get
            {
                return reviewer;
            }
            set
            {
                this.reviewer = value;
            }
        }

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