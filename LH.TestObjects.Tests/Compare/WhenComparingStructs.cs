namespace LH.TestObjects.Tests.Compare
{
    using Domain;
    using FluentAssertions;
    using NUnit.Framework;
    using TestObjects.Compare;

    [TestFixture]
    public class WhenComparingStructs
    {
        private ObjectComparator<StructDomain> comparator;
            
        [SetUp]
        public void Setup()
        {
            this.comparator = new ObjectComparator<StructDomain>();
        }

        [Test]
        public void ThenShouldPassIfValuesMatch()
        {
            var objA = new StructDomain {StringProp = "AAA"};
            var objB = new StructDomain {StringProp = "AAA"};

            var result = this.comparator.Compare(objA, objB);
            result.AreSame.Should().BeTrue();
        }

        [Test]
        public void ThenShouldFailIfValuesDoNotMatch()
        {
            var objA = new StructDomain { StringProp = "AAA" };
            var objB = new StructDomain { StringProp = "BBB" };

            var result = this.comparator.Compare(objA, objB);
            result.AreSame.Should().BeFalse();
        }
    }
}