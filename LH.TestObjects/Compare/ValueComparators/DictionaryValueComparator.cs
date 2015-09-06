namespace LH.TestObjects.Compare.ValueComparators
{
    using System;
    using System.Collections;

    internal class DictionaryValueComparator : IValueComparator
    {
        public bool CanHandle(Type type)
        {
            return typeof(IDictionary).IsAssignableFrom(type);
        }

        public void Compare(IComparisonContext comparisonContext, AddDifferenceDelegate addDifferenceCall)
        {
            var actual = (IDictionary)comparisonContext.ActualValue;
            var expected = (IDictionary)comparisonContext.ExpectedValue;

            if (this.AreKeysEqual(expected, actual))
            {
                throw new NotImplementedException();
            }

            throw new NotImplementedException();
        }

        private bool AreKeysEqual(IDictionary expected, IDictionary actual)
        {
            throw new NotImplementedException();
        }
    }
}
