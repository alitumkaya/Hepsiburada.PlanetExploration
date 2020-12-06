using Hepsiburada.PlanetExploration.ApplicationContract;
using Hepsiburada.PlanetExploration.Domain;
using Hepsiburada.PlanetExploration.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hepsiburada.PlanetExploration.Application
{
    public class PlanetService : IPlanetService
    {
        private readonly PlateauManager _plateauManager;

        private readonly Dictionary<string, Command> commands = new Dictionary<string, Command>()
        {
            {"L",Command.Left},
            {"R",Command.Right},
            {"M",Command.Move}
        };
        public PlanetService(PlateauManager plateauManager)
        {
            _plateauManager = plateauManager;
        }
        public async Task<string> ControlRoverAsync(string command)
        {
            var positionList = command.ToCharArray()
                .Where(w => !char.IsWhiteSpace(w))
                .Select(s => s.ToString())
                .ToList();

            if (positionList.Any(a => !commands.ContainsKey(a)))
                throw new Exception("The position is made up of two integers and a letter separated by spaces, corresponding to the x and y co-ordinates and the rover's orientation.");

            var finalCoordinate = "";
            positionList.ForEach(async (f) => finalCoordinate = await _plateauManager.RunCommandAsync(commands[f]));

            return finalCoordinate;
        }

        public async Task PlaceNewRoverOnPlateauAsync(string roverName, string position)
        {
            var positionList = position.Split(' ')
                .Where(w => !string.IsNullOrWhiteSpace(w))
                .ToList();

            int tryX = -1;
            int tryY = -1;
            CompassPoint compassPoint;
            if (positionList.Count != 3
                || !int.TryParse(positionList[0], out tryX)
                || !int.TryParse(positionList[1], out tryY)
                || !Enum.TryParse(positionList[2], out compassPoint)
                )
                throw new Exception("The position is made up of two integers and a letter separated by spaces, corresponding to the x and y co-ordinates and the rover's orientation.");
            await _plateauManager.AddRoverAsync(new Rover(roverName, tryX, tryY, (int)compassPoint));
        }

        //public async Task SelectNextRoverAsync()
        //{
        //    await _plateauManager.SelectNextRoverAsync();
        //}

        public async Task SelectPlateauAsync(string coordinates)
        {
            var coordinatesList = coordinates
                .ToCharArray()
                .Where(w => !char.IsWhiteSpace(w))
                .Select(s => s.ToString())
                .ToList();

            int tryX = -1;
            int tryY = -1;
            if (coordinatesList.Count != 2
                || !int.TryParse(coordinatesList[0], out tryX)
                || !int.TryParse(coordinatesList[1], out tryY)
                )
                throw new Exception("The coordinates is made up of two integers separated by spaces.");
            var plateau = new Plateau(tryX, tryY);

            _plateauManager.SetPlateau(plateau);
        }
    }
}
