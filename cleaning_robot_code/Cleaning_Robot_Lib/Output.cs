
using System;
using System.Collections.Generic;
using System.Text;

namespace Cleaning_Robot_Lib
{
    /// <summary>
    /// Class used to serialize the json object
    /// this object will be send it
    /// </summary>
   public  class Output
    {

        public List<Visited> visited { get; set; }
        public List<Cleaned> cleaned { get; set; }
        public Final final { get; set; }
        public int battery { get; set; }

    }
}
