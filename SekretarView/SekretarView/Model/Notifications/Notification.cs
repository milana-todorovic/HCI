// File:    Notification.cs
// Author:  Lana
// Created: 27 May 2020 20:38:16
// Purpose: Definition of Class Notification

using System;

namespace Model.Notifications
{
    public abstract class Notification : Repository.Generics.Entity<int>
    {
        private int id;
        private Boolean read;

        private Model.Users.UserAccounts.UserAccount user;

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