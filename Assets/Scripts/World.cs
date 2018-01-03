using System;
using System.Collections.Generic;
using System.Linq;

public static class World {
	public static List<WorldObject> Objects = new List<WorldObject>();

	public static WorldObject AddObject(string archetype, Identity identity = null) {
		WorldObject newObject = new WorldObject(archetype, identity);
		Objects.Add(newObject);

		return newObject;
	}

	public static WorldObject GetObject(Func<WorldObject, bool> predicate) {
		return Objects.First(predicate);
	}

	public static List<WorldObject> GetObjects(Func<WorldObject, bool> predicate) {
		return Objects.Where(predicate).ToList();
	}

	public static WorldObject GetObjectByArchetype(string archetype) {
		return Objects.First(x => x.ArchetypeId == archetype);
	}

	public static List<WorldObject> GetObjectsByArchetype(string archetype) {
		return Objects.Where(x => x.ArchetypeId == archetype).ToList();
	}

	public static WorldObject GetObjectByName(string name) {
		return Objects.First(x => x.Identity != null && x.Identity.Name.ToLowerInvariant() == name.ToLowerInvariant());
	}

	public static List<WorldObject> GetObjectsByName(string name) {
		return Objects.Where(x => x.Identity != null && x.Identity.Name.ToLowerInvariant() == name.ToLowerInvariant()).ToList();
	}
}