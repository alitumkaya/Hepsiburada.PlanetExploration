using Hepsiburada.PlanetExploration.Domain.Shared;

namespace Hepsiburada.PlanetExploration.Domain
{
    public class Rover
    {
        public int LocationX { get; private set; }

        public int LocationY { get; private set; }
        public string RoverName { get; }
        internal int CurrentAngle { get; private set; }

        public Rover(string roverName, int locationX, int locationY, int currentAngle)
        {

            LocationX = locationX;
            LocationY = locationY;
            RoverName = roverName;

            CurrentAngle = currentAngle;
        }
        private int ConvertCommandToAngle(Command signal)
        {
            return (int)signal;
        }
        internal void TurnLeft()
        {
            CurrentAngle += ConvertCommandToAngle(Command.Left);

        }
        internal void TurnRight()
        {
            CurrentAngle += ConvertCommandToAngle(Command.Right);

        }
        internal void MoveOnX(int step) => LocationX += step;
        internal void MoveOnY(int step) => LocationY += step;
    }
}
