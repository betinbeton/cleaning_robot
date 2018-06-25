using System;
using System.Collections.Generic;
using System.Text;

namespace Cleaning_Robot_Lib
{
    /// <summary>
    /// Objet use to create Json Response
    /// final point that the robot visit
    /// </summary>
   public class Final
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string facing { get; set; }
    }
}
