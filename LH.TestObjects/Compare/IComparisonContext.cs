namespace LH.TestObjects.Compare
{
    public interface IComparisonContext
    {
        void AddDifference(IValueComparison valueComparison, string message = null);
    }
}