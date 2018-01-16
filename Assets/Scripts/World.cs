using System;
using System.Collections.Generic;
using System.Linq;

public static class World {
	public static List<Entity> Entities = new List<Entity>();

	public static T AddEntity<T>(Identity identity, Location location) where T : Entity, new() {
		T newEntity = new T();

		Entities.Add(newEntity);

		newEntity.Identity = identity;
		newEntity.Location = location;
		
		newEntity.Initialize();

		return newEntity;
	}

	public static T AddEntity<T>(Identity identity, string locationId) where T : Entity, new() {
		return AddEntity<T>(identity, Location.Get(locationId));
	}

	public static T AddEntity<T>(Identity identity) where T : Entity, new() {
		return AddEntity<T>(identity: identity, location: null);
	}

	public static T AddEntity<T>(string locationId) where T : Entity, new() {
		return AddEntity<T>(null, locationId);
	}

	public static T AddEntity<T>(Location location) where T : Entity, new() {
		return AddEntity<T>(null, location);
	}

	public static T AddEntity<T>() where T : Entity, new() {
		return AddEntity<T>(identity: null, location: null);
	}

	public static Entity GetEntity(Func<Entity, bool> predicate) {
		if (Entities.Any(predicate)) {
			return Entities.First(predicate);
		} else {
			throw new KeyNotFoundException();
		}
	}

	public static void RemoveEntity(Entity entity) {
		Entities.Remove(entity);
	}

	public static List<Entity> GetEntities(Func<Entity, bool> predicate) {
		if (Entities.Any(predicate)) {
			return Entities.Where(predicate).ToList();
		} else {
			throw new KeyNotFoundException();
		}
	}

	public static Entity GetEntityByName(string name) {
		Entity returned = Entities.First(x => x.Identity != null && x.Identity.Name.ToLowerInvariant() == name.ToLowerInvariant());
		if (returned != null) {
			return returned;
		} else {
			throw new KeyNotFoundException(name);
		}
	}
}