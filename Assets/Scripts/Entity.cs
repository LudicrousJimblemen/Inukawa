using System;
using System.Collections.Generic;
using System.Linq;

public abstract class Entity {
	public abstract void Initialize();

	public Cases Cases;

	public Identity Identity;
	public Location Location;

	public List<Entity> Parts = new List<Entity>();
	public Entity PartOf = null;

	public List<Entity> Possessions = new List<Entity>();
	public Entity PossessionOf = null;
	
	public IEntityContainer In = null;

	public List<Reference> References = new List<Reference>();

	public Entity AddPart(Entity entity) {
		this.Parts.Add(entity);
		entity.PartOf = this;

		return entity;
	}

	public Entity AddPossession(Entity entity) {
		if (entity.PossessionOf != null) {
			entity.PossessionOf.RemovePossession(entity);
		}

		if (entity.In != null) {
			entity.In = null;
		}

		entity.PossessionOf = this;
		this.Possessions.Add(entity);

		return entity;
	}

	public void RemovePossession(Entity entity) {
		if (this.Possessions.Contains(entity)) {
			this.Possessions.Remove(entity);
		}
		if (entity.PossessionOf == this) {
			entity.PossessionOf = null;
		}
	}

	public void AddReference(string alias, Entity entity) {
		this.References.Add(new Reference(alias, entity));
	}

	public Entity GetReference(string alias) {
		if (this.References.Any(x => x.Alias == alias)) {
			return this.References.First(x => x.Alias == alias).Entity;
		} else {
			throw new KeyNotFoundException(alias);
		}
	}

	public bool Accessible(Entity to) {
		if (this.Location != to.Location) {
			return false;
		}
		
		if (this.In != null) {
			if (this.In is IEntityOpenable) {
				IEntityOpenable openable = this.In as IEntityOpenable;

				return openable.Open;
			} else {
				return true;
			}
		}

		if (this.PartOf == null) {
			return true;
		} else {
			return this.PartOf.Accessible(to);
		}
	}

	public bool Contains(Entity entity) {
		return this.Parts.Contains(entity) || this.Possessions.Contains(entity);
	}
}