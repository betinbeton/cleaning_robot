using System;
using System.Collections.Generic;
using System.Text;

namespace Cleaning_Robot_Lib
{
    /// <summary>
    /// This class is to generate the Commands that we have, if we reuse the class we wil have the values for other projects
    /// </summary>
    class Commads
    {

        public Commads()
        {


        }

        /// <summary>
        /// /Enumeration of the commands that the robot can do
        /// with the value of unit of the battery that consume
        /// </summary>
        public enum cmds
        {
            TL,
            TR,
            A,
            B,
            C
        }

    }
}
