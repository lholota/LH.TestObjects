namespace LH.TestObjects.Tests.Compare
{
    using System.Linq;
    using Domain;
    using FluentAssertions;
    using NUnit.Framework;

    [TestFixture]
    public class WhenUsingCustomComparator : ComparatorTestsBase
    {
        private SimpleDomain objA;
        private SimpleDomain objB;

        public override void Setup()
        {
            base.Setup();

            this.objA = SimpleDomain.CreateObjectWithValueSet1();
            this.objB = SimpleDomain.CreateObjectWithValueSet1();
        }

        [Test]
        public void ThenShouldPassWhenComparisonAlwaysReturnsTrueAndObjectsAreDifferent()
        {
            this.objA.StringProp = "AAA";
            this.objB.StringProp = "BBB";

            this.Comparator
                .Property(x => x.StringProp)
                .CustomCompare(x => true);

            var result = this.Comparator.Compare(this.objA, this.objB);

            result.AreSame.Should().BeTrue();
        }

        [Test]
        public void ThenShouldFailWhenCustomComparatorAlwaysReturnsFalse()
        {
            this.objA.StringProp = "AAA".EnsureUniqueInstance();
            this.objB.StringProp = "AAA".EnsureUniqueInstance();

            this.Comparator
                .Property(x => x.StringProp)
                .CustomCompare(x => false);

            var result = this.Comparator.Compare(this.objA, this.objB);

            result.AreSame.Should().BeFalse();
            result.Differences.Should().NotBeNull();
            result.Differences.Count().Should().Be(1);
            result.Differences.Single().PropertyPath.Should().Be("StringProp");
        }

        [Test]
        public void ThenShouldFailWhenUserCallsNestedObjectComparisonAndValuesDiffer()
        {
            var complexA = ComplexDomain.CreateObjectWithValueSet1();
            var complexB = ComplexDomain.CreateObjectWithValueSet1();

            complexA.Simple.StringProp = "AAA";
            complexB.Simple.StringProp = "BBB";

            var comparator = Extensions.CreateComparator<ComplexDomain>();
            comparator
                .Property(x => x.Simple)
                .CustomCompare(x => x.CompareItem(x.ExpectedValue.StringProp, x.ActualValue.StringProp, "StringProp"));

            var result = comparator.Compare(complexA, complexB);
            result.AreSame.Should().BeFalse();
            result.Differences.Count().Should().Be(2);
            result.Differences.Single(x => x.PropertyName == "Simple").PropertyPath.Should().Be("Simple");
            result.Differences.Single(x => x.PropertyName == "StringProp").PropertyPath.Should().Be("Simple.StringProp");
        }

        [Test]
        public void ThenShouldReturnCustomMessageWhenCustomLogicSetsIt()
        {
            const string customMessage = "MyMessage";

            this.objA.StringProp = "AAA".EnsureUniqueInstance();
            this.objB.StringProp = "AAA".EnsureUniqueInstance();

            this.Comparator
                .Property(x => x.StringProp)
                .CustomCompare(x =>
                {
                    x.Message = customMessage;
                    return false;
                });

            var result = this.Comparator.Compare(this.objA, this.objB);

            result.AreSame.Should().BeFalse();
            result.Differences.Single().Message.Should().Be(customMessage);
        }
    }
}