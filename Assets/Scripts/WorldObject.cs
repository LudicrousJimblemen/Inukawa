using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Represents an object in the <see cref="World"/>.
/// </summary>
public class WorldObject {
	/// <summary>
	/// The <see cref="global::Archetype"/> of this <see cref="WorldObject"/>. 
	/// </summary>
	public Archetype Archetype;

	/// <summary>
	/// The <see cref="global::Identity"/> of this <see cref="WorldObject"/>.
	/// </summary>
	public Identity Identity;

	/// <summary>
	/// The <see cref="global::Location"/> of this <see cref="WorldObject"/>.
	/// </summary>
	public Location Location;

	/// <summary>
	/// A collection of <see cref="WorldObject"/>s which are immutable parts of this <see cref="WorldObject"/>.
	/// </summary>
	public List<WorldObject> Parts = new List<WorldObject>();

	/// <summary>
	/// The <see cref="WorldObject"/> which this <see cref="WorldObject"/> is a part of, if any.
	/// </summary>
	public WorldObject PartOf = null;

	/// <summary>
	/// A collection of <see cref="WorldObject"/> which this <see cref="WorldObject"/> possesses.
	/// </summary>
	public List<WorldObject> Possessions = new List<WorldObject>();
	
	/// <summary>
	/// The <see cref="WorldObject"/> which this <see cref="WorldObject"/> is a possession of.
	/// </summary>
	public WorldObject PossessionOf = null;

	/// <summary>
	/// A collection of <see cref="Reference"/>s this <see cref="WorldObject"/> has to other <see cref="WorldObject"/>s.
	/// </summary>
	public List<Reference> References = new List<Reference>();

	/// <summary>
	/// Creates a new <see cref="WorldObject"/>.
	/// </summary>
	/// <param name="archetype">The unique id of the archetype of this <see cref="WorldObject"/></param>
	/// <param name="identity">The <see cref="global::Identity"/> of this <see cref="WorldObject"/>.</param>
	/// <param name="location">The <see cref="global::Location"/> of this <see cref="WorldObject"/>.</param>
	public WorldObject(string archetype, Identity identity = null, Location location = null) {
		this.Archetype = Archetype.Get(archetype);
		this.Identity = identity;

		this.Location = location;

		string currentArchetype = this.Archetype.Id;
		while (currentArchetype != null) {
			if (Archetype.Get(currentArchetype).Parts != null) {
				foreach (var part in Archetype.Get(currentArchetype).Parts) {
					WorldObject newPart = World.AddObject(part, null, this.Location);

					this.AddPart(newPart);
				}
			}

			currentArchetype = Archetype.Get(currentArchetype).InheritsFrom;
		}
	}

	/// <summary>
	/// Adds a <see cref="WorldObject"/> as a part of this <see cref="WorldObject"/>.
	/// </summary>
	/// <param name="worldObject">The <see cref="WorldObject"/> to make a part of this <see cref="WorldObject"/>.</param>
	/// <returns>The <see cref="WorldObject"/> which is now a part of this <see cref="WorldObject"/>.</returns>
	public WorldObject AddPart(WorldObject worldObject) {
		this.Parts.Add(worldObject);
		worldObject.PartOf = this;

		return worldObject;
	}
	
	/// <summary>
	/// Gets a part of this <see cref="WorldObject"/> given its <see cref="Archetype"/>.
	/// </summary>
	/// <param name="archetype">The unique identifier of the <see cref="Archetype"/> of the part.</param>
	/// <returns>The found <see cref="WorldObject"/> if any; null otherwise.</returns>
	public WorldObject GetPart(string archetype) {
		if (this.Parts.Any(x => x.Archetype.Id == archetype)) {
			return this.Parts.First(x => x.Archetype.Id == archetype);
		} else {
			WorldObject found = null;
			foreach (var part in this.Parts) {
				WorldObject innerFound = part.GetPart(archetype);
				if (innerFound != null) {
					found = innerFound;
					break;
				}
			}

			return found;
		}
	}

	/// <summary>
	/// Gets parts of this <see cref="WorldObject"/> given their <see cref="Archetype"/>.
	/// </summary>
	/// <param name="archetype">The unique identifier of the <see cref="Archetype"/> of the parts.</param>
	/// <returns>The found <see cref="WorldObject"/>s if any; null otherwise.</returns>
	public List<WorldObject> GetParts(string archetype) {
		IEnumerable<WorldObject> found = new List<WorldObject>();

		found = found.Concat(this.Parts.Where(x => x.Archetype.Id == archetype));

		foreach (var part in this.Parts) {
			found = found.Concat(part.GetParts(archetype));
		}

		return found.Count() > 0 ? found.ToList() : null;
	}

	/// <summary>
	/// Adds a <see cref="WorldObject"/> as a possession of this <see cref="WorldObject"/>.
	/// </summary>
	/// <param name="worldObject">The <see cref="WorldObject"/> to make a possession of this <see cref="WorldObject"/>.</param>
	/// <returns>The <see cref="WorldObject"/> which is now a possession of this <see cref="WorldObject"/>.</returns>
	public WorldObject AddPossession(WorldObject worldObject) {
		if (worldObject.PossessionOf != null) {
			worldObject.PossessionOf.RemovePossession(worldObject);
		}

		worldObject.PossessionOf = this;
		this.Possessions.Add(worldObject);

		return worldObject;
	}

	/// <summary>
	/// Removes a <see cref="WorldObject"/> as a possession of this <see cref="WorldObject"/>.
	/// </summary>
	/// <param name="worldObject">The <see cref="WorldObject"/> to remove from the possession of this <see cref="WorldObject"/>.</param>
	public void RemovePossession(WorldObject worldObject) {
		if (this.Possessions.Contains(worldObject)) {
			this.Possessions.Remove(worldObject);
		}
		if (worldObject.PossessionOf == this) {
			worldObject.PossessionOf = null;
		}
	}

	/// <summary>
	/// Gets a possession of this <see cref="WorldObject"/> given its <see cref="Archetype"/>.
	/// </summary>
	/// <param name="archetype">The unique identifier of the <see cref="Archetype"/> of the possession.</param>
	/// <returns>The found <see cref="WorldObject"/> if any; null otherwise.</returns>
	public List<WorldObject> GetPossessions(string archetype) {
		return this.Possessions.Where(x => x.Archetype.Id == archetype).ToList();
	}

	/// <summary>
	/// Adds a <see cref="Reference"/> to this <see cref="WorldObject"/>.
	/// </summary>
	/// <param name="alias">The alias of the <see cref="Reference."/></param>
	/// <param name="worldObject">This <see cref="WorldObject"/> to reference.</param>
	public void AddReference(string alias, WorldObject worldObject) {
		this.References.Add(new Reference(alias, worldObject));
	}

	/// <summary>
	/// Gets a <see cref="WorldObject"/> from a <see cref="Reference"/>'s alias.
	/// </summary>
	/// <param name="alias">The alias of the <see cref="Reference"/>.</param>
	/// <returns>The found <see cref="WorldObject"/> if any; null otherwise. </returns>
	public WorldObject GetReference(string alias) {
		if (this.References.Any(x => x.Alias == alias)) {
			return this.References.First(x => x.Alias == alias).Object;
		} else {
			return null;
		}
	}

	/// <summary>
	/// Gets <see cref="WorldObject"/>s from their <see cref="Reference"/>s' aliases.
	/// </summary>
	/// <param name="alias">The alias of the <see cref="Reference"/>s.</param>
	/// <returns>The found <see cref="WorldObject"/>s if any; null otherwise. </returns>
	public List<WorldObject> GetReferences(string alias) {
		if (this.References.Any(x => x.Alias == alias)) {
			return this.References.Where(x => x.Alias == alias).Select(x => x.Object).ToList();
		} else {
			return null;
		}
	}

	/// <summary>
	/// Moves this <see cref="WorldObject"/> to another <see cref="global::Location"/>.
	/// </summary>
	/// <param name="location">The <see cref="global::Location"/> to move this <see cref="WorldObject"/> to.</param>
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
	/// <param name="directObject">The <see cref="WorldObject"/> to act upon, if any.</param>
	/// <param name="indirectObject">The secondary <see cref="WorldObject"/> affected, if any.</param>
	public void Act(string action, WorldObject directObject = null, WorldObject indirectObject = null) {
		this.Archetype.GetAction(action).Function(this, directObject, indirectObject);
	}
}