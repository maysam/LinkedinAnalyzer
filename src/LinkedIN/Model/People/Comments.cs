using System.Collections.ObjectModel;
namespace LinkedIN.Model.People
{
    /// <summary>
    /// </summary>
    public class Comments : PagedCollection
    {
        #region Properties
        /// <summary>
        /// The <see cref="Like"/>s.
        /// </summary>
        public Collection<Comment> Comment { get; set; }
        #endregion
    }
}
