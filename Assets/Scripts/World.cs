using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// A manager encompassing every <see cref="WorldObject"/>.
/// </summary>
public static class World {
	/// <summary>
	/// A collection of every <see cref="WorldObject"/>.
	/// </summary>
	public static List<WorldObject> Objects = new List<WorldObject>();

	/// <summary>
	/// Creates a new <see cref="WorldObject"/>.
	/// </summary>
	/// <param name="archetype">The id of the <see cref="Archetype"/> of the new <see cref="WorldObject"/>.</param>
	/// <param name="identity">The <see cref="Identity"/> of the new object.</param>
	/// <param name="location">The <see cref="Location"/> of the new object.</param>
	/// <returns>The new <see cref="WorldObject"/>.</returns>
	public static WorldObject AddObject(string archetype, Identity identity = null, Location location = null) {
		WorldObject newObject = new WorldObject(archetype, identity, location);
		Objects.Add(newObject);

		return newObject;
	}

	/// <summary>
	/// Gets a <see cref="WorldObject"/> which matches a predicate.
	/// </summary>
	/// <param name="predicate">The function by which to search for an <see cref="WorldObject"/>.</param>
	/// <returns>The found <see cref="WorldObject"/> if any; null otherwise.</returns>
	public static WorldObject GetObject(Func<WorldObject, bool> predicate) {
		if (Objects.Any(predicate)) {
			return Objects.First(predicate);
		} else {
			return null;
		}
	}

	/// <summary>
	/// Gets <see cref="WorldObject"/>s which match a predicate.
	/// </summary>
	/// <param name="predicate">The function by which to search for <see cref="WorldObject"/>s.</param>
	/// <returns>The found <see cref="WorldObject"/>s if any; null otherwise.</returns>
	public static List<WorldObject> GetObjects(Func<WorldObject, bool> predicate) {
		if (Objects.Any(predicate)) {
			return Objects.Where(predicate).ToList();
		} else {
			return null;
		}
	}

	/// <summary>
	/// Gets a <see cref="WorldObject"/> by its <see cref="Archetype"/>.
	/// </summary>
	/// <param name="archetype">The id of the <see cref="Archetype"/> to search for.</param>
	/// <returns>The found <see cref="WorldObject"/> if any; null otherwise.</returns>
	public static WorldObject GetObjectByArchetype(string archetype) {
		return World.GetObject(x => x.Archetype.Id == archetype);
	}

	/// <summary>
	/// Gets <see cref="WorldObject"/>s by their <see cref="Archetype"/>.
	/// </summary>
	/// <param name="archetype">The id of the <see cref="Archetype"/> to search for.</param>
	/// <returns>The found <see cref="WorldObject"/>s if any; null otherwise.</returns>
	public static List<WorldObject> GetObjectsByArchetype(string archetype) {
		return World.GetObjects(x => x.Archetype.Id == archetype);
	}

	/// <summary>
	/// Gets a <see cref="WorldObject"/> by its <see cref="Identity"/>'s name.
	/// </summary>
	/// <param name="name">The name of the <see cref="Identity"/> of the <see cref="WorldObject"/> to search for.</param>
	/// <returns>The found <see cref="WorldObject"/> if any; null otherwise.</returns>
	public static WorldObject GetObjectByName(string name) {
		return Objects.First(x => x.Identity != null && x.Identity.Name.ToLowerInvariant() == name.ToLowerInvariant());
	}
}