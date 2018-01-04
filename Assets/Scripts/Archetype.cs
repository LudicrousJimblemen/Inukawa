using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Defines a type of <see cref="Entity"/>.
/// </summary>
public class Archetype {
	/// <summary>
	/// A unique identifier for this <see cref="Archetype"/>.
	/// </summary>
	public string Id;

	/// <summary>
	/// An <see cref="Archetype"/>, if any, which this <see cref="Archetype"/> inherits parts and actions from.
	/// </summary>
	public string InheritsFrom;

	/// <summary>
	/// A collection of <see cref="Archetype"/>s which are an immutable part of this <see cref="Archetype"/>.
	/// </summary>
	public string[] Parts;

	/// <summary>
	/// If this <see cref="Archetype"/> is an element of its location and is unable to be moved.
	/// </summary>
	public bool Static;

	/// <summary>
	/// Collection of actions which this <see cref="Archetype"/> may perform.
	/// </summary>
	public List<WorldAction> Actions;

	private static Archetype[] archetypes = new Archetype[] {
		new Archetype {
			Id = "object",
			InheritsFrom = null,
			Parts = null,
			Actions = new List<WorldAction> {
				new WorldAction {
					Id = "take",
					Function = (subject, direct, indirect) => {
						if (direct == null) {
							return false;
						} else {
							if (direct.PartOf == null) {
								subject.AddPossession(direct);
								return true;
							} else {
								return false;
							}
						}
					}
				},
				new WorldAction {
					Id = "give",
					Function = (subject, direct, indirect) => {
						if (direct == null) {
							return false;
						} else {
							if (direct.PartOf == null) {
								if (indirect == null) {
									return false;
								} else {
									indirect.AddPossession(direct);
									return true;
								}
							} else {
								return false;
							}
						}
					}
				}
			}
		},
		new Archetype {
			Id = "face",
			InheritsFrom = "object",
			Parts = new string[] { },
			Actions = new List<WorldAction> { }
		},
		new Archetype {
			Id = "elbow",
			InheritsFrom = "object",
			Parts = new string[] { },
			Actions = new List<WorldAction> { }
		},
		new Archetype {
			Id = "knee",
			InheritsFrom = "object",
			Parts = new string[] { },
			Actions = new List<WorldAction> { }
		},
		new Archetype {
			Id = "head",
			InheritsFrom = "object",
			Parts = new string[] { "face" },
			Actions = new List<WorldAction> { }
		},
		new Archetype {
			Id = "arm",
			InheritsFrom = "object",
			Parts = new string[] { "elbow" },
			Actions = new List<WorldAction> { }
		},
		new Archetype {
			Id = "leg",
			InheritsFrom = "object",
			Parts = new string[] { "knee" },
			Actions = new List<WorldAction> { }
		},
		new Archetype {
			Id = "humanoid",
			InheritsFrom = "object",
			Parts = new string[] { "leg", "leg", "arm", "arm", "head" },
			Actions = new List<WorldAction> { }
		},
		new Archetype {
			Id = "human",
			InheritsFrom = "humanoid",
			Parts = new string[] { },
			Actions = new List<WorldAction> { }
		},
		new Archetype {
			Id = "alien",
			InheritsFrom = "humanoid",
			Parts = new string[] { },
			Actions = new List<WorldAction> { }
		},
		new Archetype {
			Id = "food",
			InheritsFrom = "object",
			Parts = new string[] { },
			Actions = new List<WorldAction> { }
		},
		new Archetype {
			Id = "egg",
			InheritsFrom = "food",
			Parts = new string[] { },
			Actions = new List<WorldAction> { }
		}
	};

	/// <summary>
	/// Finds an <see cref="Archetype"/> by its id.
	/// </summary>
	/// <param name="id">The id of the archetype to search for.</param>
	/// <returns>The matching <see cref="Archetype"/> if any; null otherwise.</returns>
	public static Archetype Get(string id) {
		if (archetypes.Any(x => x.Id == id)) {
			return archetypes.First(x => x.Id == id);
		} else {
			return null;
		}
	}

	/// <summary>
	/// Finds an action performable by this archetype. 
	/// </summary>
	/// <param name="action">The id of the action to search for.</param>
	/// <returns>The matching action if any; null otherwise.</returns>
	public WorldAction GetAction(string action) {
		if (this.Actions.Any(x => x.Id == action)) {
			return this.Actions.First(x => x.Id == action);
		} else {
			if (this.InheritsFrom == null) {
				return null;
			} else {
				return Archetype.Get(this.InheritsFrom).GetAction(action);
			}
		}
	}
}