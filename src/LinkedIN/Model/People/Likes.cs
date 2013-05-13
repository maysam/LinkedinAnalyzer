using System.Collections.ObjectModel;

namespace LinkedIN.Model.People
{
    /// <summary>
    /// Represents a collection of <see cref="Like"/>s.
    /// </summary>
    public class Likes : PagedCollection
    {
        #region Properties
        /// <summary>
        /// The <see cref="Like"/>s.
        /// </summary>
        public Collection<Like> likes { get; set; }
        #endregion
    }
}