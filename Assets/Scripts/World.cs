﻿using System;
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
	/// <param name="identity">The <see cref="Identity"/> of the new <see cref="Entity"/>.</param>
	/// <param name="location">The <see cref="Location"/> of the new <see cref="Entity"/>.</param>
	/// <returns>The new <see cref="Entity"/>.</returns>
	public static T AddEntity<T>(Identity identity = null, Location location = null) where T : Entity, new() {
		T newEntity = new T();

		Entities.Add(newEntity);

		newEntity.Identity = identity;
		newEntity.Location = location;
		
		newEntity.Initialize();

		return newEntity;
	}

	/// <summary>
	/// Gets a <see cref="Entity"/> which matches a predicate.
	/// </summary>
	/// <param name="predicate">The function by which to search for an <see cref="Entity"/>.</param>
	/// <returns>The found <see cref="Entity"/>.</returns>
	public static Entity GetEntity(Func<Entity, bool> predicate) {
		if (Entities.Any(predicate)) {
			return Entities.First(predicate);
		} else {
			throw new KeyNotFoundException();
		}
	}

	/// <summary>
	/// Gets <see cref="Entity"/>s which match a predicate.
	/// </summary>
	/// <param name="predicate">The function by which to search for <see cref="Entity"/>s.</param>
	/// <returns>The found <see cref="Entity"/>.</returns>
	public static List<Entity> GetEntities(Func<Entity, bool> predicate) {
		if (Entities.Any(predicate)) {
			return Entities.Where(predicate).ToList();
		} else {
			throw new KeyNotFoundException();
		}
	}

	/// <summary>
	/// Gets a <see cref="Entity"/> by its <see cref="Identity"/>'s name.
	/// </summary>
	/// <param name="name">The name of the <see cref="Identity"/> of the <see cref="Entity"/> to search for.</param>
	/// <returns>The found <see cref="Entity"/>.</returns>
	public static Entity GetEntityByName(string name) {
		Entity returned = Entities.First(x => x.Identity != null && x.Identity.Name.ToLowerInvariant() == name.ToLowerInvariant());
		if (returned != null) {
			return returned;
		} else {
			throw new KeyNotFoundException(name);
		}
	}
}