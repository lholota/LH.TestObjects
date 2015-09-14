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

        /// <summary>
        /// Will compare the values using ReferenceEquals (i.e. both expected and actual will have to contain the same instance)
        /// </summary>
        void UseReferenceEquals();
    }
}