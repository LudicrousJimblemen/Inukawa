using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour {
	private void Start() {
		WorldObject king = World.AddObject("king", new Identity("Zimb"));
		WorldObject queen = World.AddObject("queen", new Identity("Ack"));
		WorldObject baby = World.AddObject("baby", new Identity("Bab"));

		king.AddRelationship("son", baby);
		king.AddRelationship("wife", queen);

		queen.AddRelationship("son", baby);
		queen.AddRelationship("husband", king);

		baby.AddRelationship("father", king);
		baby.AddRelationship("mother", queen);

		WorldObject egg = World.AddObject("egg");

		baby.AddPossession(egg);

		king.Act("take", egg, baby);
	}
}
