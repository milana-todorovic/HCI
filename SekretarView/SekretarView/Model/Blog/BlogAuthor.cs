// File:    BlogAuthor.cs
// Author:  Gudli
// Created: 20 April 2020 11:54:01
// Purpose: Definition of Class BlogAuthor

using Model.Users.Employee;
using System;

namespace Model.Blog
{
    public class BlogAuthor : Repository.Generics.Entity<int>
    {
        private string description;
        private int id;

        private Model.Users.Employee.Doctor doctor;

        public string Description { get => description; set => description = value; }
        public Doctor Doctor { get => doctor; set => doctor = value; }

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