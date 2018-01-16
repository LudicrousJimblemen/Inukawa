using System.Collections.Generic;
using System.Linq;

public class Location {
	public string Id;

	private static Location[] locations = new Location[] {
		new Location { Id = "room" }
	};

	public static Location Get(string id) {
		if (locations.Any(x => x.Id == id)) {
			return locations.First(x => x.Id == id);
		} else {
			throw new KeyNotFoundException(id);
		}
	}
}