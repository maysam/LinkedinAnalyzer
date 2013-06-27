
namespace LinkedIN.Model.People
{
    /// <summary>
    /// Represents a collection of <see cref="Person"/>s.
    /// </summary>
    public class Update
    {
        #region Properties
        /// <summary>
        /// </summary>
        public string UpdateKey { get; set; }
        /// <summary>
        /// </summary>
        public long TimeStamp { get; set; }
        /// <summary>
        /// </summary>
        public string UpdateType { get; set; }
        /// <summary>
        /// </summary>
        public string UpdateUrl { get; set; }
        
        /// <summary>
        /// </summary>
        public UpdateContent UpdateContent { get; set; }
          
        /// <summary>
        /// </summary>
        public UpdateAction UpdateAction { get; set; }
        
        /// <summary>
        /// </summary>
        public bool IsLikeable { get; set; }
        /// <summary>
        /// </summary>
        public int NumLikes { get; set; }
        /// <summary>
        /// </summary>
        public Likes Likes { get; set; }

        /// <summary>
        /// </summary>
        public bool IsCommentable { get; set; }

        /// <summary>
        /// </summary>
        public UpdateComments UpdateComments { get; set; }
        
        #endregion
    }
}