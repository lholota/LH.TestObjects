namespace LH.TestObjects.Tests.Compare.ValueComparators.DictionaryValueComparator
{
    using System.Collections.Generic;
    using Domain;
    using NUnit.Framework;

    [TestFixture]
    public class WhenComparingDictionaries : WhenComparingIDictionaryBase<Dictionary<string, SimpleDomain>>
    {
    }
}
