namespace LH.TestObjects.Compare
{
    using System.Reflection;
    using System.Text;

    internal class PropertyPathItem
    {
        public PropertyPathItem(PropertyInfo propertyInfo, PropertyPathItem parentPropertyPathItem)
        {
            this.ParentProperty = parentPropertyPathItem;
            this.PropertyInfo = propertyInfo;
        }

        public PropertyPathItem ParentProperty { get; }

        public PropertyInfo PropertyInfo { get; }

        public string GetPathString()
        {
            var builder = new StringBuilder();

            if (this.ParentProperty != null)
            {
                this.ParentProperty.WritePath(builder);
            }

            builder.Append(this.PropertyInfo.Name);

            return builder.ToString();
        }

        private void WritePath(StringBuilder builder)
        {
            builder.Append(this.PropertyInfo.Name);
            builder.Append('.');
        }
    }
}