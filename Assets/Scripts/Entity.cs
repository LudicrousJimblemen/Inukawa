using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Represents an entity in the <see cref="World"/>.
/// </summary>
public class Entity {
	/// <summary>
	/// The <see cref="global::Archetype"/> of this <see cref="Entity"/>. 
	/// </summary>
	public Archetype Archetype;

	/// <summary>
	/// The <see cref="global::Identity"/> of this <see cref="Entity"/>.
	/// </summary>
	public Identity Identity;

	/// <summary>
	/// The <see cref="global::Location"/> of this <see cref="Entity"/>.
	/// </summary>
	public Location Location;

	/// <summary>
	/// A collection of <see cref="Entity"/>s which are immutable parts of this <see cref="Entity"/>.
	/// </summary>
	public List<Entity> Parts = new List<Entity>();

	/// <summary>
	/// The <see cref="Entity"/> which this <see cref="Entity"/> is a part of, if any.
	/// </summary>
	public Entity PartOf = null;

	/// <summary>
	/// A collection of <see cref="Entity"/> which this <see cref="Entity"/> possesses.
	/// </summary>
	public List<Entity> Possessions = new List<Entity>();
	
	/// <summary>
	/// The <see cref="Entity"/> which this <see cref="Entity"/> is a possession of, if any.
	/// </summary>
	public Entity PossessionOf = null;

	/// <summary>
	/// A collection of <see cref="Reference"/>s this <see cref="Entity"/> has to other <see cref="Entity"/>s.
	/// </summary>
	public List<Reference> References = new List<Reference>();

	/// <summary>
	/// Creates a new <see cref="Entity"/>.
	/// </summary>
	/// <param name="archetype">The unique id of the archetype of this <see cref="Entity"/></param>
	/// <param name="identity">The <see cref="global::Identity"/> of this <see cref="Entity"/>.</param>
	/// <param name="location">The <see cref="global::Location"/> of this <see cref="Entity"/>.</param>
	public Entity(string archetype, Identity identity = null, Location location = null) {
		this.Archetype = Archetype.Get(archetype);

		this.Identity = identity;

		this.Location = location;

		string currentArchetype = this.Archetype.Id;
		while (currentArchetype != null) {
			if (Archetype.Get(currentArchetype).Parts != null) {
				foreach (var part in Archetype.Get(currentArchetype).Parts) {
					Entity newPart = World.AddEntity(part, null, this.Location);

					this.AddPart(newPart);
				}
			}

			currentArchetype = Archetype.Get(currentArchetype).InheritsFrom;
		}
	}

	/// <summary>
	/// Adds a <see cref="Entity"/> as a part of this <see cref="Entity"/>.
	/// </summary>
	/// <param name="entity">The <see cref="Entity"/> to make a part of this <see cref="Entity"/>.</param>
	/// <returns>The <see cref="Entity"/> which is now a part of this <see cref="Entity"/>.</returns>
	public Entity AddPart(Entity entity) {
		this.Parts.Add(entity);
		entity.PartOf = this;

		return entity;
	}
	
	/// <summary>
	/// Gets a part of this <see cref="Entity"/> given its <see cref="Archetype"/>.
	/// </summary>
	/// <param name="archetype">The unique identifier of the <see cref="Archetype"/> of the part.</param>
	/// <returns>The found <see cref="Entity"/> if any; null otherwise.</returns>
	public Entity GetPart(string archetype) {
		if (this.Parts.Any(x => x.Archetype.Id == archetype)) {
			return this.Parts.First(x => x.Archetype.Id == archetype);
		} else {
			Entity found = null;
			foreach (var part in this.Parts) {
				Entity innerFound = part.GetPart(archetype);
				if (innerFound != null) {
					found = innerFound;
					break;
				}
			}

			return found;
		}
	}

	/// <summary>
	/// Gets parts of this <see cref="Entity"/> given their <see cref="Archetype"/>.
	/// </summary>
	/// <param name="archetype">The unique identifier of the <see cref="Archetype"/> of the parts.</param>
	/// <returns>The found <see cref="Entity"/>s if any; null otherwise.</returns>
	public List<Entity> GetParts(string archetype) {
		IEnumerable<Entity> found = new List<Entity>();

		found = found.Concat(this.Parts.Where(x => x.Archetype.Id == archetype));

		foreach (var part in this.Parts) {
			found = found.Concat(part.GetParts(archetype));
		}

		return found.Count() > 0 ? found.ToList() : null;
	}

	/// <summary>
	/// Adds a <see cref="Entity"/> as a possession of this <see cref="Entity"/>.
	/// </summary>
	/// <param name="entity">The <see cref="Entity"/> to make a possession of this <see cref="Entity"/>.</param>
	/// <returns>The <see cref="Entity"/> which is now a possession of this <see cref="Entity"/>.</returns>
	public Entity AddPossession(Entity entity) {
		if (entity.PossessionOf != null) {
			entity.PossessionOf.RemovePossession(entity);
		}

		entity.PossessionOf = this;
		this.Possessions.Add(entity);

		return entity;
	}

	/// <summary>
	/// Removes a <see cref="Entity"/> as a possession of this <see cref="Entity"/>.
	/// </summary>
	/// <param name="entity">The <see cref="Entity"/> to remove from the possession of this <see cref="Entity"/>.</param>
	public void RemovePossession(Entity entity) {
		if (this.Possessions.Contains(entity)) {
			this.Possessions.Remove(entity);
		}
		if (entity.PossessionOf == this) {
			entity.PossessionOf = null;
		}
	}

	/// <summary>
	/// Gets a possession of this <see cref="Entity"/> given its <see cref="Archetype"/>.
	/// </summary>
	/// <param name="archetype">The unique identifier of the <see cref="Archetype"/> of the possession.</param>
	/// <returns>The found <see cref="Entity"/> if any; null otherwise.</returns>
	public List<Entity> GetPossessions(string archetype) {
		return this.Possessions.Where(x => x.Archetype.Id == archetype).ToList();
	}

	/// <summary>
	/// Adds a <see cref="Reference"/> to this <see cref="Entity"/>.
	/// </summary>
	/// <param name="alias">The alias of the <see cref="Reference."/></param>
	/// <param name="entity">This <see cref="Entity"/> to reference.</param>
	public void AddReference(string alias, Entity entity) {
		this.References.Add(new Reference(alias, entity));
	}

	/// <summary>
	/// Gets a <see cref="Entity"/> from a <see cref="Reference"/>'s alias.
	/// </summary>
	/// <param name="alias">The alias of the <see cref="Reference"/>.</param>
	/// <returns>The found <see cref="Entity"/> if any; null otherwise. </returns>
	public Entity GetReference(string alias) {
		if (this.References.Any(x => x.Alias == alias)) {
			return this.References.First(x => x.Alias == alias).Entity;
		} else {
			return null;
		}
	}

	/// <summary>
	/// Gets <see cref="Entity"/>s from their <see cref="Reference"/>s' aliases.
	/// </summary>
	/// <param name="alias">The alias of the <see cref="Reference"/>s.</param>
	/// <returns>The found <see cref="Entity"/>s if any; null otherwise. </returns>
	public List<Entity> GetReferences(string alias) {
		if (this.References.Any(x => x.Alias == alias)) {
			return this.References.Where(x => x.Alias == alias).Select(x => x.Entity).ToList();
		} else {
			return null;
		}
	}

	/// <summary>
	/// Moves this <see cref="Entity"/> to another <see cref="global::Location"/>.
	/// </summary>
	/// <param name="location">The <see cref="global::Location"/> to move this <see cref="Entity"/> to.</param>
	public void MoveTo(Location location) {
		this.Location = location;

		foreach (var part in this.Parts) {
			part.MoveTo(location);
		}
		foreach (var possession in Possessions) {
			possession.MoveTo(location);
		}
	}

	/// <summary>
	/// Executes an action.
	/// </summary>
	/// <param name="action">The unique id of the <see cref="WorldAction"/> to execute.</param>
	/// <param name="directObject">The <see cref="Entity"/> to act upon, if any.</param>
	/// <param name="indirectObject">The secondary <see cref="Entity"/> affected, if any.</param>
	/// <returns>True if successful, false otherwise.</returns>
	public bool Act(string action, Entity directObject = null, Entity indirectObject = null) {
		return this.Archetype.GetAction(action).Function(this, directObject, indirectObject);
	}
}