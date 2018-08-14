using System;
using System.Reflection;

namespace ExpenseTracker.ViewModels
{
    internal abstract class ViewModel
    {
        public ViewModel()
        {
            Type currentType = GetType();
            PropertyInfo[] properties = currentType.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                HandlePropertiesAttributes(property);
            }
        }

        protected virtual void HandlePropertiesAttributes(PropertyInfo appliedProperty)
        {
        }
    }
}