namespace LH.TestObjects.Tests.Compare
{
    using System.Linq;
    using Domain;
    using FluentAssertions;
    using NUnit.Framework;
    using TestObjects.Compare;

    [TestFixture]
    public class WhenUsingCustomComparator : ComparatorTestsBase
    {
        [Test]
        public void ThenShouldPassWhenComparisonAlwaysReturnsTrueAndObjectsAreDifferent()
        {
            var objA = SimpleDomain.CreateObjectWithValueSet1();
            var objB = SimpleDomain.CreateObjectWithValueSet1();

            objA.StringProp = "AAA";
            objB.StringProp = "BBB";

            this.Comparator
                .Property(x => x.StringProp)
                .CustomCompare(x => true);

            var result = this.Comparator.Compare(objA, objB);

            result.AreSame.Should().BeTrue();
        }

        [Test]
        public void ThenShouldFailWhenCustomComparatorAlwaysReturnsFalse()
        {
            var objA = SimpleDomain.CreateObjectWithValueSet1();
            var objB = SimpleDomain.CreateObjectWithValueSet1();

            objA.StringProp = "AAA".EnsureUniqueInstance();
            objB.StringProp = "AAA".EnsureUniqueInstance();

            this.Comparator
                .Property(x => x.StringProp)
                .CustomCompare(x => false);

            var result = this.Comparator.Compare(objA, objB);

            result.AreSame.Should().BeFalse();
            result.Differences.Should().NotBeNull();
            result.Differences.Count().Should().Be(1);
            result.Differences.Single().PropertyPath.Should().Be("StringProp");
        }

        [Test]
        public void ThenShouldFailWhenUserCallsNestedObjectComparisonAndValuesDiffer()
        {
            var objA = ComplexDomain.CreateObjectWithValueSet1();
            var objB = ComplexDomain.CreateObjectWithValueSet1();

            objA.Simple.StringProp = "AAA";
            objB.Simple.StringProp = "BBB";

            var comparator = new ObjectComparator<ComplexDomain>();
            comparator
                .Property(x => x.Simple)
                .CustomCompare(x => x.CompareItem(x.ExpectedValue.StringProp, x.ActualValue.StringProp, "StringProp"));

            var result = comparator.Compare(objA, objB);
            result.AreSame.Should().BeFalse();
            result.Differences.Count().Should().Be(2);
            result.Differences.Single(x => x.PropertyName == "Simple").PropertyPath.Should().Be("Simple");
            result.Differences.Single(x => x.PropertyName == "StringProp").PropertyPath.Should().Be("Simple.StringProp");
        }

        [Test]
        public void ThenShouldReturnCustomMessageWhenCustomLogicSetsIt()
        {
            const string customMessage = "MyMessage";

            var objA = SimpleDomain.CreateObjectWithValueSet1();
            var objB = SimpleDomain.CreateObjectWithValueSet1();

            objA.StringProp = "AAA".EnsureUniqueInstance();
            objB.StringProp = "AAA".EnsureUniqueInstance();

            this.Comparator
                .Property(x => x.StringProp)
                .CustomCompare(x =>
                {
                    x.Message = customMessage;
                    return false;
                });

            var result = this.Comparator.Compare(objA, objB);

            result.AreSame.Should().BeFalse();
            result.Differences.Single().Message.Should().Be(customMessage);
        }
    }
}