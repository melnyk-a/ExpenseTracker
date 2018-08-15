using System;

namespace ExpenseTracker.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    internal sealed class DatabaseValidateRuleAttributeAttribute : ValidationAttribute
    {
        private readonly string dependentDatabaseItemName;
        private readonly string rule;

        public DatabaseValidateRuleAttributeAttribute(string rule, 
            string dependentDatabaseItemName
        )
        {
            this.rule = rule;
            this.dependentDatabaseItemName = dependentDatabaseItemName;
        }

        public string DependentDatabaseItemName => dependentDatabaseItemName;

        public string Rule => rule;
    }
}