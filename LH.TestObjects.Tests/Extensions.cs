namespace LH.TestObjects.Tests
{
    public static class Extensions
    {
        public static string EnsureUniqueInstance(this string value)
        {
            return new string(value.ToCharArray());
        }
    }
}