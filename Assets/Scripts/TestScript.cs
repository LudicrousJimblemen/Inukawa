using UnityEngine;

public class TestScript : MonoBehaviour {
	public Console Console;
	
	private void Start() {
		EntityHuman player = World.AddEntity<EntityHuman>(new Identity("Inukawa"), Location.Get("room"));
		EntityDoor door = World.AddEntity<EntityDoor>(null, Location.Get("room"));
		EntityChest chest = World.AddEntity<EntityChest>(null, Location.Get("room"));
		EntityKey key = World.AddEntity<EntityKey>(null, Location.Get("room"));
		EntityEgg egg = World.AddEntity<EntityEgg>(null, Location.Get("room"));

		door.Open = false;
		door.Locked = true;
		door.Key = key;

		chest.Open = false;

		key.In = chest;
		egg.In = chest;

		player.Open(chest);
		player.Take(key, chest);
		player.Take(egg, chest);
		player.Unlock(door, key);
		player.Open(door);
		player.Eat(egg);
	}
}
