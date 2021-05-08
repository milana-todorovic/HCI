// File:    EmployeeAccount.cs
// Author:  Lana
// Created: 21 April 2020 11:34:47
// Purpose: Definition of Class EmployeeAccount

using System;

namespace Model.Users.UserAccounts
{
    public class EmployeeAccount : UserAccount
    {
        private Model.Users.Employee.Employee employee;

        private EmployeeType employeeType;

        public Employee.Employee Employee { get => employee; set => employee = value; }
        public EmployeeType EmployeeType { get => employeeType; set => employeeType = value; }
    }
}