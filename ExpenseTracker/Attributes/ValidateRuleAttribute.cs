using System;

namespace ExpenseTracker.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    internal sealed class ValidateRuleAttribute : ValidationAttribute
    {
        private readonly string rule;

        public ValidateRuleAttribute(string rule)
        {
            this.rule = rule;
        }

        public string DependentDataBaseItemName { get; set; }

        public string Rule => rule;
    }
}