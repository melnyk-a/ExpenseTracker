using System;

namespace ExpenseTracker.Attributes
{
    internal abstract class ValidationAttribute : Attribute
    {
        public string Name { get; set; }
    }
}