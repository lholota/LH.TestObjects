namespace LH.TestObjects.Tests.Compare
{
    using FluentAssertions;
    using NUnit.Framework;
    using TestDomain;

    [TestFixture]
    public class WhenIgnoringProperties : ComparatorTestsBase
    {
        [Test]
        public void ThenShouldPassIfIgnoredPropertyValuesDiffer()
        {
            var objA = new SimpleDomain();
            objA.StringProp = "AAA";
            
            var objB = new SimpleDomain();
            objB.StringProp = "BBB";

            this.Comparator.Property(x => x.StringProp).Ignore();
            var result = this.Comparator.Compare(objA, objB);

            result.AreSame.Should().BeTrue();
        }
    }
}