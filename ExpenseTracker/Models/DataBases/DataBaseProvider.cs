using ExpenseTracker.Models.BasicIdentities;
using ExpenseTracker.Models.Databases;
using System;
using System.Collections.Generic;

namespace ExpenseTracker.Models
{
    internal class DatabaseProvider<T> : 
        IDatabaseProvider<T> 
        where T : BasicIdentity
    {
        private readonly IList<T> items = new List<T>();

        public IList<T> Items => items;

        public event EventHandler<DataItemEventArgs<T>> DatabaseChanged;

        public void AddItem(T item)
        {
            items.Add(item);
            item.IdentityChanged += new EventHandler<IdentityPropertyChangedEventArgs>(OnItemChanged);
            OnDatabaseChange(new DataItemEventArgs<T>(item, ChangedAction.Add));
        }

        public bool Contains(string itemName)
        {
            bool isContain = false;

            foreach (T item in items)
            {
                if (item.Name.Equals(itemName))
                {
                    isContain = true;
                    break;
                }
            }

            return isContain;
        }

        public void DeleteItem(T item)
        {
            foreach (T currentItem in items)
            {
                if (currentItem.Name.Equals(item.Name))
                {
                    if (items.Remove(currentItem))
                    {
                        OnDatabaseChange(new DataItemEventArgs<T>(currentItem, ChangedAction.Remove));
                        item.IdentityChanged -= new EventHandler<IdentityPropertyChangedEventArgs>(OnItemChanged);
                    }
                    break;
                }
            }
        }

        public T Find(string itemName)
        {
            T foundAccount = null;

            foreach (T account in items)
            {
                if (account.Name.Equals(itemName))
                {
                    foundAccount = account;
                    break;
                }
            }

            return foundAccount;
        }

        private void OnDatabaseChange(DataItemEventArgs<T> e)
        {
            DatabaseChanged?.Invoke(this, e);
        }

        private void OnItemChanged(object sender, IdentityPropertyChangedEventArgs e)
        {
            OnDatabaseChange(new DataItemEventArgs<T>((T)sender, ChangedAction.Update));
        }
    }
}