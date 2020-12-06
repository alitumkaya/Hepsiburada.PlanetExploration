using System.Collections.Generic;

namespace Hepsiburada.PlanetExploration.Domain
{
    public class Plateau
    {
        public int CoordinateX { get; }

        public int CoordinateY { get; }

        internal List<Rover> Rovers { get; }
        public Plateau(int coordinateX, int coordinateY)
        {
            CoordinateX = coordinateX;
            CoordinateY = coordinateY;
            Rovers = new List<Rover>();
        }
    }
}
