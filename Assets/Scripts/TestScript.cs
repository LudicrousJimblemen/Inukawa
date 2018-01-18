using System;
using UnityEngine;

public class TestScript : MonoBehaviour {
	public Console Console;
	
	private void Start() {
		EntityHuman player = World.AddEntity<EntityHuman>(new Identity {
			Name = "Inukawa",
			Cases = new Cases {
				NominativeSingular = "Inukawa",
				GenitiveSingular = "Inukawa's",
				NominativePlural = "Inukawas",
				GenitivePlural = "Inukawas'"
			}
		}, "room");

		InputHandler.Player = player;

		EntityBaby baby = World.AddEntity<EntityBaby>("room");
		EntityEgg babyEgg = World.AddEntity<EntityEgg>("room");
		EntitySandwich babySandwich = World.AddEntity<EntitySandwich>("room");
		EntityBaseballBat babyBaseballBat = World.AddEntity<EntityBaseballBat>("room");
		baby.AddPossession(babyEgg);
		baby.AddPossession(babyBaseballBat);
		baby.AddPossession(babySandwich);

		EntityDoor door = World.AddEntity<EntityDoor>("room");
		EntityBox box = World.AddEntity<EntityBox>("room");
		EntityKey key = World.AddEntity<EntityKey>("room");
		EntityEgg egg = World.AddEntity<EntityEgg>("room");
		EntitySandwich sandwich = World.AddEntity<EntitySandwich>("room");

		door.Open = false;
		door.Locked = true;
		door.Key = key;

		box.Open = false;

		box.AddPossession(egg);
		box.AddPossession(key);

		player.AddPossession(sandwich);
		
		Parse("open the box");
		Parse("take the egg from the box");
		Parse("take the key");
		Parse("unlock the door with the key");
		Parse("open the door");
		Parse("eat the egg");
		Parse("eat the sandwich");
		Parse("eat the eggs");
		Parse("take the baby's baseball bat");
	}

	private void Parse(string input) {
		ParseResult result = InputHandler.Parse(input, this.Console);
		
		switch (result.ResultType) {
			case ParseResultType.Success:
				Console.Write(String.Format("success"));
				break;
			case ParseResultType.ErrorUnpairedGenitive:
				Console.Write(String.Format("unpaired genitive: {0}", result.Tokens[result.Index].String));
				break;
			case ParseResultType.ErrorInvalidGenitive:
				Console.Write(String.Format("invalid genitive: none of ({0}) own all of ({1})", result.Tokens[result.Index - 1].PreviousEntityMatches.Flatten(", "), result.Tokens[result.Index].PreviousEntityMatches.Flatten(", ")));
				break;
			case ParseResultType.ErrorAmbiguousToken:
				Console.Write(String.Format("ambiguous '{0}': could be any of ({1})", result.Tokens[result.Index].String, result.Tokens[result.Index].EntityMatches.Flatten(", ")));
				break;
			default:
				break;
		}

		Console.Write();
	}
}
