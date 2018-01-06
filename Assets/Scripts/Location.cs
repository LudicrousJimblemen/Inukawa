using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Defines a location which <see cref="Entity"/>s may interact which each other in.
/// </summary>
public class Location {
	/// <summary>
	/// A unique identifier for this <see cref="Location"/>.
	/// </summary>
	public string Id;

	private static Location[] locations = new Location[] {
		new Location { Id = "room" }
	};

	/// <summary>
	/// Finds an <see cref="Location"/> by its id.
	/// </summary>
	/// <param name="id">The id of the location to search for.</param>
	/// <returns>The matching <see cref="Location"/>.</returns>
	public static Location Get(string id) {
		if (locations.Any(x => x.Id == id)) {
			return locations.First(x => x.Id == id);
		} else {
			throw new KeyNotFoundException(id);
		}
	}
}