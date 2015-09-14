namespace LH.TestObjects.Tests.Domain
{
    using System.Collections;

    public class DictionaryDomain<T>
        where T : IDictionary
    {
        public T DictProp { get; set; }
    }
}
