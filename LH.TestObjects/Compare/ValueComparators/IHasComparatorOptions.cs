namespace LH.TestObjects.Compare.ValueComparators
{
    internal interface IHasComparatorOptions<in TOptions>
    {
        TOptions Options { set; }
    }
}