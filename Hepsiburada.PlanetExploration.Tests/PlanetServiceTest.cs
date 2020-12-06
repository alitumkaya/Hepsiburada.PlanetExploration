using Hepsiburada.PlanetExploration.Application;
using Hepsiburada.PlanetExploration.ApplicationContract;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Hepsiburada.PlanetExploration.Tests
{
    [TestClass]
    public class PlanetServiceTest
    {
        private readonly IPlanetService planetService;
        public PlanetServiceTest()
        {
            var serviceProvider = new ServiceCollection()
           .AddApplicationServices()
           .BuildServiceProvider();

            planetService = serviceProvider.GetService<IPlanetService>();
        }
        [TestMethod]
        [ExpectedException(typeof(Exception),
    "The rover cannot be placed outside the boundaries of the plateau.")]
        public async Task PlanetServicePlateauBoundariesTest()
        {
            await planetService.SelectPlateauAsync("5 5");

            await planetService.PlaceNewRoverOnPlateauAsync("Rover 1", "6 2 N"); // Plateau boundaries test
        }

        [TestMethod]
        [ExpectedException(typeof(Exception),
    "The position is made up of two integers and a letter separated by spaces, corresponding to the x and y co-ordinates and the rover's orientation.")]
        public async Task PlanetServiceRoverPositionTest()
        {
            await planetService.SelectPlateauAsync("5 5");

            await planetService.PlaceNewRoverOnPlateauAsync("Rover 1", "6 2 T"); // Rover position test
        }
        [TestMethod]
        public async Task PlanetServiceTestMethod()
        {
            await planetService.SelectPlateauAsync("5 5");

            await planetService.PlaceNewRoverOnPlateauAsync("Rover 1", "1 2 N");
            var rover1Position = await planetService.ControlRoverAsync("LMLMLMLMM");

            Assert.AreEqual(rover1Position, "1 3 N");

            await planetService.PlaceNewRoverOnPlateauAsync("Rover 2", "3 3 E");
            var rover2Position = await planetService.ControlRoverAsync("MMRMMRMRRM");

            Assert.AreEqual(rover2Position, "5 1 E");
        }
    }
}
