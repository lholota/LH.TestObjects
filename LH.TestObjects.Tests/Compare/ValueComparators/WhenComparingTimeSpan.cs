namespace LH.TestObjects.Tests.Compare.ValueComparators
{
    using System;
    using System.Linq;
    using FluentAssertions;
    using NUnit.Framework;

    [TestFixture]
    public class WhenComparingTimeSpan
    {
        [Test]
        public void ThenShouldPassIfTimeSpansAreEqual()
        {
            var comparator = Extensions.CreateComparator<TimeSpanDomain>();
            var objA = new TimeSpanDomain {TimeSpanProp = TimeSpan.FromDays(1)};
            var objB = new TimeSpanDomain {TimeSpanProp = TimeSpan.FromDays(1)};

            var result = comparator.Compare(objA, objB);
            result.AreSame.Should().BeTrue();
        }

        [Test]
        public void ThenShouldFailIfTimeSpansAreDifferent()
        {
            var comparator = Extensions.CreateComparator<TimeSpanDomain>();
            var objA = new TimeSpanDomain { TimeSpanProp = TimeSpan.FromDays(1) };
            var objB = new TimeSpanDomain { TimeSpanProp = TimeSpan.FromDays(2) };

            var result = comparator.Compare(objA, objB);
            result.AreSame.Should().BeFalse();
            result.Differences.Single().PropertyName.Should().Be("TimeSpanProp");
            result.Differences.Single().PropertyPath.Should().Be("TimeSpanProp");
        }


        private class TimeSpanDomain
        {
            // ReSharper disable once UnusedAutoPropertyAccessor.Local
            public TimeSpan TimeSpanProp { get; set; } 
        }
    }
}