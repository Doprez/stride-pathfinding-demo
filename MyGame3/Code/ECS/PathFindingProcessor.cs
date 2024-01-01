using Stride.Core.Annotations;
using System.Collections.Generic;
using System;
using Stride.Core.Threading;
using PathfindingDemo.Code.Commands;

namespace PathfindingDemo.Code.ECS;
public class PathFindingProcessor : EntityProcessor<PathFindingComponent>
{
	public InstanceComponent InstanceComponent { get; set; }

	private readonly Random _random = new();
	private float _elapsedTime = 0;
	 
	private List<PathFindingComponent> _components = new();
	private readonly ConcurrentCollector<ICommand> _commands = new();

	public override void Update(GameTime time)
	{
		UpdateAgents(time);
	}

	private void UpdateAgents(GameTime time)
	{
		Dispatcher.For(0, _components.Count, i =>
		{
			_components[i].Pathfinder.Move();
			_components[i].Pathfinder.Rotate();
			DelayedWaypointChange(_components[i], time);
		});

		_commands.Close();
		for (var i = 0; i < _commands.Count; i++)
		{
			_commands[i].Execute();
		}
		_commands.Clear(true);

		//for(int i = 0; i < _count; i++)
		//{
		//	_components[i].Pathfinder.Move();
		//	_components[i].Pathfinder.Rotate();
		//	DelayedWaypointChange(_components[i], time);
		//}
	}

	protected override void OnEntityComponentAdding(Entity entity, [NotNull] PathFindingComponent component, [NotNull] PathFindingComponent data)
	{
		_components.Add(component);
	}

	/// <summary>
	/// An Async method to delay changing the units Waypoint every second
	/// </summary>
	private void DelayedWaypointChange(PathFindingComponent component, GameTime time)
	{
		_elapsedTime += (float)time.Elapsed.TotalSeconds;

		if (_elapsedTime >= 1f && !component.Pathfinder.HasPath)
		{
			var newWaypoint = new Vector3(Random.Shared.Next(-100, 100), 0, Random.Shared.Next(-100, 100));
			_commands.Add(new ChangeWayPointCommand(component, newWaypoint));
			_elapsedTime = 0;
		}
	}
}
