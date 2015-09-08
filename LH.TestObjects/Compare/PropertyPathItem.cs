namespace LH.TestObjects.Compare
{
    using System.Reflection;
    using System.Text;

    internal class PropertyPathItem
    {
        public static readonly PropertyPathItem Root = new PropertyPathItem();

        private readonly bool descriptionIsCollectionItem;

        public PropertyPathItem(PropertyInfo propertyInfo, PropertyPathItem parentPropertyPathItem)
        {
            this.ParentProperty = parentPropertyPathItem;
            this.ItemDescription = propertyInfo.Name;
            this.PropertyInfo = propertyInfo;
        }

        public PropertyPathItem(string itemDescription, PropertyPathItem parentPropertyPathItem, bool descriptionIsCollectionItem = true)
        {
            this.descriptionIsCollectionItem = descriptionIsCollectionItem;
            this.ParentProperty = parentPropertyPathItem;
            this.ItemDescription = itemDescription;
        }

        private PropertyPathItem()
        {
            this.IsRoot = true;
        }

        public bool IsRoot { get; }

        public string ItemDescription { get; }

        public PropertyPathItem ParentProperty { get; }

        public PropertyInfo PropertyInfo { get; }

        public string GetPathString()
        {
            if (this.IsRoot)
            {
                return string.Empty;
            }

            var builder = new StringBuilder();
            this.WritePath(builder);

            return builder.ToString();
        }

        private void WritePath(StringBuilder builder)
        {
            if (this.IsRoot)
            {
                return;
            }

            if (this.ParentProperty != null && !this.ParentProperty.IsRoot)
            {
                this.ParentProperty.WritePath(builder);
            }

            if (this.descriptionIsCollectionItem)
            {
                builder.AppendFormat("[{0}]", this.ItemDescription);
            }
            else
            {
                if (builder.Length > 0)
                {
                    builder.Append('.');
                }

                builder.Append(this.ItemDescription);
            }
        }
    }
}