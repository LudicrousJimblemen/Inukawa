using System;
using System.Collections.Generic;
using System.Linq;

public class WorldObject {
	public string ArchetypeId;
	public Identity Identity;

	public Location Location;

	public List<WorldObject> Parts = new List<WorldObject>();
	public WorldObject PartOf = null;

	public List<WorldObject> Possessions = new List<WorldObject>();
	public WorldObject PossessionOf = null;

	public Dictionary<string, WorldObject> Relationships = new Dictionary<string, WorldObject>();

	public WorldObject(string archetype, Identity identity = null, Location location = null) {
		this.ArchetypeId = archetype;
		this.Identity = identity;

		this.Location = location;

		string currentArchetype = this.ArchetypeId;
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

	public WorldObject AddPart(WorldObject worldObject) {
		this.Parts.Add(worldObject);
		worldObject.PartOf = this;

		return worldObject;
	}

	public WorldObject GetPart(string archetype) {
		if (this.Parts.Any(x => x.ArchetypeId == archetype)) {
			return this.Parts.First(x => x.ArchetypeId == archetype);
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

	public List<WorldObject> GetParts(string archetype) {
		IEnumerable<WorldObject> found = new List<WorldObject>();

		found = found.Concat(this.Parts.Where(x => x.ArchetypeId == archetype));

		foreach (var part in this.Parts) {
			found = found.Concat(part.GetParts(archetype));
		}

		return found.ToList();
	}

	public WorldObject AddPossession(WorldObject worldObject) {
		if (worldObject.PossessionOf != null) {
			worldObject.PossessionOf.RemovePossession(worldObject);
		}

		worldObject.PossessionOf = this;
		this.Possessions.Add(worldObject);

		return worldObject;
	}

	public void RemovePossession(WorldObject worldObject) {
		if (this.Possessions.Contains(worldObject)) {
			this.Possessions.Remove(worldObject);
		}
		if (worldObject.PossessionOf == this) {
			worldObject.PossessionOf = null;
		}
	}

	public List<WorldObject> GetPossessions(string archetype) {
		return this.Possessions.Where(x => x.ArchetypeId == archetype).ToList();
	}

	public void AddRelationship(string alias, WorldObject worldObject) {
		this.Relationships.Add(alias, worldObject);
	}

	public WorldObject GetRelationship(string alias) {
		return this.Relationships.First(x => x.Key == alias).Value;
	}

	public void MoveTo(Location location) {
		this.Location = location;

		foreach (var part in this.Parts) {
			part.MoveTo(location);
		}
		foreach (var possession in Possessions) {
			possession.MoveTo(location);
		}
	}

	public void Act(string action, WorldObject directObject = null, WorldObject indirectObject = null) {
		Archetype.Get(this.ArchetypeId).GetAction(action).Function(this, directObject, indirectObject);
	}
}