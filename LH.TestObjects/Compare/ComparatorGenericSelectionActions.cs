namespace LH.TestObjects.Compare
{
    internal class ComparatorGenericSelectionActions : IGenericSelectionActions
    {
        private readonly PropertyOptions options;

        public ComparatorGenericSelectionActions(PropertyOptions propertyOptions)
        {
            this.options = propertyOptions;
        }

        public void Ignore()
        {
            this.options.IsIgnored = true;
        }
    }
}
