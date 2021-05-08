// File:    MedicationPrescriptionNotification.cs
// Author:  Lana
// Created: 27 May 2020 20:46:49
// Purpose: Definition of Class MedicationPrescriptionNotification

using System;

namespace Model.Notifications
{
    public class MedicationPrescriptionNotification : Notification
    {
        public Model.Medication.MedicationPrescription medicationPrescription;

    }
}