using PathfindingDemo.Core.Navigation;

namespace PathfindingDemo.Code.ECS;
[DataContract(nameof(PathFindingComponent))]
[DefaultEntityComponentProcessor(typeof(PathFindingProcessor), ExecutionMode = ExecutionMode.Runtime)]
public class PathFindingComponent : EntityComponent
{

	public Pathfinder Pathfinder { get; set; }

}
