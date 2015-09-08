namespace LH.TestObjects.Tests
{
    using System;
    using TestObjects.Compare;
    using TestObjects.Compare.Logging;

    public static class Extensions
    {
        public static ObjectComparator<T> CreateComparator<T>()
        {
            var result = new ObjectComparator<T>();
            result.Log.Callback(x => Console.WriteLine(x.Message));
            result.Log.SetLevel(LogLevel.Debug);

            return result;
        }

        public static string EnsureUniqueInstance(this string value)
        {
            return new string(value.ToCharArray());
        }
    }
}