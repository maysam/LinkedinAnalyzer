using System.Collections.ObjectModel;

namespace LinkedIN.Model.People
{
    /// <summary>
    /// Represents a collection of <see cref="Person"/>s.
    /// </summary>
    public class UpdateAction
    {
        #region Properties
        /// <summary>
        /// </summary>
        public Action Action { get; set; }
        /// <summary>
        /// </summary>
        public Update OriginalUpdate { get; set; }

        #endregion
    }
}