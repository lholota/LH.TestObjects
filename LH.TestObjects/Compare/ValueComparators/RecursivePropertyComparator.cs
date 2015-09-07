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

        public void Compare(ComparisonContext context, ValueComparison comparison)
        {
            if (comparison.Type == null)
            {
                throw new ArgumentException("Could not determine type to reflect.", "comparison");
            }

            var properties = comparison.Type
               .GetProperties(BindingFlags.Instance | BindingFlags.Public);

            foreach (var property in properties)
            {
                var propertyPathItem = new PropertyPathItem(property, comparison.PropertyPathItem);

                var expectedPropValue = property.GetValue(comparison.ExpectedValue, null);
                var actualPropValue = property.GetValue(comparison.ActualValue, null);

                context.CompareItem(expectedPropValue, actualPropValue, propertyPathItem);
            }
        }
    }
}
