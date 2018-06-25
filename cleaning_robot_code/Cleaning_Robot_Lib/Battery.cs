using System;
using System.Collections.Generic;
using System.Text;

namespace Cleaning_Robot_Lib
{
    /// <summary>
    /// This class is use to manipulate the battery
    /// we can check the battery life
    /// use the battery
    /// also in the future if we have to change the units that consume we can do this where
    /// </summary>
    class Batterry
    {
        private Dictionary<string, int> batteryDrain = new Dictionary<string, int>();
        private int battery;
        /// <summary>
        /// Constructor that inicialize a diccionary with the unit of battery that consume
        /// depending of the instruction
        /// </summary>
        /// <param name="battery">battery life</param>
        public Batterry(int battery)
        {
            batteryDrain.Add("TL", 1);
            batteryDrain.Add("TR", 1);
            batteryDrain.Add("A", 2);
            batteryDrain.Add("B", 3);
            batteryDrain.Add("C", 5);
            this.battery = battery;

        }

        /// <summary>
        /// Propieti to set or get the battery life
        /// </summary>
        public int batteryLife
        {
            get
            {
                return this.battery;
                
            }
            set
            {
                this.battery = value;
            }
        }
        /// <summary>
        /// get the unit of batery that use 
        /// </summary>
        /// <param name="key">the action to perform</param>
        /// <returns></returns>
        public int getBatteryDrain(string key)
        {
            return batteryDrain[key];
        }
        /// <summary>
        /// This method is use to see if we have plenty of batery to do some acction
        /// also if in the future we need to change the logic of how to check the battery for some operation we can change the logic here
        /// </summary>
        /// <param name="costOperation"></param>
        /// <param name="batteryLife"></param>
        /// <returns></returns>
        public bool checkIfCanExecute(int costOperation, int batteryLife)
        {
            //check if we have plenty of battery life
            if (batteryLife < costOperation)
            {
                return false;
            }
            else
            {
                return true;
            }

        }


    }
}
