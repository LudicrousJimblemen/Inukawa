using System.Linq;

public class Location {
	public string Id;

	private static Location[] locations = new Location[] {
		new Location { Id = "house" },
		new Location { Id = "hell" }
	};

	public static Location Get(string id) {
		return locations.First(x => x.Id == id);
	}
}