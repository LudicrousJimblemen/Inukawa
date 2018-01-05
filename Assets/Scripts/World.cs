using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// A manager encompassing every <see cref="Entity"/>.
/// </summary>
public static class World {
	/// <summary>
	/// A collection of every <see cref="Entity"/>.
	/// </summary>
	public static List<Entity> Entities = new List<Entity>();

	/// <summary>
	/// Creates a new <see cref="Entity"/>.
	/// </summary>
	/// <param name="archetype">The id of the <see cref="Archetype"/> of the new <see cref="Entity"/>.</param>
	/// <param name="identity">The <see cref="Identity"/> of the new <see cref="Entity"/>.</param>
	/// <param name="location">The <see cref="Location"/> of the new <see cref="Entity"/>.</param>
	/// <returns>The new <see cref="Entity"/>.</returns>
	public static Entity AddEntity(string archetype, Identity identity = null, Location location = null, Position position = null) {
		Entity newEntity = new Entity(archetype, identity, location, position);
		Entities.Add(newEntity);

		return newEntity;
	}

	/// <summary>
	/// Gets a <see cref="Entity"/> which matches a predicate.
	/// </summary>
	/// <param name="predicate">The function by which to search for an <see cref="Entity"/>.</param>
	/// <returns>The found <see cref="Entity"/> if any; null otherwise.</returns>
	public static Entity GetEntity(Func<Entity, bool> predicate) {
		if (Entities.Any(predicate)) {
			return Entities.First(predicate);
		} else {
			return null;
		}
	}

	/// <summary>
	/// Gets <see cref="Entity"/>s which match a predicate.
	/// </summary>
	/// <param name="predicate">The function by which to search for <see cref="Entity"/>s.</param>
	/// <returns>The found <see cref="Entity"/>s if any; null otherwise.</returns>
	public static List<Entity> GetEntities(Func<Entity, bool> predicate) {
		if (Entities.Any(predicate)) {
			return Entities.Where(predicate).ToList();
		} else {
			return null;
		}
	}

	/// <summary>
	/// Gets a <see cref="Entity"/> by its <see cref="Archetype"/>.
	/// </summary>
	/// <param name="archetype">The id of the <see cref="Archetype"/> to search for.</param>
	/// <returns>The found <see cref="Entity"/> if any; null otherwise.</returns>
	public static Entity GetEntityByArchetype(string archetype) {
		return World.GetEntity(x => x.Archetype.Id == archetype);
	}

	/// <summary>
	/// Gets <see cref="Entity"/>s by their <see cref="Archetype"/>.
	/// </summary>
	/// <param name="archetype">The id of the <see cref="Archetype"/> to search for.</param>
	/// <returns>The found <see cref="Entity"/>s if any; null otherwise.</returns>
	public static List<Entity> GetEntitiesByArchetype(string archetype) {
		return World.GetEntities(x => x.Archetype.Id == archetype);
	}

	/// <summary>
	/// Gets a <see cref="Entity"/> by its <see cref="Identity"/>'s name.
	/// </summary>
	/// <param name="name">The name of the <see cref="Identity"/> of the <see cref="Entity"/> to search for.</param>
	/// <returns>The found <see cref="Entity"/> if any; null otherwise.</returns>
	public static Entity GetEntityByName(string name) {
		return Entities.First(x => x.Identity != null && x.Identity.Name.ToLowerInvariant() == name.ToLowerInvariant());
	}
}