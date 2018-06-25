using System;
using System.Collections.Generic;
using System.Text;

namespace Cleaning_Robot_Lib
{
    /// <summary>
    /// This class implement a barrier 
    /// </summary>
    class Barrier
    {
        //Enum the types of barrier that we have
        public enum type
        {
            C,
            @null
        }


        /// <summary>
        /// this method is use to se if in the space exist a barrier
        /// if we need to add more barriers we only need to update the method with another barrier and the enum
        /// </summary>
        /// <param name="space">what is in the space</param>
        /// <returns>True if a barrier exits</returns>
        public bool checkForBarrier(string space)
        {
            if ((space == type.C.ToString()))
            {
                return true;
            }
            else if (space == type.@null.ToString())
            {
                return true;
            }
            else
            {
                return false;
            }



        }

    }
}
