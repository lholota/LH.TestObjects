namespace LH.TestObjects.Tests.Compare.ValueComparators
{
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using Domain;
    using FluentAssertions;
    using NUnit.Framework;
    using TestObjects.Compare;

    [TestFixture]
    public class WhenComparingDynamicObjects
    {
        [Test]
        public void ThenShouldFailIfOneObjectHasAnExtraProperty()
        {
            this.RunExtraPropertyTest<ExpandoObject>();
            this.RunExtraPropertyTest<DummyDynamic>();
        }

        [Test]
        public void ThenShouldFailIfValuesHaveDifferentType()
        {
            this.RunDifferentValueTypeTest<ExpandoObject>();
            this.RunDifferentValueTypeTest<DummyDynamic>();
        }

        [Test]
        public void ThenShouldFailIfValuesDiffer()
        {
            this.RunDifferentValueTest<ExpandoObject>();
            this.RunDifferentValueTest<DummyDynamic>();
        }

        [Test]
        public void ThenShouldPassIfDynamicObjectsAreEqual()
        {
            this.RunEqualTest<ExpandoObject>();
            this.RunEqualTest<DummyDynamic>();
        }

        [Test]
        public void ThenShouldPassWhenDynamicObjectsAreRoots()
        {
            dynamic objA = new ExpandoObject();
            dynamic objB = new ExpandoObject();

            objA.StringProp = "AAA";
            objB.StringProp = "AAA";

            var comparator = Extensions.CreateComparator<dynamic>();
            var result = (IComparisonResult)comparator.Compare(objA, objB);

            result.AreSame.Should().BeTrue();
        }

        private void RunExtraPropertyTest<T>() where T : new()
        {
            dynamic objA = new GenericDomain<T> { GenericProp = new T() };
            dynamic objB = new GenericDomain<T> { GenericProp = new T() };

            objA.GenericProp.StringProp = "AAA";
            objB.GenericProp.StringProp = "AAA";

            objB.GenericProp.DummyProp = 5;

            var comparator = Extensions.CreateComparator<GenericDomain<T>>();
            var result = (IComparisonResult)comparator.Compare(objA, objB);

            result.AreSame.Should().BeFalse();
            result.Differences.Single().PropertyPath.Should().Be("GenericProp");
        }

        private void RunDifferentValueTypeTest<T>() where T : new()
        {
            dynamic objA = new GenericDomain<T> { GenericProp = new T() };
            dynamic objB = new GenericDomain<T> { GenericProp = new T() };

            objA.GenericProp.StringProp = "AAA";
            objB.GenericProp.StringProp = "AAA";

            objA.GenericProp.DummyProp = 5;
            objB.GenericProp.DummyProp = "5";

            var comparator = Extensions.CreateComparator<GenericDomain<T>>();
            var result = (IComparisonResult)comparator.Compare(objA, objB);

            result.AreSame.Should().BeFalse();
            result.Differences.Single().PropertyPath.Should().Be("GenericProp.DummyProp");
        }

        private void RunDifferentValueTest<T>() where T : new()
        {
            dynamic objA = new GenericDomain<T> { GenericProp = new T() };
            dynamic objB = new GenericDomain<T> { GenericProp = new T() };

            objA.GenericProp.StringProp = "AAA";
            objB.GenericProp.StringProp = "BBB";

            var comparator = Extensions.CreateComparator<GenericDomain<T>>();
            var result = (IComparisonResult)comparator.Compare(objA, objB);

            result.AreSame.Should().BeFalse();
            result.Differences.Single().PropertyPath.Should().Be("GenericProp.StringProp");
        }

        private void RunEqualTest<T>() where T : new()
        {
            dynamic objA = new GenericDomain<T> { GenericProp = new T() };
            dynamic objB = new GenericDomain<T> { GenericProp = new T() };

            objA.GenericProp.StringProp = "AAA";
            objB.GenericProp.StringProp = "AAA";

            var comparator = new ObjectComparator<GenericDomain<T>>();
            var result = (IComparisonResult)comparator.Compare(objA, objB);

            result.AreSame.Should().BeTrue();
        }

        /*
        - When dynamic is a root object
        */

        private class DummyDynamic : DynamicObject
        {
            readonly Dictionary<string, object> dictionary = new Dictionary<string, object>();

            public override IEnumerable<string> GetDynamicMemberNames()
            {
                return this.dictionary.Select(x => x.Key);
            }

            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {
                return this.dictionary.TryGetValue(binder.Name, out result);
            }

            public override bool TrySetMember(SetMemberBinder binder, object value)
            {
                this.dictionary[binder.Name] = value;
                return true;
            }
        }
    }
}