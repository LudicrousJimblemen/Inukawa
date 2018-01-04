/// <summary>
/// Defines a reference between a <see cref="global::Entity"/> and another.
/// </summary>
public class Reference {
	/// <summary>
	/// The name of the reference.
	/// </summary>
	public string Alias;

	/// <summary>
	/// The <see cref="global::Entity"/> which this <see cref="Reference"/> references.
	/// </summary>
	public Entity Entity;

	/// <summary>
	/// Creates a new <see cref="Reference"/>.
	/// </summary>
	/// <param name="alias">The name of the reference.</param>
	/// <param name="entity">The <see cref="global::Entity"/> which this <see cref="Reference"/> references.</param>
	public Reference(string alias, Entity entity) {
		this.Alias = alias;
		this.Entity = entity;
	}
}