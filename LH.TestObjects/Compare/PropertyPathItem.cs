namespace LH.TestObjects.Compare
{
    using System.Reflection;
    using System.Text;

    internal class PropertyPathItem
    {
        public PropertyPathItem ParentProperty { get; set; }

        public PropertyInfo PropertyInfo { get; set; }

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