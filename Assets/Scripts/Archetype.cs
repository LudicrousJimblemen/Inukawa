﻿using System;
using System.Collections.Generic;
using System.Linq;

public class Archetype {
	public string Id;
	public string InheritsFrom;
	public string[] Parts;
	public List<WorldAction> Actions;

	private static Archetype[] archetypes = new Archetype[] {
		new Archetype {
			Id = "object",
			InheritsFrom = null,
			Parts = null,
			Actions = new List<WorldAction> {
				new WorldAction {
					Name = "take",
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
				}
			}
		},
		new Archetype {
			Id = "ear",
			InheritsFrom = "object",
			Parts = new string[] { },
			Actions = new List<WorldAction> { }
		},
		new Archetype {
			Id = "mouth",
			InheritsFrom = "object",
			Parts = new string[] { },
			Actions = new List<WorldAction> { }
		},
		new Archetype {
			Id = "nose",
			InheritsFrom = "object",
			Parts = new string[] { },
			Actions = new List<WorldAction> { }
		},
		new Archetype {
			Id = "eye",
			InheritsFrom = "object",
			Parts = new string[] { },
			Actions = new List<WorldAction> { }
		},
		new Archetype {
			Id = "face",
			InheritsFrom = "object",
			Parts = new string[] { "nose", "mouth", "eye", "eye" },
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
			Parts = new string[] { "face", "ear", "ear" },
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
			Id = "human",
			InheritsFrom = "object",
			Parts = new string[] { "leg", "leg", "arm", "arm", "head" },
			Actions = new List<WorldAction> { }
		},
		new Archetype {
			Id = "king",
			InheritsFrom = "human",
			Parts = new string[] { },
			Actions = new List<WorldAction> { }
		},
		new Archetype {
			Id = "queen",
			InheritsFrom = "human",
			Parts = new string[] { },
			Actions = new List<WorldAction> { }
		},
		new Archetype {
			Id = "baby",
			InheritsFrom = "human",
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

	public static Archetype Get(string id) {
		return archetypes.First(x => x.Id == id);
	}

	public WorldAction GetAction(string action) {
		if (this.Actions.Any(x => x.Name == action)) {
			return this.Actions.First(x => x.Name == action);
		} else {
			if (this.InheritsFrom == null) {
				return null;
			} else {
				return Archetype.Get(this.InheritsFrom).GetAction(action);
			}
		}
	}
}