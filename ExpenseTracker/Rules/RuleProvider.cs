using ExpenseTracker.Rules.RuleFactories;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace ExpenseTracker.Rules
{
    internal sealed class RuleProvider : IRuleProvider
    {
        private readonly IDictionary<string, Rule> nameToRule = new Dictionary<string, Rule>();
        private readonly IRuleFactory ruleFactory;

        public RuleProvider(IRuleFactory ruleFactory)
        {
            this.ruleFactory = ruleFactory;

            FillNameToRule();
        }

        private void FillNameToRule()
        {
            Type factoryType = ruleFactory.GetType();

            MethodInfo[] methods = factoryType.GetMethods();
            foreach (MethodInfo method in methods)
            {
                if (method.ReturnType == typeof(Rule))
                {
                    string ruleName = method.Name.Replace("Create", "");
                    nameToRule.Add(ruleName, (Rule)method.Invoke(ruleFactory, null));
                }
            }
        }

        public Rule ProvideRule(string ruleName)
        {
            nameToRule.TryGetValue(ruleName, out Rule rule);
            return rule;
        }
    }
}