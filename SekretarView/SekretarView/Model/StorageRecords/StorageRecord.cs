// File:    StorageRecord.cs
// Author:  Lana
// Created: 18 April 2020 18:19:45
// Purpose: Definition of Class StorageRecord

using System;

namespace Model.StorageRecords
{
    public abstract class StorageRecord : Repository.Generics.Entity<int>
    {
        protected int availableAmount;
        protected int id;

        protected System.Collections.ArrayList supplyHistory;

        /// <summary>
        /// Property for collection of AmountChangeRecord
        /// </summary>
        /// <pdGenerated>Default opposite class collection property</pdGenerated>
        public System.Collections.ArrayList SupplyHistory
        {
            get
            {
                if (supplyHistory == null)
                    supplyHistory = new System.Collections.ArrayList();
                return supplyHistory;
            }
            set
            {
                RemoveAllSupplyHistory();
                if (value != null)
                {
                    foreach (AmountChangeRecord oAmountChangeRecord in value)
                        AddSupplyHistory(oAmountChangeRecord);
                }
            }
        }

        /// <summary>
        /// Add a new AmountChangeRecord in the collection
        /// </summary>
        /// <pdGenerated>Default Add</pdGenerated>
        public void AddSupplyHistory(AmountChangeRecord newAmountChangeRecord)
        {
            if (newAmountChangeRecord == null)
                return;
            if (this.supplyHistory == null)
                this.supplyHistory = new System.Collections.ArrayList();
            if (!this.supplyHistory.Contains(newAmountChangeRecord))
                this.supplyHistory.Add(newAmountChangeRecord);
        }

        /// <summary>
        /// Remove an existing AmountChangeRecord from the collection
        /// </summary>
        /// <pdGenerated>Default Remove</pdGenerated>
        public void RemoveSupplyHistory(AmountChangeRecord oldAmountChangeRecord)
        {
            if (oldAmountChangeRecord == null)
                return;
            if (this.supplyHistory != null)
                if (this.supplyHistory.Contains(oldAmountChangeRecord))
                    this.supplyHistory.Remove(oldAmountChangeRecord);
        }

        /// <summary>
        /// Remove all instances of AmountChangeRecord from the collection
        /// </summary>
        /// <pdGenerated>Default removeAll</pdGenerated>
        public void RemoveAllSupplyHistory()
        {
            if (supplyHistory != null)
                supplyHistory.Clear();
        }
        protected System.Collections.ArrayList usageHistory;

        /// <summary>
        /// Property for collection of AmountChangeRecord
        /// </summary>
        /// <pdGenerated>Default opposite class collection property</pdGenerated>
        public System.Collections.ArrayList UsageHistory
        {
            get
            {
                if (usageHistory == null)
                    usageHistory = new System.Collections.ArrayList();
                return usageHistory;
            }
            set
            {
                RemoveAllUsageHistory();
                if (value != null)
                {
                    foreach (AmountChangeRecord oAmountChangeRecord in value)
                        AddUsageHistory(oAmountChangeRecord);
                }
            }
        }

        /// <summary>
        /// Add a new AmountChangeRecord in the collection
        /// </summary>
        /// <pdGenerated>Default Add</pdGenerated>
        public void AddUsageHistory(AmountChangeRecord newAmountChangeRecord)
        {
            if (newAmountChangeRecord == null)
                return;
            if (this.usageHistory == null)
                this.usageHistory = new System.Collections.ArrayList();
            if (!this.usageHistory.Contains(newAmountChangeRecord))
                this.usageHistory.Add(newAmountChangeRecord);
        }

        /// <summary>
        /// Remove an existing AmountChangeRecord from the collection
        /// </summary>
        /// <pdGenerated>Default Remove</pdGenerated>
        public void RemoveUsageHistory(AmountChangeRecord oldAmountChangeRecord)
        {
            if (oldAmountChangeRecord == null)
                return;
            if (this.usageHistory != null)
                if (this.usageHistory.Contains(oldAmountChangeRecord))
                    this.usageHistory.Remove(oldAmountChangeRecord);
        }

        /// <summary>
        /// Remove all instances of AmountChangeRecord from the collection
        /// </summary>
        /// <pdGenerated>Default removeAll</pdGenerated>
        public void RemoveAllUsageHistory()
        {
            if (usageHistory != null)
                usageHistory.Clear();
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