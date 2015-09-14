namespace LH.TestObjects.Tests.Compare.ValueComparators
{
    using System.Linq;
    using Domain;
    using FluentAssertions;
    using NUnit.Framework;

    [TestFixture]
    public class WhenComparingUsingReferenceEquals
    {
        [Test]
        public void ThenShouldPassIfValuesAreTheSameInstance()
        {
            var comparator = Extensions.CreateComparator<ComplexDomain>();
            comparator
                .Property(x => x.Simple)
                .UseReferenceEquals();

            var objA = ComplexDomain.CreateObjectWithValueSet1();
            var objB = ComplexDomain.CreateObjectWithValueSet1();

            objA.Simple = objB.Simple;

            var result = comparator.Compare(objA, objB);
            result.AreSame.Should().BeTrue();
        }

        [Test]
        public void ThenShouldFailIfValuesAreEqualButInstancesDiffer()
        {
            var comparator = Extensions.CreateComparator<ComplexDomain>();
            comparator
                .Property(x => x.Simple)
                .UseReferenceEquals();

            var objA = ComplexDomain.CreateObjectWithValueSet1();
            var objB = ComplexDomain.CreateObjectWithValueSet1();

            var result = comparator.Compare(objA, objB);
            result.AreSame.Should().BeFalse();
            result.Differences.Single().Message.Should().Contain("reference");
        }
    }
}