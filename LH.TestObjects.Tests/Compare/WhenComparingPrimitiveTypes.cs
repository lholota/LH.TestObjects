namespace LH.TestObjects.Tests.Compare
{
    using System.Linq;
    using FluentAssertions;
    using NUnit.Framework;
    using TestObjects.Compare;

    [TestFixture]
    public class WhenComparingPrimitiveTypes
    {
        [Test]
        public void ThenShouldPassWhenComparingTwoEqualStrings()
        {
            var comparator = new ObjectComparator<string>();
            var result = comparator.Compare("AAA", "AAA");

            result.AreSame.Should().BeTrue();
        }

        [Test]
        public void ThenShouldFailWhenComparingTwoDifferentStrings()
        {
            var comparator = new ObjectComparator<string>();
            var result = comparator.Compare("AAA", "BBB");

            result.AreSame.Should().BeFalse();
            result.Differences.Single().PropertyPath.Should().BeNullOrEmpty();
        }
    }
}
