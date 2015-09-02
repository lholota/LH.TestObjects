namespace LH.TestObjects.Compare
{
    using System.Reflection;
    using System.Text;

    internal class PropertyPathItem
    {
        public static readonly PropertyPathItem Root = new PropertyPathItem();

        public PropertyPathItem(PropertyInfo propertyInfo, PropertyPathItem parentPropertyPathItem)
        {
            this.ParentProperty = parentPropertyPathItem;
            this.PropertyInfo = propertyInfo;
        }

        private PropertyPathItem()
        {
        }

        public PropertyPathItem ParentProperty { get; }

        public PropertyInfo PropertyInfo { get; }

        public string GetPathString()
        {
            if (this == Root)
            {
                return string.Empty;
            }

            var builder = new StringBuilder();

            if (this.ParentProperty != null && this.ParentProperty != Root)
            {
                this.ParentProperty.WritePath(builder);
            }

            return builder.ToString();
        }

        private void WritePath(StringBuilder builder)
        {
            builder.Append(this.PropertyInfo.Name);
            builder.Append('.');
        }
    }
}