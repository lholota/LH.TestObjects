namespace LH.TestObjects.Tests.Compare
{
    using System.Linq;
    using Domain;
    using FluentAssertions;
    using NUnit.Framework;
    using TestObjects.Compare;

    [TestFixture]
    public class WhenComparingComplexObjects
    {
        private ObjectComparator<ComplexDomain> comparator;
        
        [SetUp]
        public void Setup()
        {
            this.comparator = Extensions.CreateComparator<ComplexDomain>();
        }
        
        [Test]
        public void ThenShouldPassIfValuesMatch()
        {
            var objA = ComplexDomain.CreateObjectWithValueSet1();
            var objB = ComplexDomain.CreateObjectWithValueSet1();

            var result = this.comparator.Compare(objA, objB);
            result.AreSame.Should().BeTrue();
        }

        [Test]
        public void ThenShouldFailIfNestedObjectValueDiffers()
        {
            var objA = ComplexDomain.CreateObjectWithValueSet1();
            var objB = ComplexDomain.CreateObjectWithValueSet1();

            objA.Simple.StringProp = "AAA";
            objB.Simple.StringProp = "BBB";

            var result = this.comparator.Compare(objA, objB);
            result.AreSame.Should().BeFalse();
            result.Differences.Should().NotBeNull();
            result.Differences.Count().Should().Be(1);
        }

        [Test]
        public void ThenShouldFailIfValuesAreOfDifferentTypes()
        {
            var objA = ComplexDomain.CreateObjectWithValueSet1();
            var objB = ComplexDomain.CreateObjectWithValueSet1();

            objB.Simple = new SimpleDomainInheritor
            {
                StringProp = objB.Simple.StringProp,
                StringProp2 = objB.Simple.StringProp2,
                IntProp = objB.Simple.IntProp
            };

            var result = this.comparator.Compare(objA, objB);

            result.AreSame.Should().BeFalse();
            result.Differences.Count().Should().Be(1);
        }

        [Test]
        public void ThenShouldFailIfOneValueIsNull()
        {
            var objA = ComplexDomain.CreateObjectWithValueSet1();
            var objB = ComplexDomain.CreateObjectWithValueSet1();

            objB.Simple = null;

            var result = this.comparator.Compare(objA, objB);
            result.AreSame.Should().BeFalse();
            result.Differences.Count().Should().Be(1);
        }

        [Test]
        public void ThenShouldPassIfBothValuesAreNull()
        {
            var objA = ComplexDomain.CreateObjectWithValueSet1();
            var objB = ComplexDomain.CreateObjectWithValueSet1();

            objA.Simple = null;
            objB.Simple = null;

            var result = this.comparator.Compare(objA, objB);
            result.AreSame.Should().BeTrue();
        }

        [Test]
        public void ThenPropertyPathShouldBeCorrect()
        {
            var objA = ComplexDomain.CreateObjectWithValueSet1();
            var objB = ComplexDomain.CreateObjectWithValueSet1();

            objA.Simple.StringProp = "AAA";
            objB.Simple.StringProp = "BBB";

            var result = this.comparator.Compare(objA, objB);

            result.AreSame.Should().BeFalse();
            result.Differences.Should().NotBeNull();
            result.Differences.Count().Should().Be(1);
            result.Differences.Single().PropertyPath.Should().Be("Simple.StringProp");
        }

        [Test]
        public void ThenShouldNotCompareNestedPropertiesIfInstancesAreReferenceEqual()
        {
            var genericDomainComparator = Extensions.CreateComparator<GenericDomain<ThrowingDomain>>();

            var throwing = new ThrowingDomain();
            var objA = new GenericDomain<ThrowingDomain> { GenericProp = throwing };
            var objB = new GenericDomain<ThrowingDomain> { GenericProp = throwing };

            var result = genericDomainComparator.Compare(objA, objB);
            result.AreSame.Should().BeTrue();
        }
    }
}
