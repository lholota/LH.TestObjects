namespace LH.TestObjects.Tests.Compare.ValueComparators
{
    using System.Linq;
    using Domain;
    using FluentAssertions;
    using NUnit.Framework;
    using TestObjects.Compare;

    [TestFixture]
    public class WhenComparingFloatValues
    {
        [Test]
        public void ThenShouldFailIfFloatValuesDifferByMoreThanEpsilon()
        {
            var comparator = new ObjectComparator<GenericDomain<float>>();

            var objA = new GenericDomain<float> { GenericProp = 0 };
            var objB = new GenericDomain<float> { GenericProp = 1 };

            var result = comparator.Compare(objA, objB);
            result.AreSame.Should().BeFalse();
            result.Differences.Single().PropertyPath.Should().Be("GenericProp");
        }

        [Test]
        public void ThenShouldPassIfFloatValuesDifferByLessThanEpsilon()
        {
            var comparator = new ObjectComparator<GenericDomain<float>>();

            comparator
                .PropertiesOfType<float>()
                .WithEpsilon(0.001f);

            var objA = new GenericDomain<float> { GenericProp = 0.00000001f };
            var objB = new GenericDomain<float> { GenericProp = 0.00000002f };

            var result = comparator.Compare(objA, objB);
            result.AreSame.Should().BeTrue();
        }

        [Test]
        public void ThenShouldPassIfFloatValuesAreSame()
        {
            var comparator = new ObjectComparator<GenericDomain<float>>();

            var value = 0.00000001f;
            var objA = new GenericDomain<float> { GenericProp = value };
            var objB = new GenericDomain<float> { GenericProp = value };

            var result = comparator.Compare(objA, objB);
            result.AreSame.Should().BeTrue();
        }

        [Test]
        public void ThenShouldFailIfDoubleValuesDifferByMoreThanEpsilon()
        {
            var comparator = new ObjectComparator<GenericDomain<double>>();

            var objA = new GenericDomain<double> { GenericProp = 0 };
            var objB = new GenericDomain<double> { GenericProp = 1 };

            var result = comparator.Compare(objA, objB);
            result.AreSame.Should().BeFalse();
            result.Differences.Single().PropertyPath.Should().Be("GenericProp");
        }

        [Test]
        public void ThenShouldPassIfDoubleValuesDifferByLessThanEpsilon()
        {
            var comparator = new ObjectComparator<GenericDomain<double>>();

            comparator
                .PropertiesOfType<double>()
                .WithEpsilon(0.001f);

            var objA = new GenericDomain<double> { GenericProp = 0.00000001f };
            var objB = new GenericDomain<double> { GenericProp = 0.00000002f };

            var result = comparator.Compare(objA, objB);
            result.AreSame.Should().BeTrue();
        }

        [Test]
        public void ThenShouldPassIfDoubleValuesAreSame()
        {
            var comparator = new ObjectComparator<GenericDomain<double>>();

            var value = 0.00000001;
            var objA = new GenericDomain<double> { GenericProp = value };
            var objB = new GenericDomain<double> { GenericProp = value };

            var result = comparator.Compare(objA, objB);
            result.AreSame.Should().BeTrue();
        }
    }
}