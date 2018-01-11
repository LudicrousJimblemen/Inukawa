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

		EntityBaby baby = World.AddEntity<EntityBaby>("room");
		EntityEgg babyEgg = World.AddEntity<EntityEgg>("room");
		EntityBaseballBat babyBaseballBat = World.AddEntity<EntityBaseballBat>("room");

		baby.AddPossession(babyEgg);
		baby.AddPossession(babyBaseballBat);

		EntityDoor door = World.AddEntity<EntityDoor>("room");
		EntityBox box = World.AddEntity<EntityBox>("room");
		EntityKey key = World.AddEntity<EntityKey>("room");
		EntityEgg egg = World.AddEntity<EntityEgg>("room");

		door.Open = false;
		door.Locked = true;
		door.Key = key;

		box.Open = false;

		key.In = box;
		egg.In = box;

		// TODO: Remove Console
		Player.Parse("open the chest", this.Console);
		Player.Parse("take the egg from the chest", this.Console);
		Player.Parse("take the baby's egg", this.Console);
		Player.Parse("take the key", this.Console);
		Player.Parse("unlock the door with the key", this.Console);
		Player.Parse("open the door", this.Console);
		Player.Parse("eat the eggs", this.Console);
		Player.Parse("take the baby's baseball bat", this.Console);
	}
}
