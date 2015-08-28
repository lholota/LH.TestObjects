namespace LH.TestObjects
{
    /// <summary>
    /// Configuration actions which can be performed on property selection without the knowledge of their type.
    /// </summary>
    public interface IGenericSelectionActions
    {
        /// <summary>
        /// Will ignore the property when comparing/generating the objects.
        /// </summary>
        void Ignore();
    }
}