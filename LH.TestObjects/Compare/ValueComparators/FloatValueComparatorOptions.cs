namespace LH.TestObjects.Compare.ValueComparators
{
    internal class FloatValueComparatorOptions
    {
        public FloatValueComparatorOptions()
        {
            this.DoubleEpsilon = double.MinValue;
            this.FloatEpsilon = float.MinValue;
        }

        public float FloatEpsilon { get; set; }

        public double DoubleEpsilon { get; set; }        
    }
}
