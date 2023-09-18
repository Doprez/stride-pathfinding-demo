using Navigation;
using PathfindingDemo.Code;
using PathfindingDemo.Code.Services;
using PathfindingDemo.Core.Navigation;
using System;
using System.Linq;

namespace PathfindingDemo.Controllers;
public class InstancedUnitController : StartupScript, IUpdateController
{
	public Pathfinder Pathfinder { get; set; }
	public InstanceComponent InstanceComponent { get; set; }

	private readonly Random _random = new();
	private float _elapsedTime = 0;

	public override void Start()
	{
		if (Pathfinder == null)
			Pathfinder = Entity.Get<Pathfinder>();
		if (Pathfinder == null)
			throw new Exception("Pathfinder is null");
		if(InstanceComponent == null)
			InstanceComponent = Entity.Get<InstanceComponent>();
		if (InstanceComponent == null)
			throw new Exception("InstanceComponent is null");

		RegisterToUnitManager();
		AddInstanceModel();
	}

	public void Step()
	{
		Pathfinder.Move();
		Pathfinder.Rotate();
		DelayedWaypointChange();
	}

	/// <summary>
	/// An Async method to delay changing the units Waypoint every second
	/// </summary>
	private void DelayedWaypointChange()
	{
		_elapsedTime += (float)Game.UpdateTime.Elapsed.TotalSeconds;

		if (_elapsedTime >= .1f)
		{
			var newWaypoint = new Vector3(_random.Next(0, 100), 0, _random.Next(0, 100));
			Pathfinder.SetWaypoint(newWaypoint);
			_elapsedTime = 0;
		}
	}

	private void RegisterToUnitManager()
	{
		Services.GetService<UpdateService>().AddUpdatable(this);
	}

	private void AddInstanceModel()
	{
		var scene = SceneSystem.SceneInstance.RootScene;
		var instancingComponent = scene.Entities.Where(x => x.Name == "Trogladyte").FirstOrDefault().Get<InstancingComponent>();

		InstanceComponent.Master = instancingComponent;
	}
}
