using Hepsiburada.PlanetExploration.Application;
using Hepsiburada.PlanetExploration.ApplicationContract;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Hepsiburada.PlanetExploration.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
           .AddApplicationServices()
           .BuildServiceProvider();

            var planetService = serviceProvider.GetService<IPlanetService>();

            await RunPlanetExplorerAsync(planetService);
        }

        private static async Task RunPlanetExplorerAsync(IPlanetService planetService)
        {
            Console.WriteLine(@"Please enter is the upper-right coordinates of the plateau, 
the lower-left coordinates are assumed to be 0, 0.");
            var input = Console.ReadLine();

            await planetService.SelectPlateauAsync(input);

            var roverCount = 2;
            for (int i = 1; i <= roverCount; i++)
            {
                Console.WriteLine(@"Please enter the position that made up of two integers and a letter separated by spaces, corresponding to the x
and y co-ordinates and the rover's orientation.");
                input = Console.ReadLine();
                var roverName = $"Rover {i}";
                await planetService.PlaceNewRoverOnPlateauAsync(roverName, input);

                Console.WriteLine(@"Please enter string of letter to control rover. The possible letters are 'L', 'R' and
'M'. 'L' and 'R' makes the rover spin 90 degrees left or right respectively, without moving from its
current spot. 'M' means move forward one grid point, and maintain the same heading.");
                input = Console.ReadLine();
                var finalCoordinate = await planetService.ControlRoverAsync(input);

                Console.WriteLine("Final Coordinate : " + finalCoordinate);
            }
        }
    }
}
