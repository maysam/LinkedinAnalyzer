namespace LinkedIN.Model.People
{
    /// <summary>
    /// </summary>
    public class Comment
    {

        /// <summary>
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// </summary>
        public long CreationTimestamp { get; set; }

        /// <summary>
        /// </summary>
        public Person Creator { get; set; }
    }
}
