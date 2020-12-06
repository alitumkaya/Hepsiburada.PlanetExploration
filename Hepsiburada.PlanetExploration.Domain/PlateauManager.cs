using Hepsiburada.PlanetExploration.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hepsiburada.PlanetExploration.Domain
{
    public class PlateauManager
    {
        private Plateau _plateau;
        private Plateau Plateau
        {
            get
            {
                if (_plateau == null)
                    throw new ArgumentNullException(nameof(_plateau), $"First set object {nameof(_plateau)} using SetPlateau() method.");

                return _plateau;
            }
        }
        private int? _currentRoverIndex = null;
        private Rover _currentRover
        {
            get
            {
                if (_currentRoverIndex == null)
                    throw new Exception("This plateau has no rovers!");
                return Plateau.Rovers[(int)_currentRoverIndex];
            }
        }
        private readonly Dictionary<CompassPoint, Action> moveRoute = new Dictionary<CompassPoint, Action>();
        private int step = 1;

        private readonly Dictionary<Command, Action> commandRoute = new Dictionary<Command, Action>();
        private const int circleAngle = 360;
        private int headAngle => (circleAngle + (_currentRover.CurrentAngle % circleAngle)) % circleAngle;

        public PlateauManager()
        {
            moveRoute.Add(CompassPoint.E, () => { ValidateCommand(Plateau.CoordinateX, _currentRover.LocationX); _currentRover.MoveOnX(step); });
            moveRoute.Add(CompassPoint.N, () => { ValidateCommand(Plateau.CoordinateY, _currentRover.LocationY); _currentRover.MoveOnY(step); });
            moveRoute.Add(CompassPoint.W, () => { ValidateCommand(0, _currentRover.LocationX); _currentRover.MoveOnX(-1 * step); });
            moveRoute.Add(CompassPoint.S, () => { ValidateCommand(0, _currentRover.LocationY); _currentRover.MoveOnY(-1 * step); });

            commandRoute.Add(Command.Left, () => _currentRover.TurnLeft());
            commandRoute.Add(Command.Right, () => _currentRover.TurnRight());
            commandRoute.Add(Command.Move, () =>
            {
                moveRoute[(CompassPoint)headAngle].Invoke();
            });
        }
        public void SetPlateau(Plateau plateau)
        {
            _plateau = plateau;
        }
        private void ValidateCommand(int plateauCoordinate, int roverCoordinate)
        {
            if (plateauCoordinate == roverCoordinate)
                throw new Exception("The rover cannot be placed outside the boundaries of the plateau");
        }

        public async Task AddRoverAsync(Rover rover)
        {
            if ((rover.LocationX > Plateau.CoordinateX || rover.LocationX < 0)
                || (rover.LocationY > Plateau.CoordinateY || rover.LocationY < 0))
                throw new Exception("The rover cannot be placed outside the boundaries of the plateau.");

            Plateau.Rovers.Add(rover);

            if (_currentRoverIndex == null)
                _currentRoverIndex = 0;
            else
                _currentRoverIndex++;

            await Task.CompletedTask;
        }
        public async Task<string> RunCommandAsync(Command signal)
        {
            commandRoute[signal].Invoke();
            return $"{_currentRover.LocationX} {_currentRover.LocationY} {((CompassPoint)headAngle).ToString()}";
        }
        //public async Task SelectNextRoverAsync()
        //{
        //    if (!IsAviableNextRover())
        //        throw new Exception("There is no another rover!");
        //    _currentRoverIndex++;
        //}
        //public bool IsAviableNextRover()
        //{
        //    return (Plateau.Rovers.Count > _currentRoverIndex + 1);
        //}
    }
}
