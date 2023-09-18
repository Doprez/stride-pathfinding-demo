namespace Stride.Physics;

[DataContract("CustomRaycast")]
[ComponentCategory("Physics")]
public class Raycast : StartupScript
{
	public CollisionFilterGroupFlags CollideWith;
	public float RaycastRange { get; set; } = 1;

	private Simulation _simulation;

	public override void Start()
	{
		_simulation = this.GetSimulation();
	}

	/// <summary>
	/// A Raycast method based on the example in the fps demo
	/// <para>Make sure you are using the actual rotating Entity otherwise you will waste hours like I did debuging a non issue</para>
	/// </summary>
	public HitResult RayCast(Entity entityPosition)
	{
		var raycastStart = entityPosition.Transform.WorldMatrix.TranslationVector;
		var forward = entityPosition.Transform.WorldMatrix.Forward;
		var raycastEnd = raycastStart + forward * RaycastRange;

		var result = _simulation.Raycast(raycastStart, raycastEnd, filterFlags: CollideWith);

		return result;
	}

	/// <summary>
	/// A Raycast method based on the example in the fps demo
	/// </summary>
	public HitResult RayCast(Entity entityPosition, Vector3 direction)
	{
		var raycastStart = entityPosition.Transform.WorldMatrix.TranslationVector;
		var raycastEnd = raycastStart + direction * RaycastRange;

		var result = _simulation.Raycast(raycastStart, raycastEnd, filterFlags: CollideWith);

		return result;
	}
}