using UnityEngine;
using UnityEngine.UI;

public class TestScript : MonoBehaviour {
	public Text Text;

	private WorldObject player = World.AddObject("human", new Identity("Inukawa"), Location.Get("house"));
	private WorldObject alien1 = World.AddObject("alien", new Identity("Alien 1"), Location.Get("alien ship"));
	private WorldObject alien2 = World.AddObject("alien", new Identity("Alien 2"), Location.Get("alien ship"));

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
	*/
}
