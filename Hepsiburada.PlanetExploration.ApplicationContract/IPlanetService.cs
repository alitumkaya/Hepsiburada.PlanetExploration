using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hepsiburada.PlanetExploration.ApplicationContract
{
    public interface IPlanetService
    {
        /// <summary>
        /// Select plateau on planet
        /// </summary>
        /// <param name="coordinates"> 
        /// is the upper-right coordinates of the plateau, the lower-left coordinates are assumed to be 0,0.
        /// The coordinates is made up of two integers and a letter separated by spaces.
        /// Sample: X Y
        /// </param>
        /// <returns></returns>
        Task SelectPlateauAsync(string coordinates);

        /// <summary>
        /// Place a rover on selected plateau
        /// </summary>
        /// <param name="roverName">Rover name</param>
        /// <param name="position">The position is made up of two integers and a letter separated by spaces, corresponding to the x and y co-ordinates and the rover's orientation.</param>
        /// <returns></returns>
        Task PlaceNewRoverOnPlateauAsync(string roverName, string position);
        /// <summary>
        /// </summary>
        /// <param name="command">
        /// The possible letters are 'L', 'R' and
        ///'M'. 'L' and 'R' makes the rover spin 90 degrees left or right respectively, without moving from its
        ///current spot. 'M' means move forward one grid point, and maintain the same heading.
        /// </param>
        /// <returns>Rover final co-ordinates and heading</returns>
        Task<string> ControlRoverAsync(string command);
        //Task SelectNextRoverAsync();
    }
}
