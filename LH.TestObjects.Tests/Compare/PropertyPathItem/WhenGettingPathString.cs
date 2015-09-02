namespace LH.TestObjects.Tests.Compare.PropertyPathItem
{
    using System.Linq;
    using FluentAssertions;
    using NUnit.Framework;
    using TestDomain;
    using TestObjects.Compare;

    [TestFixture]
    public class WhenGettingPathString
    {
        [Test]
        public void ThenShouldReturnPropertyNameIfDoesNotHaveParent()
        {
            var propInfo = typeof(SimpleDomain).GetProperties()
                .First(x => x.Name == "StringProp");

            var item = new PropertyPathItem(propInfo, PropertyPathItem.Root);

            item.GetPathString().Should().Be("StringProp");
        }

        [Test]
        public void ThenShouldReturnDotSeparatedStringIfHasParent()
        {
            var stringPropInfo = typeof(SimpleDomain).GetProperties()
                .First(x => x.Name == "StringProp");

            var intPropInfo = typeof(SimpleDomain).GetProperties()
                .First(x => x.Name == "IntProp");

            var item1 = new PropertyPathItem(stringPropInfo, PropertyPathItem.Root);
            var item2 = new PropertyPathItem(intPropInfo, item1);
            var item3 = new PropertyPathItem(stringPropInfo, item2);

            item2.GetPathString().Should().Be("StringProp.IntProp");
            item3.GetPathString().Should().Be("StringProp.IntProp.StringProp");
        }
    }
}
