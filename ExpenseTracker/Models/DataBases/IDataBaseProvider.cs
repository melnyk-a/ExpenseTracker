using ExpenseTracker.Models.BasicIdentities;
using System;
using System.Collections.Generic;

namespace ExpenseTracker.Models.Databases
{
    internal interface IDatabaseProvider<T>
    {
        IList<T> Items { get; }

        event EventHandler<DataItemEventArgs<T>> DatabaseChanged;

        void AddItem(T item);
        T Find(string itemName);
        bool Contains(string itemName);
        void DeleteItem(T item);
    }
}