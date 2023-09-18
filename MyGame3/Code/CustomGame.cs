using PathfindingDemo.Code.Extensions;

namespace PathfindingDemo.Code;
public class CustomGame : Game
{
	protected override void BeginRun()
	{
		base.BeginRun();
		this.AddUpdateService();
	}
}
