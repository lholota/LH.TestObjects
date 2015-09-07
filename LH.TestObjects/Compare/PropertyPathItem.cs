namespace LH.TestObjects.Compare
{
    using System.Reflection;
    using System.Text;

    internal class PropertyPathItem
    {
        public PropertyPathItem(System.Type type)
        {
            this.IsRoot = true;
            this.Type = type;
        }

        public PropertyPathItem(PropertyInfo propertyInfo, PropertyPathItem parentPropertyPathItem)
        {
            this.ParentProperty = parentPropertyPathItem;
            this.PropertyInfo = propertyInfo;
            this.Type = propertyInfo.PropertyType;
        }

        public PropertyPathItem(string collectionItemDescription, PropertyPathItem parentPropertyPathItem)
        {
            this.CollectionItemDescription = collectionItemDescription;
            this.ParentProperty = parentPropertyPathItem;
        }

        public bool IsRoot { get; }

        public string CollectionItemDescription { get; }

        public PropertyPathItem ParentProperty { get; }

        public PropertyInfo PropertyInfo { get; }

        public System.Type Type { get; }

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

            if (this.PropertyInfo != null)
            {
                if (builder.Length > 0)
                {
                    builder.Append('.');
                }

                builder.Append(this.PropertyInfo.Name);
            }
            else
            {
                builder.AppendFormat("[{0}]", this.CollectionItemDescription);
            }
        }
    }
}