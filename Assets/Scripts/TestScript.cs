using UnityEngine;
using UnityEditor;

public class TestScript : MonoBehaviour {
	public Console Console;
	
	private void Start() {
		EntityHuman player = World.AddEntity<EntityHuman>(new Identity("Inukawa"), Location.Get("room"));
		EntityDoor door = World.AddEntity<EntityDoor>(null, Location.Get("room"));
		EntityChest chest = World.AddEntity<EntityChest>(null, Location.Get("room"));
		EntityKey key = World.AddEntity<EntityKey>(null, Location.Get("room"), new Position("under", chest));
		
		player.Open(door); // Will Not Work !!!!! Door Locked !!!!!
		player.Unlock(door, null); // Will Not Work !!!!! No Key !!!!!
		player.Take(key); // Will Not Work !!!!! Key Where ?????
		player.Open(chest); // Ja Ja Ja Ja Ja
		player.Take(key, chest); // Ja Ja Ja Ja Ja
		player.Open(door); // Will Not Work !!!!! Door Locked !!!!!
		player.Unlock(door, key); // Ja Ja Ja Ja Ja
		player.Open(door); // Ja Ja Ja Ja Ja
	}

	private const int square = 11;
	private float half = (square - 1) / 2f;

	private void OnDrawGizmos() {
		Gizmos.color = Color.white;
		for (int i = 0; i < World.Entities.Count; i++) {
			Gizmos.DrawSphere(new Vector3(i % square - half, i / square - half), 0.3f);
			// Handles.Label(new Vector3(i % square - half - 0.2f, i / square - half - 0.2f), World.Entities[i].Archetype.Id);
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
