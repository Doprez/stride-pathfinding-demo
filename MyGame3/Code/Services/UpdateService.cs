using System;
using System.Collections.Generic;

namespace PathfindingDemo.Code.Services;
public class UpdateService : SyncScript
{
	/// <summary>
	/// This is to allow world positions to register in Stride
	/// </summary>
	public int FrameToStartUpdating = 1;
	public int CurrentFrame = 0;

	/// <summary>
	/// Probably shouldn't go past 1000 without further optimizations
	/// </summary>
	public int MaxBatch { get; set; } = 1000;

	public int UnitCount => _updatables.Count;

	private int _currentBatch = 0;
	private List<IUpdateController> _updatables = new();

	public override void Start()
	{
		base.Start();
	}

	public override void Update()
	{
		if (CurrentFrame > FrameToStartUpdating)
		{
			//DebugText.Print($"Units: {UnitCount} FPS: {Game.DrawTime.FramePerSecond}", new Int2(50, 50));
			UpdateBatch();
			//UpdateAll();
		}
		else
		{
			CurrentFrame++;
		}
	}

	private void UpdateBatch()
	{
		// Get the next batch of controllers
		var batch = _updatables.GetRange(_currentBatch, Math.Min(MaxBatch, UnitCount - _currentBatch));

		// Update each controller in the batch
		foreach (var controller in batch)
		{
			controller.Step();
		}

		if (_currentBatch >= UnitCount)
		{
			_currentBatch = 0;
		}
		else
		{
			_currentBatch += Math.Min(MaxBatch, UnitCount - _currentBatch);
		}
	}

	private void UpdateAll()
	{
		// Update each controller in the batch
		foreach (var controller in _updatables)
		{
			controller.Step();
		}
	}

	public void AddUpdatable(IUpdateController updatable)
	{
		_updatables.Add(updatable);
	}
}
