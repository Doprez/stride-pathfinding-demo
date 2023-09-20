using PathfindingDemo.Core.Navigation;
using PathfindingDemo.Code;
using System;
using PathfindingDemo.Code.Services;

namespace PathfindingDemo.Controllers;
public class UnitController : StartupScript, IUpdateController
{
	public Pathfinder Pathfinder { get; set; }

	private Random _random = new Random();
	private float _elapsedtime = 0;

	public override void Start()
	{
		if (Pathfinder == null)
			Pathfinder = Entity.Get<Pathfinder>();
		if (Pathfinder == null)
			throw new Exception("Pathfinder is null");

		RegisterToUnitManager();
	}

	public void Step()
	{
		Pathfinder.Move();
		Pathfinder.Rotate();
		DelayedWaypointChange();
	}

	/// <summary>
	/// Delay changing the units Waypoint every second
	/// </summary>
	private void DelayedWaypointChange()
	{
		_elapsedtime += (float)Game.UpdateTime.Elapsed.TotalSeconds;

		if (_elapsedtime >= .1f)
		{
			var newWaypoint = new Vector3(_random.Next(0, 100), 0, _random.Next(0, 100));
			Pathfinder.SetWaypoint(newWaypoint);
			_elapsedtime = 0;
		}
	}

	private void RegisterToUnitManager()
	{
		Services.GetService<UpdateService>().AddUpdatable(this);
	}
}
