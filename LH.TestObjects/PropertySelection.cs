namespace LH.TestObjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Compare;

    internal class PropertySelection
    {
        public bool AppliesToRoot { get; set; }

        public Func<IValueComparison, bool> Predicate { get; set; }

        public Expression PropertyExpression { get; set; }

        public Type PropertyType { get; set; }

        public bool IncludeInheritedTypes { get; set; }

        public Type DeclarationType { get; set; }

        public bool IsMatch(ValueComparison valueComparison)
        {
            if (valueComparison.PropertyPathItem.IsRoot)
            {
                return this.AppliesToRoot;
            }

            if (this.PropertyType != null)
            {
                var propType = valueComparison.PropertyType;
                if (this.IncludeInheritedTypes 
                    && !propType.IsSubclassOf(this.PropertyType) 
                    && propType != this.PropertyType)
                {
                    return false;
                }

                if (!this.IncludeInheritedTypes && propType != this.PropertyType)
                {
                    return false;
                }
            }

            if (this.DeclarationType != null && valueComparison.PropertyInfo != null)
            {
                if (valueComparison.PropertyInfo.DeclaringType != this.DeclarationType)
                {
                    return false;
                }
            }
            
            if (this.Predicate != null && !this.Predicate.Invoke(valueComparison))
            {
                return false;
            }

            if (this.PropertyExpression != null)
            {
                var propertyNames = this.PropertyExpression.GetPropertyNames();
                if (!this.IsPropertyPathMatch(valueComparison.PropertyPathItem, propertyNames))
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsPropertyPathMatch(PropertyPathItem propertyPath, IEnumerable<string> propertyNames)
        {
            foreach (var propertyName in propertyNames)
            {
                if (propertyPath.IsRoot || propertyPath.PropertyInfo == null || propertyPath.PropertyInfo.Name != propertyName)
                {
                    return false;
                }

                propertyPath = propertyPath.ParentProperty;
            }

            if (!propertyPath.IsRoot)
            {
                return false;
            }

            return true;
        }
    }
}