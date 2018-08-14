using System;

namespace ExpenseTracker.ValidationRules
{
    internal sealed class ValidationErrorExeption : Exception
    {
        private readonly string error;

        public ValidationErrorExeption(string error)
        {
            this.error = error;
        }

        public string Error => error;
    }
}