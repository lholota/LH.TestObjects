namespace LH.TestObjects.Compare
{
    internal class ComparatorPropertySelection : PropertySelection
    {
        public ComparatorPropertySelection()
        {
            this.Options = new PropertyOptions();
        }

        public PropertyOptions Options { get; set; }
    }
}
