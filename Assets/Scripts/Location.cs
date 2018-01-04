using System.Linq;

/// <summary>
/// Defines a location which <see cref="WorldObject"/>s may interact which each other in.
/// </summary>
public class Location {
	/// <summary>
	/// A unique identifier for this <see cref="Location"/>.
	/// </summary>
	public string Id;

	private static Location[] locations = new Location[] {
		new Location { Id = "house" },
		new Location { Id = "alien ship" }
	};

	/// <summary>
	/// Finds an <see cref="Location"/> by its id.
	/// </summary>
	/// <param name="id">The id of the location to search for.</param>
	/// <returns>The matching <see cref="Location"/> if any; null otherwise.</returns>
	public static Location Get(string id) {
		if (locations.Any(x => x.Id == id)) {
			return locations.First(x => x.Id == id);
		} else {
			return null;
		}
	}
}