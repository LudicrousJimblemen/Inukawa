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

		EntityDoor door = World.AddEntity<EntityDoor>(null, Location.Get("room"));
		EntityBox box = World.AddEntity<EntityBox>(null, Location.Get("room"));
		EntityKey key = World.AddEntity<EntityKey>(null, Location.Get("room"));
		EntityEgg egg = World.AddEntity<EntityEgg>(null, Location.Get("room"));

		door.Open = false;
		door.Locked = true;
		door.Key = key;

		box.Open = false;

		key.In = box;
		egg.In = box;

		// TODO: Remove console
		Player.Parse("open the chest", this.Console);
		Player.Parse("take the key from the chest", this.Console);
		Player.Parse("take the egg", this.Console); // implicit egg in chest
		Player.Parse("unlock the door with the key", this.Console);
		Player.Parse("open the door", this.Console);
		Player.Parse("eat the egg", this.Console);
		Player.Parse("kick the knee", this.Console);
	}
}
