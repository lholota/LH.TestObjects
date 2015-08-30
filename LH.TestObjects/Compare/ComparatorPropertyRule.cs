namespace LH.TestObjects.Compare
{
    internal class ComparatorPropertyRule
    {
        public ComparatorPropertyRule()
        {
            this.Selection = new PropertySelection();
            this.Options = new PropertyOptions();
        }

        public PropertySelection Selection { get; private set; }

        public PropertyOptions Options { get; private set; }

        public int OrderIndex { get; set; }
    }
}
