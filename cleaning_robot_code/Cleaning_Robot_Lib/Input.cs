using System;
using System.Collections.Generic;
using System.Text;


namespace Cleaning_Robot_Lib
{
    /// <summary>
    /// Class used to deserialize the json object
    /// </summary>
    public class Input
    {
        //Interface of a list because is not going to be modify
        public IList<IList<string>> map { get; set; }
       // public Start start { get; set; }
        public Start start  { get; set; }
        public IList<string> commands { get; set; }
        public int battery { get; set; }
    }
}

