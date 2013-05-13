using System.Collections.ObjectModel;

namespace LinkedIN.Model.People
{

	/// <summary>
	/// Defines a Network Statistics.
	/// </summary>
    public class NetworkStats
    {
        /// <summary>
        /// number of first degree connections
		/// </summary>
        /// 
        public int Total { get; set; }
        /// <summary>
        /// number of second degree connections
        /// </summary>
        /// 
        public Collection<Property> properties { get; set; }
    }
}