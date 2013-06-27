namespace LinkedIN.Model.People
{
	/// <summary>
	/// a group that the member is following
	/// </summary>
	public class Group
    {
        /// <summary>
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// </summary>
        public CountsByCategory CountsByCategory { get; set; }
    }
}
