namespace LH.TestObjects.Tests.Compare
{
    using System.Linq;
    using Domain;
    using FluentAssertions;
    using NUnit.Framework;
    using TestObjects.Compare;

    [TestFixture]
    public class WhenSelectingComplexProperties
    {
        private ObjectComparator<ComplexDomain> comparator;

        [SetUp]
        public void Setup()
        {
            this.comparator = new ObjectComparator<ComplexDomain>();
        }

        [Test]
        public void ThenShouldPassWhenIgnoringPropertiesByPredicate()
        {
            var objA = ComplexDomain.CreateObjectWithValueSet1();
            var objB = ComplexDomain.CreateObjectWithValueSet1();

            objA.Simple.StringProp = "AAA";
            objB.Simple.StringProp = "BBB";

            this.comparator
                .PropertiesMatching(x => x.PropertyName == "StringProp")
                .Ignore();

            var result = this.comparator.Compare(objA, objB);

            result.AreSame.Should().BeTrue();
        }

        [Test]
        public void ThenShouldPassWhenIgnoringPropertiesByType()
        {
            var objA = ComplexDomain.CreateObjectWithValueSet1();
            var objB = ComplexDomain.CreateObjectWithValueSet2();

            objA.Simple.IntProp = objB.Simple.IntProp;

            this.comparator
                .PropertiesOfType<string>()
                .Ignore();

            var result = this.comparator.Compare(objA, objB);

            result.AreSame.Should().BeTrue();
        }

        [Test]
        public void ThenShouldPassWhenIgnoringPropertiesByTypeAndPredicate()
        {
            var objA = ComplexDomain.CreateObjectWithValueSet1();
            var objB = ComplexDomain.CreateObjectWithValueSet1();

            objA.Simple.StringProp = "AAA";
            objB.Simple.StringProp = "BBB";

            this.comparator
                .PropertiesOfType<string>(
                    predicate: x => x.PropertyName == nameof(objA.Simple.StringProp)
                )
                .Ignore();

            var result = this.comparator.Compare(objA, objB);

            result.AreSame.Should().BeTrue();
        }

        [Test]
        public void ThenShouldPassWhenIgnoringPropertiesByExpression()
        {
            var objA = ComplexDomain.CreateObjectWithValueSet1();
            var objB = ComplexDomain.CreateObjectWithValueSet1();

            objA.Simple.StringProp = "AAA";
            objB.Simple.StringProp = "BBB";

            this.comparator
                .Property(x => x.Simple.StringProp)
                .Ignore();

            var result = this.comparator.Compare(objA, objB);

            result.AreSame.Should().BeTrue();
        }

        [Test]
        public void ThenShouldFailWhenIgnoringByExpressionAndBothStringPropsDiffer()
        {
            var objA = ComplexDomain.CreateObjectWithValueSet1();
            var objB = ComplexDomain.CreateObjectWithValueSet2();

            objA.Simple.IntProp = objB.Simple.IntProp;

            this.comparator
                .Property(x => x.Simple.StringProp)
                .Ignore();

            var result = this.comparator.Compare(objA, objB);

            result.AreSame.Should().BeFalse();
            result.Differences.Should().NotBeNull();
            result.Differences.Count().Should().Be(2); // Name & Simple.StringProp2
        }

        [Test]
        public void ThenShouldPassWhenIgnoringMultiplePropertiesByExpression()
        {
            var objA = ComplexDomain.CreateObjectWithValueSet1();
            var objB = ComplexDomain.CreateObjectWithValueSet2();

            objA.Simple.StringProp2 = objB.Simple.StringProp2;

            this.comparator
                .Property(x => x.Name)
                .Ignore();

            this.comparator
                .Property(x => x.Simple.StringProp)
                .Ignore();

            this.comparator
                .Property(x => x.Simple.IntProp)
                .Ignore();

            var result = this.comparator.Compare(objA, objB);

            result.AreSame.Should().BeTrue();
        }

    }
}