﻿using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Represents an entity in the <see cref="World"/>.
/// </summary>
public abstract class Entity {
	/// <summary>
	/// Initializes this <see cref="Entity"/>.
	/// </summary>
	public virtual void Initialize() { }

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
	/// A specific position in the <see cref="Location"/> where this <see cref="Entity"/> is located, if any.
	/// </summary>
	public Position Position = null;

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
	/// <returns>The found <see cref="Entity"/>. </returns>
	public Entity GetReference(string alias) {
		if (this.References.Any(x => x.Alias == alias)) {
			return this.References.First(x => x.Alias == alias).Entity;
		} else {
			throw new KeyNotFoundException(alias);
		}
	}
}