namespace LH.TestObjects.Tests.Compare.ValueComparators
{
    using System;
    using System.Linq;
    using Domain;
    using FluentAssertions;
    using NUnit.Framework;

    [TestFixture]
    public class WhenComparingNumberValues
    {
        [Test]
        public void ThenShouldFailIfValuesDiffer()
        {
            this.RunThenShouldFailIfValuesDiffer(1,2);
            this.RunThenShouldFailIfValuesDiffer<uint>(1,2);
            this.RunThenShouldFailIfValuesDiffer<decimal>(1,2);
            this.RunThenShouldFailIfValuesDiffer<long>(1,2);
            this.RunThenShouldFailIfValuesDiffer<ulong>(1,2);
            this.RunThenShouldFailIfValuesDiffer<short>(1,2);
            this.RunThenShouldFailIfValuesDiffer<ushort>(1,2);
            this.RunThenShouldFailIfValuesDiffer(DummyEnum.One, DummyEnum.Zero);
        }

        [Test]
        public void ShouldPassIfValuesMatch()
        {
            this.RunThenShouldFailIfValuesAreEqual(1);
            this.RunThenShouldFailIfValuesAreEqual((uint)1);
            this.RunThenShouldFailIfValuesAreEqual((decimal)1);
            this.RunThenShouldFailIfValuesAreEqual((long)1);
            this.RunThenShouldFailIfValuesAreEqual((ulong)1);
            this.RunThenShouldFailIfValuesAreEqual((short)1);
            this.RunThenShouldFailIfValuesAreEqual((ushort)1);
            this.RunThenShouldFailIfValuesAreEqual(DummyEnum.One);
        }

        private void RunThenShouldFailIfValuesAreEqual<T>(T value)
        {
            Console.WriteLine(typeof(T));

            var comparator = Extensions.CreateComparator<GenericDomain<T>>();

            var objA = new GenericDomain<T> { GenericProp = value };
            var objB = new GenericDomain<T> { GenericProp = value };

            var result = comparator.Compare(objA, objB);

            result.AreSame.Should().BeTrue();
        }

        private void RunThenShouldFailIfValuesDiffer<T>(T expected, T actual)
        {
            Console.WriteLine(typeof(T));

            var comparator = Extensions.CreateComparator<GenericDomain<T>>();

            var objA = new GenericDomain<T> { GenericProp = expected };
            var objB = new GenericDomain<T> { GenericProp = actual };

            var result = comparator.Compare(objA, objB);

            result.AreSame.Should().BeFalse();
            result.Differences.Should().NotBeNull();
            result.Differences.Count().Should().Be(1);
            result.Differences.Single().PropertyName.Should().Be("GenericProp");
        }

        private enum DummyEnum
        {
            Zero = 0,
            One = 1
        }
    }
}