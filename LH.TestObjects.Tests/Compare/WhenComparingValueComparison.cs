namespace LH.TestObjects.Tests.Compare
{
    using FluentAssertions;
    using NUnit.Framework;
    using TestObjects.Compare;

    [TestFixture]
    public class WhenComparingValueComparison
    {
        private static readonly TestObjects.Compare.PropertyPathItem propPath = TestObjects.Compare.PropertyPathItem.Root;

        [Test]
        public void ThenShouldReturnTrueIfValuesAreRefEqual()
        {
            var actual = "AAA";
            var expected = "BBB";

            var comparisonA = new ValueComparison(propPath, expected, actual);
            var comparisonB = new ValueComparison(propPath, expected, actual);

            comparisonB.Equals(comparisonA).Should().BeTrue();
        }

        [Test]
        public void ThenShouldReturnFalseIfValuesAreNotRefEqual()
        {
            var actual = "AAA";
            var expected = "BBB";

            var comparisonA = new ValueComparison(propPath, expected.EnsureUniqueInstance(), actual);
            var comparisonB = new ValueComparison(propPath, expected.EnsureUniqueInstance(), actual);

            comparisonB.Equals(comparisonA).Should().BeFalse();
        }
    }
}