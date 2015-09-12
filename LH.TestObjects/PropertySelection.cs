namespace LH.TestObjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Compare;

    internal class PropertySelection
    {
        public Func<IValueComparison, bool> Predicate { get; set; }

        public Expression PropertyExpression { get; set; }

        public Type PropertyType { get; set; }

        public bool IncludeInheritedTypes { get; set; }

        public bool IsMatch(ValueComparison valueComparison)
        {
            if (valueComparison.PropertyPathItem.IsRoot)
            {
                return false;
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

            // TODO: What to do when PropInfo is null ???
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

            return true;
        }
    }
}