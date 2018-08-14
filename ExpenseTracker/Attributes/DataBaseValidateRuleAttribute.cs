using System;

namespace ExpenseTracker.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    internal sealed class DataBaseValidateRuleAttribute : ValidationAttribute
    {
        private readonly string dependentDataBaseItemName;
        private readonly string rule;

        public DataBaseValidateRuleAttribute(string rule, 
            string dependentDataBaseItemName
        )
        {
            this.rule = rule;
            this.dependentDataBaseItemName = dependentDataBaseItemName;
        }

        public string DependentDataBaseItemName => dependentDataBaseItemName;

        public string Rule => rule;
    }
}