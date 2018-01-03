using System;
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
					Function = (subj, direct, indirect) => {
						throw new NotImplementedException("haha");
					}
				}
			}
		},
		new Archetype {
			Id = "ear",
			InheritsFrom = "object",
			Parts = new [] {"object"}
		},
		new Archetype {
			Id = "mouth",
			InheritsFrom = "object",
			Parts = new [] {"object"}
		},
		new Archetype {
			Id = "nose",
			InheritsFrom = "object",
			Parts = new [] {"object"}
		},
		new Archetype {
			Id = "eye",
			InheritsFrom = "object",
			Parts = new [] {"object"}
		},
		new Archetype {
			Id = "face",
			InheritsFrom = "object",
			Parts = new [] {"object"}
		},
		new Archetype {
			Id = "elbow",
			InheritsFrom = "object",
			Parts = new [] {"object"}
		},
		new Archetype {
			Id = "knee",
			InheritsFrom = "object",
			Parts = new [] {"object"}
		},
		new Archetype {
			Id = "head",
			InheritsFrom = "object",
			Parts = new [] {"object"}
		},
		new Archetype {
			Id = "arm",
			InheritsFrom = "object",
			Parts = new [] {"object"}
		},
		new Archetype {
			Id = "leg",
			InheritsFrom = "object",
			Parts = new [] {"object"}
		},
		new Archetype {
			Id = "human",
			InheritsFrom = "object",
			Parts = new [] {"object"}
		},
		new Archetype {
			Id = "king",
			InheritsFrom = "human",
			Parts = new [] {"human"}
		},
		new Archetype {
			Id = "queen",
			InheritsFrom = "human",
			Parts = new [] {"human"}
		},
		new Archetype {
			Id = "baby",
			InheritsFrom = "human",
			Parts = new [] {"human"}
		},
			new Archetype {
			Id = "food",
			InheritsFrom = "object",
			Parts = new [] {"object"}
		},
		new Archetype {
			Id = "egg",
			InheritsFrom = "food",
			Parts = new [] {"food"} }
		};

	public static Archetype Get(string id) {
		return archetypes.First(x => x.Id == id);
	}

	public Action<WorldObject, WorldObject, WorldObject> GetAction(string action) {
		throw new NotImplementedException("haha");
	}
}