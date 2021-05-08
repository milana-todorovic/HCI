// File:    UserAccount.cs
// Author:  Lana
// Created: 21 April 2020 11:34:09
// Purpose: Definition of Class UserAccount

using System;

namespace Model.Users.UserAccounts
{
    public abstract class UserAccount : Repository.Generics.Entity<int>
    {
        protected String username;
        protected String password;
        protected int id;

        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }

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