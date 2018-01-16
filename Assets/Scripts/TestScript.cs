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

		Player.Parse("kick the baby's dog's egg", this.Console);
		Player.Parse("kick the baby's cat's egg", this.Console);
		Player.Parse("kick the baby's sandwich", this.Console);

		/*
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
		*/
	}
}
