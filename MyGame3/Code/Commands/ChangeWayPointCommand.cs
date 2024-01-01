using PathfindingDemo.Code.ECS;

namespace PathfindingDemo.Code.Commands;
public struct ChangeWayPointCommand : ICommand
{
	private readonly PathFindingComponent _component;
	private readonly Vector3 _newWaypoint;

	public ChangeWayPointCommand(PathFindingComponent component, Vector3 newWaypoint)
	{
		_component = component;
		_newWaypoint = newWaypoint;
	}

	public void Execute()
	{
		_component.Pathfinder.SetWaypoint(_newWaypoint);
	}
}
