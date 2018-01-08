public interface IEntityLockable {
	bool Locked { get; set; }
	Entity Key { get; set; }
}