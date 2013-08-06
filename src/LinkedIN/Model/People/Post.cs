using System.Collections.ObjectModel;
namespace LinkedIN.Model.People
{
    /// <summary>
    /// </summary>
    public class Post
    {
        /// <summary>
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// </summary>
        public string summary { get; set; }
        /// <summary>
        /// </summary>
        public long CreationTimestamp { get; set; }

        /// <summary>
        /// </summary>
        public Likes likes { get; set; }

        /// <summary>
        /// </summary>
        public Collection<Comment> Comments { get; set; }
        
        /// <summary>
        /// </summary>
        public string SiteGroupPostUrl { get; set; }
    }
}
