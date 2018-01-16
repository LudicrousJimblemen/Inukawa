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
		}, Location.Get("room"));

		Player.Human = player;

		EntityBaby baby1 = World.AddEntity<EntityBaby>("room");
		EntityBaby baby2 = World.AddEntity<EntityBaby>("room");
		EntityDog dog1 = World.AddEntity<EntityDog>("room");
		EntityDog dog2 = World.AddEntity<EntityDog>("room");
		EntityCat cat1 = World.AddEntity<EntityCat>("room");
		EntityEgg egg1 = World.AddEntity<EntityEgg>("room");
		EntityEgg egg2 = World.AddEntity<EntityEgg>("room");
		EntitySandwich sandwich1 = World.AddEntity<EntitySandwich>("room");

		baby1.AddPossession(dog1);
		baby2.AddPossession(dog2);
		dog2.AddPossession(egg1);
		cat1.AddPossession(egg2);

		Parse("kick the baby's dog's egg");
		Parse("kick the baby's cat's egg");
		Parse("kick the baby's sandwich");
		Parse("kick the baby's");
	}

	private void Parse(string input) {
		ParseResult result = Player.Parse(input, this.Console);

		//Console.Write(String.Format("unmatched genitive: {0}", result.Tokens[result.Index].String));

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
			default:
				break;
		}

		Console.Write();
	}
}
