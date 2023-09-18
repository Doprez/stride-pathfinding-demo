using PathfindingDemo.Code.Services;
using Stride.Engine;
using System.Linq;

namespace PathfindingDemo.Code.Extensions;
public static class GameExtensions
{
	public static void AddUpdateService(this Game game)
	{
		var mainScene = game.SceneSystem.SceneInstance.RootScene;

		var service = new Entity(nameof(UpdateService));
		service.Add(new UpdateService());

		//add the service to the scene
		mainScene.Entities.Add(service);

		//get all entities in scene that implement IUpdatable
		var updatables = mainScene.Entities.Where(x => x.GetComponent<IUpdateController>() != null);
		var updateService = service.Get<UpdateService>();
		foreach (var updatable in updatables)
		{
			updateService.AddUpdatable(updatable.GetComponent<IUpdateController>());
		}

		//register the UpdateService script to the Services of the game
		game.Services.AddService(service.Get<UpdateService>());
	}
}
