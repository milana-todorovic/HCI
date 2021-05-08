// File:    Employee.cs
// Author:  Lana
// Created: 18 April 2020 19:42:16
// Purpose: Definition of Class Employee

using System;

namespace Model.Users.Employee
{
    public class Employee : Model.Users.Generalities.Person, Repository.Generics.Entity<int>
    {
        protected int employeeID;
        protected EmployeeStatus status;

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