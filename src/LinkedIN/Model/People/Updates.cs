using System.Collections.ObjectModel;

namespace LinkedIN.Model.People
{
    /// <summary>
    /// Represents a collection of <see cref="Person"/>s.
    /// </summary>
    public class Updates : PagedCollection
    {
        #region Properties
        /// <summary>
        /// The <see cref="Person"/>s.
        /// </summary>
        public Collection<Update> updates { get; set; }
        #endregion
    }
}