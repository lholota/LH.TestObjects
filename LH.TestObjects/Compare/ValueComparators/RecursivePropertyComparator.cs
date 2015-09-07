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
            var properties = comparison.PropertyPathItem.Type
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
