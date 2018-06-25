using System;
using System.Collections.Generic;
using System.Text;

namespace Cleaning_Robot_Lib
{
    /// <summary>
    /// This class implements the orientation of the robot
    /// where is facing and where need to be facing
    /// </summary>
    class Orientation
    {
        private Dictionary<string, string> tr = new Dictionary<string, string>();
        private Dictionary<string, string> tl = new Dictionary<string, string>();

        /// <summary>
        /// constructor to inizialize a dictionary
        ///this dictionary contains a key with the facing and instruction 
        ///and the value of the result of the key(facing + instruction)
        /// </summary>
        public Orientation()
        {
            tr.Add("WTR", "N");
            tr.Add("NTR", "E");
            tr.Add("ETR", "S");
            tr.Add("STR", "W");

            tl.Add("NTL", "W");
            tl.Add("WTL", "S");
            tl.Add("STL", "E");
            tl.Add("ETL", "N");
        }

        /// <summary>
        /// enum of facing or orientation
        /// </summary>
        public enum direction
        {
            N,
            S,
            W,
            E

        }

        /// <summary>
        /// change facing position when a TL instruction is given 
        /// </summary>
        /// <param name="key">facing position and the instruction</param>
        /// <returns>new facing</returns>
        public string getTR(string key)
        {
            return tr[key];
        }
        /// <summary>
        /// change facing position when a TL instruction is given  
        /// </summary>
        /// <param name="key">facing position and the instruction</param>
        /// <returns>new facing</returns>
        public string getTL(string key)
        {
            return tl[key];
        }


    }
}
