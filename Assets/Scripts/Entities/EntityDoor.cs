public class EntityDoor : EntityObject, IEntityOpenable, IEntityLockable {
	public Entity Key { get; set; }

	public bool Open { get; set; }

	public bool Locked { get; set; }
}