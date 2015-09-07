namespace LH.TestObjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using Compare;

    internal class PropertySelection
    {
        public Func<PropertyInfo, bool> Predicate { get; set; }

        public Expression PropertyExpression { get; set; }

        public Type PropertyType { get; set; }

        public bool IncludeInheritedTypes { get; set; }

        public bool IsMatch(PropertyPathItem propertyPath)
        {
            if (propertyPath.IsRoot)
            {
                return false;
            }

            if (this.PropertyType != null)
            {
                var propType = propertyPath.PropertyInfo.PropertyType;
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

            if (this.Predicate != null && !this.Predicate.Invoke(propertyPath.PropertyInfo))
            {
                return false;
            }

            if (this.PropertyExpression != null)
            {
                var propertyNames = this.PropertyExpression.GetPropertyNames();
                if (!this.IsPropertyPathMatch(propertyPath, propertyNames))
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
                if (propertyPath.IsRoot || propertyPath.PropertyInfo.Name != propertyName)
                {
                    return false;
                }

                propertyPath = propertyPath.ParentProperty;
            }

            return true;
        }
    }
}