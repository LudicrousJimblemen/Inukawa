using UnityEngine;
using UnityEditor;

public class TestScript : MonoBehaviour {
	public Console Console;
	
	private void Start() {
		Entity player = World.AddEntity("human", new Identity("Inukawa"), Location.Get("room"));
		Entity door = World.AddEntity("door", null, Location.Get("room"));
		Entity chest = World.AddEntity("chest", null, Location.Get("room"));
		Entity key = World.AddEntity("key", null, Location.Get("room"), new Position("under", chest));

		door.AddReference("key", key);

		player.Act("open", door);       // Will Not Work !!!!! Door Locked !!!!!
		player.Act("unlock", door);     // Will Not Work !!!!! No Key !!!!!
		player.Act("take", key);		// Will Not Work !!!!! Key Where ?????
		player.Act("open", chest);		// Ja Ja Ja Ja Ja
		player.Act("take", key, chest); // Ja Ja Ja Ja Ja
		player.Act("open", door);		// Will Not Work !!!!! Door Locked !!!!!
		player.Act("unlock", door);     // Ja Ja Ja Ja Ja
		player.Act("open", door);       // Ja Ja Ja Ja Ja
	}

	private const int square = 11;
	private float half = (square - 1) / 2f;

	private void OnDrawGizmos() {
		Gizmos.color = Color.white;
		for (int i = 0; i < World.Entities.Count; i++) {
			Gizmos.DrawSphere(new Vector3(i % square - half, i / square - half), 0.3f);
			Handles.Label(new Vector3(i % square - half - 0.2f, i / square - half - 0.2f), World.Entities[i].Archetype.Id);
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
			foreach (var reference in World.Entities[i].References) {
				int j = World.Entities.IndexOf(reference.Entity);
				Gizmos.DrawLine(new Vector3(i % square - half + 0.03f, i / square - half), new Vector3(j % square - half, j / square - half));
			}
		}
	}
}
