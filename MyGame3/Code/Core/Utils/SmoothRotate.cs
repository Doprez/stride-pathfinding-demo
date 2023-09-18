namespace MagicAndMight.Code.Core.Utils;

[DataContract(nameof(SmoothRotate))]
[ComponentCategory("Utils")]
public class SmoothRotate : SyncScript
{
	public Entity EntityToFollow { get; set; }
	public Vector3 Speed { get; set; } = new Vector3(1, 1, 1);

	public override void Update()
	{
		var currentRotation = Entity.Transform.Rotation;
		EntityToFollow.Transform.GetWorldTransformation(out var _, out var otherRotation, out var _);

		Quaternion.Slerp(ref currentRotation, ref otherRotation, Speed.X * this.DeltaTime(), out var newRotation);

		Entity.Transform.Rotation = newRotation;
	}
}