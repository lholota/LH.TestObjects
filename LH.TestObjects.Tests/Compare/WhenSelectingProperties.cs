namespace LH.TestObjects.Tests.Compare
{
    using System.Linq;
    using FluentAssertions;
    using NUnit.Framework;
    using TestDomain;

    [TestFixture]
    public class WhenSelectingProperties : ComparatorTestsBase, IPropertySelectorTests
    {
        [Test]
        public void ThenShouldPassWhenIgnoringPropertiesByPredicate()
        {
            var objA = SimpleDomain.CreateObjectWithValueSet1();
            var objB = SimpleDomain.CreateObjectWithValueSet1();

            objA.StringProp = "AAA";
            objB.StringProp = "BBB";

            this.Comparator
                .PropertiesMatching(x => x.Name == "StringProp")
                .Ignore();

            var result = this.Comparator.Compare(objA, objB);

            result.AreSame.Should().BeTrue();
        }

        [Test]
        public void ThenShouldPassWhenIgnoringPropertiesByType()
        {
            var objA = SimpleDomain.CreateObjectWithValueSet1();
            var objB = SimpleDomain.CreateObjectWithValueSet2();

            objA.IntProp = objB.IntProp;

            this.Comparator
                .PropertiesOfType<string>()
                .Ignore();

            var result = this.Comparator.Compare(objA, objB);

            result.AreSame.Should().BeTrue();
        }

        [Test]
        public void ThenShouldPassWhenIgnoringPropertiesByTypeAndPredicate()
        {
            var objA = SimpleDomain.CreateObjectWithValueSet1();
            var objB = SimpleDomain.CreateObjectWithValueSet1();

            objA.StringProp = "AAA";
            objB.StringProp = "BBB";

            this.Comparator
                .PropertiesOfType<string>(predicate: x => x.Name == nameof(objA.StringProp))
                .Ignore();

            var result = this.Comparator.Compare(objA, objB);

            result.AreSame.Should().BeTrue();
        }

        [Test]
        public void ThenShouldPassWhenIgnoringPropertiesByExpression()
        {
            var objA = SimpleDomain.CreateObjectWithValueSet1();
            var objB = SimpleDomain.CreateObjectWithValueSet1();

            objA.StringProp = "AAA";
            objB.StringProp = "BBB";

            this.Comparator
                .Property(x => x.StringProp)
                .Ignore();

            var result = this.Comparator.Compare(objA, objB);
            
            result.AreSame.Should().BeTrue();
        }

        [Test]
        public void ThenShouldFailWhenIgnoringByExpressionAndBothStringPropsDiffer()
        {
            var objA = SimpleDomain.CreateObjectWithValueSet1();
            var objB = SimpleDomain.CreateObjectWithValueSet2();

            objA.IntProp = objB.IntProp;

            this.Comparator
                .Property(x => x.StringProp)
                .Ignore();

            var result = this.Comparator.Compare(objA, objB);

            result.AreSame.Should().BeFalse();
            result.Differences.Should().NotBeNull();
            result.Differences.Count().Should().Be(1);
        }

        [Test]
        public void ThenShouldPassWhenIgnoringMultiplePropertiesByExpression()
        {
            var objA = SimpleDomain.CreateObjectWithValueSet1();
            var objB = SimpleDomain.CreateObjectWithValueSet2();

            objA.StringProp2 = objB.StringProp2;

            this.Comparator
                .Property(x => x.StringProp)
                .Ignore();

            this.Comparator
                .Property(x => x.IntProp)
                .Ignore();

            var result = this.Comparator.Compare(objA, objB);

            result.AreSame.Should().BeTrue();
        }
    }
}