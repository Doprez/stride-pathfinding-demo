using System;
using System.Threading.Tasks;

namespace PathfindingDemo.Code;
public class CreatePrefabAtRuntime : AsyncScript
{
	public Prefab PrefabToSpawn { get; set; }
	public int EntitiesToSpawn { get; set; }
	public Vector3 AreaToSpawnIn { get; set; }

	private readonly Random _random = new();

	public async override Task Execute()
	{
		await SpawnEntities();
	}

	public async Task SpawnEntities()
	{
		var prefab = PrefabToSpawn;

		for (int i = 0; i < EntitiesToSpawn; i++)
		{
			var instance = await Task.FromResult(prefab.Instantiate());

			instance[0].Transform.Position =
				new Vector3(
					_random.Next(-((int)AreaToSpawnIn.X), ((int)AreaToSpawnIn.X))
					, 0,
					_random.Next(-((int)AreaToSpawnIn.X), ((int)AreaToSpawnIn.X)));

			SceneSystem.SceneInstance.RootScene.Entities.Add(instance[0]);
		}
	}
}
