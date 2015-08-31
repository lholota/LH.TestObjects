namespace LH.TestObjects.Compare
{
    internal class ComparatorGenericSelectionActions : IGenericSelectionActions
    {
        protected readonly PropertyOptions Options;

        public ComparatorGenericSelectionActions(PropertyOptions propertyOptions)
        {
            this.Options = propertyOptions;
        }

        public void Ignore()
        {
            this.Options.IsIgnored = true;
        }
    }
}
