using ExpenseTracker.Attributes;
using ExpenseTracker.Models.BasicIdentities;
using ExpenseTracker.Rules;
using ExpenseTracker.Rules.DatabaseRules;
using ExpenseTracker.Rules.SimpleRules;
using ExpenseTracker.ValidationRules;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace ExpenseTracker.ViewModels
{
    internal abstract class NotifyDataErrorViewModel<T> : NotifyPropertyChangedViewModel, INotifyDataErrorInfo where T : BasicIdentity
    {
        private readonly IDictionary<string, string> propertyToErrors = new Dictionary<string, string>();
        private readonly IDictionary<string, ICollection<Rule>> propertyToRule = new Dictionary<string, ICollection<Rule>>();
        private readonly IRuleProvider ruleProvider;

        public NotifyDataErrorViewModel(IRuleProvider ruleProvider)
        {
            this.ruleProvider = ruleProvider;
            FillPropertyToRule();
        }

        public bool HasErrors => propertyToErrors.Count > 0;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        protected void AddError(string propertyName, string error)
        {
            if (propertyToErrors.ContainsKey(propertyName))
            {
                propertyToErrors[propertyName] = error;
            }
            else
            {
                propertyToErrors.Add(propertyName, error);
            }
            OnErrorsChanged(new DataErrorsChangedEventArgs(propertyName));
        }

        protected void ClearError(string propertyName)
        {
            if (propertyToErrors.Remove(propertyName))
            {
                OnErrorsChanged(new DataErrorsChangedEventArgs(propertyName));
            }
        }

        private void FillPropertyToRule()
        {
            Type currentType = GetType();
            PropertyInfo[] properties = currentType.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                IEnumerable<ValidateRuleAttribute> attributes = property.GetCustomAttributes<ValidateRuleAttribute>();
                ICollection<Rule> rules = new List<Rule>();
                foreach (ValidateRuleAttribute attribute in attributes)
                {
                    rules.Add(ruleProvider.ProvideRule(attribute.Rule));
                }
                propertyToRule.Add(property.Name, rules);
            }
        }

        public IEnumerable GetErrors(string propertyName)
        {
            IEnumerable result;

            if (string.IsNullOrEmpty(propertyName))
            {
                result = propertyToErrors.Values;
            }
            else
            {
                if (propertyToErrors.ContainsKey(propertyName))
                {
                    result = new[] { propertyToErrors[propertyName] };
                }
                else
                {
                    result = new string[0];
                }
            }

            return result;
        }

        protected override void HandlePropertiesAttributes(PropertyInfo appliedProperty)
        {
            base.HandlePropertiesAttributes(appliedProperty);

            IEnumerable<ValidateRuleAttribute> attributes = appliedProperty.GetCustomAttributes<ValidateRuleAttribute>();
            PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == appliedProperty.Name)
                {
                    ValidateAttributeProperty(appliedProperty, attributes);
                }
            };
        }

        private void OnErrorsChanged(DataErrorsChangedEventArgs e)
        {
            ErrorsChanged?.Invoke(this, e);
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(HasErrors)));
        }

        protected void Validate()
        {
            Type currentType = GetType();
            PropertyInfo[] properties = currentType.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                IEnumerable<ValidateRuleAttribute> attributes = property.GetCustomAttributes<ValidateRuleAttribute>();
                ValidateAttributeProperty(property, attributes);
            }
        }

        private void ValidateAttributeProperty(PropertyInfo appliedProperty, IEnumerable<ValidateRuleAttribute> attributes)
        {
            foreach (ValidateRuleAttribute attribute in attributes)
            {
                foreach (Rule rule in propertyToRule[appliedProperty.Name])
                {
                    if (rule.ShortName.Equals(attribute.Rule))
                    {
                        try
                        {
                            object value = appliedProperty.GetValue(this);
                            string stringValue = value?.ToString();
                            if (rule is SimpleRule simpleRule)
                            {
                                simpleRule.Check(stringValue);
                            }
                            else if (rule is DatabaseRule<T> databaseRule && attribute.DependentDatabaseItemName != null)
                            {
                                PropertyInfo property = GetType().GetProperty(attribute.DependentDatabaseItemName);
                                databaseRule.Check(stringValue, (string)property.GetValue(this));
                            }

                            var testString = rule is DatabaseRule<T>;
                            ClearError(appliedProperty.Name);
                        }
                        catch (ValidationErrorExeption e)
                        {
                            string error = attribute.Name != null ? $"{attribute.Name} {e.Error}" : e.Error;
                            AddError(appliedProperty.Name, error);
                            break;
                        }
                    }
                }
                if (HasErrors)
                {
                    break;
                }
            }
        }
    }
}