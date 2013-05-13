using System.Collections.ObjectModel;

namespace LinkedIN.Model.People
{
    /// <summary>
    /// Represents a collection of <see cref="Person"/>s.
    /// </summary>
    public class UpdateComment
    {
        #region Properties
        /// <summary>
        /// The <see cref="Person"/>s.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// </summary>
        public Person Person { get; set; }
        #endregion
    }
}