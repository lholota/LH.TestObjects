namespace LH.TestObjects.Tests.Compare.ValueComparators
{
    using System;
    using System.Linq;
    using Domain;
    using FluentAssertions;
    using NUnit.Framework;
    using TestObjects.Compare;

    [TestFixture]
    public class WhenComparingStringValues : ComparatorTestsBase
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
        public void ThenShouldPassWhenValuesAreSame()
        {
            var result = this.Comparator.Compare(this.objA, this.objB);

            result.AreSame.Should().BeTrue();
        }

        [Test]
        public void ThenShouldFailIfValuesDiffer()
        {
            this.objA.StringProp = "AAA";
            this.objB.StringProp = "BBB";

            var result = this.Comparator.Compare(this.objA, this.objB);

            result.AreSame.Should().BeFalse();
            result.Differences.Should().NotBeNull();
            result.Differences.Single().PropertyPath.Should().Be("StringProp");
        }

        [Test]
        public void ThenShouldFailIfValuesDifferByCase()
        {
            this.objA.StringProp = "AAA";
            this.objB.StringProp = "aaa";

            var result = this.Comparator.Compare(this.objA, this.objB);

            result.AreSame.Should().BeFalse();
            result.Differences.Single().PropertyName.Should().Be("StringProp");
        }

        [Test]
        public void ThenShouldPassIfValuesDifferByCaseAndOptionsSetToInsensitive()
        {
            this.objA.StringProp = "AAA";
            this.objB.StringProp = "aaa";

            this.Comparator.Property(x => x.StringProp)
                .WithCaseSensitivity(StringComparison.OrdinalIgnoreCase);

            var result = this.Comparator.Compare(this.objA, this.objB);

            result.AreSame.Should().BeTrue();
        }
    }
}