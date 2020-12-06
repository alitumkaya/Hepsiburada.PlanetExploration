namespace Hepsiburada.PlanetExploration.Domain.Shared
{
    public enum Command
    {
        Left = 90, Right = -90, Move = 0
    }
    public enum CompassPoint
    {
        E = 0, // East
        N = 90, // North
        W = 180, // West
        S = 270 // South
    }
}
