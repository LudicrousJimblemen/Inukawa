/// <summary>
/// Defines a position of an <see cref="global::Entity"/> relative to another <see cref="global::Entity"/>.
/// </summary>
public class Position {
	/// <summary>
	/// The position of the <see cref="global::Entity"/> with regards to <see cref="OtherEntity"/>.
	/// </summary>
	public string RelativePosition;

	/// <summary>
	/// The <see cref="Entity"/> which this <see cref="Position"/> is relative to.
	/// </summary>
	public Entity OtherEntity;

	/// <summary>
	/// Creates a new <see cref="Position"/>.
	/// </summary>
	/// <param name="relativePosition"></param>
	/// <param name="otherEntity"></param>
	public Position(string relativePosition, Entity otherEntity) {
		this.RelativePosition = relativePosition;
		this.OtherEntity = otherEntity;
	}
}