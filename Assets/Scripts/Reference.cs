/// <summary>
/// Defines a reference a <see cref="WorldObject"/> and another.
/// </summary>
public class Reference {
	/// <summary>
	/// The name of the reference.
	/// </summary>
	public string Alias;

	/// <summary>
	/// The <see cref="WorldObject"/> which this <see cref="Reference"/> references.
	/// </summary>
	public WorldObject Object;

	/// <summary>
	/// Creates a new <see cref="Reference"/>.
	/// </summary>
	/// <param name="alias">The name of the reference.</param>
	/// <param name="worldObject">The <see cref="WorldObject"/> which this <see cref="Reference"/> references.</param>
	public Reference(string alias, WorldObject worldObject) {
		this.Alias = alias;
		this.Object = worldObject;
	}
}