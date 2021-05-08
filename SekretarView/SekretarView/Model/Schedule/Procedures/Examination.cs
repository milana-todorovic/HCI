// File:    Examination.cs
// Author:  Lana
// Created: 20 April 2020 23:40:27
// Purpose: Definition of Class Examination

using Model.Miscellaneous;
using System;

namespace Model.Schedule.Procedures
{
    public class Examination : Procedure
    {
        private Model.Miscellaneous.Diagnosis diagnosis = null;
        private String anamnesis = null;

        public Diagnosis Diagnosis { get => diagnosis; set => diagnosis = value; }
        public string Anamnesis { get => anamnesis; set => anamnesis = value; }
    }
}