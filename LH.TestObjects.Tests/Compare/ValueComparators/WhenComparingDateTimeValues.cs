namespace LH.TestObjects.Tests.Compare.ValueComparators
{
    using System;
    using System.Linq;
    using Domain;
    using FluentAssertions;
    using NUnit.Framework;

    [TestFixture]
    public class WhenComparingDateTimeValues
    {
        [Test]
        public void ThenShouldPassIfValuesAreEqual()
        {
            var comparator = Extensions.CreateComparator<GenericDomain<DateTime>>();

            var objA = new GenericDomain<DateTime> {GenericProp = DateTime.Parse("1/1/2001 14:55:33")};
            var objB = new GenericDomain<DateTime> {GenericProp = DateTime.Parse("1/1/2001 14:55:33")};

            var result = comparator.Compare(objA, objB);
            result.AreSame.Should().BeTrue();
        }

        [Test]
        public void ThenShouldFailIfValuesDiffer()
        {
            var comparator = Extensions.CreateComparator<GenericDomain<DateTime>>();

            var objA = new GenericDomain<DateTime> { GenericProp = DateTime.Parse("1/1/2001 14:55:33") };
            var objB = new GenericDomain<DateTime> { GenericProp = DateTime.Parse("1/1/2001 14:55:34") };

            var result = comparator.Compare(objA, objB);
            result.AreSame.Should().BeFalse();
            result.Differences.Single().PropertyPath.Should().Be("GenericProp");
        }
    }
}