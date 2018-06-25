using System;
using System.Collections.Generic;
using System.Text;

namespace Cleaning_Robot_Lib
{
    /// <summary>
    /// Object to represent the map where the robot is going to move
    /// </summary>
    class Map
    {
        /// <summary>
        /// x axis
        /// </summary>
        public int x { get; set; }
        /// <summary>
        /// y axis
        /// </summary>
        public int y { get; set; }
        /// <summary>
        /// what is on the space
        /// </summary>
        public string space { get; set; }
        /// <summary>
        /// where is facing the robot
        /// </summary>
        public string facing { get; set; }
        /// <summary>
        /// Flag to see is the robot visit the space
        /// </summary>
        public bool visited { get; set; }
        /// <summary>
        /// Flag to indicate where the robot when out of battery
        /// </summary>
        public bool outBattery { get; set; }
        /// <summary>
        /// Flag to see is the robot clean the space
        /// </summary>
        public bool cleaned { get; set; }

    }
}
