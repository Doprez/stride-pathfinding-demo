using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stride.Engine;
public static class GameExtensions
{
	/// <summary>
	/// Shows the current FPS.
	/// </summary>
	/// <param name="game"></param>
	/// <returns></returns>
	public static float FPS(this Game game)
	{
		return game.UpdateTime.FramePerSecond;
	}

	public static float Deltatime(this Game game)
	{
		return (float)game.UpdateTime.Elapsed.TotalSeconds;
	}
}
