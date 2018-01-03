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

	private void OnDrawGizmos() {
		const int square = 11;
		float half = (square - 1) / 2f;

		Gizmos.color = Color.white;
		for (int i = 0; i < World.Objects.Count; i++) {
			Gizmos.DrawSphere(new Vector3(i % square - half, i / square - half), 0.3f);
		}

		for (int i = 0; i < World.Objects.Count; i++) {
			Gizmos.color = Color.red;
			foreach (var part in World.Objects[i].Parts) {
				int j = World.Objects.IndexOf(part);
				Gizmos.DrawLine(new Vector3(i % square - half + 0.01f, i / square - half), new Vector3(j % square - half, j / square - half));
			}

			Gizmos.color = Color.green;
			foreach (var possession in World.Objects[i].Possessions) {
				int j = World.Objects.IndexOf(possession);
				Gizmos.DrawLine(new Vector3(i % square - half + 0.02f, i / square - half), new Vector3(j % square - half, j / square - half));
			}

			Gizmos.color = Color.blue;
			foreach (var relationship in World.Objects[i].Relationships) {
				int j = World.Objects.IndexOf(relationship.Value);
				Gizmos.DrawLine(new Vector3(i % square - half + 0.03f, i / square - half), new Vector3(j % square - half, j / square - half));
			}
		}
	}
}
