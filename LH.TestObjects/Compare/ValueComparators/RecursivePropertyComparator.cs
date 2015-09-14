namespace LH.TestObjects.Compare.ValueComparators
{
    using System;
    using System.Reflection;

    internal class RecursivePropertyComparator : IValueComparator
    {
        public bool CanHandle(Type type)
        {
            return true;
        }

        public bool Compare(ComparisonContext context, ValueComparison comparison)
        {
            if (comparison.PropertyType == null)
            {
                throw new ArgumentException("Could not determine type to reflect.", "comparison");
            }

            var areEqual = true;
            var properties = comparison.PropertyType
               .GetProperties(BindingFlags.Instance | BindingFlags.Public);

            foreach (var property in properties)
            {
                var propertyPathItem = new PropertyPathItem(property, comparison.PropertyPathItem);

                var expectedPropValue = property.GetValue(comparison.ExpectedValue, null);
                var actualPropValue = property.GetValue(comparison.ActualValue, null);

                if (!context.CompareItem(expectedPropValue, actualPropValue, propertyPathItem))
                {
                    areEqual = false;
                }
            }

            return areEqual;
        }
    }
}
