namespace LH.TestObjects.Tests.Compare
{
    using Domain;
    using FluentAssertions;
    using NUnit.Framework;

    [TestFixture]
    public class WhenIgnoringProperties : ComparatorTestsBase
    {
        [Test]
        public void ThenShouldPassIfIgnoredPropertyValuesDiffer()
        {
            var objA = SimpleDomain.CreateObjectWithValueSet1();
            var objB = SimpleDomain.CreateObjectWithValueSet1();

            objA.StringProp = "AAA";
            objB.StringProp = "BBB";

            this.Comparator.Property(x => x.StringProp).Ignore();
            var result = this.Comparator.Compare(objA, objB);

            result.AreSame.Should().BeTrue();
        }
    }
}