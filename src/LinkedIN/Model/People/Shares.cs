using System.Collections.ObjectModel;

namespace LinkedIN.Model.People
{
	/// <summary>
	/// Represents a collection of <see cref="Person"/>s.
	/// </summary>
	public class Shares : PagedCollection
	{
		#region Properties
		/// <summary>
		/// </summary>
		public Collection<Share> shares { get; set; }
		#endregion
	}
}