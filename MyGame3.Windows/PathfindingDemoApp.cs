using PathfindingDemo.Code;

namespace PathfindingDemo
{
    class PathfindingDemoApp
    {
        static void Main(string[] args)
        {
            using (var game = new CustomGame())
            {
                game.Run();
            }
        }
    }
}
