
using System;
using System.Collections.Generic;
using System.Text;


namespace Cleaning_Robot_Lib
{
    /// <summary>
    /// This is the object of the robot 
    /// we use this to manipulate the actions of the robot
    /// </summary>
   public class Robot
    {
        /// <summary>
        /// input object that is use from a json object
        /// </summary>
        private Input input;
        //max row
        private int maxRow;
        //max column
        private int maxColumn;
        //map of the robot
        private List<Map> map = new List<Map>();

        private int battery;

        public Robot(Input input)
        {
            this.input = input;
            this.map = studytMap(this.input);
            this.battery = input.battery;
        }

        private int getMaxRow()
        {
            return this.maxRow;
        }

        private int getMaxColumn()
        {
            return this.maxColumn;
        }


        /// <summary>
        /// This method get the input map and create a new object map with more attributes
        /// </summary>
        /// <param name="input">map</param>
        /// <returns>new map object</returns>
        private List<Map> studytMap(Input input)
        {
            List<Map> positionList = new List<Map>();
            Map map;
            int row = 0;
            int column = 0;

            foreach (var i in input.map)
            {
                column = 0;
                foreach (var position in i)
                {
                    map = new Map();
                    if ((input.start.y == row) && (input.start.x == column))
                    {
                        map.facing = input.start.facing;
                        map.visited = true;
                    }
                    else
                    {
                        map.facing = "";
                        map.visited = false;
                    }
                    map.y = row;
                    map.x = column;

                    map.space = input.map[row][column];
                    map.outBattery = false;
                    map.cleaned = false;
                    positionList.Add(map);

                    //  Console.Write("Y:" + row.ToString() + " " + "X:" + column.ToString() + " " + "" + y + " " + "facing:" + input.start.facing + " ");

                    column += 1;
                }
                row += 1;
            }

            this.maxRow = row - 1;
            this.maxColumn = column - 1;

            return positionList;
        }

        /// <summary>
        /// The robot perform an instruction
        /// </summary>
        /// <param name="cmd">command</param>
        /// <returns>new map with the instruction applied</returns>
        private List<Map> perform(string cmd)
        {
            //Taking a list of point(objects) of the map
            List<Map> map = new List<Map>();
            map = this.map;

            //Take the max row and MaxColumn
            int maxRow = this.maxRow;
            int maxColumn = this.maxColumn;

            //Generate a new object orientation to se where is going to face
            Orientation orientation = new Orientation();
            //New object to see interact with the position of the robot
            Position robot = new Position();

            Batterry battery = new Batterry(this.battery);

            string facing = "";
            int x = 0, y = 0;

            foreach (var position in map)
            {
                if (position.facing != "")
                {
                    //TL Logic
                    if (Commads.cmds.TL.ToString() == cmd)
                    {
                        int costOperation = battery.getBatteryDrain(Commads.cmds.TL.ToString());
                        if (battery.checkIfCanExecute(costOperation, battery.batteryLife))
                        {
                            facing = position.facing;
                            position.facing = orientation.getTL(facing + cmd);
                            //drain baterry
                            battery.batteryLife -= costOperation;
                            this.battery = battery.batteryLife;
                            return map;
                        }
                        else
                        {
                            map = robot.outBatteryPosition(map, position.x, position.y);
                            return map;


                        }
                    }
                    else if (Commads.cmds.TR.ToString() == cmd)
                    {
                        int costOperation = battery.getBatteryDrain(Commads.cmds.TR.ToString());
                        if (battery.checkIfCanExecute(costOperation, battery.batteryLife))
                        {
                            facing = position.facing;
                            position.facing = orientation.getTR(facing + cmd);
                            //drain baterry
                            battery.batteryLife -= costOperation;
                            this.battery = battery.batteryLife;
                            return map;

                        }
                        else
                        {
                            map = robot.outBatteryPosition(map, position.x, position.y);
                            return map;
                        }
                    }
                    else if (Commads.cmds.C.ToString() == cmd)
                    {
                        int costOperation = battery.getBatteryDrain(Commads.cmds.C.ToString());
                        if (battery.checkIfCanExecute(costOperation, battery.batteryLife))
                        {
                            map = robot.cleanPosition(map, position.x, position.y);
                            //drain baterry
                            battery.batteryLife -= costOperation;
                            this.battery = battery.batteryLife;
                            return map;
                        }
                        else
                        {
                            map = robot.outBatteryPosition(map, position.x, position.y);
                            return map;
                        }
                    }
                    else if (Commads.cmds.A.ToString() == cmd)
                    {

                        facing = position.facing;

                        x = position.x;
                        y = position.y;


                        string tmpFacing = position.facing;
                        int costOperation = battery.getBatteryDrain(Commads.cmds.A.ToString());
                        if (battery.checkIfCanExecute(costOperation, battery.batteryLife))
                        {
                            map = robot.advancePosition(map, facing, x, y, maxRow, maxColumn, 0, battery);
                            //clean the actual position, becouse the new position is in the map
                            if (position.outBattery == false)
                            {
                                position.facing = "";
                            }
                            this.battery = battery.batteryLife;
                            return map;
                        }
                        else
                        {
                            map = robot.outBatteryPosition(map, position.x, position.y);
                            return map;
                        }


                    }
                }
            }
            return map;
        }


        /// <summary>
        /// get what spaces the robot visited
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        private List<Visited> getVisited(List<Map> map)
        {
            List<Visited> visitList = new List<Visited>();

            foreach (var visited in map)
            {
                if (visited.visited)
                {
                    Visited visits = new Visited();
                    visits.X = visited.x;
                    visits.Y = visited.y;
                    visitList.Add(visits);
                }

            }
            return visitList;
        }

        /// <summary>
        /// get what spaces the robot cleaned
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        private List<Cleaned> getCleaned(List<Map> map)
        {
            List<Cleaned> cleanedList = new List<Cleaned>();

            foreach (var clean in map)
            {
                if (clean.cleaned)
                {
                    Cleaned cleaned = new Cleaned();
                    cleaned.X = clean.x;
                    cleaned.Y = clean.y;
                    cleanedList.Add(cleaned);
                }

            }
            return cleanedList;
        }

        /// <summary>
        /// get the final position of the robot
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        private Final getFinal(List<Map> map)
        {
            Final finalPosition = new Final();
            foreach (var final in map)
            {
                if (final.facing != "")
                {
                    finalPosition.X = final.x;
                    finalPosition.Y = final.y;
                    finalPosition.facing = final.facing;
                }
            }
            return finalPosition;
        }

        /// <summary>
        /// This method start the robot to do something
        /// </summary>
        /// <returns>output of the robot, what does the robot did</returns>
        public Output start()
        {
            //Taking a list of point(objects) of the map
            List<Map> map = new List<Map>();
            map = this.map;

            IList<string> commands = new List<string>();
            commands = this.input.commands;

            Output output = new Output();

            foreach (var cmd in commands)
            {
                map = perform(cmd);
            }

            output.visited = getVisited(map);
            output.cleaned = getCleaned(map);
            output.final = getFinal(map);
            output.battery = this.battery;

            return output;
        }


    }
}
