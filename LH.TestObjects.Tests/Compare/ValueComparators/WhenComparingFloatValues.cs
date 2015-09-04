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
            var comparator = new ObjectComparator<FloatDomain>();

            var objA = new FloatDomain { FloatProp = 0 };
            var objB = new FloatDomain { FloatProp = 1 };

            var result = comparator.Compare(objA, objB);
            result.AreSame.Should().BeFalse();
            result.Differences.Single().PropertyPath.Should().Be("FloatProp");
        }

        [Test]
        public void ThenShouldPassIfFloatValuesDifferByLessThanEpsilon()
        {
            var comparator = new ObjectComparator<FloatDomain>();

            comparator
                .PropertiesOfType<float>()
                .WithEpsilon(0.001f);

            var objA = new FloatDomain { FloatProp = 0.00000001f };
            var objB = new FloatDomain { FloatProp = 0.00000002f };

            var result = comparator.Compare(objA, objB);
            result.AreSame.Should().BeTrue();
        }

        [Test]
        public void ThenShouldPassIfFloatValuesAreSame()
        {
            var comparator = new ObjectComparator<FloatDomain>();

            var value = 0.00000001f;
            var objA = new FloatDomain { FloatProp = value };
            var objB = new FloatDomain { FloatProp = value };

            var result = comparator.Compare(objA, objB);
            result.AreSame.Should().BeTrue();
        }

        [Test]
        public void ThenShouldFailIfDoubleValuesDifferByMoreThanEpsilon()
        {
            var comparator = new ObjectComparator<FloatDomain>();

            var objA = new FloatDomain { DoubleProp = 0 };
            var objB = new FloatDomain { DoubleProp = 1 };

            var result = comparator.Compare(objA, objB);
            result.AreSame.Should().BeFalse();
            result.Differences.Single().PropertyPath.Should().Be("DoubleProp");
        }

        [Test]
        public void ThenShouldPassIfDoubleValuesDifferByLessThanEpsilon()
        {
            var comparator = new ObjectComparator<FloatDomain>();

            comparator
                .PropertiesOfType<double>()
                .WithEpsilon(0.001f);

            var objA = new FloatDomain { DoubleProp = 0.00000001f };
            var objB = new FloatDomain { DoubleProp = 0.00000002f };

            var result = comparator.Compare(objA, objB);
            result.AreSame.Should().BeTrue();
        }

        [Test]
        public void ThenShouldPassIfDoubleValuesAreSame()
        {
            var comparator = new ObjectComparator<FloatDomain>();

            var value = 0.00000001;
            var objA = new FloatDomain { DoubleProp = value };
            var objB = new FloatDomain { DoubleProp = value };

            var result = comparator.Compare(objA, objB);
            result.AreSame.Should().BeTrue();
        }
    }
}