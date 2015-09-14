namespace LH.TestObjects.Compare.ValueComparators.KnownTypes
{
    using System;
    using System.Linq;

    internal class MultidimensionArrayValueComparator : IValueComparator
    {
        public bool CanHandle(Type type)
        {
            return type.IsArray && type.GetArrayRank() > 1;
        }

        public bool Compare(ComparisonContext context, ValueComparison comparison)
        {
            var maxDimentions = comparison.PropertyType.GetArrayRank();
            var currentIndices = new int[maxDimentions];

            if (!this.CompareArrayDimensionLengths(comparison, maxDimentions))
            {
                context.AddDifference(comparison);
                return false;
            }

            return this.CompareArrayItems(context, comparison, 0, currentIndices);
        }

        private bool CompareArrayItems(ComparisonContext context, ValueComparison comparison, int currentDimension, int[] currentIndices)
        {
            var actual = (Array)comparison.ActualValue;
            var expected = (Array)comparison.ExpectedValue;

            var length = expected.GetLength(currentDimension);

            var areEqual = true;
            for (var i = 0; i < length; i++)
            {
                currentIndices[currentDimension] = i;
                if (currentDimension == currentIndices.Length - 1)
                {
                    // Last dimension, get value and compare
                    var actualValue = actual.GetValue(currentIndices);
                    var expectedValue = expected.GetValue(currentIndices);

                    var indicesString = this.GetArrayIndicesString(currentIndices);
                    var propertyPath = new PropertyPathItem(indicesString, comparison.PropertyPathItem);
                    if (!context.CompareItem(expectedValue, actualValue, propertyPath))
                    {
                        areEqual = false;
                    }
                }
                else
                {
                    if (!this.CompareArrayItems(context, comparison, currentDimension + 1, currentIndices))
                    {
                        areEqual = false;
                    }
                }
            }

            return areEqual;
        }

        private bool CompareArrayDimensionLengths(ValueComparison comparison, int maxDimensions)
        {
            var actual = (Array)comparison.ActualValue;
            var expected = (Array)comparison.ExpectedValue;

            for (var i = 0; i < maxDimensions; i++)
            {
                var actualLength = actual.GetLength(i);
                var expectedLength = expected.GetLength(i);

                if (actualLength != expectedLength)
                {
                    comparison.Message = string.Format("The arrays have different lengths at the dimension {0}", i);
                    return false;
                }
            }

            return true;
        }

        private string GetArrayIndicesString(int[] indices)
        {
            return string.Join(", ", indices.Select(x => x.ToString()));
        }
    }
}
