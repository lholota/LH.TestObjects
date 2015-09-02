namespace LH.TestObjects.Compare
{
    internal class ComparatorGenericSelectionActions : IGenericSelectionActions
    {
        private readonly PropertyOptions options;

        public ComparatorGenericSelectionActions(PropertyOptions propertyOptions)
        {
            this.options = propertyOptions;
        }

        internal PropertyOptions Options
        {
            get { return this.options; }
        }

        public void Ignore()
        {
            this.options.IsIgnored = true;
        }
    }
}
