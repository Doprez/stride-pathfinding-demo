using Stride.Core.Annotations;
using System.Collections.Generic;
using System;
using System.Linq;

namespace PathfindingDemo.Code.ECS;
public class PathFindingProcessor : EntityProcessor<PathFindingComponent>
{
	public InstanceComponent InstanceComponent { get; set; }

	private readonly Random _random = new();
	private float _elapsedTime = 0;

	private int _count => ComponentDatas.Values.Count;
	private int _currentBatch = 0;
	private int _maxBatch = 10000;

	public override void Update(GameTime time)
	{
		var components = ComponentDatas.Values;
		UpdateAll(components, time);
		//UpdateBatch(components, time);
	}

	private void UpdateAll(Dictionary<PathFindingComponent, PathFindingComponent>.ValueCollection components, GameTime time)
	{
		foreach (var component in ComponentDatas.Values)
		{
			component.Pathfinder.Move();
			component.Pathfinder.Rotate();
			DelayedWaypointChange(component, time);
		}
	}

	private void UpdateBatch(Dictionary<PathFindingComponent, PathFindingComponent>.ValueCollection components, GameTime time)
	{
		// Get the next batch of controllers
		var batch = components.ToList().GetRange(_currentBatch, Math.Min(_maxBatch, _count - _currentBatch));

		// Update each controller in the batch
		foreach (var component in batch)
		{
			component.Pathfinder.Move();
			component.Pathfinder.Rotate();
			DelayedWaypointChange(component, time);
		}

		if (_currentBatch >= _count)
		{
			_currentBatch = 0;
		}
		else
		{
			_currentBatch += Math.Min(_maxBatch, _count - _currentBatch);
		}
	}

	private void UpdateBatch(PathFindingComponent[] components, GameTime time)
	{
	}

	protected override void OnEntityComponentAdding(Entity entity, [NotNull] PathFindingComponent component, [NotNull] PathFindingComponent data)
	{
		base.OnEntityComponentAdding(entity, component, data);
	}

	/// <summary>
	/// An Async method to delay changing the units Waypoint every second
	/// </summary>
	private void DelayedWaypointChange(PathFindingComponent component, GameTime time)
	{
		_elapsedTime += (float)time.Elapsed.TotalSeconds;

		if (_elapsedTime >= 1f)
		{
			var newWaypoint = new Vector3(_random.Next(0, 100), 0, _random.Next(0, 100));
			component.Pathfinder.SetWaypoint(newWaypoint);
			_elapsedTime = 0;
		}
	}

}
