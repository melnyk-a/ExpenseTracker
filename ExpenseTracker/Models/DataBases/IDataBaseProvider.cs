using ExpenseTracker.Models.BasicIdentities;
using System;
using System.Collections.Generic;

namespace ExpenseTracker.Models.DataBases
{
    internal interface IDataBaseProvider<T>
    {
        IList<T> Items { get; }

        event EventHandler<DataItemEventArgs<T>> DataBaseChanged;

        void AddItem(T item);
        T Find(string itemName);
        bool Contains(string itemName);
        void DeleteItem(T item);
    }
}