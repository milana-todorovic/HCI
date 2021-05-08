// File:    UserFeedback.cs
// Author:  Lana
// Created: 21 April 2020 18:20:24
// Purpose: Definition of Class UserFeedback

using System;

namespace Model.Users.UserFeedback
{
    public class UserFeedback : Repository.Generics.Entity<int>
    {
        private DateTime date;
        private String userComment;
        private int id;

        private Model.Users.UserAccounts.UserAccount userAccount;

        /// <summary>
        /// Property for Model.Users.UserAccounts.UserAccount
        /// </summary>
        /// <pdGenerated>Default opposite class property</pdGenerated>
        public Model.Users.UserAccounts.UserAccount UserAccount
        {
            get
            {
                return userAccount;
            }
            set
            {
                this.userAccount = value;
            }
        }

        public DateTime Date { get => date; set => date = value; }
        public string UserComment { get => userComment; set => userComment = value; }

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