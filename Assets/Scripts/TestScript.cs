using UnityEngine;
using UnityEngine.UI;

public class TestScript : MonoBehaviour {
	public Text Text;

	private Entity player = World.AddEntity("human", new Identity("Inukawa"), Location.Get("house"));
	private Entity alien1 = World.AddEntity("alien", new Identity("Alien 1"), Location.Get("alien ship"));
	private Entity alien2 = World.AddEntity("alien", new Identity("Alien 2"), Location.Get("alien ship"));

	private void Start() {
		Add("whoa! cool!!! aliens!!!!!");

		player.MoveTo(Location.Get("alien ship"));

		Add("now you in the alien ship");
	}
	
	private void Add(string text) {
		Text.text += "\n\n" + text;
	}

	/*
	private void OnDrawGizmos() {
		const int square = 11;
		float half = (square - 1) / 2f;

		Gizmos.color = Color.white;
		for (int i = 0; i < World.Entities.Count; i++) {
			Gizmos.DrawSphere(new Vector3(i % square - half, i / square - half), 0.3f);
		}

		for (int i = 0; i < World.Entities.Count; i++) {
			Gizmos.color = Color.red;
			foreach (var part in World.Entities[i].Parts) {
				int j = World.Entities.IndexOf(part);
				Gizmos.DrawLine(new Vector3(i % square - half + 0.01f, i / square - half), new Vector3(j % square - half, j / square - half));
			}

			Gizmos.color = Color.green;
			foreach (var possession in World.Entities[i].Possessions) {
				int j = World.Entities.IndexOf(possession);
				Gizmos.DrawLine(new Vector3(i % square - half + 0.02f, i / square - half), new Vector3(j % square - half, j / square - half));
			}

			Gizmos.color = Color.blue;
			foreach (var relationship in World.Entities[i].Relationships) {
				int j = World.Entities.IndexOf(relationship.Value);
				Gizmos.DrawLine(new Vector3(i % square - half + 0.03f, i / square - half), new Vector3(j % square - half, j / square - half));
			}
		}
	}
	*/
}
