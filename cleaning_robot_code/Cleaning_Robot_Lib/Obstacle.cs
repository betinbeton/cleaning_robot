
using System;
using System.Collections.Generic;
using System.Text;

namespace Cleaning_Robot_Lib
{
    /// <summary>
    /// This class is use to handle the obstacles that the robot have
    /// </summary>
    class Obstacle
    {

        /// <summary>
        /// This method is use to se how many attemps the robot have
        /// depeing of the attemp the robot use a diferent algorithm
        /// </summary>
        /// <param name="map">map of the robot</param>
        /// <param name="facing">where is facing the robot</param>
        /// <param name="x">x axis</param>
        /// <param name="y">y axis</param>
        /// <param name="maxRow">what is the max row</param>
        /// <param name="maxColumn">what is the max column</param>
        /// <param name="attemp">number of attemp</param>
        /// <param name="baterry">object baterry</param>
        /// <returns>map that handle the obtacle</returns>
        public List<Map> obstacle(List<Map> map, string facing, int x, int y, int maxRow, int maxColumn, int attemp, Batterry baterry)
        {


            if (attemp == 1)
            {
                map = obstacleAttemp1(map, facing, x, y, maxRow, maxColumn, 1, baterry);
            }
            else if (attemp == 2)
            {
                map = obstacleAttemp2(map, facing, x, y, maxRow, maxColumn, 2, baterry);
            }
            else if (attemp == 3)
            {
                map = obstacleAttemp3(map, facing, x, y, maxRow, maxColumn, 3, baterry);
            }
            else if (attemp == 4)
            {
                map = obstacleAttemp4(map, facing, x, y, maxRow, maxColumn, 4, baterry);
            }
            else if (attemp == 5)
            {
                map = obstacleAttemp5(map, facing, x, y, maxRow, maxColumn, 5, baterry);
            }
            else if (attemp == 6)
            {
                map = obstacleAttemp6(map, facing, x, y, maxRow, maxColumn, 6, baterry);
            }


            return map;
        }

        /// <summary>
        /// obstacle attemp 1 logic
        /// Turn right, then advance. (TR, A)
        /// </summary>
        /// <param name="map">map of the robot</param>
        /// <param name="facing">where is facing the robot</param>
        /// <param name="x">x axis</param>
        /// <param name="y">y axis</param>
        /// <param name="maxRow">what is the max row</param>
        /// <param name="maxColumn">what is the max column</param>
        /// <param name="attemp">number of attemp</param>
        /// <param name="baterry">object baterry</param>
        /// <returns>new map</returns>
        public List<Map> obstacleAttemp1(List<Map> map, string facing, int x, int y, int maxRow, int maxColumn, int attemp, Batterry baterry)
        {

            Orientation orientation = new Orientation();
            Position robot = new Position();

            bool fisrtTime = true;

            foreach (var position in map)
            {
                if (position.facing != "")
                {

                    if (fisrtTime)
                    {
                        //TR
                        int costOperation = baterry.getBatteryDrain(Commads.cmds.TR.ToString());
                        if (baterry.checkIfCanExecute(costOperation, baterry.batteryLife))
                        {
                            facing = position.facing;
                            //change the facing position
                            //TR
                            position.facing = orientation.getTR(facing + Commads.cmds.TR.ToString());
                            facing = position.facing;
                            baterry.batteryLife -= costOperation;
                        }
                        else
                        {
                            map = robot.outBatteryPosition(map, position.x, position.y);
                            return map;
                            //break

                        }
                        //A
                        costOperation = baterry.getBatteryDrain(Commads.cmds.A.ToString());
                        if (baterry.checkIfCanExecute(costOperation, baterry.batteryLife))
                        {
                            //A
                            map = robot.advancePosition(map, facing, x, y, maxRow, maxColumn, attemp, baterry);
                            if (position.outBattery == false)
                            {
                                position.facing = "";
                            }
                            //Validate to enter one time
                            fisrtTime = false;
                            //break;
                            return map;

                        }
                        else
                        {
                            map = robot.outBatteryPosition(map, position.x, position.y);
                            return map;
                            //break
                        }
                    }
                }
            }
            return map;
        }

        /// <summary>
        /// obstacle attemp 2 logic
        /// If that also hits an obstacle: Turn Left, Back, Turn Right, Advance (TL, B, TR, A)
        /// </summary>
        /// <param name="map">map of the robot</param>
        /// <param name="facing">where is facing the robot</param>
        /// <param name="x">x axis</param>
        /// <param name="y">y axis</param>
        /// <param name="maxRow">what is the max row</param>
        /// <param name="maxColumn">what is the max column</param>
        /// <param name="attemp">number of attemp</param>
        /// <param name="baterry">object baterry</param>
        /// <returns>new map</returns>
        public List<Map> obstacleAttemp2(List<Map> map, string facing, int x, int y, int maxRow, int maxColumn, int attemp, Batterry baterry)
        {
            Orientation orientation = new Orientation();
            Position robot = new Position();
            bool tl = true;
            bool tr = true;
            bool b = true;
            bool a = true;

            for (int i = 0; i < map.Count; i++)
            {
                if (map[i].facing != "")
                {
                    if (tl)
                    {
                        //TL
                        int costOperation = baterry.getBatteryDrain(Commads.cmds.TL.ToString());
                        if (baterry.checkIfCanExecute(costOperation, baterry.batteryLife))
                        {
                            facing = map[i].facing;
                            //change the facing position
                            //TL
                            map[i].facing = orientation.getTL(facing + Commads.cmds.TL.ToString());
                            //new facing
                            facing = map[i].facing;
                            //this flag is so this parte it is only call one time
                            tl = false;
                            baterry.batteryLife -= costOperation;
                        }
                        else
                        {
                            map = robot.outBatteryPosition(map, map[i].x, map[i].y);
                            return map;
                        }
                    }
                    if (b)
                    {
                        //B
                        int costOperation = baterry.getBatteryDrain(Commads.cmds.B.ToString());
                        if (baterry.checkIfCanExecute(costOperation, baterry.batteryLife))
                        {
                            //B
                            map = robot.backPosition(map, facing, x, y, maxRow, maxColumn, attemp, baterry);
                            //Cleaning the old facing position

                            if (map[i].outBattery == false)
                            {
                                map[i].facing = "";
                            }
                            //Restart the iteration so the next intructions can be done in the new position
                            i = -1;
                            //this flag is so this parte it is only call one time
                            b = false;
                            // in this step the method backposition decrease the battery life
                            //baterry.batteryLife -= costOperation;
                        }
                        else
                        {
                            robot.outBatteryPosition(map, map[i].x, map[i].y);
                            return map;
                        }
                    }
                    if (tr && (i >= 0))
                    {
                        //TR
                        int costOperation = baterry.getBatteryDrain(Commads.cmds.TR.ToString());
                        if (baterry.checkIfCanExecute(costOperation, baterry.batteryLife))
                        {
                            //TR
                            map[i].facing = orientation.getTR(facing + Commads.cmds.TR.ToString());
                            //new facing
                            facing = map[i].facing;
                            //this flag is so this parte it is only call one time
                            tr = false;
                            baterry.batteryLife -= costOperation;
                        }
                        else
                        {
                            map = robot.outBatteryPosition(map, map[i].x, map[i].y);
                            return map;
                        }
                    }
                    if (a && (i >= 0))
                    {
                        //A
                        int costOperation = baterry.getBatteryDrain(Commads.cmds.A.ToString());
                        if (baterry.checkIfCanExecute(costOperation, baterry.batteryLife))
                        {
                            //A
                            map = robot.advancePosition(map, facing, x, y, maxRow, maxColumn, attemp, baterry);
                            if (map[i].outBattery == false)
                            {
                                map[i].facing = "";
                            }
                            a = false;
                            return map;
                            // in this step the method backposition decrease the battery life
                        }
                        else
                        {
                            map = robot.outBatteryPosition(map, map[i].x, map[i].y);
                            return map;
                        }
                    }
                }
            }
            return map;
        }

        /// <summary>
        /// obstacle attemp 3 logic
        /// If that also hits an obstacle: Turn Left, Turn Left, Advance (TL, TL, A)
        /// </summary>
        /// <param name="map">map of the robot</param>
        /// <param name="facing">where is facing the robot</param>
        /// <param name="x">x axis</param>
        /// <param name="y">y axis</param>
        /// <param name="maxRow">what is the max row</param>
        /// <param name="maxColumn">what is the max column</param>
        /// <param name="attemp">number of attemp</param>
        /// <param name="baterry">object baterry</param>
        /// <returns>new map</returns>
        public List<Map> obstacleAttemp3(List<Map> map, string facing, int x, int y, int maxRow, int maxColumn, int attemp, Batterry baterry)
        {

            Orientation orientation = new Orientation();
            Position robot = new Position();

            foreach (var position in map)
            {
                if (position.facing != "")
                {
                    //TL
                    int costOperation = baterry.getBatteryDrain(Commads.cmds.TL.ToString());
                    if (baterry.checkIfCanExecute(costOperation, baterry.batteryLife))
                    {
                        //get the actual facing position
                        facing = position.facing;
                        //TL
                        position.facing = orientation.getTL(facing + Commads.cmds.TL.ToString());
                        //new facing
                        facing = position.facing;
                        baterry.batteryLife -= costOperation;
                    }
                    else
                    {
                        map = robot.outBatteryPosition(map, position.x, position.y);
                        return map;
                    }

                    //TL
                    costOperation = baterry.getBatteryDrain(Commads.cmds.TL.ToString());
                    if (baterry.checkIfCanExecute(costOperation, baterry.batteryLife))
                    {
                        //TL
                        position.facing = orientation.getTL(facing + Commads.cmds.TL.ToString());
                        //new facing
                        facing = position.facing;
                        baterry.batteryLife -= costOperation;
                    }
                    else
                    {
                        map = robot.outBatteryPosition(map, position.x, position.y);
                        return map;
                    }

                    //A
                    costOperation = baterry.getBatteryDrain(Commads.cmds.A.ToString());
                    if (baterry.checkIfCanExecute(costOperation, baterry.batteryLife))
                    {
                        //A
                        map = robot.advancePosition(map, facing, position.x, position.y, maxRow, maxColumn, attemp, baterry);
                        //Clean facing position old one
                        if (position.outBattery == false)
                        {
                            position.facing = "";
                        }
                        return map;
                        // in this step the method backposition decrease the battery life
                    }
                    else
                    {
                        map = robot.outBatteryPosition(map, position.x, position.y);
                        return map;
                    }
                }
            }
            return map;
        }

        /// <summary>
        /// obstacle attemp 4 logic
        /// If that also hits and obstacle: Turn Right, Back, Turn Right, Advance (TR, B, TR, A)
        /// </summary>
        /// <param name="map">map of the robot</param>
        /// <param name="facing">where is facing the robot</param>
        /// <param name="x">x axis</param>
        /// <param name="y">y axis</param>
        /// <param name="maxRow">what is the max row</param>
        /// <param name="maxColumn">what is the max column</param>
        /// <param name="attemp">number of attemp</param>
        /// <param name="baterry">object baterry</param>
        /// <returns>new map</returns>
        public List<Map> obstacleAttemp4(List<Map> map, string facing, int x, int y, int maxRow, int maxColumn, int attemp, Batterry baterry)
        {

            Orientation orientation = new Orientation();
            Position robot = new Position();

            bool tr = true;
            bool tr2 = true;
            bool b = true;
            bool a = true;

            for (int i = 0; i < map.Count; i++)
            {
                if (map[i].facing != "")
                {
                    if (tr)
                    {
                        //TR
                        int costOperation = baterry.getBatteryDrain(Commads.cmds.TR.ToString());
                        if (baterry.checkIfCanExecute(costOperation, baterry.batteryLife))
                        {
                            facing = map[i].facing;
                            //change the facing position
                            //TR
                            map[i].facing = orientation.getTR(facing + Commads.cmds.TR.ToString());
                            //new facing
                            facing = map[i].facing;
                            //this flag is so this step is done onle one time in the iteration
                            tr = false;
                            baterry.batteryLife -= costOperation;

                        }
                        else
                        {
                            map = robot.outBatteryPosition(map, map[i].x, map[i].y);
                            return map;
                        }
                    }
                    if (b)
                    {
                        //B
                        int costOperation = baterry.getBatteryDrain(Commads.cmds.B.ToString());
                        if (baterry.checkIfCanExecute(costOperation, baterry.batteryLife))
                        {
                            //B
                            map = robot.backPosition(map, facing, x, y, maxRow, maxColumn, attemp, baterry);
                            //Clean the old position facing
                            if (map[i].outBattery == false)
                            {
                                map[i].facing = "";
                            }
                            //reset the iteration so the next steps can be done with the new position
                            i = -1;
                            //this flag is so this step is done onle one time in the iteration
                            b = false;
                        }
                        else
                        {
                            map = robot.outBatteryPosition(map, map[i].x, map[i].y);
                            return map;
                        }
                    }
                    if (tr2 && (i >= 0))
                    {
                        //TR
                        int costOperation = baterry.getBatteryDrain(Commads.cmds.TR.ToString());
                        if (baterry.checkIfCanExecute(costOperation, baterry.batteryLife))
                        {
                            //TR
                            map[i].facing = orientation.getTR(facing + Commads.cmds.TR.ToString());
                            //new facing
                            facing = map[i].facing;
                            //this flag is so this step is done onle one time in the iteration
                            tr2 = false;
                            baterry.batteryLife -= costOperation;
                        }
                        else
                        {
                            map = robot.outBatteryPosition(map, map[i].x, map[i].y);
                            return map;
                        }
                    }
                    if (a && (i >= 0))
                    {
                        //A
                        int costOperation = baterry.getBatteryDrain(Commads.cmds.A.ToString());
                        if (baterry.checkIfCanExecute(costOperation, baterry.batteryLife))
                        {
                            //A
                            map = robot.advancePosition(map, facing, x, y, maxRow, maxColumn, attemp, baterry);
                            //Clean facing position old one
                            if (map[i].outBattery == false)
                            {
                                map[i].facing = "";
                            }
                            // in this step the method backposition decrease the battery life
                            a = false;
                            return map;
                        }
                        else
                        {
                            map = robot.outBatteryPosition(map, map[i].x, map[i].y);
                            return map;
                        }
                    }
                }
            }
            return map;
        }

        /// <summary>
        /// obstacle attemp 5 logic
        /// If that also hits and obstacle: Turn Left, Turn Left, Advance (TL, TL, A)
        /// </summary>
        /// <param name="map">map of the robot</param>
        /// <param name="facing">where is facing the robot</param>
        /// <param name="x">x axis</param>
        /// <param name="y">y axis</param>
        /// <param name="maxRow">what is the max row</param>
        /// <param name="maxColumn">what is the max column</param>
        /// <param name="attemp">number of attemp</param>
        /// <param name="baterry">object baterry</param>
        /// <returns>new map</returns>
        public List<Map> obstacleAttemp5(List<Map> map, string facing, int x, int y, int maxRow, int maxColumn, int attemp, Batterry baterry)
        {

            Orientation orientation = new Orientation();
            Position robot = new Position();

            foreach (var position in map)
            {
                if (position.facing != "")
                {
                    //TL
                    int costOperation = baterry.getBatteryDrain(Commads.cmds.TL.ToString());
                    if (baterry.checkIfCanExecute(costOperation, baterry.batteryLife))
                    {
                        //get the actual facing position
                        facing = position.facing;
                        //TL
                        position.facing = orientation.getTL(facing + Commads.cmds.TL.ToString());
                        //TODO:mover la bateria bajar
                        facing = position.facing;
                        baterry.batteryLife -= costOperation;
                    }
                    else
                    {
                        map = robot.outBatteryPosition(map, position.x, position.y);
                        return map;
                    }

                    //TL
                    costOperation = baterry.getBatteryDrain(Commads.cmds.TL.ToString());
                    if (baterry.checkIfCanExecute(costOperation, baterry.batteryLife))
                    {
                        //TL
                        position.facing = orientation.getTL(facing + Commads.cmds.TL.ToString());
                        facing = position.facing;
                        baterry.batteryLife -= costOperation;
                    }
                    else
                    {
                        map = robot.outBatteryPosition(map, position.x, position.y);
                        return map;
                    }
                    //A
                    costOperation = baterry.getBatteryDrain(Commads.cmds.A.ToString());
                    if (baterry.checkIfCanExecute(costOperation, baterry.batteryLife))
                    {
                        //A
                        map = robot.advancePosition(map, facing, position.x, position.y, maxRow, maxColumn, attemp, baterry);
                        //Clean facing position old one
                        if (position.outBattery == false)
                        {
                            position.facing = "";
                        }
                        return map;
                    }
                    else
                    {
                        map = robot.outBatteryPosition(map, position.x, position.y);
                        return map;
                    }
                }
            }
            return map;
        }

        /// <summary>
        /// obstacle attemp 6 logic
        /// If an obstacle is hit again the robot will stop and return.
        /// </summary>
        /// <param name="map">map of the robot</param>
        /// <param name="facing">where is facing the robot</param>
        /// <param name="x">x axis</param>
        /// <param name="y">y axis</param>
        /// <param name="maxRow">what is the max row</param>
        /// <param name="maxColumn">what is the max column</param>
        /// <param name="attemp">number of attemp</param>
        /// <param name="baterry">object baterry</param>
        /// <returns>new map</returns>
        public List<Map> obstacleAttemp6(List<Map> map, string facing, int x, int y, int maxRow, int maxColumn, int attemp, Batterry baterry)
        {
            ///cambiar para que regresa falso
            return map;
        }


    }
}
