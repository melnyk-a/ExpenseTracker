using System;

namespace ExpenseTracker.Rules
{
    public abstract class Rule
    {
        private readonly string shortName;
        private readonly Type type;

        public Rule()
        {
            type = GetType();
            shortName = type.Name;
            if (type.IsGenericType)
            {
                int index = shortName.IndexOf('`');
                shortName = shortName.Remove(index);
            }
        }

        public string ShortName => shortName;
    }
}